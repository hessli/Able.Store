using System.Collections.Generic;
using System.Text;

namespace Able.Store.IService
{
   
    public abstract class ServiceInvalidBase
    {
        private List<ServiceRule> _brokenRules = new List<ServiceRule>();

        protected abstract void Validate();
        public void ThrowExceptionIfInvalid()
        {

            Validate();

            if (_brokenRules.Count > 0)
            {
                StringBuilder issues = new StringBuilder();

                foreach (var item in _brokenRules)
                {
                    issues.AppendLine(item.Rule);
                }

                throw new ServiceInvalidException(issues.ToString());
            }
        }
        public void AddBrokenRule(ServiceRule rule)
        {
            _brokenRules.Add(rule);
        }
    }
}
