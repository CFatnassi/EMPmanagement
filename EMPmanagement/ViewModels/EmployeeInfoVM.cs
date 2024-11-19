using EMPmanagement.Models;
using System.ComponentModel.DataAnnotations;

namespace EMPmanagement.ViewModels
{
    public class EmployeeInfoVM
    {
        public int EmpId { get; set; }
        public string EmpNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {MiddleName} {LastName}";
        public int PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Country { get; set; }
        public string sDateOfBirth { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string DepEngName { get; set; }
        public string DesigEngName { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public IEnumerable<Designation> Designations { get; set; }
        public IEnumerable<Department> Departments { get; set; }
    }
}
