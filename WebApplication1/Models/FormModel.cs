using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class FormModel
    {
        [Key]
        public int TrIndex { get; set; }
        public DateTime? TrDateTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate  { get; set; }
    }
}
