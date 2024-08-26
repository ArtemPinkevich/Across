﻿using System.Collections.Generic;

namespace Entities;

public class Truck: EntityBase
{
#warning move from here
    public string TruckLocation { set; get; }
#warning move from here
    /// <summary>
    /// List of Orders which truck wants to take
    /// </summary>
    /// <returns></returns>
    public List<TransportationOrder> OrdersOfferedForTruck { set; get; }
    
    public string CreatedId { set; get; }
    
    public string RegNumber { set; get; }
    
    public TrailerType TrailerType { set; get; }
    
    public CarBodyType CarBodyType { set; get; }
    
    public LoadingType LoadingType { set; get; }
    
    public bool HasLtl { set; get; }
    
    public bool HasLiftGate { set; get; }
    
    public bool HasStanchionTrailer { set; get; }
    
    public int CarryingCapacity { set; get; }
    
    public int BodyVolume { set; get; }
    
    public int InnerBodyLength { set; get; }
    
    public int InnerBodyWidth { set; get; }
    
    public int InnerBodyHeight { set; get; }
    
    public bool Adr1 { set; get; }
    
    public bool Adr2 { set; get; }
    
    public bool Adr3 { set; get; }
    
    public bool Adr4 { set; get; }
    
    public bool Adr5 { set; get; }
    
    public bool Adr6 { set; get; }
    
    public bool Adr7 { set; get; }
    
    public bool Adr8 { set; get; }
    
    public bool Adr9 { set; get; }
    
    public bool Tir { set; get; }
    
    public bool Ekmt { set; get; }
    
    public string UserId { set; get; }
    public User User { set; get; }
}