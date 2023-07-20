using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteWithNETCore.Data
{
    internal class DbHelper
    {
       
        const string constr = "Data Source=App.db";
        
        //INSERT UPDATE DELETE
        public static int RunIUD(string query, Dictionary<string, object> parameters)
        {
            CheckIfTableExists();

            if (string.IsNullOrEmpty(query.Trim())) return 0;

            int rowsAffected = 0;
            //Creating Connection
            using (var con = new SQLiteConnection(constr))
            {
                using (var cmd = new SQLiteCommand(query, con))
                {
                    //Setting Parameters in query
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                    con.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }

        //SELECT
        public static DataTable RunSelect(string query, Dictionary<string, object> parameters)
        {
            CheckIfTableExists();

            if (string.IsNullOrEmpty(query.Trim())) return null;

            using (var con = new SQLiteConnection(constr))
            {
                using (var cmd = new SQLiteCommand(query, con))
                {
                    //Setting Parameters in query
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }

                    var da = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }

        }

        // TO CHECK IF TABLE EXISTS
        private static void CheckIfTableExists()
        {
            string selectQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='User'";
            DataTable dt;

            using (var con = new SQLiteConnection(constr))
            {
                using (var cmd = new SQLiteCommand(selectQuery, con))
                {
                    var da = new SQLiteDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                }
            }

            if (dt == null || dt.Rows.Count == 0)
            {
                string createTableQuery = "CREATE TABLE User " +
                    "(Id INT ," +
                    "Name TEXT," +
                    "Age INT," +
                    "Gender TEXT," +
                    "City TEXT)";
                using (var con = new SQLiteConnection(constr))
                {
                    using (var cmd = new SQLiteCommand(createTableQuery,con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }

        }
    }
}
