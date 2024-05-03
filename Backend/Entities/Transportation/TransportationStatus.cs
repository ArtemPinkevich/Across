namespace Entities;

public enum TransportationStatus
{
    NotPublished,
    CarrierFinding,
    ApprovedByManager,
    ApprovedByLawyer,
    ReadyToLoad,
    WaitingForLoading,
    Transporting,
    Delivered
}