using Marval.Domain.Abstracts;
using Marval.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marval.Domain.Contracts
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(MarvalDomainContext context) : base(context)
        {

        }
    }
}
