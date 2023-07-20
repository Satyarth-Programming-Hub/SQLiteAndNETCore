﻿using SQLiteWithNETCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteWithNETCore.Data
{
    internal class DbCrudOps
    {

        #region INSERT UPDATE DELETE
        public static int AddUser(User user)
        {
            const string query = "INSERT INTO User VALUES(@id,@name,@age,@gender,@city)";

            var parameters = new Dictionary<string, object>
            {
                {"@id",user.Id },
                {"@name",user.Name },
                {"@age",user.Age },
                {"@gender",user.Gender },
                {"@city",user.City }
            };

            return DbHelper.RunIUD(query, parameters);
        }

        public static int UpdateUser(User user)
        {
            const string query = "UPDATE User SET Name=@name, Age=@age, Gender=@gender, City=@city WHERE Id=@id";

            var parameters = new Dictionary<string, object>
            {
                {"@id",user.Id },
                {"@name",user.Name },
                {"@age",user.Age },
                {"@gender",user.Gender },
                {"@city",user.City }
            };

            return DbHelper.RunIUD(query, parameters);
        }

        public static int DeleteUser(int id)
        {
            const string query = "DELETE FROM User WHERE Id=@id";

            var parameters = new Dictionary<string, object>
            {
                {"@id",id }
            };

            return DbHelper.RunIUD(query, parameters);
        }
        #endregion INSERT UPDATE DELETE

        #region SELECT
        public static User GetUserById(int id)
        {
            var query = "SELECT * FROM User WHERE Id=@id";

            var parameters = new Dictionary<string, object>
            {
                {"@id",id }
            };

            DataTable dt = DbHelper.RunSelect(query, parameters);

            if (dt == null || dt.Rows.Count == 0) return null;

            var user = new User()
            {
                Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                Name = Convert.ToString(dt.Rows[0]["Name"]),
                Age = Convert.ToInt32(dt.Rows[0]["Age"]),
                Gender = Convert.ToString(dt.Rows[0]["Gender"]),
                City = Convert.ToString(dt.Rows[0]["City"]),
            };

            return user;

        }
        #endregion SELECT
    }
}
