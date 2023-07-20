using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SP.Identity.BusinessLayer.DTOs
{
    public class UserNewEmailDTO
    {
        public string NewEmail { get; set; }
    }
}
