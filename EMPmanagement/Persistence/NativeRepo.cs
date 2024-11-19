using EMPmanagement.Data;
using EMPmanagement.Repository;
using Microsoft.EntityFrameworkCore;

namespace EMPmanagement.Persistence
{
    public class NativeRepo : INativeRepo
    {
        private readonly ApplicationDbContext _context;

        public NativeRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public int CheckEmployee(int EmpId)
        {
            string Sql = string.Format(@"select count(*) as Value from Employees where Id={0} ", EmpId);
            return _context.Database.SqlQueryRaw<int>(Sql).FirstOrDefault();
        }
        public int CheckDesignation(int id)
        {
            string Sql = string.Format(@"select count(*) as Value from Designations where DesigId={0} ", id);
            return _context.Database.SqlQueryRaw<int>(Sql).FirstOrDefault();
        }
        public int CheckDepartment(int id)
        {
            string Sql = string.Format(@"select count(*) as Value from Departments where DepId={0} ", id);
            return _context.Database.SqlQueryRaw<int>(Sql).FirstOrDefault();
        }
    }
}
