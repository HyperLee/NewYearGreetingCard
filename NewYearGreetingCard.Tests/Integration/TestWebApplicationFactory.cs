using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace NewYearGreetingCard.Tests.Integration;

/// <summary>
/// 提供整合測試使用的 WebApplicationFactory 設定。
/// </summary>
public sealed class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    /// <inheritdoc />
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
    }
}
