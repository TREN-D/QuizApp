using BLL.Models.URLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUrlService
    {
        Task<URLModel> CreateAsync(CreateURLModel createUrlModel, CancellationToken cancellationToken);
        Task<URLModel> GetAsync(string identifier, CancellationToken cancellationToken);
    }
}
