namespace Entities;

public class WashServiceSettings : EntityBase
{
    public int WashServiceId { set; get; }
    public WashService WashService { set; get; }
    
    public int PriceGroupId { set; get; }
    public PriceGroup PriceGroup { set; get; }
    
    public int DurationMinutes { set; get; }
    
    public int Price { set; get; }

    public bool Enabled { set; get; }
}