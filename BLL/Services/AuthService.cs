using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IClientRepository _clientRepository;

        public AuthService(IRefreshTokenRepository refreshTokenRepository,
                                        IClientRepository clientRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _clientRepository = clientRepository;
        }

        public Task<RefreshToken> CreateTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            return _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);
        }

        public Task<RefreshToken> DeleteTokenAsync(string refreshTokenId, CancellationToken cancellationToken)
        {
            return _refreshTokenRepository.DeleteAsync(refreshTokenId, cancellationToken);
        }

        public Task<RefreshToken> FindTokenAsync(string refreshTokenId, CancellationToken cancellationToken)
        {
            return _refreshTokenRepository.GetAsync(refreshTokenId, cancellationToken, shouldThrow: false);
        }

        public Task<Client> FindClientAsync(string clientId, CancellationToken cancellationToken)
        {
            return _clientRepository.GetAsync(clientId, cancellationToken, shouldThrow: false);
        }

        public int GetPrincipalId(IPrincipal principal)
        {
            try
            {
                var claims = principal as ClaimsPrincipal;
                var claim = claims.Claims.Single(c => c.Type.Equals(ClaimTypes.NameIdentifier));
                var id = int.Parse(claim.Value);
                return id;
            }
            catch
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
