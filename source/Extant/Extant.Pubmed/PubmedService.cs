//-----------------------------------------------------------------------
// <copyright file="PubmedService.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Extant.Pubmed.eFetchPubmed;
using Extant.Pubmed.eUtils;
using eUtilsServiceSoapClient = Extant.Pubmed.eUtils.eUtilsServiceSoapClient;

namespace Extant.Pubmed
{
    public interface IPubmedService
    {
        PubmedResult Summary(string pmid);
        IEnumerable<PubmedResult> Search(string term, int page, int pageSize, out int count);
        IEnumerable<string> MeshTerms(string pmid);
        PubmedDetails Details(string pmid);
    }

    public class PubmedService : IPubmedService
    {
        public const int DefaultResultSize = 5;

        public PubmedResult Summary(string pmid)
        {
            var client = new eUtilsServiceSoapClient();
            var result = client.run_eSummary(new eSummaryRequest {db = "pubmed", id = pmid});
            if ( null != result.DocSum && 1 == result.DocSum.Length)
            {
                return new PubmedResult
                           {
                               Id = result.DocSum[0].Id,
                               Title = result.DocSum[0].Item.Where(x => x.Name.Equals("Title")).Select(x => x.ItemContent).FirstOrDefault(),
                               Authors = result.DocSum[0].Item.Where(x => x.Name.Equals("AuthorList")).Select(x => x.Item).FirstOrDefault().Select(x => x.ItemContent).ToList(),
                               Journal = result.DocSum[0].Item.Where(x => x.Name.Equals("Source")).Select(x => x.ItemContent).FirstOrDefault(),
                               PublicationDate = result.DocSum[0].Item.Where(x => x.Name.Equals("PubDate")).Select(x => x.ItemContent).FirstOrDefault()
                           };
            }
            return null;
        }

        public IEnumerable<PubmedResult> Search(string term, int page, int pageSize, out int count)
        {
            var client = new eUtilsServiceSoapClient();
            var searchResult = client.run_eSearch(new eSearchRequest
            {
                db = "pubmed",
                term = term,
                RetMax = pageSize.ToString(),
                RetStart = (page * pageSize).ToString()
            });
            if (0 == searchResult.IdList.Length)
            {
                count = 0;
                return new List<PubmedResult>();
            }

            var summaryResult = client.run_eSummary(new eSummaryRequest
            {
                db = "pubmed",
                id = string.Join(",", searchResult.IdList)
            });
            count = Convert.ToInt32(searchResult.Count);
            return summaryResult.DocSum.Select(ds => new PubmedResult
            {
                Id = ds.Id,
                Title = ds.Item.Where(x => x.Name.Equals("Title"))
                                .Select(x => x.ItemContent).FirstOrDefault(),
                Authors = ds.Item.Where(x => x.Name.Equals("AuthorList")).Select(x => x.Item).FirstOrDefault().Select(x => x.ItemContent).ToList(),
                Journal = ds.Item.Where(x => x.Name.Equals("Source")).Select(x => x.ItemContent).FirstOrDefault(),
                PublicationDate = ds.Item.Where(x => x.Name.Equals("PubDate")).Select(x => x.ItemContent).FirstOrDefault()
            });

        }

        public IEnumerable<string> MeshTerms(string pmid)
        {
            var client = new eFetchPubmed.eUtilsServiceSoapClient();
            var result = client.run_eFetch(new eFetchRequest {id = pmid});
            if ( null != result.PubmedArticleSet && result.PubmedArticleSet.Any() )
            {
                var article = result.PubmedArticleSet.Cast<PubmedArticleType>().First();
                if ( null != article.MedlineCitation.MeshHeadingList )
                    return article.MedlineCitation.MeshHeadingList.Select(mh => mh.DescriptorName.Value).ToList();
            }
            return new List<string>();
        }


        public PubmedDetails Details(string pmid)
        {
            var details = new PubmedDetails();
            var client = new eFetchPubmed.eUtilsServiceSoapClient();
            var result = client.run_eFetch(new eFetchRequest { id = pmid });
            if (null != result.PubmedArticleSet && result.PubmedArticleSet.Any())
            {
                var article = result.PubmedArticleSet.Cast<PubmedArticleType>().First();
                if (null != article.MedlineCitation.MeshHeadingList)
                    details.MeshTerms = article.MedlineCitation.MeshHeadingList.Select(mh => mh.DescriptorName.Value).ToList();
                if (null != article.MedlineCitation.MedlineJournalInfo)
                    details.Journal = article.MedlineCitation.MedlineJournalInfo.MedlineTA;
                if ( null != article.MedlineCitation.Article )
                {
                    details.Authors =
                        article.MedlineCitation.Article.AuthorList.Author.Select(
                        a => 3 == a.Items.Length ? string.Format("{0} {1}", a.Items[0], a.Items[2]) : a.Items[0]).ToList();
                    details.PublicationDate = string.Join(" ", article.MedlineCitation.Article.Journal.JournalIssue.PubDate.Items);
                }
            }
            return details;
        }
    }
}