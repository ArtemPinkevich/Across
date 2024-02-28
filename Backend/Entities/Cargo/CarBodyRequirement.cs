namespace Entities;

public class CarBodyRequirement
{
    // все закр.+изотерм
    public bool TentTruck { set; get; }              // тентованный
    public bool Container { set; get; }              // контейнер
    public bool Van { set; get; }                    // фургон
    public bool AllMetal { set; get; }               // цельнометалл
    public bool Isothermal { set; get; }             // изотермический

    // реф.+изотерм
    public bool Refrigerator { set; get; }           // рефрижератор
    public bool RefrigeratorMult { set; get; }       // реф. мультирежимный
    public bool BulkheadRefr { set; get; }           // реф. с перегородкой
    public bool MeatRailsRef { set; get; }           // реф.-тушевоз
    
    // Открытые
    public bool Flatbed { set; get; }                // бортовой
    public bool Opentop { set; get; }                // открытый конт.
    public bool Opentrailer { set; get; }            // площадка без бортов
    public bool DumpTruck { set; get; }              // самосвал
    public bool Barge { set; get; }                  // шаланда

    // Негабарит
    public bool Dolly { set; get; }                  // низкорамный
    public bool DollyPlat { set; get; }              // низкорам.платф.
    public bool Adjustable { set; get; }             // телескопический
    public bool Tral { set; get; }                   // трал
    public bool BeamTruckNgb { set; get; }           // балковоз(негабарит)

    // Остальные
    public bool Bus { set; get; }                    // автобус
    public bool Autocart { set; get; }               // автовоз
    public bool Autotower { set; get; }              // автовышка
    public bool AutoCarrier { set; get; }            // автотранспортер
    public bool ConcreteTruck { set; get; }          // бетоновоз
    public bool BitumenTruck { set; get; }           // битумовоз
    public bool FuelTank { set; get; }               // бензовоз
    public bool OffRoader { set; get; }              // вездеход
    public bool Gas { set; get; }                    // газовоз
    public bool GrainTruck { set; get; }             // зерновоз
    public bool HorseTruck { set; get; }             // коневоз
    public bool ContainerTrail { set; get; }         // контейнеровоз
    public bool FurageTuck { set; get; }             // кормовоз
    public bool Crane { set; get; }                  // кран
    public bool TimberTruck { set; get; }            // лесовоз
    public bool ScrapTruck { set; get; }             // ломовоз
    public bool Manipulator { set; get; }            // манипулятор
    public bool Microbus { set; get; }               // микроавтобус
    public bool FlourTruck { set; get; }             // муковоз
    public bool PanelsTruck { set; get; }            // панелевоз
    public bool Pickup { set; get; }                 // пикап
    public bool Ripetruck { set; get; }              // пухтовоз
    public bool Pyramid { set; get; }                // пирамида
    public bool RollTruck { set; get; }              // рулоновоз
    public bool Tractor { set; get; }                // седельный тягач
    public bool Cattle { set; get; }                 // скотовоз
    public bool Innloader { set; get; }              // стекловоз
    public bool PipeTruck { set; get; }              // трубовоз
    public bool CementTruck { set; get; }            // цементовоз
    public bool TankerTruck { set; get; }            // автоцистерна
    public bool ChipTruck { set; get; }              // щеповоз
    public bool Wrecker { set; get; }                // эвакуатор
    public bool DualPurpose { set; get; }            // грузопассажирский
    public bool Klyushkovoz { set; get; }            // клюшковоз
    public bool GarbageTruck { set; get; }           // мусоровоз
    public bool Jumbo { set; get; }                  // jumbo
    public bool TankContainer20 { set; get; }        // 20' танк-контейнер
    public bool TankContainer40 { set; get; }        // 40' танк-контейнер
    public bool Mega { set; get; }                   // мега фура
    public bool Doppelstock { set; get; }            // допельшток
    public bool SlidingSemiTrailer2040 { set; get; } // Раздвижной полуприцеп 20'/40'
}