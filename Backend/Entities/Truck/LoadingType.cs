using System;

namespace Entities;

[Flags]
public enum LoadingType
{
    None = 0,
    Top = 1 << 0,                     // верхняя
    Side  = 1 << 1,                   // боковая
    Rear  = 1 << 2,                   // задняя
    Full = 1 << 3,                    // с полной растентовкой
    WithSlidingRoof = 1 << 4,         // со снятием поперечных перекладин
    WithRemovablePillars = 1 << 5,    // со снятием стоек
    WithoutGates = 1 << 6,            // без ворот
    Hydroboard = 1 << 7,              // гидроборт
    Apparels = 1 << 8,                // аппарели
    WithCrate = 1 << 9,               // с обрешеткой
    WithBoards = 1 << 10,             // с бортами
    SideBySide = 1 << 11,             // боковая с 2-х сторон
    Pour = 1 << 12,                   // налив
    Electric = 1 << 13,               // электрический
    Hydraulic = 1 << 14,              // гидравлический
    Unspecified = 1 << 15,            // не указан
    Pneumatic = 1 << 16,              // пневматический
    DieselCompressor = 1 << 17,       // дизельный компрессор
}