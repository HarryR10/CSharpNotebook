using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Social
{
    public class JsonFileWriter
    {
        public  static void Writer<T>(string path, IList<T> list)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };

            //string jsonString = "";
            //foreach (var el in list)
            //{
            //    jsonString += JsonSerializer.Serialize<object>(el, options);
            //}

            if (File.Exists(path))
            {
                File.Delete(path);
            }


            using (StreamWriter fs = new StreamWriter(path))
            {
                var jsonString = new StringBuilder("[\n");

                jsonString.Append(JsonSerializer.Serialize<T>(list[0], options));

                for (int i = 1; i < list.Count; i++)
                {
                    jsonString.Append(",\n" + JsonSerializer.Serialize<T>(list[i], options));
                }

                jsonString.Append("\n]");

                fs.Write(jsonString.ToString());
            }
        }
    }
}
