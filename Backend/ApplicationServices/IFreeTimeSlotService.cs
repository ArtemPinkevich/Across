using System;
using System.Collections.Generic;

using Entities;

namespace ApplicationServices.Interfaces;

public interface IFreeTimeSlotService
{
    bool HasBoxFreeTimeSlot(TimeOnly startTime, TimeOnly endTime, List<Record> records, int reservedMinBetweenRecords);
}