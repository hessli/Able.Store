

namespace Able.Store.Infrastructure.Domain.Business
{
    public class BusinessRule
    {
        public BusinessRule(string property, string rule)
        {
            this.Property = property;

            this.Rule = rule;
        }

        public string Property { get; set; }

        public string Rule { get; set; }
    }
}
