using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApp
{
    public class DataCore
    {


        static DataCore Instance;
        private static Mutex mut = new Mutex();

        // Data
        public class TestData { 
        public int a { get; set; }
        public int b { get; set; }
        public int c { get; set; }
       

        }
        public class ConfigData
        {
            public string conf { get; set; }
        }


        public TestData Test { get; set; }
        public ConfigData Config { get; set; }
        private DataCore()
        {
            Test = new TestData();
            Config = new ConfigData();

        }

        public static DataCore getInstance()
        {
            mut.WaitOne(); // wait till it is safe to enter
            if (Instance == null)
            {
                Instance = new DataCore();
            }
            mut.ReleaseMutex();
            return Instance;
        }

        class DataFields
        {
            public Vector<int> DataVector = new Vector<int>();
        }
    }



}
