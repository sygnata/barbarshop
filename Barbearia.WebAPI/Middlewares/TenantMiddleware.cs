namespace Barbearia.WebAPI.Middlewares
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var tenantId = context.User.Claims.FirstOrDefault(c => c.Type == "tenant_id")?.Value.ToString();

            if (string.IsNullOrEmpty(tenantId) || !Guid.TryParse(tenantId, out var tenantGuid))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Tenant Id inválido no token.");
                return;
            }

            context.Items["TenantId"] = tenantGuid;
            await _next(context);
        }
    }
}
