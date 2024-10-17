namespace EMPmanagement.Repository
{
    public interface IUnitOfWork
    {
        public IEmployeeRepo EmployeeRepo { get; set; }


        void Complete();
    }
}
