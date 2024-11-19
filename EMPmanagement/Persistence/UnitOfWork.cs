using EMPmanagement.Data;
using EMPmanagement.Repository;

namespace EMPmanagement.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IEmployeeRepo EmployeeRepo { get; set; }
        public IDepartmentRepo DepartmentRepo { get; set; }
        public IDesignationRepo DesignationRepo { get; set; }
        public INativeRepo NativeRepo { get; set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            EmployeeRepo = new EmployeeRepo(_context);
            DepartmentRepo = new DepartmentRepo(_context);
            DesignationRepo = new DesignationRepo(_context);
            NativeRepo = new NativeRepo(_context);
           


        }
        public void Complete()
        {
            _context.SaveChanges();
        }


    }
}
