

namespace Able.Store.Infrastructure.Http
{
   public interface IHttpParamter
    {
        void AddParameter<T>(T data) where T : class;

        string GetParameter();

        string GetPostParameter();

        void AddParameter(string key, string val);


    }
}
