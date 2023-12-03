using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class RoleModelView
    {

        [Key]
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
