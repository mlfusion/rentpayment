using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.DataAccess.SqlDataAccess 
{
    public interface ISqlDbHelper
    {
        DataTable ExecuteDataTable(string commandName, CommandType cmdType);
        DataTable ExecuteDataTable(string commandName, CommandType cmdType, SqlParameter[] param);
        int ExecuteScalar(string commandName, CommandType cmdType, SqlParameter[] pars);
    }
}
