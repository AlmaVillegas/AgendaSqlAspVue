﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace AgendaSqlAspVue.Conexion
{
    public class Conexion
    {  public SqlConnection getConnection()
        {
            string cadenaString = "Data Source=PROSERVER;Initial Catalog=IntelisisTmp";
            SqlConnection sqlConnection = new SqlConnection(cadenaString);
            string pass = "Ramona1995";
            SecureString secure = new SecureString();
            foreach (char c in pass)
                secure.AppendChar(c);
            SecureString password = secure;
            password.MakeReadOnly();
            SqlCredential sqlcrendential = new SqlCredential("arbolanos",password);
            sqlConnection.Credential = sqlcrendential;
            return sqlConnection;
        }
    }
}
