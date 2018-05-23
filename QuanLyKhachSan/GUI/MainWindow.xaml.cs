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

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadTrangChu();
            LoadAnUong();
        }

        // Load nội dung trang chủ
        public void LoadTrangChu()
        {
            Grid gridRow = new Grid();
            gridPhong.Children.Add(gridRow);

            for (int i = 0; i < 7; i++)
            {
                RowDefinition row = new RowDefinition();
                gridRow.RowDefinitions.Add(row);
                row.Height = new GridLength(80);
                Grid gridCollumn = new Grid();
                gridRow.Children.Add(gridCollumn);

                Grid.SetRow(gridCollumn, i);
                for (int j = 0; j < 6; j++)
                {
                    ColumnDefinition col = new ColumnDefinition();
                    gridCollumn.ColumnDefinitions.Add(col);
                    Button btn = new Button();
                    btn.Content = "Dong " + i + " Cot " + j;
                    btn.BorderBrush = null;
                    btn.Margin = new Thickness(5, 0, 0, 5);
                    gridCollumn.Children.Add(btn);
                    Grid.SetColumn(btn, j);
                }
            }
        }

        // Load nội dung trang ăn uống
        public void LoadAnUong()
        {
            lstHangHoa.Items.Add(new HangHoa() { Number = 1, ID = 3431, Name = "Cocacola", PriceUnit = 10000, Quantity = 1, button = new Button() });
            lstHangHoa.Items.Add(new HangHoa() { Number = 2, ID = 6541, Name = "Pepsi", PriceUnit = 18000, Quantity = 4, button = new Button() });
            lstHangHoa.Items.Add(new HangHoa() { Number = 3, ID = 3881, Name = "Twister", PriceUnit = 23000, Quantity = 1, button = new Button() });
            lstHangHoa.Items.Add(new HangHoa() { Number = 4, ID = 9321, Name = "Aquafina", PriceUnit = 6700, Quantity = 2, button = new Button() });

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


    }
}
