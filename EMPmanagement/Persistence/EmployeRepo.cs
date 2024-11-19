using EMPmanagement.Data;
using EMPmanagement.Models;
using EMPmanagement.Repository;
using EMPmanagement.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EMPmanagement.Persistence
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Employee ObjToSave)
        {
            ObjToSave.EmpNo = GenerateEmpNo();
            _context.Employees.Add(ObjToSave);
        }

        public void Delete(int Id)
        {
            var emp = _context.Employees.FirstOrDefault(x => x.Id == Id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
            }
        }

        public IEnumerable<EmployeeInfoVM> GetAllEmployees()
        {
            string Sql =
                @"  SELECT E.*,
                    Des.DesigEngName AS DesigEngName,
                    Dep.DepEngName AS DepEngName
                            
                   

                    FROM Employees E, Departments Dep, Designations Des
                     
                    WHERE E.DepartmentId = Dep.DepId AND E.DesignationId = Des.DesigId

                  
                ";
            return _context.Database.SqlQueryRaw<EmployeeInfoVM>(Sql).ToList();
        }

        public IEnumerable<EmployeeInfoVM> GetEmployeeInfo(int id)
        {
            
            string Sql = string.Format(
               @"  SELECT E.*,
                    Des.DesigEngName AS DesigEngName,
                    Dep.DepEngName AS DepEngName
                            
                   

                    FROM Employees E, Departments Dep, Designations Des
                     
                    WHERE E.DepartmentId = Dep.DepId AND E.DesignationId = Des.DesigId AND E.Id = {0}

                ", id);

            return _context.Database.SqlQueryRaw<EmployeeInfoVM>(Sql).ToList();
        }

        public Employee GetEmployee(int Id)
        {
            string Sql = string.Format("SELECT * FROM Employees where Id = '{0}' ", Id);

            return _context.Employees.FromSqlRaw(Sql).FirstOrDefault();
        }
        public string GenerateEmpNo()
        {
            var emp = _context.Employees.LastOrDefault();
            var empNb = emp.EmpNo + 1;
            return empNb;
        }

        public void Update(Employee ObjToSave)
        {
            var emp = _context.Employees.FirstOrDefault(x => x.Id == ObjToSave.Id);
            if (emp != null)
            {
                emp.FirstName = ObjToSave.FirstName;
                emp.MiddleName = ObjToSave.MiddleName;
                emp.LastName = ObjToSave.LastName;
                emp.PhoneNumber = ObjToSave.PhoneNumber;
                emp.EmailAddress = ObjToSave.EmailAddress;
                emp.Country = ObjToSave.Country;
                emp.DateOfBirth = ObjToSave.DateOfBirth;
                emp.Address = ObjToSave.Address;
                emp.DepartmentId = ObjToSave.DepartmentId;
                emp.DesignationId = ObjToSave.DesignationId;
            }
        }
    }
}
