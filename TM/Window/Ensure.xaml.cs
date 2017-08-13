using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TM
{
    /// <summary>
    /// Ensure.xaml 的交互逻辑
    /// </summary>
    public partial class Ensure : Window
    {
        public bool IsEnsure = false;
        public Ensure()
        {
            InitializeComponent();
        }
        public void Init(string msg)
        {
            tb_title.Text = msg;
            IsEnsure = false;
        }
        private void bt_cancle_Click(object sender, RoutedEventArgs e)
        {
            IsEnsure = false;
            this.Close();
        }

        private void bt_ensure_Click(object sender, RoutedEventArgs e)
        {
            IsEnsure = true;
            this.Close();
        }
    }
}
