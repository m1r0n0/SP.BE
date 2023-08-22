using System.Runtime.InteropServices.JavaScript;

namespace SP.Service.BusinessLayer.DTOs;

public class AvailabilityScheduleDTO
{
    public DateTime Date { get; set; }
    public List<int> UnavailableHours { get; set; } = new List<int>();

    public AvailabilityScheduleDTO(){}
    public AvailabilityScheduleDTO(DateTime date)
    {
        Date = date;
    }
}