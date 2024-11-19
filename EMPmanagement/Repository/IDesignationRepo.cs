using EMPmanagement.Models;

namespace EMPmanagement.Repository
{
    public interface IDesignationRepo
    {
        void Add(Designation ObjToSave);
        void Update(Designation ObjToSave);
        Designation GetDesignation(int Id);
        void Delete(int Id);

        IEnumerable<Designation> GetAllDesignations();
    }
}
