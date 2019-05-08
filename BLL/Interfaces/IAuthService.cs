using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        Task<Client> FindClientAsync(string clientId, CancellationToken cancellationToken);
        Task<RefreshToken> CreateTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
        Task<RefreshToken> DeleteTokenAsync(string refreshTokenId, CancellationToken cancellationToken);
        Task<RefreshToken> FindTokenAsync(string refreshTokenId, CancellationToken cancellationToken);
        int GetPrincipalId(IPrincipal principal);
    }
}
