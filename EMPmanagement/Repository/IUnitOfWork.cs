namespace EMPmanagement.Repository
{
    public interface IUnitOfWork
    {
        public IEmployeeRepo EmployeeRepo { get; set; }
        public IDepartmentRepo DepartmentRepo { get; set; }
        public IDesignationRepo DesignationRepo { get; set; }
        public INativeRepo NativeRepo { get; set; }


        void Complete();
    }
}
