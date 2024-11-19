using EMPmanagement.Models;
using EMPmanagement.ViewModels;

namespace EMPmanagement.Repository
{
    public interface IEmployeeRepo
    {
        void Add(Employee ObjToSave);
        void Update(Employee ObjToSave);
        Employee GetEmployee(int Id);
        void Delete(int Id);
        IEnumerable<EmployeeInfoVM> GetEmployeeInfo(int id);
        IEnumerable<EmployeeInfoVM> GetAllEmployees();
    }
}
