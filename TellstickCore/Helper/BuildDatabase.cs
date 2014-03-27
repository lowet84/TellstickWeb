using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TellstickCore.Context;

namespace TellstickCore.Helper
{
    // Tillfälligt tills skapandet av databasen från EF funkar.
    public class BuildDatabase
    {
        //private const string DataBaseName = @"c:\temp\Settings.db";
        private static string _databaseName;
        private static string DataBaseName 
        { 
            get 
            { 
                if(_databaseName== null)
                    _databaseName = System.Configuration.ConfigurationManager.ConnectionStrings["SettingsContext"].ConnectionString.Split('=')[1];
                return _databaseName;
                } 
        }
        private static SQLiteConnection _dataBaseConnection;
        private static SQLiteConnection DataBaseConnection
        {
            get
            {
                BuildDbIfNotExists();
                    
                if (_dataBaseConnection == null)
                {
                    _dataBaseConnection = new SQLiteConnection();
                    _dataBaseConnection.ConnectionString = "Data Source=" + DataBaseName;
                }
                return _dataBaseConnection;
            }
        }

        private static void ExecuteSqlNonQuery(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(DataBaseConnection);
            command.CommandText = sql;
            DataBaseConnection.Open();
            command.ExecuteNonQuery();
            DataBaseConnection.Close();
        }

        internal static bool BuildDbIfNotExists()
        {
            if (!File.Exists(DataBaseName))
            {
                SQLiteConnection.CreateFile(DataBaseName);
                var tables = typeof(SettingsContext).GetProperties().Where(d => d.PropertyType.IsGenericType && d.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)).Select(d => new { Name = d.Name, Type = d.PropertyType.GenericTypeArguments[0] });
                foreach (var type in tables)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("CREATE TABLE IF NOT EXISTS [" + type.Name + "](");
                    sb.Append("[Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,");
                    foreach (var prop in type.Type.GetProperties().Where(d => d.Name != "Id"))
                    {
                        if (prop.PropertyType == typeof(Int64))
                            sb.Append("[" + prop.Name + "] INTEGER NULL,");
                        else if (prop.PropertyType == typeof(string))
                            sb.Append("[" + prop.Name + "] NVARCHAR(500) NULL,");
                        else
                            throw new NotImplementedException();
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(")");
                    var sql = sb.ToString();
                    ExecuteSqlNonQuery(sql);
                }
                return true;
            }
            return false;
        }
    }
}
