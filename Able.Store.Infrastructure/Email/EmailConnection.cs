namespace Able.Store.Infrastructure.Email
{
    public class EmailConnection
    {
       public string TagName { get; }
        public string Host { get; }
        public int Port { get; }
        public string PassWord { get; }
        public string Account { get; }
        public EmailConnection(string tagName,string host,int port,string password,string account)
        {

            if (string.IsNullOrWhiteSpace(tagName))
            {
                throw new System.ArgumentNullException("tagName");
            }
            if (string.IsNullOrWhiteSpace(host))
            {
                throw new System.ArgumentNullException("host");
            }
            if (port<0 && port> 65535)
            {
                throw new System.ArgumentOutOfRangeException("port");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new System.ArgumentNullException("password");
            }
            if (string.IsNullOrWhiteSpace(account))
            {
                throw new System.ArgumentNullException("account");
            }

            this.TagName = tagName;

            this.Host = host;

            this.Port = port;

            this.PassWord = password;

            this.Account = account;
        }
    }
}
