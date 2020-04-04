using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using test.Exceptions;

namespace test.TsqlCommands
{
    class AddOneUserCommand : ICommandTsql
    {
        Regex AddOneRgxString = new Regex(@"\s\w+\s?\,\s?(male|female)\s?(\,\s?(\d\d\.\d\d\.\d\d\d\d)\s?){2}\,\s?(y|n)");
        SqlDataSet _data = new SqlDataSet { };

        public void ExecuteCommand(string connectionString)
        {
            if (!String.IsNullOrEmpty(_data.QueryText) && _data.Parameters.Count > 0)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(_data.QueryText, connection);
                    foreach(var el in _data.Parameters)
                    {
                        command.Parameters.AddWithValue(el.Key, el.Value);
                    }
                    command.ExecuteNonQuery();
                    // connection.Close();
                }
            }
            else
            {
                throw new EmptyDataException();
            }
        }

        public void ReadCommand(string command)
        {
            if (AddOneRgxString.IsMatch(command))
            {
                string[] separators = { " ", "," };
                string[] afterSplit = AddOneRgxString.Match(command).Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                _data.Parameters = new Dictionary<string, string>
                {
                    { @"name", afterSplit[0] },
                    { @"gender", afterSplit[1] == "male"? "true" : "false" },
                    { @"birthday",  afterSplit[2] },
                    { @"lastVisit", afterSplit[3] },
                    { @"isOnline", afterSplit[4] == "y" ? "true" : "false" }
                };

                _data.QueryText = @"INSERT INTO dbo.Users (name, gender, dateOfBirth, lastVisit, isOnline) " +
                                  @"VALUES (@name, @gender, @birthday, @lastVisit, @isOnline)";
            }
            else
            {
                throw new BadFormatException();
            }
        }
    }
}
