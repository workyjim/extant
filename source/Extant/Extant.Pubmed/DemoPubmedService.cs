//-----------------------------------------------------------------------
// <copyright file="DemoPubmedService.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Extant.Pubmed
{
    public class DemoPubmedService : IPubmedService
    {
        public PubmedResult Summary(string pmid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PubmedResult> Search(string term, int page, int pageSize, out int count)
        {
            count = 7;
            var results = new List<PubmedResult>
                       {
                           new PubmedResult
                               {
                                   Title = "The association between systemic glucocorticoid therapy and the risk of infection in patients with rheumatoid arthritis: systematic review and meta-analyses.",
                                   Authors = new List<string>{"Dixon WG", "Suissa S", "Hudson M"},
                                   Id = "21884589",
                                   Journal = "Arthritis Res Ther",
                                   PublicationDate = "2011 Aug 31"
                               },
                           new PubmedResult
                               {
                                   Title = "Risk of septic arthritis in patients with rheumatoid arthritis and the effect of anti-TNF therapy: results from the British Society for Rheumatology Biologics Register.",
                                   Authors = new List<string>{"Galloway JB", "Hyrich KL", "Mercer LK", "Dixon WG", "Ustianowski AP", "Helbert M", "Watson KD", "Lunt M", "Symmons DP", "BSR Biologics Register"},
                                   Id = "21784730",
                                   Journal = "Ann Rheum Dis",
                                   PublicationDate = "2011 Oct"
                               },
                           new PubmedResult
                               {
                                   Title = "The influence of systemic glucocorticoid therapy upon the risk of non-serious infection in older patients with rheumatoid arthritis: a nested case-control study.",
                                   Authors = new List<string>{"Dixon WG", "Kezouh A", "Bernatsky S", "Suissa S"},
                                   Id = "21285116",
                                   Journal = "Ann Rheum Dis",
                                   PublicationDate = "2011 Jun"
                               },       
                           new PubmedResult
                               {
                                   Title = "Anti-TNF therapy is associated with an increased risk of serious infections in patients with rheumatoid arthritis especially in the first 6 months of treatment: updated results from the British Society for Rheumatology Biologics Register with special emphasis on risks in the elderly.",
                                   Authors = new List<string>{"Galloway JB", "Hyrich KL", "Mercer LK", "Dixon WG", "Fu B", "Ustianowski AP", "Watson KD", "Lunt M", "Symmons DP", "BSRBR Control Centre Consortium", "British Society for Rheumatology Biologics Register"},
                                   Id = "20675706",
                                   Journal = "Rheumatology (Oxford)",
                                   PublicationDate = "2011 Jan"
                               },
                           new PubmedResult
                               {
                                   Title = "No evidence of association between anti-tumor necrosis factor treatment and mortality in patients with rheumatoid arthritis: results from the British Society for Rheumatology Biologics Register.",
                                   Authors = new List<string>{"Lunt M", "Watson KD", "Dixon WG", "British Society for Rheumatology Biologics Register Control Centre Consortium", "Symmons DP", "Hyrich KL", "British Society for Rheumatology Biologics Register"},
                                   Id = "20662063",
                                   Journal = "Arthritis Rheum",
                                   PublicationDate = "2010 Nov"
                               },
                           new PubmedResult
                               {
                                   Title = "Influence of anti-tumor necrosis factor therapy on cancer incidence in patients with rheumatoid arthritis who have had a prior malignancy: results from the British Society for Rheumatology Biologics Register.",
                                   Authors = new List<string>{"Dixon WG", "Watson KD", "Lunt M", "Mercer LK", "Hyrich KL", "Symmons DP", "British Society For Rheumatology Biologics Register Control Centre Consortium", "British Society for Rheumatology Biologics Register"},
                                   Id = "20535785",
                                   Journal = "Arthritis Care Res (Hoboken)",
                                   PublicationDate = "2010 Jun"
                               },
                           new PubmedResult
                               {
                                   Title = "EULAR points to consider when establishing, analysing and reporting safety data of biologics registers in rheumatology.",
                                   Authors = new List<string>{"Dixon WG", "Carmona L", "Finckh A", "Hetland ML", "Kvien TK", "Landewe R", "Listing J", "Nicola PJ", "Tarp U", "Zink A", "Askling J"},
                                   Id = "20525843",
                                   Journal = "Ann Rheum Dis",
                                   PublicationDate = "2010 Sep"
                               },
                       };
            return results.Skip(page*pageSize).Take(pageSize).ToList();
        }

        public IEnumerable<string> MeshTerms(string pmid)
        {
            throw new NotImplementedException();
        }

        public PubmedDetails Details(string pmid)
        {
            switch(pmid)
            {
                case "21884589":
                    return new PubmedDetails
                    {
                        Authors = new List<string> { "Dixon WG", "Suissa S", "Hudson M" },
                        Journal = "Arthritis Res Ther",
                        PublicationDate = "2011 Aug 31",
                        MeshTerms = new List<string>()
                    };
                case "21784730":
                    return new PubmedDetails
                    {
                        Authors = new List<string>{"Galloway JB", "Hyrich KL", "Mercer LK", "Dixon WG", "Ustianowski AP", "Helbert M", "Watson KD", "Lunt M", "Symmons DP", "BSR Biologics Register"},
                        Journal = "Ann Rheum Dis",
                        PublicationDate = "2011 Oct",
                        MeshTerms = new List<string>
                                        {
                                            "Adult",
                                            "Aged",
                                            "Antirheumatic Agents/adverse effects",
                                            "Arthritis, Infectious/complications",
                                            "Arthritis, Infectious/epidemiology",
                                            "Arthritis, Rheumatoid/complications",
                                            "Arthritis, Rheumatoid/drug therapy",
                                            "Arthritis, Rheumatoid/epidemiology",
                                            "Epidemiologic Methods",
                                            "Female",
                                            "Great Britain/epidemiology",
                                            "Humans",
                                            "Immunosuppressive Agents/adverse effects",
                                            "Joint Prosthesis/adverse effects",
                                            "Male",
                                            "Middle Aged",
                                            "Opportunistic Infections/complications",
                                            "Opportunistic Infections/epidemiology",
                                            "Prosthesis-Related Infections/complications",
                                            "Prosthesis-Related Infections/epidemiology",
                                            "Tumor Necrosis Factor-alpha/antagonists & inhibitors"
                                        }
                    };
                case "21285116":
                    return new PubmedDetails
                    {
                        Authors = new List<string>{"Dixon WG", "Kezouh A", "Bernatsky S", "Suissa S"},
                        Journal = "Ann Rheum Dis",
                        PublicationDate = "2011 Jun",
                        MeshTerms = new List<string>
                                        {
                                            "Administration, Oral",
                                            "Aged",
                                            "Antirheumatic Agents/adverse effects",
                                            "Antirheumatic Agents/therapeutic use",
                                            "Arthritis, Rheumatoid/complications",
                                            "Arthritis, Rheumatoid/drug therapy",
                                            "Arthritis, Rheumatoid/epidemiology",
                                            "Case-Control Studies",
                                            "Dose-Response Relationship, Drug",
                                            "Female",
                                            "Glucocorticoids/administration & dosage",
                                            "Glucocorticoids/adverse effects",
                                            "Glucocorticoids/therapeutic use",
                                            "Humans",
                                            "Injections, Intravenous",
                                            "Male",
                                            "Opportunistic Infections/chemically induced",
                                            "Opportunistic Infections/complications",
                                            "Opportunistic Infections/epidemiology",
                                            "Quebec/epidemiology",
                                            "Risk Factors"                                   
                                        }
                    };
                case "20675706":
                    return new PubmedDetails
                    {
                        Authors = new List<string>{"Galloway JB", "Hyrich KL", "Mercer LK", "Dixon WG", "Fu B", "Ustianowski AP", "Watson KD", "Lunt M", "Symmons DP", "BSRBR Control Centre Consortium", "British Society for Rheumatology Biologics Register"},
                        Journal = "Rheumatology (Oxford)",
                        PublicationDate = "2011 Jan",
                        MeshTerms = new List<string>
                                        {
                                            "Aged",
                                            "Antibodies, Monoclonal/adverse effects",
                                            "Antirheumatic Agents/adverse effects",
                                            "Arthritis, Rheumatoid/drug therapy",
                                            "Female",
                                            "Great Britain",
                                            "Humans",
                                            "Infection/etiology",
                                            "Male",
                                            "Middle Aged",
                                            "Registries",
                                            "Regression Analysis",
                                            "Risk Factors",
                                            "Tumor Necrosis Factors/adverse effects",
                                            "Tumor Necrosis Factors/antagonists & inhibitors",                                       
                                        }
                    };
                case "20662063":
                    return new PubmedDetails
                    {
                        Authors = new List<string>{"Lunt M", "Watson KD", "Dixon WG", "British Society for Rheumatology Biologics Register Control Centre Consortium", "Symmons DP", "Hyrich KL", "British Society for Rheumatology Biologics Register"},
                        Journal = "Arthritis Rheum",
                        PublicationDate = "2010 Nov",
                        MeshTerms = new List<string>
                                        {
                                            "Age Factors",
                                            "Aged",
                                            "Antibodies, Monoclonal/adverse effects*",
                                            "Antibodies, Monoclonal/therapeutic use",
                                            "Antirheumatic Agents/adverse effects",
                                            "Antirheumatic Agents/therapeutic use",
                                            "Arthritis, Rheumatoid/mortality",
                                            "Arthritis, Rheumatoid/therapy",
                                            "Female",
                                            "Humans",
                                            "Male",
                                            "Middle Aged",
                                            "Proportional Hazards Models",
                                            "Questionnaires",
                                            "Registries",
                                            "Severity of Illness Index",
                                            "Tumor Necrosis Factor-alpha/antagonists & inhibitors"                                       
                                        }
                    };
                case "20535785":
                    return new PubmedDetails
                    {
                        Authors = new List<string>{"Dixon WG", "Watson KD", "Lunt M", "Mercer LK", "Hyrich KL", "Symmons DP", "British Society For Rheumatology Biologics Register Control Centre Consortium", "British Society for Rheumatology Biologics Register"},
                        Journal = "Arthritis Care Res (Hoboken)",
                        PublicationDate = "2010 Jun",
                        MeshTerms = new List<string>
                                        {
                                            "Aged",
                                            "Antirheumatic Agents/adverse effects",
                                            "Antirheumatic Agents/therapeutic use",
                                            "Arthritis, Rheumatoid/drug therapy",
                                            "Arthritis, Rheumatoid/epidemiology",
                                            "Biological Products/adverse effects",
                                            "Biological Products/therapeutic use",
                                            "Cohort Studies",
                                            "Female",
                                            "Follow-Up Studies",
                                            "Great Britain/epidemiology",
                                            "Humans",
                                            "Incidence",
                                            "Male",
                                            "Middle Aged",
                                            "Neoplasms/chemically induced",
                                            "Neoplasms/epidemiology*",
                                            "Prospective Studies",
                                            "Registries",
                                            "Rheumatology*/trends",
                                            "Societies, Medical*/trends",
                                            "Tumor Necrosis Factor-alpha/antagonists & inhibitors"                                           
                                        }
                    };
                case "20525843":
                    return new PubmedDetails
                    {
                        Authors = new List<string>{"Dixon WG", "Carmona L", "Finckh A", "Hetland ML", "Kvien TK", "Landewe R", "Listing J", "Nicola PJ", "Tarp U", "Zink A", "Askling J"},
                        Journal = "Ann Rheum Dis",
                        PublicationDate = "2010 Sep",
                        MeshTerms = new List<string>
                                        {
                                            "Adverse Drug Reaction Reporting Systems/organization & administration",
                                            "Antirheumatic Agents/adverse effects",
                                            "Antirheumatic Agents/therapeutic use",
                                            "Biological Products/adverse effects",
                                            "Biological Products/therapeutic use",
                                            "Epidemiologic Methods",
                                            "Europe",
                                            "Humans",
                                            "Patient Selection",
                                            "Registries/standards",
                                            "Rheumatic Diseases/drug therapy",
                                            "Treatment Outcome"
                                        }
                    };
                default:
                    throw new Exception();
            }
        }
    }
}