using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM
{
    public class CResourceItem: INotifyPropertyChanged
    {
        public string Icon { get; set; }
        public string DisplayName { get; set; }
        public string Path { get; set; }
        public ObservableCollection<CResourceItem> Children { get; set; }

        public bool IsKeep = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public CResourceItem()
        {
            Children = new ObservableCollection<CResourceItem>();
        }

        private void Changed(string PropertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
