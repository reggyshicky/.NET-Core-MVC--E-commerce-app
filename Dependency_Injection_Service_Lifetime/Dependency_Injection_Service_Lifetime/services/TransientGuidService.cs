namespace Dependency_Injection_Service_Lifetime.services
{
    public class TransientGuidService : ITransientGuidService
    {

        private readonly Guid Id;
        public TransientGuidService()
        {
            Id = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Id.ToString();
        }
    }
}
