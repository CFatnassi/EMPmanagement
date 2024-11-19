using EMPmanagement.Data;
using EMPmanagement.Models;
using EMPmanagement.Repository;

namespace EMPmanagement.Persistence
{
    public class DesignationRepo : IDesignationRepo
    {
        private readonly ApplicationDbContext _context;

        public DesignationRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Designation ObjToSave)
        {
            _context.Designations.Add(ObjToSave);
        }

        public void Delete(int Id)
        {
            var desig = _context.Designations.FirstOrDefault(x => x.DesigId == Id);
            if (desig != null)
            {
                _context.Designations.Remove(desig);
            }
        }

        public IEnumerable<Designation> GetAllDesignations()
        {
            return _context.Designations.ToList();
        }

        public Designation GetDesignation(int Id)
        {
            var desig = _context.Designations.FirstOrDefault(x => x.DesigId == Id);
            return desig;
        }

        public void Update(Designation ObjToSave)
        {
            var desig = _context.Designations.FirstOrDefault(x => x.DesigId == ObjToSave.DesigId);
            if (desig != null)
            {
                desig.DesigArName = ObjToSave.DesigArName;
                desig.DesigEngName = ObjToSave.DesigEngName;
                
            }
        }
    }
}
