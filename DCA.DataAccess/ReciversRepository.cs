using DCA.DataAccess.Abstract;
using DCA.Models;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Configuration;
using System.Data.SqlClient;

namespace DCA.DataAccess
{
    public class ReciversRepository : IRepository<Reciver>
    {
        private DbConnection _connection;

        public ReciversRepository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;
            _connection = new SqlConnection(connectionString);
        }

        public void Add(Reciver item)
        {
            var sql = "insert into Reciver (Id,CreationDate,FullName,Address,DeletedDate) values (@Id,@CreationDate,@FullName,@Address,@DeletedDate)";
            var result = _connection.Execute(sql, item);
            if (result < 1) throw new Exception();
        }

        public void Delete(Guid id)
        {
            var sql = "update Reciver set DeletedDate = GETDATE() WHERE Id = @ID";
            _connection.Query(sql, new { ID = id });
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public ICollection<Reciver> GetAll()
        {
            var sql = "select * from Reciver";
            return _connection.Query<Reciver>(sql).AsList();
        }

        public void Update(Reciver item)
        {
            var result = _connection.Execute("update Reciver set FullName = @FullName WHERE Id = @Id", item);
            if (result < 1) throw new Exception();

            result = _connection.Execute("update Reciver set Address = @Address WHERE Id = @Id", item);
            if (result < 1) throw new Exception();
        }
    }
}
