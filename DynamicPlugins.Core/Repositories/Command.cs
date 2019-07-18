﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DynamicPlugins.Core.Repositories
{
    public class Command
    {
        public Command(string sql, List<SqlParameter> parameters)
        {
            Sql = sql;
            Parameters = parameters;
        }

        public string Sql { get; set; }

        public List<SqlParameter> Parameters { get; set; }
    }
}