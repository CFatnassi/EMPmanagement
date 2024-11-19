using EMPmanagement.Data;
using EMPmanagement.Models;
using EMPmanagement.Repository;

namespace EMPmanagement.Persistence
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Department ObjToSave)
        {
            _context.Departments.Add(ObjToSave);
        }

        public void Delete(int Id)
        {
            var desig = _context.Departments.FirstOrDefault(x => x.DepId == Id);
            if (desig != null)
            {
                _context.Departments.Remove(desig);
            }
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _context.Departments.ToList();
        }

        public Department GetDepartment(int Id)
        {
            var desig = _context.Departments.FirstOrDefault(x => x.DepId == Id);
            return desig;
        }

        public void Update(Department ObjToSave)
        {
            var desig = _context.Departments.FirstOrDefault(x => x.DepId == ObjToSave.DepId);
            if (desig != null)
            {
                desig.DepArName = ObjToSave.DepArName;
                desig.DepEngName = ObjToSave.DepEngName;

            }
        }
    }
}
