namespace SP.Service.BusinessLayer.DTOs
{
    public class ServiceInfoDTO
    {
        public int Price { get; set; }
        public string ProviderId { get; set; }
        public List<EventInfoDTO> Events { get; set; }
    }
}
