using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MachineTestRebin.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        [Column(TypeName = "CHAR")]
        [MaxLength(1)]
        public string Gender { get; set; }
        [Column(TypeName = "date")]
        public DateTime Birthday { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string Time { get; set; }
    }
}
