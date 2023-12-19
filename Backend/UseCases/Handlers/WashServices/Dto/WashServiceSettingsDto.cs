namespace UseCases.Handlers.WashServices.Dto;

public class WashServiceSettingsDto
{
    public bool Enabled { set; get; }

    public int PriceGroupId { set; get; }
    
    public int Price { set; get; }
    
    public int Duration { set; get; }
}