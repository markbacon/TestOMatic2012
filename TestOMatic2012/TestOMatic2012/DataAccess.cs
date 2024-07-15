using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {
    
    internal class DataAccess {


        public DataAccess(string connectionString) {

            _connectionString = connectionString;
        }
        //---------------------------------------------------------------------------------------------------
        public int ExecuteActionQuery(string sql) {

			int numRowsAffected = 0;

			using (SqlConnection dataConn = new SqlConnection(_connectionString)) {

				SqlCommand comm = new SqlCommand();
				comm.CommandType = CommandType.Text;
				comm.CommandText = sql;
				comm.CommandTimeout = 240;
				comm.Connection = dataConn;
				comm.Connection.Open();
				numRowsAffected = comm.ExecuteNonQuery();
			}

			return numRowsAffected;
		}
        //-----------------------------------------------------------------------------------------
        public DataTable ExecuteQuery(string sql) {

            try {

                using (SqlConnection dataConn = new SqlConnection(_connectionString)) {

                    SqlCommand comm = new SqlCommand();
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = sql;
                    comm.CommandTimeout = 240;
                    comm.Connection = dataConn;
                    comm.Connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(comm);
                    adapter.SelectCommand = comm;

                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    dataConn.Close();

                    return dt;
                }
            }
            catch (Exception ex) {
                Logger.WriteError(ex);
                return null;
            }
        }
        //-----------------------------------------------------------------------------------------
        public void ExecuteQuery(string sql, DataTable dt) {

            try {

                using (SqlConnection dataConn = new SqlConnection(_connectionString)) {

                    SqlCommand comm = new SqlCommand();
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = sql;
                    comm.CommandTimeout = 240;
                    comm.Connection = dataConn;
                    comm.Connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(comm);
                    adapter.SelectCommand = comm;

                    adapter.Fill(dt);

                    dataConn.Close();
                }
            }
            catch (Exception ex) {
                Logger.WriteError(ex);
            }
        }
        //---------------------------------------------------------------------------------------------------
        //-- Private Variables
        //---------------------------------------------------------------------------------------------------
        private string _connectionString;
    }
}
