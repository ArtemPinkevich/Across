namespace Entities;

public enum CarBodyType
{
        // все закр.+изотерм
    tentTruck,              // тентованный
    container,              // контейнер
    van,                    // фургон
    allMetal,               // цельнометалл
    isothermal,             // изотермический

    // реф.+изотерм
    refrigerator,           // рефрижератор
    refrigeratorMult,       // реф. мультирежимный
    bulkheadRefr,           // реф. с перегородкой
    meatRailsRef,           // реф.-тушевоз
    
    // Открытые
    flatbed,                // бортовой
    opentop,                // открытый конт.
    opentrailer,            // площадка без бортов
    dumpTruck,              // самосвал
    barge,                  // шаланда

    // Негабарит
    dolly,                  // низкорамный
    dollyPlat,              // низкорам.платф.
    adjustable,             // телескопический
    tral,                   // трал
    beamTruckNgb,           // балковоз(негабарит)

    // Остальные
    bus,                    // автобус
    Autocart,               // автовоз
    autotower,              // автовышка
    autoCarrier,            // автотранспортер
    сoncreteTruck,          // бетоновоз
    bitumenTruck,           // битумовоз
    fuelTank,               // бензовоз
    offRoader,              // вездеход
    gas,                    // газовоз
    grainTruck,             // зерновоз
    horseTruck,             // коневоз
    containerTrail,         // контейнеровоз
    furageTuck,             // кормовоз
    crane,                  // кран
    timberTruck,            // лесовоз
    scrapTruck,             // ломовоз
    manipulator,            // манипулятор
    microbus,               // микроавтобус
    flourTruck,             // муковоз
    panelsTruck,            // панелевоз
    pickup,                 // пикап
    ripetruck,              // пухтовоз
    pyramid,                // пирамида
    rollTruck,              // рулоновоз
    tractor,                // седельный тягач
    cattle,                 // скотовоз
    innloader,              // стекловоз
    pipeTruck,              // трубовоз
    cementTruck,            // цементовоз
    tankerTruck,            // автоцистерна
    chipTruck,              // щеповоз
    wrecker,                // эвакуатор
    dualPurpose,            // грузопассажирский
    klyushkovoz,            // клюшковоз
    garbageTruck,           // мусоровоз
    jumbo,                  // jumbo
    tankContainer20,        // 20' танк-контейнер
    tankContainer40,        // 40' танк-контейнер
    mega,                   // мега фура
    doppelstock,            // допельшток
    slidingSemiTrailer2040, // Раздвижной полуприцеп 20'/40'
}