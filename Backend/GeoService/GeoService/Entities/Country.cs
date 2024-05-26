namespace GeoService.Entities;

public class Country
{
    public int Id { set; get; }
    
    public string Name { set; get; }
    
    public List<Region> Regions { set; get; }

    public List<City> Cities { set; get; }
}