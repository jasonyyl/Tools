using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TM
{
    public class CResourceManager
    {

        public void GetResources(string dir, CCustomTreeItem res, string[] p)
        {
            if (Directory.Exists(dir))
            {
                string[] ds = Directory.GetDirectories(dir);
                IEnumerable<string> fs = Directory.GetFiles(dir, "*.*").Where(f => p.Contains(Path.GetExtension(f).ToLower()));
                foreach (var d in ds)
                {
                    CCustomTreeItem resItem = new CCustomTreeItem();
                    string curName = Path.GetFileNameWithoutExtension(d);
                    resItem.Icon = CCommon.StrFolderIconPath;
                    resItem.DisplayName = curName;
                    res.Children.Add(resItem);
                    GetResources(d, resItem, p);
                }
                foreach (var f in fs)
                {
                    CCustomTreeItem resItem = new CCustomTreeItem();
                    string curName = Path.GetFileNameWithoutExtension(f);
                    resItem.Icon = CCommon.StrExcelIconPath;
                    resItem.DisplayName = curName;
                    res.Children.Add(resItem);
                }
            }
        }

    }
}
