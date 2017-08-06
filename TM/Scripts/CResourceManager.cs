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
        List<CResourceItem> m_CacheResItems;
        public CResourceManager()
        {
            m_CacheResItems = new List<CResourceItem>();
        }
        public void GetResources(string dir, CResourceItem res, string[] p)
        {
            if (Directory.Exists(dir))
            {         
                #region dir
                string[] ds = Directory.GetDirectories(dir);
                string[] fss = Directory.GetFiles(dir, "*.*");
                IEnumerable<string> fs = fss.Where(f => p.Contains(Path.GetExtension(f).ToLower()));
                foreach (var c in res.Children)
                    c.IsKeep = false;
                foreach (var d in ds)
                {
                    bool isNew = true;
                    CResourceItem resItem = null;
                    foreach (var c in res.Children)
                    {
                        if (c.Path == d)
                        {
                            c.IsKeep = true;
                            resItem = c;
                            isNew = false;
                        }
                    }
                    if (isNew)
                    {
                        resItem = new CResourceItem();
                        string curName = Path.GetFileNameWithoutExtension(d);
                        resItem.Icon = CCommon.StrFolderIconPath;
                        resItem.DisplayName = curName;
                        resItem.Path = d;
                        resItem.IsKeep = true;
                        res.Children.Add(resItem);
                    }
                    GetResources(d, resItem, p);


                }
                #endregion

                #region file
                foreach (var f in fs)
                {
                    bool isNew = true;
                    foreach (var c in res.Children)
                    {
                        if (c.Path == f)
                        {
                            c.IsKeep = true;
                            isNew = false;
                        }
                    }
                    if (isNew)
                    {
                        CResourceItem resItem = new CResourceItem();
                        string curName = Path.GetFileNameWithoutExtension(f);
                        resItem.Icon = CCommon.StrExcelIconPath;
                        resItem.DisplayName = curName;
                        resItem.Path = f;
                        resItem.IsKeep = true;
                        res.Children.Add(resItem);
                    }
                }
                #endregion

                #region Remove
                m_CacheResItems.Clear();
                foreach (var item in res.Children)
                {
                    if (!item.IsKeep)
                    {
                        m_CacheResItems.Add(item);
                    }
                }
                foreach (var item in m_CacheResItems)
                {
                    res.Children.Remove(item);
                }
                #endregion
            }
        }

    }
}
