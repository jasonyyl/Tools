using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM
{
    public class CSingleton<T> where T : new()
    {
        public static T Instance;
        public CSingleton()
        {
            if (Instance == null)
            {
                Instance = new T();
            }
        }
    }
}
