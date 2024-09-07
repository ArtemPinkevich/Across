namespace Entities;

public enum DriverRequestStatus
{
    PendingLogistReview,
    PendingShipperApprove,
    Approved,
    Hold,
    TakenByOtherDriver,
    Declined,
}