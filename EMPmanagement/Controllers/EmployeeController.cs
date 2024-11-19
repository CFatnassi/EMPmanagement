using EMPmanagement.Helper;
using EMPmanagement.Models;
using EMPmanagement.Repository;
using EMPmanagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EMPmanagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllEmployees()
        {
            var Data = _unitOfWork.EmployeeRepo.GetAllEmployees();
            return Json(Data);
        }


        [HttpPost]
        [Consumes("application/json")]
        public IActionResult CreateEmployee([FromBody] EmployeeInfoVM ObjToSave)
        {
            MsgUnit Msg = new MsgUnit();
            if (_unitOfWork.NativeRepo.CheckEmployee(ObjToSave.EmpId) > 0)
            {
                Msg.Msg = Resources.Resource.ThisIDAlreadyExists;
                Msg.Code = 0;
                return Json(Msg);
            }
            try
            {
                var data = new Employee();
                data.FirstName = ObjToSave.FirstName;
                data.MiddleName = ObjToSave.MiddleName;
                data.LastName = ObjToSave.LastName;
                data.PhoneNumber = ObjToSave.PhoneNumber;
                data.EmailAddress = ObjToSave.EmailAddress;
                data.Country = ObjToSave.Country;
                data.DateOfBirth = DateTime.Parse(ObjToSave.sDateOfBirth);
                data.Address = ObjToSave.Address;
                data.DepartmentId = ObjToSave.DepartmentId;
                data.DesignationId = ObjToSave.DesignationId;
               
                _unitOfWork.EmployeeRepo.Add(data);
                _unitOfWork.Complete();



                Msg.Code = 1;
                Msg.Msg = Resources.Resource.AddedSuccessfully;

                return Json(Msg);


            }
            catch (Exception ex)
            {
                Msg.Code = 0;
                Msg.Msg = Resources.Resource.SomthingWentWrong + ex.ToString();

                return Json(Msg);

            }


        }



        [HttpPost]
        [Consumes("application/json")]
        public IActionResult UpdateEmployee([FromBody] EmployeeInfoVM ObjToUpdate)
        {
            MsgUnit Msg = new MsgUnit();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var data = _unitOfWork.EmployeeRepo.GetEmployee(ObjToUpdate.EmpId);

                data.FirstName = ObjToUpdate.FirstName;
                data.MiddleName = ObjToUpdate.MiddleName;
                data.LastName = ObjToUpdate.LastName;
                data.EmpNo = ObjToUpdate.EmpNo;
                data.PhoneNumber = ObjToUpdate.PhoneNumber;
                data.EmailAddress = ObjToUpdate.EmailAddress;
                data.Country = ObjToUpdate.Country;
                data.DateOfBirth = DateTime.Parse(ObjToUpdate.sDateOfBirth);
                data.Address = ObjToUpdate.Address;
                data.DepartmentId = ObjToUpdate.DepartmentId;
                data.DesignationId = ObjToUpdate.DesignationId;

                _unitOfWork.EmployeeRepo.Update(data);
                _unitOfWork.Complete();



                Msg.Code = 1;
                Msg.Msg = Resources.Resource.UpdatedSuccessfully;
                return Json(Msg);


            }
            catch (Exception ex)
            {
                Msg.Code = 0;
                Msg.Msg = Resources.Resource.SomthingWentWrong + ex.ToString();
                return Json(Msg);

            }


        }




        [HttpPost]
        [Consumes("application/json")]
        public IActionResult RemoveEmployee([FromBody] Employee ObjToRemove)
        {
            MsgUnit Msg = new MsgUnit();
            try
            {
                _unitOfWork.EmployeeRepo.Delete(ObjToRemove.Id);
                _unitOfWork.Complete();



                Msg.Code = 1;
                Msg.Msg = Resources.Resource.DeletedSuccessfully;
                return Json(Msg);


            }
            catch (Exception ex)
            {
                Msg.Code = 0;
                Msg.Msg = Resources.Resource.SomthingWentWrong + ex.ToString();
                return Json(Msg);

            }


        }

        public IActionResult AddNew()
        {
            EmployeeInfoVM emp = new EmployeeInfoVM();
            var designations = _unitOfWork.DesignationRepo.GetAllDesignations();
            var departments = _unitOfWork.DepartmentRepo.GetAllDepartments();

            emp.Designations = designations;
            emp.Departments = departments;
            return PartialView(emp);
        }


        public IActionResult Update(int Id)
        {

            var Fs = _unitOfWork.EmployeeRepo.GetEmployeeInfo(Id);
            EmployeeInfoVM emp = new EmployeeInfoVM();
            var designations = _unitOfWork.DesignationRepo.GetAllDesignations();
            var departments = _unitOfWork.DepartmentRepo.GetAllDepartments();

            emp.Designations = designations;
            emp.Departments = departments;
            return PartialView(emp);
        }


        public IActionResult Remove(int Id)
        {
            var emp = _unitOfWork.EmployeeRepo.GetEmployeeInfo(Id);

            return PartialView(emp);
        }
    }
}
