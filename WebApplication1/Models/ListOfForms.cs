namespace WebApplication1.Models
{
    public class ListOfForms
    {
        public IEnumerable<FormModel> FormList { get; set; }
        public string? Errormessage { get; set; }
        public FormModel formSingle { get; set; }
    }
}
