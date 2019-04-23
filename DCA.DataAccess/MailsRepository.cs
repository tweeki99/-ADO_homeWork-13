using Dapper;
using DCA.DataAccess.Abstract;
using DCA.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCA.DataAccess
{
    public class MailsRepository : IRepository<Mail>
    {
        private DbConnection _connection;

        public MailsRepository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;
            _connection = new SqlConnection(connectionString);
        }

        public void Add(Mail item)
        {
            var sql = "insert into Mails (Id,ReciverId,Theme,Text,CreationDate,DeletedDate) values (@Id,@ReciverId,@Theme,@Text,@CreationDate,@DeletedDate)";
            var result = _connection.Execute(sql, item);
            if (result < 1) throw new Exception();
        }

        public void Delete(Guid id)
        {
            var sql = "update Mails set DeletedDate = GETDATE() WHERE Id = @ID";
            _connection.Query(sql, new { ID = id });
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public ICollection<Mail> GetAll()
        {
            var sql = "select * from Mails";
            return _connection.Query<Mail>(sql).AsList();
        }

        public void Update(Mail item)
        {
            var sql = "update Mails set Theme = @Theme WHERE Id = @Id";
            var result = _connection.Execute(sql, item);
            if (result < 1) throw new Exception();

            sql = "update Mails set Text = @Text WHERE Id = @Id";
            result = _connection.Execute(sql, item);
            if (result < 1) throw new Exception();

            sql = "update Mails set ReciverId = @ReciverId WHERE Id = @Id";
            result = _connection.Execute(sql, item);
            if (result < 1) throw new Exception();
        }
    }
}
