using Application.Contracts.Infrastructure.ExternalServices;

namespace Infrastructure.Repositories;

internal class VendorProviderService : IVendorProviderService
{
    private readonly IDictionary<string, Func<IVendor>> _factory;

    public VendorProviderService(IDictionary<string, Func<IVendor>> factory)
    {
        _factory = factory;
    }

    public IVendor GetVendorServiceAsync(string vendorName)
    {
        var factory = _factory[vendorName];
        var notMethod = factory == null ? null : factory();
        return notMethod;
    }
}
