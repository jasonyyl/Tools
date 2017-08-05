using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM
{
    public class CCustomTreeItem
    {
        public string Icon { get; set; }
        public string DisplayName { get; set; }
        public List<CCustomTreeItem> Children { get; set; }
        public CCustomTreeItem()
        {
            Children = new List<CCustomTreeItem>();
        }
    }
}
