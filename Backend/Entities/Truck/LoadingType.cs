namespace Entities.Truck;

public enum LoadingType
{
    top,                    // верхняя
    side,                   // боковая
    rear,                   // задняя
    full,                   // с полной растентовкой
    withSlidingRoof,        // со снятием поперечных перекладин
    withRemovablePillars,   // со снятием стоек
    withoutGates,           // без ворот
    hydroboard,             // гидроборт
    apparels,               // аппарели
    withCrate,              // с обрешеткой
    withBoards,             // с бортами
    sideBySide,             // боковая с 2-х сторон
    pour,                   // налив
    electric,               // электрический
    hydraulic,              // гидравлический
    unspecified,            // не указан
    pneumatic,              // пневматический
    dieselCompressor,       // дизельный компрессор
}