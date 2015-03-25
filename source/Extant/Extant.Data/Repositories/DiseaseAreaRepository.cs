//-----------------------------------------------------------------------
// <copyright file="DiseaseAreaRepository.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Extant.Data.Entities;

namespace Extant.Data.Repositories
{
    public interface IDiseaseAreaRepository : IRepository<DiseaseArea>
    {
        IEnumerable<DiseaseArea> GetAllPublished();
        IDictionary<int, int> GetStudyCounts();
    }

    public class DiseaseAreaRepository : Repository<DiseaseArea>, IDiseaseAreaRepository
    {
        public DiseaseAreaRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<DiseaseArea> GetAllPublished()
        {
            return Find("Published", true).OrderBy(da => da.DiseaseAreaName);
        }

        public IDictionary<int, int> GetStudyCounts()
        {
            return
                UnitOfWork.CurrentSession.CreateQuery(
                    "select da.Id, count(*) from Study s join s.DiseaseAreas da where s.Published=? group by da.Id"
                ).SetBoolean(0, true).List<object[]>().ToDictionary(o => (int) o[0], o => Convert.ToInt32(o[1]));
        }
    }
}