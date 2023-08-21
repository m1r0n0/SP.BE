namespace SP.Service.BusinessLayer.DTOs;

public class AvailabilityScheduleDTO
{
    public DateTime Date { get; set; }
    public List<int> UnavailableHours { get; set; }
}