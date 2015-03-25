//-----------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Extant.Data;
using Extant.Data.Entities;
using Extant.Data.Search;
using Extant.Pubmed;
using Extant.Web.Helpers;
using Extant.Web.Infrastructure;
using Extant.Web.Models;
using StructureMap;

namespace Extant.Web
{
    public static class Bootstrapper
    {
        public static void Bootstrap()
        {
            var modelBinderTypeMappingDictionary = new ModelBinderTypeMappingDictionary
                                                       {
                                                           {typeof (DateTime), typeof (DateTimeModelBinder)},
                                                           {typeof (FileUpload), typeof (FileUploadModelBinder)},
                                                           {typeof (IPrincipal), typeof (PrincipalModelBinder)}
                                                       };

            var smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            var fromEmail = ConfigurationManager.AppSettings["FromEmail"];

            ObjectFactory.Initialize(x =>
                {
                    x.AddRegistry(new NHibernateRegistry());
                    x.AddRegistry(new RepositoryRegistry());
                    x.For<IMailer>().Singleton().Use(Mailer.GetMailer(smtpServer, fromEmail));
                    x.For<IPubmedService>().Singleton().Use<PubmedService>();
                    x.For<IModelBinderProvider>().Use<StructureMapModelBinderProvider>();
                    x.For<ModelBinderTypeMappingDictionary>().Use(modelBinderTypeMappingDictionary);
                }
            );

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));

            Mapper.Initialize(m => m.ConstructServicesUsing(ObjectFactory.GetInstance));
            Mapper.CreateMap<DiseaseArea, int>().ConvertUsing(da => da.Id);
            Mapper.CreateMap<int, DiseaseArea>().ConvertUsing<IntToDiseaseAreaConverter>();
            Mapper.CreateMap<HttpPostedFileBase, FileUpload>().ConvertUsing<FileUploadConverter>();
            Mapper.CreateMap<Study, StudyModel>().ForMember(m => m.PatientInformationLeaflet, opt => opt.Ignore())
                                                 .ForMember(m => m.PatientInformationLeafletCurrent, opt => opt.MapFrom(s => s.PatientInformationLeaflet))
                                                 .ForMember(m => m.ConsentForm, opt => opt.Ignore())
                                                 .ForMember(m => m.ConsentFormCurrent, opt => opt.MapFrom(s => s.ConsentForm))
                                                 .ForMember(m => m.DataAccessPolicy, opt => opt.Ignore())
                                                 .ForMember(m => m.DataAccessPolicyCurrent, opt => opt.MapFrom(s => s.DataAccessPolicy));
            Mapper.CreateMap<AdditionalDocument, AdditionalDocumentModel>()
                  .ForMember(m => m.File, opt => opt.Ignore())
                  .ForMember(m => m.FileCurrent, opt => opt.MapFrom(ad => ad.File));
            Mapper.CreateMap<StudyModel, Study>();
            Mapper.CreateMap<AdditionalDocumentModel, AdditionalDocument>();
            Mapper.CreateMap<TimePoint, TimePointModel>();
            Mapper.CreateMap<TimePointModel, TimePoint>();

            Mapper.CreateMap<DateTime?, string>()
                  .ConvertUsing(d => d.HasValue ? d.Value.ToString("dd-MM-yyyy") : String.Empty );
            Mapper.CreateMap<DateTime, string>()
                  .ConvertUsing(d => d.ToString("dd-MM-yyyy"));
            Mapper.CreateMap<FileUpload, FileUploadModel>()
                  .ForMember(f => f.FileSize, opt => opt.ResolveUsing(f => f.FileSize.FileSize()));
            Mapper.CreateMap<DiseaseArea, string>().ConvertUsing(da => da.DiseaseAreaName);
            Mapper.CreateMap<Study, StudyIndexModel>()
                  .ForMember(m => m.DiseaseAreasText, opt => opt.MapFrom(s => string.Join(", ", s.DiseaseAreas.Select(da => da.DiseaseAreaName))))
                  .ForMember(m => m.StudyStatus, opt => opt.MapFrom(s => s.StudyStatus.EnumToString()))
                  .ForMember(m => m.OnPortfolio, opt => opt.MapFrom(s => s.OnPortfolio ? "Yes" : "No"))
                  .ForMember(m => m.ContactEmailEncoded, opt => opt.MapFrom(s => s.ContactEmail.Rotate()))
                  .ForMember(m => m.ContactEmailReversed, opt => opt.MapFrom(s => s.ContactEmail.Reverse()))
                  .ForMember(m => m.IsLongitudinal, opt => opt.MapFrom(s => s.IsLongitudinal ? "Yes" : "No"))
                  .ForMember(m => m.HasDataAccessPolicy, opt => opt.MapFrom(s => s.HasDataAccessPolicy ? "Yes" : "No"))
                  .ForMember(m => m.SampleNumbers, opt => opt.MapFrom(s => s.SampleNumbers()));
            Mapper.CreateMap<AdditionalDocument, AdditionalDocumentDisplayModel>()
                  .ForMember(m => m.DocumentType, opt => opt.MapFrom(ad => ad.DocumentType.EnumToString()));
            Mapper.CreateMap<Sample, SampleDisplayModel>()
                  .ForMember(m => m.SampleType, opt => opt.MapFrom(s => s.SampleType.EnumToString()))
                  .ForMember(m => m.NumberOfSamples, opt => opt.MapFrom(s => s.NumberOfSamples.EnumToString()))
                  .ForMember(m => m.SourceBiologicalMaterial, opt => opt.MapFrom(s => s.SourceBiologicalMaterial()))
                  .ForMember(m => m.TissueSamplesPreserved, opt => opt.MapFrom(s => s.TissueSamplesPreserved.HasValue ? s.TissueSamplesPreserved.EnumToString() : null))
                  .ForMember(m => m.SampleVolume, opt => opt.MapFrom(s => s.SampleVolume.EnumToString()))
                  .ForMember(m => m.Concentration, opt => opt.MapFrom(s => s.Concentration.HasValue ? s.Concentration.EnumToString() : null))
                  .ForMember(m => m.NumberOfAliquots, opt => opt.MapFrom(s => s.NumberOfAliquots.HasValue ? s.NumberOfAliquots.EnumToString() : null))
                  .ForMember(m => m.WhenCollected, opt => opt.MapFrom(s => s.WhenCollected.EnumToString()))
                  .ForMember(m => m.SnapFrozen, opt => opt.MapFrom(s => s.SnapFrozen.HasValue ? s.SnapFrozen.EnumToString() : null))
                  .ForMember(m => m.HowDnaExtracted, opt => opt.MapFrom(s => s.HowDnaExtracted.HasValue ? s.HowDnaExtracted.EnumToString() : null))
                  .ForMember(m => m.TimeBetweenCollectionAndStorage, opt => opt.MapFrom(s => s.TimeBetweenCollectionAndStorage.HasValue ? s.TimeBetweenCollectionAndStorage.EnumToString() : null))
                  .ForMember(m => m.CollectionToStorageTemp, opt => opt.MapFrom(s => s.CollectionToStorageTemp.EnumToString()))
                  .ForMember(m => m.StorageTemp, opt => opt.MapFrom(s => s.StorageTemp.EnumToString()))
                  .ForMember(m => m.AlwayStoredAtThisTemp, opt => opt.MapFrom(s => s.AlwayStoredAtThisTemp.EnumToString()))
                  .ForMember(m => m.DnaQuality, opt => opt.MapFrom(s => s.DnaQuality()))
                  .ForMember(m => m.FreezeThawCycles, opt => opt.MapFrom(s => s.FreezeThawCycles.EnumToString()))
                  .ForMember(m => m.Analysis, opt => opt.MapFrom(s => s.Analysis()))
                  .ForMember(m => m.DnaExtracted, opt => opt.MapFrom(s => s.DnaExtracted.HasValue ? s.DnaExtracted.EnumToString() : null))
                  .ForMember(m => m.CellsGrown, opt => opt.MapFrom(s => s.CellsGrown.HasValue ? s.CellsGrown.EnumToString() : null));

            Mapper.CreateMap<Study, StudyPublicationsModel>();
            Mapper.CreateMap<PublicationModel, Publication>();
            Mapper.CreateMap<Publication, PublicationModel>();

            Mapper.CreateMap<Study, StudyDataItemsModel>();
            Mapper.CreateMap<DiseaseArea, DiseaseAreaModel>();
            Mapper.CreateMap<Category, CategoryModel>();
            Mapper.CreateMap<CategoryDataItem, CategoryDataItemModel>();
            Mapper.CreateMap<DataItem, DataItemModel>();
            Mapper.CreateMap<StudyDataItem, StudyDataItemModel>()
                  .ForMember(m => m.Id, opt => opt.MapFrom(sdi => sdi.DataItem.Id))
                  .ForMember(m => m.DataItemName, opt => opt.MapFrom(sdi => sdi.DataItem.DataItemName))
                  .ForMember(m => m.TimePoints, opt => opt.MapFrom(sdi => sdi.TimePoints.Select(tp => tp.Id).ToArray()));

            Mapper.CreateMap<Study, StudySamplesModel>();
            Mapper.CreateMap<Sample, SampleModel>()
                  .ForMember(m => m.TissueSamplesPreserved, opt => opt.MapFrom(s => (int?)s.TissueSamplesPreserved))
                  .ForMember(m => m.Concentration, opt => opt.MapFrom(s => (int?)s.Concentration))
                  .ForMember(m => m.NumberOfAliquots, opt => opt.MapFrom(s => (int?)s.NumberOfAliquots))
                  .ForMember(m => m.SnapFrozen, opt => opt.MapFrom(s => (int?)s.SnapFrozen))
                  .ForMember(m => m.HowDnaExtracted, opt => opt.MapFrom(s => (int?)s.HowDnaExtracted))
                  .ForMember(m => m.DnaExtracted, opt => opt.MapFrom(s => (int?)s.DnaExtracted))
                  .ForMember(m => m.CellsGrown, opt => opt.MapFrom(s => (int?)s.CellsGrown));
            
            Mapper.CreateMap<StudySamplesModel, Study>();
            Mapper.CreateMap<SampleModel, Sample>()
                  .ForMember(s => s.TissueSamplesPreserved, opt => opt.MapFrom(m => (TissueSamplesPreserved?)m.TissueSamplesPreserved))
                  .ForMember(s => s.Concentration, opt => opt.MapFrom(m => (Concentration?)m.Concentration ))
                  .ForMember(s => s.NumberOfAliquots, opt => opt.MapFrom(m => (NumberOfAliquots?)m.NumberOfAliquots))
                  .ForMember(s => s.SnapFrozen, opt => opt.MapFrom(m => (YesNoUnknown?)m.SnapFrozen))
                  .ForMember(s => s.HowDnaExtracted, opt => opt.MapFrom(m => (HowDnaExtracted?)m.HowDnaExtracted))
                  .ForMember(s => s.DnaExtracted, opt => opt.MapFrom(m => (YesNoUnknown?)m.DnaExtracted))
                  .ForMember(s => s.CellsGrown, opt => opt.MapFrom(m => (YesNoUnknown?)m.CellsGrown));

            Mapper.CreateMap<DiseaseArea, DiseaseAreaBasicModel>();

            Mapper.CreateMap<Study, StudyBasicModel>();

            Mapper.CreateMap<User, UserModel>();
            Mapper.CreateMap<User, UserBasicModel>();
            Mapper.CreateMap<User, AdminUserModel>();
            Mapper.CreateMap<User, EditUserModel>()
                  .ForMember(m => m.LastLoginDate, opt => opt.MapFrom(u => u.LastLoginDate.HasValue ? u.LastLoginDate.Value.ToString("dd-MM-yyyy HH:mm:ss") : String.Empty))
                  .ForMember(m => m.Password, opt => opt.Ignore())
                  .ForMember(m => m.IsAdministrator, opt => opt.MapFrom(u => u.Roles.Select(r => r.RoleName).Contains(Constants.AdministratorRole)))
                  .ForMember(m => m.IsHubLead, opt => opt.MapFrom(u => u.Roles.Select(r => r.RoleName).Contains(Constants.HubLeadRole)));

            Mapper.CreateMap<SearchAdvancedModel, AdvancedSearch>();
            Mapper.CreateMap<SearchLineModel, SearchLine>();

            Mapper.CreateMap<Study, StudyEditorsModel>();
        }
    }

}