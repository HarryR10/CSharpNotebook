using System;
using System.Text;
using Social.Models;
using Newtonsoft.Json;
using System.IO;
using Social.Exceptions;

namespace Social
{
    public class SettingUpdater
    {
        public Settings CurrentSettings { get; }

        public SettingUpdater()
        {
            string path = @"Data/settings.json";

            if (File.Exists(path))
            {
                using (StreamReader file = File.OpenText(path))
                {
                    string fromFile = File.ReadAllText(path);
                    CurrentSettings = JsonConvert.DeserializeObject<Settings>(fromFile);
                }
            }
            else
            {
                throw new SettingsNotFoundException();
            }
        }

        public void PrintSettingsToConsole()
        {
            var information = new StringBuilder("using next settings:");

            information.AppendFormat("\nPath to working directory is: \"{0}\";",
                CurrentSettings.PathDirectory);

            information.AppendFormat("\nOutput to console: {0};\nLogging: {1};",
                CurrentSettings.Output == MessageRecipient.Console,
                CurrentSettings.Output == MessageRecipient.Log);

            if(CurrentSettings.Output == MessageRecipient.Email)
            {
                information.AppendFormat("\nAll output text will be sending to: {0};",
                    CurrentSettings.EmailAdress);
            }

            information.Append("\n\nWrite here ID of next parameter, or press Enter, to start programm:");

            information.Append("\n1: Reset main directory to default;\n");
            //etc...

            Console.Write(information);


            string answer = Console.ReadLine();

            if (answer.Length == 0)
            {
                ApplySettings();
                return;
            }
            SetParameters(answer);
            PrintSettingsToConsole();
        }

        public void ApplySettings()
        {
            //сериализовать и применить
            throw new NotImplementedException();
        }

        public void SetParameters(string command)
        {
            //распарсить и записать в json
            throw new NotImplementedException();
        }

        public Print DefineDelegate(string message)
        {
            Print result = (msg) => { };

            if(CurrentSettings.Output == MessageRecipient.Console)
            {
                result += Console.WriteLine;
            }

            if (CurrentSettings.Output == MessageRecipient.Log)
            {
                result += WriteToLog;
            }

            if (CurrentSettings.Output == MessageRecipient.Email)
            {
                result += SendEmail;
            }

            return result;
        }

        private void WriteToLog(string message)
        {
            // variation for output function
            throw new NotImplementedException();
        }

        private void SendEmail(string message)
        {
            // variation for output function
            throw new NotImplementedException();
        }
    }
}
