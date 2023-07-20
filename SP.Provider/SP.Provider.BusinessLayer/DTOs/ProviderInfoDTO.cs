using System.ComponentModel.DataAnnotations;

namespace SP.Provider.BusinessLayer.DTOs
{
    public class ProviderInfoDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EnterpriseName { get; set; }

        [Required]
        [Range(0, 23)]
        public int WorkHoursBegin { get; set; }

        [Required]
        [Range(0, 23)]
        public int WorkHoursEnd { get; set; }
    }
}
