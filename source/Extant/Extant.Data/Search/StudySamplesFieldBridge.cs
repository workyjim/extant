//-----------------------------------------------------------------------
// <copyright file="StudySamplesFieldBridge.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using Extant.Data.Entities;
using Lucene.Net.Documents;
using NHibernate.Search.Bridge;

namespace Extant.Data.Search
{
    public class StudySamplesFieldBridge : IFieldBridge
    {
        public void Set(string name, object value, Document document, Field.Store store, Field.Index index, float? boost)
        {
            var field = new Field(name, SamplesSearchTerms((Study)value), store, index);
            field.SetBoost(boost ?? 1);
            document.Add(field);
        }

        public static string SamplesSearchTerms(Study study)
        {
            var samples = new List<string>();
            if (study.NumberOfDnaSamples != NumberOfSamples.None)
                samples.Add("DNA");
            if (study.NumberOfSerumSamples != NumberOfSamples.None)
                samples.Add("Serum");
            if (study.NumberOfPlasmaSamples != NumberOfSamples.None)
                samples.Add("Plasma");
            if (study.NumberOfWholeBloodSamples != NumberOfSamples.None)
                samples.Add("Whole Blood");
            if (study.NumberOfSalivaSamples != NumberOfSamples.None)
                samples.Add("Saliva");
            if (study.NumberOfTissueSamples != NumberOfSamples.None)
                samples.Add("Tissue");
            if (study.NumberOfCellSamples != NumberOfSamples.None)
                samples.Add("Cell");
            if (study.NumberOfOtherSamples != NumberOfSamples.None)
                samples.Add("Other");

            return string.Join(" ", samples);
        }
    }
}