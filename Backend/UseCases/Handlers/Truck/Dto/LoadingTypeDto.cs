namespace UseCases.Handlers.Truck.Dto;

public enum LoadingTypeDto
{
    Top,                    // верхняя
    Side,                   // боковая
    Rear,                   // задняя
    Full,                   // с полной растентовкой
    WithSlidingRoof,        // со снятием поперечных перекладин
    WithRemovablePillars,   // со снятием стоек
    WithoutGates,           // без ворот
    Hydroboard,             // гидроборт
    Apparels,               // аппарели
    WithCrate,              // с обрешеткой
    WithBoards,             // с бортами
    SideBySide,             // боковая с 2-х сторон
    Pour,                   // налив
    Electric,               // электрический
    Hydraulic,              // гидравлический
    Unspecified,            // не указан
    Pneumatic,              // пневматический
    DieselCompressor,       // дизельный компрессор
}