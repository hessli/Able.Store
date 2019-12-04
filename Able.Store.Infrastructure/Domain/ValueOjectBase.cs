using Able.Store.Infrastructure.Domain.Business;
using System.Collections.Generic;
using System.Text;

namespace Able.Store.Infrastructure.Domain
{
    /// <summary>
    /// 值对象基类
    /// </summary>
    public abstract class ValueOjectBase
    {
        private List<BusinessRule> _brokenRules = new List<BusinessRule>();

        public ValueOjectBase()
        {
        }

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

                throw new ValueObjectIsInvalidException(issues.ToString());
            }
        }
        public void AddBrokenRule(BusinessRule rule)
        {
         
            _brokenRules.Add(rule);
        }
    }
}
