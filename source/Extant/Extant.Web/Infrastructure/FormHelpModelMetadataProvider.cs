//-----------------------------------------------------------------------
// <copyright file="FormHelpModelMetadataProvider.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Extant.Web.Infrastructure
{
    public class FormHelpModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            var helpTextAttribute = attributes.ToList().OfType<HelpTextAttribute>().FirstOrDefault();
            if (helpTextAttribute != null)
            {
                metadata.AdditionalValues.Add("HelpText", helpTextAttribute.Text);
            }
            return metadata;
        }
    }
}