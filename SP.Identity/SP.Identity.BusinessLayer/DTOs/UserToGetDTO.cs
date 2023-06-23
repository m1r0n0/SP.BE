namespace SP.Identity.BusinessLayer.DTOs
{
    public class UserToGetDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = string.Empty;
        public string DateOfSingUp { get; set; } = string.Empty;
    }
}
