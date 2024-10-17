using EMPmanagement.Data;
using EMPmanagement.Repository;

namespace EMPmanagement.Persistence
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepo(ApplicationDbContext context)
        {
            _context = context;
        }
      
    }
}
