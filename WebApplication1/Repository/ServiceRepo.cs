using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class ServiceRepo : IServicesRepo
    {
        private IDbConnection mssqlDb;
        public ServiceRepo(IConfiguration configuration)
        {
            mssqlDb = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public IEnumerable<FormModel> FindAlltheList()
        {
            string sql = "Select * from FormModel";
            List<FormModel> returninglist = mssqlDb.Query<FormModel>(sql).ToList();

            return returninglist;
        }

    }
}
