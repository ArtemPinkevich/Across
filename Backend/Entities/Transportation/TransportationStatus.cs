namespace Entities;

public enum TransportationStatus
{
    NotPublished,
    ApprovedByManager,
    ApprovedByLawyer,
    ReadyToLoad,
    WaitingForLoading,
    Transporting,
    Delivered
}