namespace Entities;

public enum TransportationOrderStatus
{
    NotPublished,
    CarrierFinding,
    ManagerApproving,
    ShipperApproving,
    Transporting,
    Delivered,
    Done
}