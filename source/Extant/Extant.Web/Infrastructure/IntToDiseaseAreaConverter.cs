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
    public class IntToDiseaseAreaConverter : ITypeConverter<int, DiseaseArea>
    {

        private readonly IDiseaseAreaRepository DiseaseAreaRepo;

        public IntToDiseaseAreaConverter(IDiseaseAreaRepository diseaseAreaRepo)
        {
            DiseaseAreaRepo = diseaseAreaRepo;
        }

        public DiseaseArea Convert(int source, DiseaseArea destination, ResolutionContext context)
        {
            return DiseaseAreaRepo.Get(source);
        }
    }
}