using EMPmanagement.Helper;
using EMPmanagement.Models;
using EMPmanagement.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EMPmanagement.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllDepartments()
        {
            var Data = _unitOfWork.DepartmentRepo.GetAllDepartments();
            return Json(Data);
        }


        [HttpPost]
        [Consumes("application/json")]
        public IActionResult CreateDepartment([FromBody] Department ObjToSave)
        {
            MsgUnit Msg = new MsgUnit();
            if (_unitOfWork.NativeRepo.CheckDepartment(ObjToSave.DepId) > 0)
            {
                Msg.Msg = Resources.Resource.ThisIDAlreadyExists;
                Msg.Code = 0;
                return Json(Msg);
            }
            try
            {
                var data = new Department();
                data.DepArName = ObjToSave.DepArName;
                data.DepEngName = ObjToSave.DepEngName;


                _unitOfWork.DepartmentRepo.Add(data);
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
        public IActionResult UpdateDepartment([FromBody] Department ObjToUpdate)
        {
            MsgUnit Msg = new MsgUnit();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var data = _unitOfWork.DepartmentRepo.GetDepartment(ObjToUpdate.DepId);

                data.DepArName = ObjToUpdate.DepArName;
                data.DepEngName = ObjToUpdate.DepEngName;


                _unitOfWork.DepartmentRepo.Update(data);
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
        public IActionResult RemoveDepartment([FromBody] Department ObjToRemove)
        {
            MsgUnit Msg = new MsgUnit();
            try
            {
                _unitOfWork.DepartmentRepo.Delete(ObjToRemove.DepId);
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
            Department dep = new Department();

            return PartialView(dep);
        }


        public IActionResult Update(int Id)
        {

            var dep = _unitOfWork.DepartmentRepo.GetDepartment(Id);

            return PartialView(dep);
        }


        public IActionResult Remove(int Id)
        {
            var dep = _unitOfWork.DepartmentRepo.GetDepartment(Id);

            return PartialView(dep);
        }
    }
}
