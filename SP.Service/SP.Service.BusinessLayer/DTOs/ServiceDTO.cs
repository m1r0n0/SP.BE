namespace SP.Service.BusinessLayer.DTOs;

public class ServiceDTO : ServiceInfoDTO
{
    public string ServiceId { get; set; } 
    public List<EventInfoDTO> Events { get; set; }
}