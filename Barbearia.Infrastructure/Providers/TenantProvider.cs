using Microsoft.AspNetCore.Http;

namespace Barbearia.Infrastructure.Providers
{
	public class TenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid ObterTenantId()
        {
            var tenantIdClaim = _httpContextAccessor.HttpContext?.User?.Claims
                .FirstOrDefault(c => c.Type == "tenant_id");

            if (tenantIdClaim == null)
                throw new UnauthorizedAccessException("TenantId não encontrado no token.");

            return Guid.Parse(tenantIdClaim.Value);
        }

    }
}
