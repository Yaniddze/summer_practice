namespace TestApi.DataBase.Context
{
    public interface IContextProvider
    {
        EntityContext GetContext();
    }
}