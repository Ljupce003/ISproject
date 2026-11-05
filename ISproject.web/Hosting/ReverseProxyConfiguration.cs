using System.Net;
using Microsoft.AspNetCore.HttpOverrides;

namespace ISproject.web.Hosting;

public static class ReverseProxyConfiguration
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        var networks = new List<Microsoft.AspNetCore.HttpOverrides.IPNetwork>();
        foreach (var cidr in (configuration["TRUSTED_PROXY_CIDRS"] ?? "")
            .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = cidr.Split('/');
            if (parts.Length != 2 || !IPAddress.TryParse(parts[0], out var address)
                || !int.TryParse(parts[1], out var prefix)
                || prefix < 0 || prefix > address.GetAddressBytes().Length * 8)
            {
                throw new InvalidOperationException($"Invalid trusted proxy CIDR: {cidr}");
            }
            networks.Add(new Microsoft.AspNetCore.HttpOverrides.IPNetwork(address, prefix));
        }

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor
                | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
            options.ForwardLimit = 1;
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
            options.KnownProxies.Add(IPAddress.Loopback);
            options.KnownProxies.Add(IPAddress.IPv6Loopback);
            foreach (var network in networks)
            {
                options.KnownNetworks.Add(network);
            }
            foreach (var host in (configuration["AllowedHosts"] ?? "")
                .Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            {
                options.AllowedHosts.Add(host);
            }
        });
    }
}
