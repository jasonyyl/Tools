using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM
{
    public class CCodeManager
    {
        public void CreateCode(string path)
        {
            FileStream fs = CFileManager.Open(path, FileMode.Create);
            StreamWriter st = new StreamWriter(fs);
            StringBuilder s = new StringBuilder();


            st.Close();
            fs.Close();
            st.Dispose();
            fs.Dispose();

        }
    }
}
