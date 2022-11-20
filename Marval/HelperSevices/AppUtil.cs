using Marval.HelperInterface;
using Marval.Models;
using Microsoft.AspNetCore.Authentication.OAuth;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace Marval.HelperSevices
{
    public class AppUtil : IAppUtil
    {
        public string GetMessage(string alertType, string Msg)
        {
            return $"<div class=\"alert alert-{alertType} alert-dismissible fade show\" role=\"alert\">{Msg}<button type=\"button\" class=\"btn-close btn-sm\" data-bs-dismiss=\"alert\" aria-label=\"Close\"></button></div>";
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            //Check if phoneNumber is a valid phone number.
            if(string.IsNullOrEmpty(phoneNumber))
                return false;

            bool isPhone = Regex.IsMatch(phoneNumber, "^[0-9]+$");
            return isPhone;
        }

        public (string message, bool isValid) ValidateEmployee(EmployeeCSVModel employeeModel)
        {
            string response = "";
            bool isvalid = true;
            int age;
            if (string.IsNullOrEmpty(employeeModel.Surname))
            {
                response = "Last name can not be empty";
                isvalid = false;
            }
            else if (string.IsNullOrEmpty(employeeModel.FirstName))
            {
                response = "first name can not be empty";
                isvalid = false;
            }
            else if (!int.TryParse(employeeModel.Age, out age))
            {
                response = $"{employeeModel.Age} is not a valid age.";
                isvalid = false;
            }
            else if (!new List<string>() { "true", "false" }.Contains(employeeModel.Active.ToLower()))
            {
                response = $"{employeeModel.Active} is not valid, you can only enter true or false.";
                isvalid = false;
            }
            else if (!new List<string>() { "m", "f" }.Contains(employeeModel.Sex.ToLower()))
            {
                response = $"{employeeModel.Sex} is not valid, you can only enter M or F.";
                isvalid = false;
            }
            else if (!IsValidPhoneNumber(employeeModel.Mobile))
            {
                response = $"{employeeModel.Mobile} is not a valid mobile number.";
                isvalid = false;
            }
            return (response, isvalid);
        }
    }
}

public enum alert
{
    success,
    danger,
    warning
}
