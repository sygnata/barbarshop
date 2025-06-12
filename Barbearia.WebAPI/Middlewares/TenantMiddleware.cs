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
            var tenantId = context.User.Claims.FirstOrDefault(c => c.Type == "tenant_id")?.Value;
            if (string.IsNullOrEmpty(tenantId))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Tenant Id não encontrado no token.");
                return;
            }
            context.Items["TenantId"] = Guid.Parse(tenantId);
            await _next(context);
        }
    }
}
