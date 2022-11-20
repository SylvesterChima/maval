using Marval.Domain.Entities;
using Marval.Models;

namespace Marval.HelperInterface
{
    public interface IAppUtil
    {
        string GetMessage(string alertType, string Msg);
        bool IsValidPhoneNumber(string phoneNumber);

        (string message, bool isValid) ValidateEmployee(EmployeeCSVModel employeeModel);
    }
}
