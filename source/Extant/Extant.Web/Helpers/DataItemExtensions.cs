//-----------------------------------------------------------------------
// <copyright file="TreeExtensions.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Extant.Web.Models;

namespace Extant.Web.Helpers
{
    public static class DataItemExtensions
    {
        public static MvcHtmlString DataItemTree(this DiseaseAreaModel diseaseArea)
        {
            return DataItemTree(new List<DiseaseAreaModel>{diseaseArea}, new List<StudyDataItemModel>());
        }

        public static MvcHtmlString DataItemTree(this IEnumerable<DiseaseAreaModel> diseaseAreas, IEnumerable<StudyDataItemModel> studyDataItems)
        {
            var treeList = new TagBuilder("ul");
            var dasBuilder = new StringBuilder();
            foreach (var diseaseArea in diseaseAreas)
            {
                var daNode = new TagBuilder("li");
                daNode.MergeAttribute("class", "folder");
                var daList = new TagBuilder("ul");
                var daListHtml = new StringBuilder();
                foreach (var cat in diseaseArea.Categories)
                {
                    daListHtml.Append(cat.CategoryTree(studyDataItems));
                }
                daList.InnerHtml = daListHtml.ToString();
                daNode.InnerHtml = diseaseArea.DiseaseAreaName + daList.ToString();
                dasBuilder.Append(daNode.ToString());
            }
            treeList.InnerHtml = dasBuilder.ToString();

            return new MvcHtmlString(treeList.ToString(TagRenderMode.Normal)); 
        }

        private static string CategoryTree(this CategoryModel category, IEnumerable<StudyDataItemModel> studyDataItems)
        {
            var catNode = new TagBuilder("li");
            catNode.MergeAttribute("id", "category-"+category.Id);
            catNode.MergeAttribute("class", "folder");
            var catList = new TagBuilder("ul");
            var catListHtml = new StringBuilder();

            foreach (var subcat in category.Subcategories)
            {
                catListHtml.Append(subcat.CategoryTree(studyDataItems));
            }

            foreach ( var di in category.DataItems )
            {
                var dataItemNode = new TagBuilder("li");
                dataItemNode.MergeAttribute("id", string.Format("dataitem-{0}-{1}",di.Id, di.DataItem.Id));
                dataItemNode.MergeAttribute("title", di.DataItem.DataItemName);
                if (studyDataItems.Select(sdi => sdi.Id).Contains(di.DataItem.Id))
                {
                    dataItemNode.MergeAttribute("class", "selected");
                }
                dataItemNode.SetInnerText(di.ShortName ?? di.DataItem.DataItemName);
                catListHtml.Append(dataItemNode.ToString());
            }

            catList.InnerHtml = catListHtml.ToString();
            catNode.InnerHtml = category.CategoryName + catList.ToString();
            return catNode.ToString(TagRenderMode.Normal);
        }

        private static readonly int IndentLevel = 15;

        public static MvcHtmlString DataItemTable(this IEnumerable<DiseaseAreaModel> diseaseAreas, IEnumerable<StudyDataItemModel> studyDataItems, bool useTimePoints, IEnumerable<TimePointModel> timePoints)
        {
            var dasBuilder = new StringBuilder();
            var usedStudyDataItems = new List<int>();
            foreach (var diseaseArea in diseaseAreas)
            {
                dasBuilder.AppendFormat("<tr class=\"disease-area\"><td>{0}</td>", diseaseArea.DiseaseAreaName);
                if (useTimePoints)
                {
                    foreach (var tp in timePoints)
                    {
                        dasBuilder.Append("<td class=\"tp-check\"></td>");
                    }
                }
                dasBuilder.Append("</tr>");
                foreach (var cat in diseaseArea.Categories)
                {
                    dasBuilder.Append(cat.CategoryTable(studyDataItems, useTimePoints, timePoints, usedStudyDataItems, 1));
                }
            }
            var unusedStudyDataItems = studyDataItems.Where(sdi => !usedStudyDataItems.Contains(sdi.Id));
            if ( unusedStudyDataItems.Any() )
            {
                dasBuilder.AppendFormat("<tr class=\"disease-area\"><td>{0}</td>", "Other");
                if (useTimePoints)
                {
                    foreach (var tp in timePoints)
                    {
                        dasBuilder.Append("<td class=\"tp-check\"></td>");
                    }
                }
                dasBuilder.Append("</tr>");
                foreach ( var sdi in unusedStudyDataItems )
                {
                    dasBuilder.Append(DataItemTableRow(0, 1, sdi.DataItemName, sdi, useTimePoints, timePoints));
                }
            }
            return new MvcHtmlString(dasBuilder.ToString());

        }

        private static string CategoryTable(this CategoryModel category, IEnumerable<StudyDataItemModel> studyDataItems, bool useTimePoints, IEnumerable<TimePointModel> timePoints, IList<int> usedStudyDataItems, int level)
        {
            var catBuilder = new StringBuilder();
            catBuilder.AppendFormat("<tr class=\"category level-{0}\"><td><span style=\"margin-left: {1}px\">{2}</span></td>", level, level * IndentLevel, category.CategoryName);
            if (useTimePoints)
            {
                foreach (var tp in timePoints)
                {
                    catBuilder.Append("<td class=\"tp-check\"></td>");
                }
            }
            catBuilder.Append("</tr>");

            foreach (var subcat in category.Subcategories)
            {
                catBuilder.Append(subcat.CategoryTable(studyDataItems, useTimePoints, timePoints, usedStudyDataItems, level + 1));
            }

            foreach (var di in category.DataItems.Where(di => studyDataItems.Select(sdi => sdi.Id).Contains(di.DataItem.Id)))
            {
                var studyDataItem = studyDataItems.Single(sdi => sdi.Id == di.DataItem.Id);
                usedStudyDataItems.Add(studyDataItem.Id);
                catBuilder.Append(DataItemTableRow(category.Id, level + 1, di.ShortName ?? di.DataItem.DataItemName, studyDataItem, useTimePoints, timePoints));
            }

            return catBuilder.ToString();
        }

        private static string DataItemTableRow(int categoryId, int level, string dataItemName, StudyDataItemModel studyDataItem, bool useTimePoints, IEnumerable<TimePointModel> timePoints)
        {
            var rowBuilder = new StringBuilder();
            rowBuilder.AppendFormat("<tr class=\"dataitem\"><td><span id=\"dataitem-{0}-{1}\" style=\"margin-left: {2}px\"", categoryId, studyDataItem.Id, level * IndentLevel);
            rowBuilder.Append(" class=\"dataitem link\" title=\"Click to see which studies include this data field\">");
            rowBuilder.AppendFormat("{0}</span></td>", dataItemName);
            if (useTimePoints)
            {
                foreach (var tp in timePoints)
                {
                    rowBuilder.Append("<td class=\"tp-check\">");
                    var inTimePoint = null != studyDataItem.TimePoints && studyDataItem.TimePoints.Contains(tp.Id);
                    rowBuilder.AppendFormat("<img src=\"/Images/{0}\" alt=\"{1}\" class=\"checkbox\" />",
                                            inTimePoint ? "checked.png" : "unchecked.png",
                                            inTimePoint ? "checked" : "unchecked"
                        );
                    rowBuilder.Append("</td>");
                }
            }
            rowBuilder.Append("</tr>");
            return rowBuilder.ToString();
        }
    }
}