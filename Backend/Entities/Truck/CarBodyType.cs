namespace Entities;

public enum CarBodyType
{
        // все закр.+изотерм
    TentTruck,              // тентованный
    Container,              // контейнер
    Van,                    // фургон
    AllMetal,               // цельнометалл
    Isothermal,             // изотермический

    // реф.+изотерм
    Refrigerator,           // рефрижератор
    RefrigeratorMult,       // реф. мультирежимный
    BulkheadRefr,           // реф. с перегородкой
    MeatRailsRef,           // реф.-тушевоз
    
    // Открытые
    Flatbed,                // бортовой
    Opentop,                // открытый конт.
    Opentrailer,            // площадка без бортов
    DumpTruck,              // самосвал
    Barge,                  // шаланда

    // Негабарит
    Dolly,                  // низкорамный
    DollyPlat,              // низкорам.платф.
    Adjustable,             // телескопический
    Tral,                   // трал
    BeamTruckNgb,           // балковоз(негабарит)

    // Остальные
    Bus,                    // автобус
    Autocart,               // автовоз
    Autotower,              // автовышка
    AutoCarrier,            // автотранспортер
    ConcreteTruck,          // бетоновоз
    BitumenTruck,           // битумовоз
    FuelTank,               // бензовоз
    OffRoader,              // вездеход
    Gas,                    // газовоз
    GrainTruck,             // зерновоз
    HorseTruck,             // коневоз
    ContainerTrail,         // контейнеровоз
    FurageTuck,             // кормовоз
    Crane,                  // кран
    TimberTruck,            // лесовоз
    ScrapTruck,             // ломовоз
    Manipulator,            // манипулятор
    Microbus,               // микроавтобус
    FlourTruck,             // муковоз
    PanelsTruck,            // панелевоз
    Pickup,                 // пикап
    Ripetruck,              // пухтовоз
    Pyramid,                // пирамида
    RollTruck,              // рулоновоз
    Tractor,                // седельный тягач
    Cattle,                 // скотовоз
    Innloader,              // стекловоз
    PipeTruck,              // трубовоз
    CementTruck,            // цементовоз
    TankerTruck,            // автоцистерна
    ChipTruck,              // щеповоз
    Wrecker,                // эвакуатор
    DualPurpose,            // грузопассажирский
    Klyushkovoz,            // клюшковоз
    GarbageTruck,           // мусоровоз
    Jumbo,                  // jumbo
    TankContainer20,        // 20' танк-контейнер
    TankContainer40,        // 40' танк-контейнер
    Mega,                   // мега фура
    Doppelstock,            // допельшток
    SlidingSemiTrailer2040, // Раздвижной полуприцеп 20'/40'
}