namespace Application.Contracts.Infrastructure.ExternalServices;

public interface IVendor
{
    Task PlaceOrderAsync();
    Task TrackOrderAsync(string orderId);
}
