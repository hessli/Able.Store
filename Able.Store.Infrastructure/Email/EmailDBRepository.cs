using Able.Store.Infrastructure.ADO;
using System;
using System.Collections.Generic;
namespace Able.Store.Infrastructure.Email
{
    public class EmailDBRepository : AbstractMysqlDbSQL, IEmailRepository
    {
        private readonly string ADDLOGSQL = "insert into  infs_email_log " +
                   "values('@tagname','@tos','@body','@subject','@from'," +
                   "'@subjectEncodingName','@bodyEncodingName'," +
                   "'@exceptionStr','@isSuccess','@errorCode','@sendTime')";

        private readonly string GETCONNECTIONSQL = "select * from cfg_provider_email";
        public EmailDBRepository() : base("baseProvider")
        {
             
        }
        public void AddSendLog(EmailSendLog data)
        {
            base.ExecuteSql(ADDLOGSQL, new MySql.Data.MySqlClient.MySqlParameter[] {
                 new MySql.Data.MySqlClient.MySqlParameter("@tagname",data.TagName),
                 new MySql.Data.MySqlClient.MySqlParameter("@tos", data.Tos),
                 new MySql.Data.MySqlClient.MySqlParameter("@body",data.Body),
                 new MySql.Data.MySqlClient.MySqlParameter("@subject",data.Subject),
                 new MySql.Data.MySqlClient.MySqlParameter("@from",data.From),
                 new MySql.Data.MySqlClient.MySqlParameter("@subjectEncodingName",data.SubjectEncodingName),
                 new MySql.Data.MySqlClient.MySqlParameter("@bodyEncodingName",data.BodyEncodingName),
                 new MySql.Data.MySqlClient.MySqlParameter("@exceptionStr",data.ExceptionStr),
                 new MySql.Data.MySqlClient.MySqlParameter("@isSuccess",data.IsSuccess),
                 new MySql.Data.MySqlClient.MySqlParameter("@errorCode",data.ErrorCode),
                 new MySql.Data.MySqlClient.MySqlParameter("@sendTime",data.SendTime),
            });
        }
        public IList<EmailConnection> GetConnections()
        {
            var dataReader= base.ExecuteReader(this.GETCONNECTIONSQL);
            IList<EmailConnection> connections = new List<EmailConnection>();
            try
            {
                while (dataReader.Read())
                {
                    var tagName = dataReader.GetString("tag_name");
                    var port = int.Parse(dataReader.GetString("port"));
                    var host = dataReader.GetString("host");
                    var password = dataReader["password"] == null ? string.Empty : dataReader.GetString("password");
                    var account = dataReader["account"] == null ? string.Empty : dataReader.GetString("account");
                    connections.Add(new EmailConnection(tagName, host, port, password, account));
                }
                return connections;
            }
            finally
            {
                dataReader.Close();
            }
        }
    }




}
