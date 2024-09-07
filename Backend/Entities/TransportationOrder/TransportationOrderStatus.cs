namespace Entities;

public enum TransportationOrderStatus
{
    NotPublished,
    CarrierFinding,
    ManagerApproving,
    ShipperApproving,
    WaitingForLoading,
    Loading,
    Transporting,
    Unloading,
    Delivered,
    Done
}