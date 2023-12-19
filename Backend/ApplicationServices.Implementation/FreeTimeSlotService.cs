using System;
using System.Collections.Generic;
using ApplicationServices.Interfaces;
using Entities;

namespace ApplicationServices.Implementation;

public class FreeTimeSlotService : IFreeTimeSlotService
{
    public bool HasBoxFreeTimeSlot(TimeOnly startTime, TimeOnly endTime, List<Record> records, int reservedMinBetweenRecords)
    {
        foreach (var record in records)
        {
            var recordStartTime = TimeOnly.Parse(record.StartTime);
            if (recordStartTime >= startTime && recordStartTime < endTime)
                return false;

            var recordEndTime = TimeOnly.Parse(record.StartTime).AddMinutes(record.TotalDurationMin + reservedMinBetweenRecords);
            if (recordEndTime > startTime && recordEndTime <= endTime)
                return false;

            if (recordStartTime <= startTime && recordEndTime >= endTime)
                return false;
        }

        return true;
    }
}