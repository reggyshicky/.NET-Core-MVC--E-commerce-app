namespace Dependency_Injection_Service_Lifetime.services
{
    public class ScopedGuidService : IScopedGuidService
    {

        private readonly Guid Id;
        public ScopedGuidService()
        {
            Id = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Id.ToString();
        }
    }
}
