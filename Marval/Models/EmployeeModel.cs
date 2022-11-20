using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Marval.Models
{
    public class EmployeeModel
    {
        public int Identity { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Sex is required")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        public string Mobile { get; set; }
        public bool Active { get; set; }
    }
}
