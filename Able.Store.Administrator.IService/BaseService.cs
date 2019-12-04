using Able.Store.Infrastructure.Domain.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Able.Store.Administrator.IService
{
    public abstract class BaseService
    {
        private IList<ServiceRule> _brokenRules = new List<ServiceRule>();
        protected ResponseView<T> OutPutResponseView<T>(T data, bool isSuccess = true, string message = "")
        {
            ResponseView<T> result = new ResponseView<T>(message, isSuccess, data);

            return result;
        }
        protected ResponseView OutPutSuccessResponseView(string message = "操作成功")
        {
            ResponseView response = new ResponseView(message, true);

            return response;
        }

        protected ResponseView OutPutErrorResponseView(IEnumerable<BusinessRule> rules)
        {
            StringBuilder issues = new StringBuilder();
            foreach (var item in rules)
            {
                issues.AppendLine(item.Rule);
            }

            return OutPutErrorResponseView(issues.ToString());
        }
        protected ResponseView OutPutErrorResponseView(string message = "操作失败")
        {
            ResponseView response = new ResponseView(message, false);
            return response;
        }
        protected void AddRuleBroke(ServiceRule rule)
        {
            _brokenRules.Add(rule);
        }

        protected bool IsBroker
        {

            get
            {

                return _brokenRules.Count > 0;
            }
        }
        protected ResponseView OutPutBrokenResponseView()
        {
            if (_brokenRules.Count > 0)
            {
                StringBuilder issues = new StringBuilder();

                foreach (var item in _brokenRules)
                {
                    issues.AppendLine(item.Rule);
                }
               return OutPutErrorResponseView(issues.ToString());
            }

            return  OutPutSuccessResponseView();
        }
        protected ResponseView<T> OutPutBrokenResponseView<T>(T data)
        {
            if (_brokenRules.Count > 0)
            {
                StringBuilder issues = new StringBuilder();

                foreach (var item in _brokenRules)
                {
                    issues.AppendLine(item.Rule);
                }
                OutPutResponseView(data, false, issues.ToString());
            }
            return this.OutPutResponseView(data, true, "");
        }

    }
}
