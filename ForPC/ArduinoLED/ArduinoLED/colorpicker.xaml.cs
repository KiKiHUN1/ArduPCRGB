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
using Xceed.Wpf.Toolkit;

namespace ArduinoLED
{
    /// <summary>
    /// Interaction logic for colorpicker.xaml
    /// </summary>
    public partial class colorpicker : Window
    {
        public float r, g, b;
       
        

        public colorpicker()
        {
            InitializeComponent();
        }

       

        private void Window_Closed(object sender, EventArgs e)
        {
           
        }

        private void Szinvaltoztatva(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
           r = colorpicker1.SelectedColor.Value.R;
           g = colorpicker1.SelectedColor.Value.G;
           b = colorpicker1.SelectedColor.Value.B;
        }
    }
}
