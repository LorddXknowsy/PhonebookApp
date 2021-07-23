using ContactApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ContactApp.Repositories
{
    public class ContactRepository
    {
        private SqlConnection conn;

        public ContactRepository()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ContactConnStr"].ConnectionString;
            conn = new SqlConnection(connStr);
        }


        public void SaveContact(Contact contact)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "dbo.addnewcontact";
            cmd.Parameters.Add("@userName", SqlDbType.Text).Value = contact.userName;
            cmd.Parameters.Add("@firstName", SqlDbType.Text).Value = contact.firstName;
            cmd.Parameters.Add("@lastName", SqlDbType.Text).Value = contact.lastName;
            cmd.Parameters.Add("@phone", SqlDbType.Int).Value = contact.phone;
            cmd.Parameters.Add("@address", SqlDbType.Text).Value = contact.address;
            cmd.Parameters.Add("@email", SqlDbType.Text).Value = contact.email;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Connection.Open(); // conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void DeleteContact(Contact contact)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "dbo.deletecontact";
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = contact.ID;
            cmd.Connection = conn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void UpdateContact(Contact contact)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "dbo.updatecontact";
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = contact.ID;
            cmd.Parameters.Add("@firstName", SqlDbType.Text).Value = contact.firstName;
            cmd.Parameters.Add("@lastName", SqlDbType.Text).Value = contact.lastName;
            cmd.Parameters.Add("@phone", SqlDbType.Int).Value = contact.phone;
            cmd.Parameters.Add("@address", SqlDbType.Text).Value = contact.address;
            cmd.Parameters.Add("@email", SqlDbType.Text).Value = contact.email;
            cmd.Connection = conn;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<Contact> GetContacts(string UserName)
        {
            var contacts = new List<Contact>();
            var cmd = new SqlCommand();
            cmd.CommandText = "dbo.getContacts";
            cmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = UserName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Connection.Open();

             var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var contact = new Contact();

                contact.ID = int.Parse(reader["ID"].ToString());
                contact.userName = reader["userName"].ToString();
                contact.firstName = reader["firstName"].ToString();
                contact.lastName = reader["lastName"].ToString();
                contact.phone = int.Parse(reader["phone"].ToString());
                contact.address = reader["address"].ToString();
                contact.email = reader["email"].ToString();
                contacts.Add(contact);
            }
            conn.Close();

            return contacts;
        }
    }
}