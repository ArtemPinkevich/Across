﻿namespace GeoService.Entities;

public class City
{
    public int Id { set; get; }
    
    public string Name { set; get; }
    
    public int CountryId { set; get; }
    public Country Country { set; get; }
}