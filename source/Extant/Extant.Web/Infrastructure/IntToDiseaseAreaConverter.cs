//-----------------------------------------------------------------------
// <copyright file="IntToDiseaseAreaConverter.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using AutoMapper;
using Extant.Data.Entities;
using Extant.Data.Repositories;

namespace Extant.Web.Infrastructure
{
    public class IntToDiseaseAreaConverter : TypeConverter<int, DiseaseArea>
    {

        private readonly IDiseaseAreaRepository DiseaseAreaRepo;

        public IntToDiseaseAreaConverter(IDiseaseAreaRepository diseaseAreaRepo)
        {
            DiseaseAreaRepo = diseaseAreaRepo;
        }

        protected override DiseaseArea ConvertCore(int source)
        {
            return DiseaseAreaRepo.Get(source);
        }
    }
}