using QLKS.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLKS
{    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        

        // Thay đổi các nội dung theo từng button chọn trên màn hình
        public void SetVisibleContents(Grid gr)
        {
            foreach (Grid grid in gridCollumn2.Children)
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
                    button.Foreground = Brushes.Black;
                }
                else
                {
                    Color color = (Color)ColorConverter.ConvertFromString("#FFB1B1B1");
                    button.Foreground = new SolidColorBrush(color);
                }
            }
        }

        #region Xử lí các button click
        private void btnTrangChu_Click(object sender, RoutedEventArgs e)
        {
            SetVisibleContents(gridTrangChu);
            SetFocusTitle(btnTrangChu);
        }

        private void btnAnUong_Click(object sender, RoutedEventArgs e)
        {
            SetVisibleContents(gridAnUong);
            SetFocusTitle(btnAnUong);
        }

        private void btnGiatUi_Click(object sender, RoutedEventArgs e)
        {
            SetVisibleContents(gridGiatUi);
            SetFocusTitle(btnGiatUi);
        }

        private void btnDiChuyen_Click(object sender, RoutedEventArgs e)
        {
            SetVisibleContents(gridDiChuyen);
            SetFocusTitle(btnDiChuyen);
        }

        private void btnTraCuu_Click(object sender, RoutedEventArgs e)
        {
            SetVisibleContents(gridTraCuu);
            SetFocusTitle(btnTraCuu);
        }

        private void btnBaoCao_Click(object sender, RoutedEventArgs e)
        {
            SetVisibleContents(gridBaoCao);
            SetFocusTitle(btnBaoCao);
        }
        #endregion

        private void cboxLoaiGiatUi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboxLoaiGiatUi.SelectedIndex == 0)
            {
                tboxCanNang.IsEnabled = true;
                dateNgayBatDau.IsEnabled = false;
                dateNgayKetThuc.IsEnabled = false;
            }
            else
            {
                tboxCanNang.IsEnabled = false;
                dateNgayBatDau.IsEnabled = true;
                dateNgayKetThuc.IsEnabled = true;
            }
        }
    }
}
