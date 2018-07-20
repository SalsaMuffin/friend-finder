using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace FriendFinder.Core
{
    public static class FileReader
    {
        public static List<T> ReadFiles<T>(string path, bool useFileName = false) where T : new()
        {
            var di = new DirectoryInfo(path);
            var filesList = di.GetFiles();
            List<T> lst = new List<T>();
            foreach (var file in filesList)
            {
                string JSON_string = File.ReadAllText(file.FullName);
                var data = new JavaScriptSerializer().Deserialize<T>(JSON_string);
                if (useFileName)
                {
                    data.GetType().GetProperty("Name").SetValue(data, file.Name.Substring(0, file.Name.LastIndexOf('.')));
                }
                lst.Add(data);
            }
            return lst;
        }
    }
}
