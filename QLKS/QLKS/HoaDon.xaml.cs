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

namespace QLKS
{
    /// <summary>
    /// Interaction logic for HoaDon.xaml
    /// </summary>
    public partial class HoaDon : Window
    {
        public HoaDon()
        {
            InitializeComponent();
        }

        private void btnHDTong_Click(object sender, RoutedEventArgs e)
        {
            SetVisibleContents(gridHoaDonTong);
            SetFocusTitle(btnHDTong);
        }

        public void SetVisibleContents(Grid gr)
        {
            foreach (Grid grid in gridColumn1.Children)
            {
                if (grid.Name == gr.Name)
                {
                    grid.Visibility = Visibility.Visible;
                }
                else
                {
                    grid.Visibility = Visibility.Hidden;
                }
            }
        }
        public void SetFocusTitle(Button btn)
        {
            foreach (Button button in groupButtonTitles.Children)
            {
                if (button.Name == btn.Name)
                {
                    button.Foreground = Brushes.White;
                }
                else
                {
                    Color color = (Color)ColorConverter.ConvertFromString("#FFB1B1B1");
                    button.Foreground = new SolidColorBrush(color);
                }
            }
        }

        private void btnHDLuuTru_Click(object sender, RoutedEventArgs e)
        {
            SetVisibleContents(gridHoaDonLuuTru);
            SetFocusTitle(btnHDLuuTru);
        }

        private void btnHDAnUong_Click(object sender, RoutedEventArgs e)
        {
            SetVisibleContents(gridHoaDonAnUong);
            SetFocusTitle(btnHDAnUong);
        }

        private void btnHDGiatUi_Click(object sender, RoutedEventArgs e)
        {
            SetVisibleContents(gridHoaDonGiatUi);
            SetFocusTitle(btnHDGiatUi);
        }

        private void btnHDDiChuyen_Click(object sender, RoutedEventArgs e)
        {
            SetVisibleContents(gridHoaDonDiChuyen);
            SetFocusTitle(btnHDDiChuyen);
        }
    }
}
