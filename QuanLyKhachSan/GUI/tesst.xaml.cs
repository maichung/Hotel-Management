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
using DTO;
using DAL1;
namespace GUI
{
    /// <summary>
    /// Interaction logic for tesst.xaml
    /// </summary>
    public partial class tesst : Window
    {
        NhanVienDAL bl = new NhanVienDAL();
        public tesst()
        {
            InitializeComponent();
            Employee_View_Load();
        }
        private void Employee_View_Load()
        {
            dt.AutoGenerateColumns = false;
           
            
            dt.ItemsSource = bl.layNhanVien(); 
            
        }
    }
}
