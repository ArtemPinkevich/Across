using System.Collections.Generic;

namespace UseCases.Handlers.Cargo.Dto;

public class CargosListDto
{
    public CargoResult Result { set; get; }
    public List<CargoDto> Cargos { set; get; }
}