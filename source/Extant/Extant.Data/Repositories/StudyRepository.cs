//-----------------------------------------------------------------------
// <copyright file="StudyRepository.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extant.Data.Entities;
using Extant.Data.Search;
using log4net;
using Lucene.Net.Search;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace Extant.Data.Repositories
{
    public interface IStudyRepository : IRepository<Study>
    {
        Study Publish(Study study);
        IEnumerable<Study> GetUsersStudies(int userId, int page, int pagesize, out int total);
        IEnumerable<Study> GetDiseaseAreasStudies(int[] daIds, int page, int pagesize, out int total);
        IEnumerable<Study> Find(string query, int page, int size, out int count);
        IEnumerable<Study> Find(string term, string diseaseArea, string studyDesign, string studyStatus, string samples, int page, int size, out int count);
        IEnumerable<Study> Find(AdvancedSearch search, int page, int size, out int count, out string query);
        IEnumerable<Study> FindByDataField(string dataField);
        IEnumerable<Study> GetLatestStudies(int number);
        void RebuildSearchIndex();
    }

    public class StudyRepository : Repository<Study>, IStudyRepository
    {
        protected readonly static ILog log = log4net.LogManager.GetLogger(typeof(StudyRepository));

        public StudyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Study Publish(Study study)
        {
            study.Published = true;
            UnitOfWork.CurrentSession.SaveOrUpdate(study);
            return study;
        }

        public IEnumerable<Study> GetUsersStudies(int userId, int page, int pagesize, out int total)
        {
            total = UnitOfWork.CurrentSession
                                .CreateCriteria<Study>()
                                .CreateAlias("Owner", "o")
                                .CreateAlias("Editors", "e", JoinType.LeftOuterJoin)
                                .Add(Restrictions.Or(Restrictions.Eq("o.Id", userId), Restrictions.Eq("e.Id", userId)))
                                .SetProjection(Projections.RowCount())
                                .UniqueResult<int>();
            return UnitOfWork.CurrentSession
                                .CreateCriteria<Study>()
                                .CreateAlias("Owner", "o")
                                .CreateAlias("Editors", "e", JoinType.LeftOuterJoin)
                                .Add(Restrictions.Or(Restrictions.Eq("o.Id", userId), Restrictions.Eq("e.Id", userId)))
                                .AddOrder(Order.Asc("StudyName"))
                                .SetFirstResult(page * pagesize)
                                .SetMaxResults(pagesize)
                                .List<Study>();
        }

        public IEnumerable<Study> GetDiseaseAreasStudies(int[] daIds, int page, int pagesize, out int total)
        {
            total = UnitOfWork.CurrentSession
                                .CreateCriteria<Study>()
                                .CreateCriteria("DiseaseAreas")
                                .Add(Restrictions.In("Id", daIds))
                                .SetProjection(Projections.RowCount())
                                .UniqueResult<int>();

            return UnitOfWork.CurrentSession
                                .CreateCriteria<Study>()
                                .CreateAlias("DiseaseAreas", "da")
                                .Add(Restrictions.In("da.Id", daIds))
                                .AddOrder(Order.Asc("StudyName"))
                                .SetFirstResult(page * pagesize)
                                .SetMaxResults(pagesize)
                                .List<Study>();
        }

        public IEnumerable<Study> Find(string term, string diseaseArea, string studyDesign, string studyStatus, string samples, int page, int size, out int count)
        {
            if (string.IsNullOrEmpty(term) && string.IsNullOrEmpty(diseaseArea) && string.IsNullOrEmpty(studyDesign) && 
                string.IsNullOrEmpty(studyStatus) && string.IsNullOrEmpty(samples))
            {
                var all = Find("Published", true);
                count = all.Count();
                return all.OrderBy(s => s.StudyName).Skip(page * size).Take(size);
            }

            var queryBuilder = new StringBuilder();
            var firstTerm = true;
            if ( !string.IsNullOrEmpty(term))
            {
                queryBuilder.AppendFormat("AllFields:({0})", term);
                firstTerm = false;
            }
            if ( !string.IsNullOrEmpty(diseaseArea) )
            {
                if (!firstTerm) queryBuilder.Append(" AND ");
                queryBuilder.AppendFormat("DiseaseAreas.DiseaseAreaName:\"{0}\"", diseaseArea);
                firstTerm = false;
            }
            if (!string.IsNullOrEmpty(studyDesign))
            {
                if (!firstTerm) queryBuilder.Append(" AND ");
                queryBuilder.AppendFormat("StudyDesign:\"{0}\"", studyDesign);
                firstTerm = false;
            }
            if (!string.IsNullOrEmpty(studyStatus))
            {
                if (!firstTerm) queryBuilder.Append(" AND ");
                queryBuilder.AppendFormat("StudyStatus:\"{0}\"", studyStatus);
                firstTerm = false;
            }
            if (!string.IsNullOrEmpty(samples))
            {
                if (!firstTerm) queryBuilder.Append(" AND ");
                queryBuilder.AppendFormat("Samples:{0}", samples);
            }
            queryBuilder.AppendFormat(" AND Published:true");

            return Find(queryBuilder.ToString(), page, size, out count);
        }

        public IEnumerable<Study> Find(AdvancedSearch search, int page, int size, out int count, out string query)
        {
            var queryBuilder = new StringBuilder();
            var firstLine = true;
            foreach ( var line in search.SearchLines.Where(s => !string.IsNullOrEmpty(s.SearchTerm)) )
            {
                if ( !firstLine ) queryBuilder.AppendFormat(" {0} ", line.SearchOperator);
                queryBuilder.AppendFormat("{0}:{1}{2}{1}", GetField(line.SearchField), line.IsPhrase ? "\"": "", line.SearchTerm);
                firstLine = false;
            }

            if ( firstLine )
            {
                //no valid lines
                count = 0;
                query = "";
                return new List<Study>();
            }

            queryBuilder.AppendFormat(" AND Published:true");
            query = queryBuilder.ToString();
            return Find(query, page, size, out count);
        }

        public IEnumerable<Study> Find(string query, int page, int size, out int count)
        {
            try
            {
                var search = NHibernate.Search.Search.CreateFullTextSession(UnitOfWork.CurrentSession);
                var ftq = search.CreateFullTextQuery<Study>(query)
                    .SetSort(new Sort(new SortField("StudyNameForSort", SortField.STRING)))
                    .SetFirstResult(page*size)
                    .SetMaxResults(size);
                count = ftq.ResultSize;
                return ftq.List<Study>();
            }
            catch(Exception ex)
            {
                //error during search - return empty result set
                log.Error("Error in StudyRepository.Find: query = " + query, ex);
                count = 0;
                return new List<Study>();
            }
        }

        private static string GetField(SearchField field)
        {
            switch(field)
            {
                case SearchField.Any:
                    return "AllFields";
                case SearchField.StudyName:
                case SearchField.Description:
                case SearchField.StudyDesign:
                case SearchField.Institution:
                case SearchField.Funder:
                case SearchField.StudyStatus:
                case SearchField.Samples:
                    return field.ToString();
                case SearchField.ChiefInvestigator:
                    return "PrincipalInvestigator";
                case SearchField.DiseaseArea:
                    return "DiseaseAreas.DiseaseAreaName";
                case SearchField.PublicationTitle:
                    return "Publications.Title";
                case SearchField.PublicationAuthor:
                    return "Publications.Authors";
                case SearchField.PublicationMeshTerm:
                    return "Publications.MeshTerms";
                case SearchField.DataField:
                    return "DataItems.DataItemName";
                default:
                    throw new ArgumentOutOfRangeException("field");
            }
        }

        public IEnumerable<Study> FindByDataField(string dataField)
        {
            var query = string.Format("DataItems.DataItemName:\"{0}\"", dataField);
            var search = NHibernate.Search.Search.CreateFullTextSession(UnitOfWork.CurrentSession);
            return search.CreateFullTextQuery<Study>(query)
                         .SetSort(new Sort(new SortField("StudyNameForSort", SortField.STRING)))
                         .List<Study>();
        }

        public IEnumerable<Study> GetLatestStudies(int number)
        {
            return UnitOfWork.CurrentSession
                                .CreateCriteria<Study>()
                                .Add(Restrictions.Eq("Published", true))
                                .AddOrder(Order.Desc("StudyAdded"))
                                .SetFirstResult(0)
                                .SetMaxResults(number)
                                .List<Study>();
        }

        public void RebuildSearchIndex()
        {
            var studies = GetAll();
            var searchSession = NHibernate.Search.Search.CreateFullTextSession(UnitOfWork.CurrentSession);
            searchSession.PurgeAll(typeof(Study));
            foreach ( var study in studies )
            {
                searchSession.Index(study);
            }
        }
    }
}