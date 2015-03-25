//-----------------------------------------------------------------------
// <copyright file="HtmlHelper.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Extant.Web.Helpers
{
    public static class InbankFormExtensions
    {

        public static MvcForm BeginFormWithAttributes(this HtmlHelper htmlHelper, object htmlAttributes)
        {
            return htmlHelper.BeginForm(null, null, FormMethod.Post, htmlAttributes);
        }

        public static MvcHtmlString LabelWithRequiredFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return LabelWithRequiredFor<TModel, TValue>(html, expression, null);
        }

        public static MvcHtmlString LabelWithRequiredFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            if ( metadata.IsRequired )
            {
                var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
                string resolvedLabelText = labelText ?? metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
                if (String.IsNullOrEmpty(resolvedLabelText))
                {
                    return MvcHtmlString.Empty;
                }

                var requiredSpan = new TagBuilder("span");
                requiredSpan.Attributes.Add("class", "required");
                requiredSpan.SetInnerText("*");
                var tag = new TagBuilder("label");
                tag.Attributes.Add("for", TagBuilder.CreateSanitizedId(html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));
                tag.InnerHtml = resolvedLabelText+" "+requiredSpan.ToString();
                return new MvcHtmlString(tag.ToString(TagRenderMode.Normal));
            }
            return html.LabelFor(expression);
        }

        public static MvcHtmlString HelpFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            if ( metadata.AdditionalValues.ContainsKey("HelpText") )
            {
                var span = new TagBuilder("span");
                span.MergeAttribute("class", "help-icon");
                span.MergeAttribute("title", metadata.AdditionalValues["HelpText"].ToString());
                span.InnerHtml = "?";
                return new MvcHtmlString(span.ToString(TagRenderMode.Normal));
            }
            return new MvcHtmlString(null);
        }

        public static string GetInputId<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            return
                TagBuilder.CreateSanitizedId(html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName));
        }

        public static MvcHtmlString LabelValidationAndTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes, bool stacked)
        {
            var label = html.LabelWithRequiredFor(expression);
            var textbox = html.TextBoxFor(expression, htmlAttributes);
            var validation = html.ValidationMessageFor(expression);
            var help = html.HelpFor(expression);
            if ( stacked )
            {
                var div1 = new TagBuilder("div");
                div1.Attributes.Add("class", "form-row");
                div1.InnerHtml = label.ToHtmlString();
                var div2 = new TagBuilder("div");
                div2.Attributes.Add("class", "form-row");
                div2.InnerHtml = textbox.ToHtmlString() + help.ToHtmlString() + validation.ToHtmlString();
                return new MvcHtmlString(div1.ToString(TagRenderMode.Normal) + div2.ToString(TagRenderMode.Normal));
            }
            else
            {
                var div1 = new TagBuilder("div");
                div1.Attributes.Add("class", "form-row");
                div1.InnerHtml = label.ToHtmlString() + textbox.ToHtmlString() + help.ToHtmlString() + validation.ToHtmlString();
                return new MvcHtmlString(div1.ToString(TagRenderMode.Normal));
            }
        }

        public static MvcHtmlString LabelValidationAndTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes, bool stacked)
        {
            var label = html.LabelWithRequiredFor(expression);
            var textarea = html.TextAreaFor(expression, htmlAttributes);
            var validation = html.ValidationMessageFor(expression);
            var help = html.HelpFor(expression);
            if (stacked)
            {
                var div1 = new TagBuilder("div");
                div1.Attributes.Add("class", "form-row");
                div1.InnerHtml = label.ToHtmlString();
                var div2 = new TagBuilder("div");
                div2.Attributes.Add("class", "form-row");
                div2.InnerHtml = textarea.ToHtmlString() + help.ToHtmlString() + validation.ToHtmlString();
                return new MvcHtmlString(div1.ToString(TagRenderMode.Normal) + div2.ToString(TagRenderMode.Normal));
            }
            else
            {
                var div1 = new TagBuilder("div");
                div1.Attributes.Add("class", "form-row");
                div1.InnerHtml = label.ToHtmlString() + textarea.ToHtmlString() + help.ToHtmlString() + validation.ToHtmlString();
                return new MvcHtmlString(div1.ToString(TagRenderMode.Normal));
            }
        }

        public static MvcHtmlString LabelAndCheckboxFor<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, bool>> expression, object htmlAttributes, bool stacked)
        {
            var label = html.LabelFor(expression);
            var textbox = html.CheckBoxFor(expression, htmlAttributes);
            var help = html.HelpFor(expression);
            if (stacked)
            {
                var div1 = new TagBuilder("div");
                div1.Attributes.Add("class", "form-row");
                div1.InnerHtml = label.ToHtmlString() + help.ToHtmlString();
                var div2 = new TagBuilder("div");
                div2.Attributes.Add("class", "form-row");
                div2.InnerHtml = textbox.ToHtmlString();
                return new MvcHtmlString(div1.ToString(TagRenderMode.Normal) + div2.ToString(TagRenderMode.Normal));
            }
            else
            {
                var div1 = new TagBuilder("div");
                div1.Attributes.Add("class", "form-row");
                div1.InnerHtml = label.ToHtmlString() + textbox.ToHtmlString() + help.ToHtmlString();
                return new MvcHtmlString(div1.ToString(TagRenderMode.Normal));
            }
        }

        public static MvcHtmlString LabelValidationAndYesNoRadioFor<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, bool>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var label = html.LabelWithRequiredFor(expression);
            var help = html.HelpFor(expression);

            var inputid = html.GetInputId(expression);
            var yesLabel = new TagBuilder("label");
            yesLabel.MergeAttribute("for", inputid + "-yes");
            yesLabel.MergeAttribute("class", "trailing");
            yesLabel.InnerHtml = "Yes";
            var yesRadio = CreateRadioButton(inputid, "true", "yes", Convert.ToBoolean(metadata.Model));

            var noLabel = new TagBuilder("label");
            noLabel.MergeAttribute("for", inputid + "-no");
            noLabel.MergeAttribute("class", "trailing");
            noLabel.InnerHtml = "No";
            var noRadio = CreateRadioButton(inputid, "false", "no", !Convert.ToBoolean(metadata.Model));
            var div1 = new TagBuilder("div");
            div1.Attributes.Add("class", "form-row");
            div1.InnerHtml = label.ToHtmlString() + yesRadio + yesLabel.ToString() + noRadio + noLabel.ToString() + help.ToHtmlString();
            return new MvcHtmlString(div1.ToString(TagRenderMode.Normal));
        }

        private static string CreateRadioButton(string id, string value, string idSuffix, bool ischecked)
        {
            var radio = new TagBuilder("input");
            radio.MergeAttributes(
                new Dictionary<string, string>
                    {
                        {"type", "radio"},
                        {"name", id},
                        {"id", string.Format("{0}-{1}",id,idSuffix)},
                        {"class", "check"},
                        {"value", value}
                    });
            if (ischecked)
            {
                radio.MergeAttribute("checked", "checked");
            }
            return radio.ToString(TagRenderMode.SelfClosing);
        }

        public static MvcHtmlString LabelValidationAndDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes, bool stacked)
        {
            var label = html.LabelWithRequiredFor(expression);
            var dropdown = html.DropDownListFor(expression, selectList, optionLabel, htmlAttributes);
            var validation = html.ValidationMessageFor(expression);
            var help = html.HelpFor(expression);
            if (stacked)
            {
                var div1 = new TagBuilder("div");
                div1.Attributes.Add("class", "form-row");
                div1.InnerHtml = label.ToHtmlString();
                var div2 = new TagBuilder("div");
                div2.Attributes.Add("class", "form-row");
                div2.InnerHtml = dropdown.ToHtmlString() + help.ToHtmlString() + validation.ToHtmlString();
                return new MvcHtmlString(div1.ToString(TagRenderMode.Normal) + div2.ToString(TagRenderMode.Normal));
            }
            else
            {
                var div1 = new TagBuilder("div");
                div1.Attributes.Add("class", "form-row");
                div1.InnerHtml = label.ToHtmlString() + dropdown.ToHtmlString() + help.ToHtmlString() + validation.ToHtmlString();
                return new MvcHtmlString(div1.ToString(TagRenderMode.Normal));
            }
        }

        public static MvcHtmlString LabelValidationAndPasswordFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes, bool stacked)
        {
            var label = html.LabelWithRequiredFor(expression);
            var password = html.PasswordFor(expression, htmlAttributes);
            var validation = html.ValidationMessageFor(expression);
            var help = html.HelpFor(expression);
            if (stacked)
            {
                var div1 = new TagBuilder("div");
                div1.Attributes.Add("class", "form-row");
                div1.InnerHtml = label.ToHtmlString();
                var div2 = new TagBuilder("div");
                div2.Attributes.Add("class", "form-row");
                div2.InnerHtml = password.ToHtmlString() + help.ToHtmlString() + validation.ToHtmlString();
                return new MvcHtmlString(div1.ToString(TagRenderMode.Normal) + div2.ToString(TagRenderMode.Normal));
            }
            else
            {
                var div1 = new TagBuilder("div");
                div1.Attributes.Add("class", "form-row");
                div1.InnerHtml = label.ToHtmlString() + password.ToHtmlString() + help.ToHtmlString() + validation.ToHtmlString();
                return new MvcHtmlString(div1.ToString(TagRenderMode.Normal));
            }
        }

        public static MvcHtmlString LabelValidationAndCalendarFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes, bool stacked)
        {
            var label = html.LabelWithRequiredFor(expression);
            var calendar = html.CalendarFor(expression);
            var validation = html.ValidationMessageFor(expression);
            var help = html.HelpFor(expression);
            if (stacked)
            {
                var div1 = new TagBuilder("div");
                div1.Attributes.Add("class", "form-row");
                div1.InnerHtml = label.ToHtmlString();
                var div2 = new TagBuilder("div");
                div2.Attributes.Add("class", "form-row");
                div2.InnerHtml = calendar.ToHtmlString() + help.ToHtmlString() + validation.ToHtmlString();
                return new MvcHtmlString(div1.ToString(TagRenderMode.Normal) + div2.ToString(TagRenderMode.Normal));
            }
            else
            {
                var div1 = new TagBuilder("div");
                div1.Attributes.Add("class", "form-row");
                div1.InnerHtml = label.ToHtmlString() + calendar.ToHtmlString() + help.ToHtmlString() + validation.ToHtmlString();
                return new MvcHtmlString(div1.ToString(TagRenderMode.Normal));
            }
        }

        public static MvcHtmlString CalendarFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string name = ExpressionHelper.GetExpressionText(expression);
            string fullName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            string value = String.Empty;
            if ( metadata.Model is DateTime )
            {
                var date = (DateTime)metadata.Model;
                value = date.ToString("dd-MM-yyyy");
            }
            else if(metadata.Model is DateTime?)
            {
                var date = (DateTime?)metadata.Model;
                value = date.HasValue ? date.Value.ToString("dd-MM-yyyy") : string.Empty;
            }
            var calendar = new TagBuilder("input");
            calendar.MergeAttribute("id", fullName, true);
            calendar.MergeAttribute("name", fullName, true);
            calendar.MergeAttribute("type", "text");
            calendar.MergeAttribute("class", "datepicker");
            calendar.MergeAttribute("value", value);

            return new MvcHtmlString(calendar.ToString(TagRenderMode.SelfClosing));
            
        }

        public static MvcHtmlString LabelValidationAndFileFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes, bool stacked)
        {
            var label = html.LabelWithRequiredFor(expression);
            var file = html.FileFor(expression);
            var validation = html.ValidationMessageFor(expression);
            var help = html.HelpFor(expression);
            if (stacked)
            {
                var div1 = new TagBuilder("div");
                div1.Attributes.Add("class", "form-row");
                div1.InnerHtml = label.ToHtmlString();
                var div2 = new TagBuilder("div");
                div2.Attributes.Add("class", "form-row");
                div2.InnerHtml = file.ToHtmlString() + help.ToHtmlString() + validation.ToHtmlString();
                return new MvcHtmlString(div1.ToString(TagRenderMode.Normal) + div2.ToString(TagRenderMode.Normal));
            }
            else
            {
                var div1 = new TagBuilder("div");
                div1.Attributes.Add("class", "form-row");
                div1.InnerHtml = label.ToHtmlString() + file.ToHtmlString() + help.ToHtmlString() + validation.ToHtmlString();
                return new MvcHtmlString(div1.ToString(TagRenderMode.Normal));
            }
        }

        public static MvcHtmlString FileFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string name = ExpressionHelper.GetExpressionText(expression);
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            var file = new TagBuilder("input");
            file.MergeAttribute("id", fullName, true);
            file.MergeAttribute("name", fullName, true);
            file.MergeAttribute("type", "file");
            file.MergeAttribute("class", "file");

            return new MvcHtmlString(file.ToString(TagRenderMode.SelfClosing));
        }

        public static IEnumerable<SelectListItem> EnumSelectList<TEnum>(this TEnum enumeration)
        {
            return enumeration.EnumSelectList(null);
        }

        public static IEnumerable<SelectListItem> EnumSelectList<TEnum>(this TEnum enumeration, int? value)
        {
            Type enumType = enumeration.GetType();
            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

            var items = values.Select(v => new SelectListItem
            {
                Text = v.EnumToString(),
                Value = Convert.ToInt32(v).ToString(),
                Selected = Convert.ToInt32(v) == value
            });

            return items;
        }

        public static IEnumerable<SelectListItem> EnumSelectListNoValues<TEnum>(this TEnum enumeration)
        {
            Type enumType = enumeration.GetType();
            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

            var items = values.Select(v => new SelectListItem
            {
                Text = v.EnumToString()
            });

            return items;
        }

    }
}