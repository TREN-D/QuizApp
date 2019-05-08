using AutoMapper;
using BLL.Interfaces;
using BLL.Models.URLModels;
using DAL.Entities;
using DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository _urlRepository;
        private readonly ITestRepository _testRepository;
        private readonly IAdminRepository _adminRepository;

        public UrlService(IUrlRepository urlRepository,
                            ITestRepository testRepository,
                            IAdminRepository adminRepository)
        {
            _urlRepository = urlRepository;
            _testRepository = testRepository;
            _adminRepository = adminRepository;
        }

        public async Task<URLModel> GetAsync(string identifier, CancellationToken cancellationToken)
        {
            var url = await _urlRepository.GetAsync(identifier, cancellationToken);

            var urlModel = Mapper.Map<URL, URLModel>(url);
            return urlModel;
        }

        public async Task<URLModel> CreateAsync(CreateURLModel createUrlModel, CancellationToken cancellationToken)
        {
            var admin = await _adminRepository.GetAsync(createUrlModel.CreatedById, cancellationToken);
            var test = await _testRepository.GetAsync(createUrlModel.TestId, cancellationToken);
            var url = Mapper.Map<CreateURLModel, URL>(createUrlModel);

            url.Identifier = Guid.NewGuid().ToString();

            url = await _urlRepository.AddAsync(url, cancellationToken);
            var urlModel = Mapper.Map<URL, URLModel>(url);
            
            return urlModel;
        }
    }
}
