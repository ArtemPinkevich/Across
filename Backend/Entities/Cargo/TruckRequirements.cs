namespace Entities;

public class TruckRequirements
{
    public LoadingType LoadingType { set; get; }
    
    public LoadingType UnloadingType { set; get; }

    public CarBodyRequirement CarBodyRequirement{ set; get; }
}