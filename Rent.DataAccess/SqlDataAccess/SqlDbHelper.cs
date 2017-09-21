using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Rent.Entities;

namespace Rent.DataAccess.SqlDataAccess
{
    public class SqlDbHelper: ISqlDbHelper
    {

        private const string ConnectionString =
            @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\NORTHWND.MDF;Integrated Security=True;User Instance=True";

        // This function will be used to execute R(CRUD) operation of parameterless commands
        public DataTable ExecuteDataTable(string commandName, CommandType cmdType)
        {
            DataTable table = null;
            using (RentEntities context = new RentEntities())
            {

                var cc = context.Database.Connection.ConnectionString;

                using (SqlConnection con = new SqlConnection(cc))
                {
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandType = cmdType;
                        cmd.CommandText = commandName;

                        try
                        {
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                table = new DataTable();
                                da.Fill(table);
                            }
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }

            }

            return table;
        }

        // This function will be used to execute R(CRUD) operation of parameterized commands
        public DataTable ExecuteDataTable(string commandName, CommandType cmdType, SqlParameter[] param)
        {
            var table = new DataTable();

            using (RentEntities context = new RentEntities())
            {
                var cc = context.Database.Connection.ConnectionString;

                using (SqlConnection con = new SqlConnection(cc))
                {
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandType = cmdType;
                        cmd.CommandText = commandName;
                        cmd.Parameters.AddRange(param);

                        try
                        {
                            if (con.State != ConnectionState.Open)
                                con.Open();

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(table);
                            }
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
            }
            return table;
        }

        // This function will be used to execute CUD(CRUD) operation of parameterized commands
        public bool ExecuteNonQuery(string commandName, CommandType cmdType, SqlParameter[] pars)
        {
            int result = 0;

            using (RentEntities context = new RentEntities())
            {
                var cc = context.Database.Connection.ConnectionString;

                using (SqlConnection con = new SqlConnection(cc))
                {
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandType = cmdType;
                        cmd.CommandText = commandName;
                        cmd.Parameters.AddRange(pars);

                        try
                        {
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }

                            result = cmd.ExecuteNonQuery();
                        }
                        catch (Exception exception)
                        {
                            throw;
                        }
                    }
                }
            }

            return (result > 0);
        }

        public int ExecuteScalar(string commandName, CommandType cmdType, SqlParameter[] pars)
        {
            int result = 0;

            using (RentEntities context = new RentEntities())
            {
                var cc = context.Database.Connection.ConnectionString;

                using (SqlConnection con = new SqlConnection(cc))
                {
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandType = cmdType;
                        cmd.CommandText = commandName;
                        cmd.Parameters.AddRange(pars);

                        try
                        {
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }

                            result = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        catch (Exception exception)
                        {
                            result = 0;
                        }
                    }
                }
            }
            return result;
        }
    }
}
