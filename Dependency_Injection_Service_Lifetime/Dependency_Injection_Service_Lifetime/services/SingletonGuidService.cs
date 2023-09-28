namespace Dependency_Injection_Service_Lifetime.services
{
    public class SingletonGuidService : ISingletonGuidService
    {
        private readonly Guid Id;
        public SingletonGuidService()
        {
            Id = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Id.ToString();
        }
    }
}
//It is a simple implementation of a singleton service that generates a unique GUID (Globally Unique Identifier)
//when an instance of the service is created and then provides a method to retrieve that generated GUID.