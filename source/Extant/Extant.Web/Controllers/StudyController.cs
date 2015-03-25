//-----------------------------------------------------------------------
// <copyright file="StudyController.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using AutoMapper;
using Extant.Data;
using Extant.Data.Entities;
using Extant.Data.Repositories;
using Extant.Data.Search;
using Extant.Pubmed;
using Extant.Web.Helpers;
using Extant.Web.Infrastructure;
using Extant.Web.Models;
using NWeH.Paging;

namespace Extant.Web.Controllers
{
    public class StudyController : Controller
    {
        private const int DefaultPagesize = 10;

        private readonly IStudyRepository StudyRepo;
        private readonly IUserRepository UserRepo;
        private readonly IDiseaseAreaRepository DiseaseAreaRepo;
        private readonly IRepository<FileUpload> FileUploadRepo;
        private readonly IRepository<Publication> PublicationRepo;
        private readonly IDataItemRepository DataItemRepo;
        private readonly IPubmedService PubmedService;

        public StudyController(IStudyRepository studyRepo, IUserRepository userRepo,
            IDiseaseAreaRepository diseaseAreaRepo, IRepository<FileUpload> fileUploadRepo,
            IDataItemRepository dataItemRepo, IRepository<Publication> publicationRepo,
            IPubmedService pubmedService)
        {
            StudyRepo = studyRepo;
            UserRepo = userRepo;
            DiseaseAreaRepo = diseaseAreaRepo;
            FileUploadRepo = fileUploadRepo;
            PublicationRepo = publicationRepo;
            DataItemRepo = dataItemRepo;
            PubmedService = pubmedService;
        }

        public ActionResult Index(int _id, IPrincipal user)
        {
            var study = StudyRepo.Get(_id);
            var model = Mapper.Map<Study, StudyIndexModel>(study);
            model.CanEdit = UserRepo.CanEditStudy(_id, user.Identity.Name, Constants.HubLeadRole);
            return View(model);
        }

        public ActionResult File(int _id)
        {
            var file = FileUploadRepo.Get(_id);
            return File(file.FileData, file.MimeType, file.FileName);
        }

        [Authorize]
        public ActionResult List(IPrincipal principal)
        {
            var user = UserRepo.GetByEmail(principal.Identity.Name);
            var model = new ListStudiesModel {IsHubLead = principal.IsInRole(Constants.HubLeadRole)};
            int myTotal;
            var myStudies = StudyRepo.GetUsersStudies(user.Id, 0, DefaultPagesize, out myTotal);
            model.MyStudies = Mapper.Map<IEnumerable<Study>, IEnumerable<StudyBasicModel>>(myStudies).ToPagedList(1, DefaultPagesize, myTotal);
            return View(model);
        }

        [Authorize(Roles = Constants.HubLeadRole)]
        public ActionResult ListHub(IPrincipal principal)
        {
            var user = UserRepo.GetByEmail(principal.Identity.Name);
            var model = new ListStudiesModel { IsHubLead = principal.IsInRole(Constants.HubLeadRole) };
            int hubTotal;
            var hubStudies = StudyRepo.GetDiseaseAreasStudies(user.DiseaseAreas.Select(da => da.Id).ToArray(), 0, DefaultPagesize, out hubTotal);
            model.MyStudies =
                Mapper.Map<IEnumerable<Study>, IEnumerable<StudyBasicModel>>(hubStudies).ToPagedList(1, DefaultPagesize, hubTotal);
            return View(model);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [AjaxAuthorize]
        public ActionResult MyStudies(int? page, int? pagesize, IPrincipal principal)
        {
            var user = UserRepo.GetByEmail(principal.Identity.Name);
            int myTotal;
            var myStudies = StudyRepo.GetUsersStudies(user.Id, (page ?? 1) - 1, pagesize ?? DefaultPagesize, out myTotal);
            return PartialView("MyStudies", 
                new PagedStudyListModel
                    {
                        Studies = Mapper.Map<IEnumerable<Study>, IEnumerable<StudyBasicModel>>(myStudies).ToPagedList(page ?? 1, pagesize ?? DefaultPagesize, myTotal),
                        Controller = "Study",
                        Action = "MyStudies"
                    });
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [AjaxAuthorize(Roles = Constants.HubLeadRole)]
        public ActionResult HubStudies(int? page, int? pagesize, IPrincipal principal)
        {
            var user = UserRepo.GetByEmail(principal.Identity.Name);
            int hubTotal;
            var hubStudies = StudyRepo.GetDiseaseAreasStudies(user.DiseaseAreas.Select(da => da.Id).ToArray(), (page ?? 1) - 1, pagesize ?? DefaultPagesize, out hubTotal);
            return PartialView("MyStudies", 
                new PagedStudyListModel
                    {
                        Studies = Mapper.Map<IEnumerable<Study>, IEnumerable<StudyBasicModel>>(hubStudies).ToPagedList(page ?? 1, pagesize ?? DefaultPagesize, hubTotal),
                        Controller = "Study",
                        Action = "HubStudies"
                    });
        }

        [HttpGet]
        [Authorize]
        public ActionResult New(IPrincipal user)
        {
            var model = new StudyModel
                            {
                                AllDiseaseAreas = DiseaseAreaRepo.GetAllPublished()
                            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult New(StudyModel model, IPrincipal principal)
        {
            var study = Mapper.Map<StudyModel, Study>(model);
            study.StudyAdded = DateTime.Now;
            study.StudyUpdated = DateTime.Now;
            study.ParticipantsRecruitedUpdated = DateTime.Now;
            study.Owner = UserRepo.GetByEmail(principal.Identity.Name);
            study = StudyRepo.Save(study);
            return RedirectToAction("Publications", new { _id = study.Id, isNew = true });
        }

        [HttpGet]
        [Authorize]
        [StudyAccessControl]
        public ActionResult Update(int _id, IPrincipal principal)
        {
            var study = StudyRepo.Get(_id);
            var studyModel = Mapper.Map<Study, StudyBasicModel>(study);
            studyModel.CanDelete = principal.IsInRole(Constants.AdministratorRole) ||
                                   UserRepo.CanDeleteStudy(_id, principal.Identity.Name, Constants.HubLeadRole);
            return View(studyModel);
        }

        [HttpGet]
        [Authorize]
        [StudyAccessControl]
        public ActionResult Details(int _id, bool? isnew)
        {
            var study = StudyRepo.Get(_id);
            var studyModel = Mapper.Map<Study, StudyModel>(study);
            studyModel.AllDiseaseAreas = DiseaseAreaRepo.GetAllPublished();
            studyModel.IsNew = isnew ?? false;
            return View(studyModel);
        }

        [HttpPost]
        [Authorize]
        [StudyAccessControl]
        public ActionResult Details(int _id, StudyModel model, IPrincipal principal)
        {
            var study = StudyRepo.Get(_id);
            var updatedStudy = Mapper.Map<StudyModel, Study>(model);
            var additionalFilesRemoved = null != model.AdditionalDocuments
                                             ? model.AdditionalDocuments.Where(ad => ad.FileRemoved).Select(
                                                 ad => ad.Id).ToList()
                                             : new List<int>();
            var filesToDelete = study.UpdateDetails(updatedStudy, model.PatientInformationLeafletRemoved, 
                model.ConsentFormRemoved, model.DataAccessPolicyRemoved, additionalFilesRemoved);
            study = StudyRepo.Save(study);
            foreach ( var file in filesToDelete )
            {
                FileUploadRepo.Delete(file);
            }
            if ( model.IsNew )
            {
                return RedirectToAction("Publications", new { _id, isnew = model.IsNew });
            }
            return RedirectToAction("Update", new { _id = study.Id });
        }

        [HttpGet]
        [Authorize]
        [StudyAccessControl]
        public ActionResult Publications(int _id, bool? isnew)
        {
            var study = StudyRepo.Get(_id);
            var model = Mapper.Map<Study, StudyPublicationsModel>(study);
            model.IsNew = isnew ?? false;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [StudyAccessControl]
        public ActionResult Publications(int _id, bool? isnew, IEnumerable<PublicationModel> publications, string action)
        {
            var study = StudyRepo.Get(_id);
            var toDelete = new List<Publication>();
            if (null == publications || !publications.Any())
            {
                toDelete = study.Publications.ToList();
                study.Publications.Clear();
            }
            else
            {
                //remove deleted publications
                toDelete = study.Publications.Where(p => !publications.Select(x => x.Id).Contains(p.Id)).ToList();
                foreach (var pub in toDelete)
                {
                    study.Publications.Remove(pub);
                }

                //add new publications
                var newPubs =
                    Mapper.Map<IEnumerable<PublicationModel>, IList<Publication>>(publications.Where(p => 0 == p.Id));
                foreach (var pub in newPubs)
                {
                    if (pub.Pmid.HasValue)
                    {
                        var details = PubmedService.Details(pub.Pmid.ToString());
                        pub.MeshTerms = details.MeshTerms;
                        pub.Authors = details.Authors;
                        pub.Journal = details.Journal;
                        pub.PublicationDate = details.PublicationDate;
                    }
                    study.Publications.Add(pub);
                }
            }

            StudyRepo.Save(study);
            foreach(var publication in toDelete)
            {
                PublicationRepo.Delete(publication);
            }

            if ( isnew ?? false )
            {
                return RedirectToAction("previous" == action ? "Details" : "DataFields", new { _id, isnew });
            }
            return RedirectToAction("Update", new { _id });            
        }

        [HttpGet]
        [Authorize]
        [StudyAccessControl]
        public ActionResult DataFields(int _id, bool? isnew)
        {
            var study = StudyRepo.Get(_id);
            var model = Mapper.Map<Study, StudyDataItemsModel>(study);
            model.IsNew = isnew ?? false;
            return View("DataItems", model);
        }

        [HttpPost]
        [Authorize]
        [StudyAccessControl]
        public ActionResult DataFields(int _id, bool? isnew, bool? useTimePoints, IEnumerable<StudyDataItemModel> dataitems, string action)
        {
            var study = StudyRepo.Get(_id);
            study.UpdateDataFields(useTimePoints ?? false, dataitems, DataItemRepo);
            StudyRepo.Save(study);
            if ( isnew ?? false )
            {
                return RedirectToAction("previous" == action ? "Publications" : "Samples", new { _id, isnew });                
            }
            return RedirectToAction("Update", new {_id});
        }

        [HttpGet]
        [Authorize]
        [StudyAccessControl]
        public ActionResult Samples(int _id, bool? isnew)
        {
            var study = StudyRepo.Get(_id);
            var model = Mapper.Map<Study, StudySamplesModel>(study);
            model.IsNew = isnew ?? false;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [StudyAccessControl]
        public ActionResult Samples(int _id, StudySamplesModel model, string action)
        {
            var study = StudyRepo.Get(_id);
            var updatedStudy = Mapper.Map<StudySamplesModel, Study>(model);
            study.UpdateSamples(updatedStudy);

            if ( "publish" == action )
            {
                StudyRepo.Publish(study);                                
            }
            else
            {
                StudyRepo.Save(study);                
            }

            if (model.IsNew)
            {
                return RedirectToAction("previous" == action ? "DataFields" : "Index", new { _id, isnew = model.IsNew });
            }
            return RedirectToAction("Update", new { _id });
        }

        [HttpGet]
        [Authorize]
        [StudyAccessControl]
        public ActionResult Editors(int _id)
        {
            var study = StudyRepo.Get(_id);
            return View(Mapper.Map<Study, StudyEditorsModel>(study));
        }

        [HttpPost]
        [Authorize]
        [StudyAccessControl]
        public ActionResult Editors(int _id, IEnumerable<int> editors)
        {
            var study = StudyRepo.Get(_id);
            foreach ( var user in study.Editors.Where(u => !editors.Contains(u.Id)).ToList() )
            {
                study.Editors.Remove(user);
            }
            foreach ( var uid in editors.Except(study.Editors.Select(u => u.Id).ToList()))
            {
                study.Editors.Add(UserRepo.Get(uid));
            }
            StudyRepo.Save(study);

            return RedirectToAction("Update", new { _id });
        }

        [HttpGet]
        [Authorize]
        [DeleteStudyAccessControl]
        public ActionResult Delete(int _id)
        {
            var study = StudyRepo.Get(_id);
            var studyTitle = study.StudyName;
            StudyRepo.Delete(study);
            return View("Delete", null, studyTitle);
        }

        [HttpGet]
        [Authorize]
        [StudyAccessControl]
        public ActionResult Publish(int _id)
        {
            var study = StudyRepo.Get(_id);
            StudyRepo.Publish(study);
            return RedirectToAction("Update", new { _id });
        }

        [HttpGet]
        [DenyQueryStringValue("term")]
        public ActionResult Search()
        {
            var diseaseAreas = DiseaseAreaRepo.GetAllPublished();
            var studyCounts = DiseaseAreaRepo.GetStudyCounts();
            var dasWithCounts = diseaseAreas.ToDictionary(da => da,
                                                          da => studyCounts.ContainsKey(da.Id) ? studyCounts[da.Id] : 0);
            return View(Mapper.Map<IDictionary<DiseaseArea, int>, IDictionary<DiseaseAreaBasicModel, int>>(dasWithCounts));
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [RequireQueryStringValue("term")]
        public ActionResult Search(string term, int? da, StudyDesign? sd, StudyStatus? st, string s, int? page, int? pagesize)
        {
            int count;
            var diseaseArea = da.HasValue ? DiseaseAreaRepo.Get(da.Value).DiseaseAreaName : null;
            var studyDesign = sd.HasValue ? sd.Value.ToString() : null;
            var studyStatus = st.HasValue ? st.Value.ToString() : null;
            var results = StudyRepo.Find(term, diseaseArea, studyDesign, studyStatus, s, (page ?? 1) - 1, pagesize ?? DefaultPagesize, out count);
            var model = new SearchResultsModel
                            {
                                Term = term,
                                DiseaseArea = da,
                                StudyDesign = (int?)sd,
                                StudyStatus = (int?)st,
                                Samples = s,
                                DiseaseAreas = Mapper.Map<IEnumerable<DiseaseArea>, IEnumerable<DiseaseAreaBasicModel>>(DiseaseAreaRepo.GetAllPublished()),
                                Studies = Mapper.Map<IEnumerable<Study>, IEnumerable<StudyBasicModel>>(results)
                                                .ToPagedList(page ?? 1, pagesize ?? DefaultPagesize, count)
                            };
            if ( HttpContext.Request.IsAjaxRequest() )
            {
                return PartialView("SearchResultsPartial", model);
            }
            else
            {
                return View("SearchResults", model);
            }
        }

        /// <summary>
        /// Used for ajax paging requests only
        /// </summary>
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult SearchResults(string term, int? da, StudyDesign? sd, StudyStatus? st, string s, int? page, int? pagesize)
        {
            return Search(term, da, sd, st, s, page, pagesize);
        }

        [HttpGet]
        public ActionResult SearchAdvanced()
        {
            var diseaseAreas = DiseaseAreaRepo.GetAllPublished();
            return View(Mapper.Map<IEnumerable<DiseaseArea>, IEnumerable<DiseaseAreaBasicModel>>(diseaseAreas));
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult SearchAdvanced(SearchAdvancedModel model, int? page, int? pagesize)
        {
            var search = Mapper.Map<SearchAdvancedModel, AdvancedSearch>(model);
            int count;
            string query;
            var results = StudyRepo.Find(search, (page ?? 1) - 1, pagesize ?? DefaultPagesize, out count, out query);
            var resultModel = new SearchAdvancedResultsModel
                                {
                                    Query = query,
                                    Studies = Mapper.Map<IEnumerable<Study>, IEnumerable<StudyBasicModel>>(results)
                                                    .ToPagedList(page ?? 1, pagesize ?? DefaultPagesize, count)
                                };
            return PartialView("SearchAdvancedResults", resultModel);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult SearchAdvancedResults(string query, int? page, int? pagesize)
        {
            int count;
            var results = StudyRepo.Find(query, (page ?? 1) - 1, pagesize ?? DefaultPagesize, out count);
            var resultModel = new SearchAdvancedResultsModel
            {
                Query = query,
                Studies = Mapper.Map<IEnumerable<Study>, IEnumerable<StudyBasicModel>>(results)
                                .ToPagedList(page ?? 1, pagesize ?? DefaultPagesize, count)
            };
            return PartialView("SearchAdvancedResults", resultModel);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult ByDiseaseArea(int _id, int? page, int? pagesize)
        {
            var diseaseArea = DiseaseAreaRepo.Get(_id);
            int count;
            var results = StudyRepo.Find(null, diseaseArea.DiseaseAreaName, null, null, null, (page ?? 1) - 1, pagesize ?? DefaultPagesize, out count);
            var model = new ByDiseaseAreaModel()
            {
                DiseaseArea = Mapper.Map<DiseaseArea, DiseaseAreaBasicModel>(diseaseArea),
                Studies = Mapper.Map<IEnumerable<Study>, IEnumerable<StudyBasicModel>>(results)
                                .ToPagedList(page ?? 1, pagesize ?? DefaultPagesize, count)
            };
            if (HttpContext.Request.IsAjaxRequest())
            {
                return PartialView("ByDiseaseAreaPartial", model);
            }
            else
            {
                return View("ByDiseaseArea", model);
            }
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult ByDataItem(int _id)
        {
            var dataItem = DataItemRepo.Get(_id);
            var results = StudyRepo.FindByDataField(dataItem.DataItemName);
            return PartialView(new StudiesByDataItemModel
                                   {
                                       DataItemName = dataItem.DataItemName,
                                       Studies = Mapper.Map<IEnumerable<Study>, IEnumerable<StudyBasicModel>>(results)                                       
                                   });
        }
    }

}
