//-----------------------------------------------------------------------
// <copyright file="SampleHelper.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using Extant.Data.Entities;

namespace Extant.Web.Helpers
{
    public static class SampleHelper
    {
        public static IEnumerable<string> SourceBiologicalMaterial(this Sample sample)
        {
            var list = new List<string>();
            if (sample.BioMatWholeBlood)
                list.Add("Whole Blood");

            if (sample.BioMatBuffyCoat)
                list.Add("Buffy Coat");

            if (sample.BioMatSaliva)
                list.Add("Saliva");

            if (sample.BioMatBuccalSwabs)
                list.Add("Buccal Swabs");

            if (sample.BioMatAcidCitrateDextrose)
                list.Add("Acid citrate dextrose (ACD)");

            if (sample.BioMatSynovialFluid)
                list.Add("Synovial Fluid");

            if (sample.BioMatSynovialTissue)
                list.Add("Synovial Tissue");

            if (sample.BioMatSerumSeparatorTube)
                list.Add("Serum Separator Tube");

            if (sample.BioMatPlasmaSeparatorTube)
                list.Add("Plasma Separator Tube");

            if (sample.BioMatUrine)
                list.Add("Urine");

            if (sample.BioMatOtherTubes)
                list.Add("Other blood collection tubes - "+sample.BioMatOtherTubesSpecify);

            if (sample.BioMatEdtaBlood)
                list.Add("EDTA Blood");

            if (sample.BioMatSalivaNoAdditive)
                list.Add("Saliva (No additive)");

            if (sample.BioMatSalivaOragene)
                list.Add("Saliva (Oragene)");

            if (sample.BioMatCulture)
                list.Add("Culture - "+sample.BioMatCultureSpecify);

            if (sample.BioMatUnknown)
                list.Add("Unknown");

            if (sample.BioMatOther)
                list.Add("Other - " + sample.BioMatOtherSpecify);

            return list;
        }

        public static IEnumerable<string> DnaQuality(this Sample sample)
        {
            var list = new List<string>();
            if (sample.DnaQualityAbsorbance) list.Add("Absorbance at 260/280 OD readings");

            if (sample.DnaQualityGel) list.Add("Samples have been run on a gel");

            if (sample.DnaQualityCommercialKit) list.Add("Samples have been analysed using a commercial testing kit e.g. DNA OK");

            if (sample.DnaQualityPcr) list.Add("Real time PCR analysis");

            if (sample.DnaQualityPicoGreen) list.Add("PicoGreen");

            if (sample.DnaQualityUnknown) list.Add("Unknown");

            if (sample.DnaQualityOther) list.Add("Other - "+sample.DnaQualityOtherSpecify);

            return list;
        }

        public static IEnumerable<string> Analysis(this Sample sample)
        {
            var list = new List<string>();

            if (sample.AnalysisNo) list.Add("No successful analysis performed");

            if (sample.AnalysisSequencing) list.Add("Sequencing");

            if (sample.AnalysisRealTimePcr) list.Add("Real Time PCR");

            if (sample.AnalysisPcr) list.Add("PCR");

            if (sample.AnalysisGenotyping) list.Add("Genotyping");

            if (sample.AnalysisBiochemistry) list.Add("Biochemistry");

            if (sample.AnalysisImmunochemistry) list.Add("Immunochemistry");

            if (sample.AnalysisDnaExtraction) list.Add("DNA extraction");

            if (sample.AnalysisImmunohistochemistry) list.Add("Immunohistochemisty");

            if (sample.AnalysisCellLinesDerived) list.Add("Cell lines derived");

            if (sample.AnalysisRnaExtraction) list.Add("RNA extraction");

            if (sample.AnalysisUnknown) list.Add("Unknown");

            if (sample.AnalysisOther) list.Add("Any other analysis - "+sample.AnalysisOtherSpecify);

            return list;
        }

        public static IDictionary<string, string> SampleNumbers(this Study study)
        {
            var list = new Dictionary<string, string>();
            if ( study.NumberOfDnaSamples != NumberOfSamples.None )
            {
                if ( study.NumberOfDnaSamples == NumberOfSamples.ExactNumber )
                {
                    list.Add("DNA", "Exact Number - "+study.NumberOfDnaSamplesExact);
                }
                else
                {
                    list.Add("DNA", study.NumberOfDnaSamples.EnumToString());
                }
            }

            if (study.NumberOfSerumSamples != NumberOfSamples.None)
            {
                if (study.NumberOfSerumSamples == NumberOfSamples.ExactNumber)
                {
                    list.Add("Serum", "Exact Number - " + study.NumberOfSerumSamplesExact);
                }
                else
                {
                    list.Add("Serum", study.NumberOfSerumSamples.EnumToString());
                }
            }

            if (study.NumberOfPlasmaSamples != NumberOfSamples.None)
            {
                if (study.NumberOfPlasmaSamples == NumberOfSamples.ExactNumber)
                {
                    list.Add("Plasma", "Exact Number - " + study.NumberOfPlasmaSamplesExact);
                }
                else
                {
                    list.Add("Plasma", study.NumberOfPlasmaSamples.EnumToString());
                }
            }

            if (study.NumberOfWholeBloodSamples != NumberOfSamples.None)
            {
                if (study.NumberOfWholeBloodSamples == NumberOfSamples.ExactNumber)
                {
                    list.Add("Whole Blood", "Exact Number - " + study.NumberOfWholeBloodSamplesExact);
                }
                else
                {
                    list.Add("Whole Blood", study.NumberOfWholeBloodSamples.EnumToString());
                }
            }

            if (study.NumberOfSalivaSamples != NumberOfSamples.None)
            {
                if (study.NumberOfSalivaSamples == NumberOfSamples.ExactNumber)
                {
                    list.Add("Saliva", "Exact Number - " + study.NumberOfSalivaSamplesExact);
                }
                else
                {
                    list.Add("Saliva", study.NumberOfSalivaSamples.EnumToString());
                }
            }

            if (study.NumberOfTissueSamples != NumberOfSamples.None)
            {
                if (study.NumberOfTissueSamples == NumberOfSamples.ExactNumber)
                {
                    list.Add("Tissue", "Exact Number - " + study.NumberOfTissueSamplesExact);
                }
                else
                {
                    list.Add("Tissue", study.NumberOfTissueSamples.EnumToString());
                }
            }

            if (study.NumberOfCellSamples != NumberOfSamples.None)
            {
                if (study.NumberOfCellSamples == NumberOfSamples.ExactNumber)
                {
                    list.Add("Cell", "Exact Number - " + study.NumberOfCellSamplesExact);
                }
                else
                {
                    list.Add("Cell", study.NumberOfCellSamples.EnumToString());
                }
            }

            if (study.NumberOfOtherSamples != NumberOfSamples.None)
            {
                if (study.NumberOfOtherSamples == NumberOfSamples.ExactNumber)
                {
                    list.Add("Other", "Exact Number - " + study.NumberOfOtherSamplesExact);
                }
                else
                {
                    list.Add("Other", study.NumberOfOtherSamples.EnumToString());
                }
            }

            return list;
        }
    }
}