//-----------------------------------------------------------------------
// <copyright file="StudyFieldBridge.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;
using System.Text;
using Extant.Data.Entities;
using Lucene.Net.Documents;
using NHibernate.Search.Bridge;

namespace Extant.Data.Search
{
    public class StudyFieldBridge : IFieldBridge
    {
        public void Set(string name, object value, Document document, Field.Store store, Field.Index index, float? boost)
        {
            var study = (Study)value;
            var basicFields = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}",
                                            study.StudyName, study.Description, study.StudySynonyms, study.StudyDesign,
                                            string.Join(" ", study.DiseaseAreas.Select(da => da.DiseaseAreaName)),
                                            string.Join(" ", study.DiseaseAreas.Select(da => da.DiseaseAreaSynonyms)),
                                            study.PrincipalInvestigator, study.Institution, study.Funder);
            var publications = string.Join(" ", 
                study.Publications.Select(p => p.Title + 
                    (null == p.Authors ? "" : string.Join(" ", p.Authors)) + 
                    (null == p.MeshTerms ? "" : string.Join(" ", p.MeshTerms))));
            var dataItems = string.Join(" ", study.DataItems.Select(di => di.DataItem.DataItemName));
            var fieldValue = basicFields + publications + dataItems + StudySamplesFieldBridge.SamplesSearchTerms(study);

            var field = new Field(name, fieldValue, store, index);
            field.SetBoost(boost ?? 1);
            document.Add(field);
        }
    }
}