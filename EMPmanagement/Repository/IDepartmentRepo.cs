using EMPmanagement.Models;

namespace EMPmanagement.Repository
{
    public interface IDepartmentRepo
    {
        void Add(Department ObjToSave);
        void Update(Department ObjToSave);
        Department GetDepartment(int Id);
        void Delete(int Id);

        IEnumerable<Department> GetAllDepartments();
    }
}
