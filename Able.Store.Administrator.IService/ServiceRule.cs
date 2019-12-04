namespace Able.Store.Administrator.IService
{
    public class ServiceRule
    {
        public ServiceRule(string property, string rule)
        {
            this.Property = property;

            this.Rule = rule;
        }
        public string Property { get; set; }

        public string Rule { get; set; }
    }
}
