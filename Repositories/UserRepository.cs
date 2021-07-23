using ContactApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactApp.Repositories
{
    public class UserRepository
    {
        private SqlConnection conn;

        public UserRepository()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ContactConnStr"].ConnectionString;
            conn = new SqlConnection(connStr);
        }


        public void SaveUser(User user)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "dbo.addnewuser";
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = user.email;
            cmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = user.UserName;
            cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = user.Password;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Connection.Open(); // conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public User GetUser(string UserName, string Password)
        {
            var authUser = new User();
            var cmd = new SqlCommand();
            cmd.CommandText = "dbo.getAuthuser";
            cmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = UserName;
            cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = Password;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Connection.Open();

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                authUser.UserName = reader["userName"].ToString();

            }
            conn.Close();

            return authUser;

        }

    }
}