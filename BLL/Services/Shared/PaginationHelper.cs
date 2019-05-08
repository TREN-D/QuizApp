using BLL.Models.Shared;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Shared
{
    public static class PaginationHelper
    {
        public static IQueryable<Test> GetPaginatedTestsById(this IQueryable<Test> tests, PaginationModel paginationModel)
        {
            return tests.OrderBy(t => t.Id).Skip(paginationModel.PageIndex * paginationModel.PageSize).Take(paginationModel.PageSize);
        }
    }
}
