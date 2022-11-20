using CsvHelper.Configuration.Attributes;

namespace Marval.Models
{
    public class EmployeeCSVModel
    {
        [Name("Identity")]
        public int Identity { get; set; }
        [Name("FirstName")]
        public string FirstName { get; set; }
        [Name("Surname")]
        public string Surname { get; set; }
        [Name("Age")]
        public string Age { get; set; }
        [Name("Sex")]
        public string Sex { get; set; }
        [Name("Mobile")]
        public string Mobile { get; set; }
        [Name("Active")]
        public string Active { get; set; }
    }
}
