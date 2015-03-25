//-----------------------------------------------------------------------
// <copyright file="HelpTextAttribute.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace Extant.Web.Infrastructure
{
    public class HelpTextAttribute : Attribute
    {
        public HelpTextAttribute(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}