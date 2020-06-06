namespace TestApi.DataBase.Context
{
    public class ContextProvider: IContextProvider
    {
        public EntityContext GetContext()
        {
            return new EntityContext();
        }
    }
}