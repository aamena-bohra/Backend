namespace BlueHarvestAPI
{
    public interface ICustomerService
    {
        Customer GetCustomerInfo(int customerId);
        void CreateDummyCustomers();
    }
}
