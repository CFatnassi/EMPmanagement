using EMPmanagement.Helper;
using EMPmanagement.Models;
using EMPmanagement.Repository;
using EMPmanagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EMPmanagement.Controllers
{
    public class DesignationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public DesignationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllDesignations()
        {
            var Data = _unitOfWork.DesignationRepo.GetAllDesignations();
            return Json(Data);
        }


        [HttpPost]
        [Consumes("application/json")]
        public IActionResult CreateDesignation([FromBody] Designation ObjToSave)
        {
            MsgUnit Msg = new MsgUnit();
            if (_unitOfWork.NativeRepo.CheckDesignation(ObjToSave.DesigId) > 0)
            {
                Msg.Msg = Resources.Resource.ThisIDAlreadyExists;
                Msg.Code = 0;
                return Json(Msg);
            }
            try
            {
                var data = new Designation();
                data.DesigArName = ObjToSave.DesigArName;
                data.DesigEngName = ObjToSave.DesigEngName;
               

                _unitOfWork.DesignationRepo.Add(data);
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
        public IActionResult UpdateDesignation([FromBody] Designation ObjToUpdate)
        {
            MsgUnit Msg = new MsgUnit();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var data = _unitOfWork.DesignationRepo.GetDesignation(ObjToUpdate.DesigId);

                data.DesigArName = ObjToUpdate.DesigArName;
                data.DesigEngName = ObjToUpdate.DesigEngName;
              

                _unitOfWork.DesignationRepo.Update(data);
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
        public IActionResult RemoveDesignation([FromBody] Designation ObjToRemove)
        {
            MsgUnit Msg = new MsgUnit();
            try
            {
                _unitOfWork.DesignationRepo.Delete(ObjToRemove.DesigId);
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
            Designation desig = new Designation();
            
            return PartialView(desig);
        }


        public IActionResult Update(int Id)
        {

            var desig = _unitOfWork.DesignationRepo.GetDesignation(Id);
          
            return PartialView(desig);
        }


        public IActionResult Remove(int Id)
        {
            var desig = _unitOfWork.DesignationRepo.GetDesignation(Id);

            return PartialView(desig);
        }
    }
}
