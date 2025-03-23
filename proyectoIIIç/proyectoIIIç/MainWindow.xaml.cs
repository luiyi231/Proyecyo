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

namespace proyectoIIIç
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnPilasColas_Click(object sender, RoutedEventArgs e)
        {
            Contenido.Content = new PilasColasControl();
        }

        private void BtnListas_Click(object sender, RoutedEventArgs e)
        {
            Contenido.Content = new ListasContro();
        }

        private void BtnOrdenamiento_Click(object sender, RoutedEventArgs e)
        {
            Contenido.Content = new OrdenamientoControl();
        }

        private void BtnCalculadora_Click(object sender, RoutedEventArgs e)
        {
            Contenido.Content = new Calculadora();
        }
    }
}
