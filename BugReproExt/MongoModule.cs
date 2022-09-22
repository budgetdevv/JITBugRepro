namespace MongoModule
{
    public abstract class MongoModule
    {
        protected MongoModule()
        {
            ConnectionPool.CreateOrGetConnection("");
        }
    }
}