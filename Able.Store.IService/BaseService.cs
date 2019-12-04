using Able.Store.IService;
using System.Collections.Generic;
using System.Text;

namespace Able.Store.Service.IService
{
    public abstract class BaseService
    {
        private IList<ServiceRule> _brokenRules = new List<ServiceRule>();
        protected ResponseView<T> OutPutResponseView<T>(T data,bool isSuccess=true,string message="")
        {
            ResponseView<T> result = new ResponseView<T>(message, isSuccess, data);

            return result;
        }
        protected ResponseView OutPutSuccessResponseView(string message="操作成功")
        {
            ResponseView response = new ResponseView(message, true);

            return response;
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

        protected bool IsBroker {

            get {

                return _brokenRules.Count > 0;
            }
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
                 OutPutResponseView(data,false, issues.ToString());
            }
            return  this.OutPutResponseView(data,true,"");
        }
         
    }
}
