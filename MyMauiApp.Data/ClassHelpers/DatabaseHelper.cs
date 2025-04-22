using SQLite;
using System;
using System.Linq;

namespace MyMauiApp.Data.ClassHelpers
{
    public class DatabaseHelper
    {
        private SQLiteConnection _connection;

        public DatabaseHelper()
        {
            _connection = new SQLiteConnection("/data/user/0/com.companyname.mymauiapp/files/XamarinDb.db");
        }

        public void AddColumn<T>(string tableName, string columnName, string columnType)
        {
            // Use GetTableInfo instead of raw SQL query
            var existingColumns = _connection.GetTableInfo(tableName);

            if (existingColumns.Any(c => c.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"Column '{columnName}' already exists in table '{tableName}'.");
                return;
            }

            // Add new column
            string alterSql = $"ALTER TABLE {tableName} ADD COLUMN {columnName} {columnType};";
            _connection.Execute(alterSql);
            Console.WriteLine($"Column '{columnName}' added to table '{tableName}'.");
        }

        public void DeleteUsers()
        {
            string alterSql = $"DELETE FROM USERS;";
            _connection.Execute(alterSql);
        }
    }
}