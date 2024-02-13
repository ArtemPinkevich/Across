using System.Collections.Generic;

namespace UseCases.Handlers.Truck.Dto;

public class TrucksListResultDto
{
    public TruckResultDto Result { set; get; }
    public List<TruckDto> Trucks { set; get; }
}