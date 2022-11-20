using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marval.Domain.Entities
{
    public class Employee
    {
        [Key]
        public int Identity { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string Surname { get; set; }
        public int Age { get; set; }
        [StringLength(16)]
        public string Sex { get; set; }
        [StringLength(15)]
        public string Mobile { get; set; }
        public bool Active { get; set; }
    }
}
