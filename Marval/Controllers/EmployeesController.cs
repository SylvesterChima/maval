using CsvHelper;
using Marval.Domain.Abstracts;
using Marval.Domain.Entities;
using Marval.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Globalization;
using System;
using Marval.HelperInterface;
using System.Text;

namespace Marval.Controllers
{
    public class EmployeesController : BaseController<EmployeesController>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFileHelper _fileHelper;

        public EmployeesController(IEmployeeRepository employeeRepository, IFileHelper fileHelper)
        {
            _employeeRepository = employeeRepository;
            _fileHelper = fileHelper;
        }

        public IActionResult Index()
        {
            var employess = _employeeRepository.GetAll().ToList();
            ViewBag.Msg = TempData["msg"];
            TempData["Msg"] = null;
            return View(_mapper.Map<List<EmployeeModel>>(employess));
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = _mapper.Map<Employee>(model);
                    _employeeRepository.Add(employee);
                    await _employeeRepository.Save(GetUserName(), GetIPAddress());
                    TempData["msg"] = _appUtil.GetMessage(alert.success.ToString(), "Employee created successfully!");
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    TempData["msg"] = _appUtil.GetMessage(alert.warning.ToString(), "All field are required");
                    return RedirectToAction(nameof(Index));

                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = _appUtil.GetMessage(alert.danger.ToString(), ex.Message);
                return RedirectToAction(nameof(Index));
            }

        }

        public IActionResult Details(int id)
        {
            var employee = _employeeRepository.FindBy(c => c.Identity == id).FirstOrDefault();
            ViewBag.Msg = TempData["msg"];
            TempData["Msg"] = null;
            return View(_mapper.Map<EmployeeModel>(employee));
        }

        [HttpPost]
        public async Task<IActionResult> Details(EmployeeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = _employeeRepository.FindBy(c => c.Identity == model.Identity).FirstOrDefault();
                    if (employee != null)
                    {
                        employee.FirstName = model.FirstName;
                        employee.Surname = model.Surname;
                        employee.Age = model.Age;
                        employee.Sex = model.Sex;
                        employee.Mobile = model.Mobile;
                        employee.Active = model.Active;

                        _employeeRepository.Edit(employee);
                        await _employeeRepository.Save(GetUserName(), GetIPAddress());
                        ViewBag.Msg = _appUtil.GetMessage(alert.success.ToString(), "Updated successfully!");
                        return View(_mapper.Map<EmployeeModel>(employee));
                    }
                    else
                    {
                        TempData["msg"] = _appUtil.GetMessage(alert.warning.ToString(), "This record does not exist or has been deleted");
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    TempData["msg"] = _appUtil.GetMessage(alert.warning.ToString(), "All fields are required!");
                    return RedirectToAction(nameof(Details), new { id = model.Identity });
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = _appUtil.GetMessage(alert.danger.ToString(), ex.Message);
                return RedirectToAction(nameof(Details), new { id = model.Identity });
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employee = _employeeRepository.FindBy(c => c.Identity == id).FirstOrDefault();
                if (employee != null)
                {
                    _employeeRepository.Delete(employee);
                    await _employeeRepository.Save(GetUserName(), GetIPAddress());
                    TempData["msg"] = _appUtil.GetMessage(alert.success.ToString(), "Record deleted");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["msg"] = _appUtil.GetMessage(alert.warning.ToString(), "This record does not exist or has been deleted");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = _appUtil.GetMessage(alert.danger.ToString(), ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadCSV(IFormFile file)
        {
            try
            {
                var fileextension = Path.GetExtension(file.FileName);
                if (fileextension == ".csv")
                {
                    //upload the csv file
                    var filepath = await _fileHelper.UploadCSV(file, GetUserName()); 
                    var sb = new StringBuilder();
                    var validRecords = new List<Employee>();
                    using (var reader = new StreamReader(filepath))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        //map the csv content to employee object
                        var records = csv.GetRecords<EmployeeCSVModel>().ToList();
                        foreach (var record in records)
                        {
                            //Validate employee record 
                            var result = _appUtil.ValidateEmployee(record);
                            if (result.isValid)
                            {

                                Employee employee = new Employee()
                                {
                                    Surname = record.Surname,
                                    FirstName = record.FirstName,
                                    Active = record.Active.ToLower() == "true" ? true : false,
                                    Age = Convert.ToInt32(record.Age),
                                    Sex = record.Sex,
                                    Mobile = record.Mobile
                                };
                                validRecords.Add(employee);
                                _employeeRepository.Add(employee);
                            }
                            else
                            {
                                sb.Append($"{record.Identity},");
                            }
                        }
                        if(validRecords.Count > 0)
                        {
                            //Save the valid record to the database
                            await _employeeRepository.Save(GetUserName(), GetIPAddress());
                        }

                        if (!string.IsNullOrEmpty(sb.ToString()))
                        {
                            TempData["msg"] = _appUtil.GetMessage(alert.warning.ToString(), $"The following record identities could not be saved because they have some invalid data. {sb}");
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            TempData["msg"] = _appUtil.GetMessage(alert.success.ToString(), $"Records uploaded successfully!");
                            return RedirectToAction(nameof(Index));
                        }
                    }

                }
                else
                {
                    TempData["msg"] = _appUtil.GetMessage(alert.warning.ToString(), "Invalid file type");
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                TempData["msg"] = _appUtil.GetMessage(alert.danger.ToString(), ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

    }

}
