using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Entities;

public class Transportation : EntityBase
{
    public int TransportationOrderId { set; get; }
    public TransportationOrder TransportationOrder { set; get; }
    
    public string DriverId { set; get; }
    public Driver Driver { set; get; }
    
    public TransportationStatus TransportationStatus { set; get; }
    
    public string Comment { set; get; }
    
    public List<RoutePoint> RoutePoints { set; get; }

    //Тут по необходимости можно добавить другие поля связанные с
    //процессом перевозки груза (время в пути, маршрут, комментарии водителя итд)
    //public string CreatedDateTime { set; get; }
    //public string FinishedDateTime { set; get; }
}