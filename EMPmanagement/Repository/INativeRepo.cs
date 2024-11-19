namespace EMPmanagement.Repository
{
    public interface INativeRepo
    {
        int CheckEmployee(int id);
        int CheckDesignation(int id);
        int CheckDepartment(int id);
    }
}
