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
            InitListPhong();
        }
        public void InitListPhong()
        {
            Grid gridRow = new Grid();
            GridTrangChu.Children.Add(gridRow);

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

        
    }
}
