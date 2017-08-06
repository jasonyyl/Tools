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
    /// CreateNew.xaml 的交互逻辑
    /// </summary>
    public partial class CreateNew : Window
    {
        public string InputName { get; set; }
        public CreateNew()
        {
            InitializeComponent();
        }

        private void bt_ensure_Click(object sender, RoutedEventArgs e)
        {
            InputName = tb_input_name.Text;
            this.Close();
        }
    }
}
