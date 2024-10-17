using EMPmanagement.Data;
using EMPmanagement.Repository;

namespace EMPmanagement.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IEmployeeRepo EmployeeRepo { get; set; }



        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            EmployeeRepo = new EmployeeRepo(_context);
           


        }
        public void Complete()
        {
            _context.SaveChanges();
        }


    }
}
