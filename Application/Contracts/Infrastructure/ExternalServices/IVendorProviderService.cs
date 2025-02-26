namespace Application.Contracts.Infrastructure.ExternalServices;

public interface IVendorProviderService
{
    IVendor GetVendorServiceAsync(string vendorName);
}
