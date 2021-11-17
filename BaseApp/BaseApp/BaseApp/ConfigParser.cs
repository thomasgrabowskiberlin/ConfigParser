using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magnum.FileSystem;
using System.Windows.Forms;
using File = System.IO.File;
using System.Reflection;

namespace BaseApp
{
    public class ConfigParser
    {
        string ConfigFilename;
        string FieldName;


        DataCore DATACore;


        public ConfigParser(string ConfName)
        {
            DATACore = DataCore.getInstance();
            if (ConfName != null)
                ConfigFilename = ConfName;
            else
                ConfigFilename = "config.cfg";

            try
            {
                OpenAndParse();
            }
            catch
            {
                MessageBox.Show("Error Parsing Config File!");
            }

        }
        /*
        public static Object GetPropValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static T GetPropValue<T>(this Object obj, String name)
        {
            Object retval = GetPropValue(obj, name);
            if (retval == null) { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }
        */

        // Reflection helps to convert a string to member name 
        private void Reflect(string[] items)
        {   // items[0] -> Name
            // items[1] -> Value
            var pinfo = DATACore.GetType().GetProperty(FieldName);
            if (pinfo != null)
            {
                var obj = pinfo.GetValue(DATACore, null);
                if (obj != null)
                {
                    var pinfo2 = obj.GetType().GetProperty(items[0]);
                    if (pinfo2 != null)
                        pinfo2.SetValue(obj, Convert.ChangeType(items[1], pinfo2.PropertyType), null);
                }
            }



        }



        private bool OpenAndParse()
        {

            string[] lrvalue;

            if (ConfigFilename != null)
            {
                StreamReader reader = File.OpenText(ConfigFilename);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] items = line.Split('[', ']');
                    if (items.Length == 3)
                    {
                        // 3 Elemente -> [blabla] ... dann nimm das zweite = blablabla [,blabla,]
                        FieldName = items[1]; // [Test] => items[1]=Test
                    }
                    lrvalue = line.Split('=');
                    if (lrvalue.Length == 2)
                    { // Two Elements? Use Reflection!
                        Reflect(lrvalue);

                    }

                    try
                    {
                        int myInteger = int.Parse(items[1]);   // Here's your integer.
                    }
                    catch
                    {
                        //MessageBox.Show("line contains no int");
                    }
                    // Now let's find the path.
                    string path = null;
                    foreach (string item in items)
                    {
                        if (item.StartsWith("item\\") && item.EndsWith(".ddj"))
                            path = item;
                    }

                    // At this point, `myInteger` and `path` contain the values we want
                    // for the current line. We can then store those values or print them,
                    // or anything else we like.
                }

            }
            return true;
        }

    }
}
