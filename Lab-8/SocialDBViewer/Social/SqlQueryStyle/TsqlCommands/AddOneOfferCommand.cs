using SocialDb.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocialDb.TsqlCommands
{
    class AddOneOfferCommand : ICommandTsql
    {
        Regex AddOneRgxString = new Regex(@"\s(\w+\s?\,\s?){2}\s?\d");
        SqlDataSet _data = new SqlDataSet { };

        public void ExecuteCommand(string connectionString)
        {
            if (!String.IsNullOrEmpty(_data.QueryText) && _data.Parameters.Count > 0)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(_data.QueryText, connection);
                    foreach (var el in _data.Parameters)
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
                    { @"sender", afterSplit[0] },
                    { @"receiver", afterSplit[1] },
                    { @"status",  afterSplit[2] },
                    { @"date",  DateTime.Today.ToString() },
                };

                _data.QueryText = @"INSERT INTO dbo.Friends (UserFrom, UserTo, FriendStatus, SendDate) " +
                                  @"VALUES (" +
                                  @"(SELECT TOP 1 UserId FROM dbo.Users WHERE Name = @sender), " +
                                  @"(SELECT TOP 1 UserId FROM dbo.Users WHERE Name = @receiver), @status, @date)";
            }
            else
            {
                throw new BadFormatException();
            }
        }
    }
}
