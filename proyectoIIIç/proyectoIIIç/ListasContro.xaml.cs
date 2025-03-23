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
    /// Lógica de interacción para ListasContro.xaml
    /// </summary>
    public partial class ListasContro : UserControl
    {
        private LinkedList<int> lista = new LinkedList<int>();

        public ListasContro()
        {
            InitializeComponent();
        }

        private void BtnAgregarNodo_Click(object sender, RoutedEventArgs e)
        {
            lista.AddLast(lista.Count + 1);
            ActualizarLista();
        }

        private void BtnEliminarNodo_Click(object sender, RoutedEventArgs e)
        {
            if (lista.Count > 0) lista.RemoveFirst();
            ActualizarLista();
        }

        private void ActualizarLista()
        {
            ListaEnlazada.Items.Clear();
            ListaEnlazada.Items.Add("Lista: " + string.Join(" -> ", lista));
        }
    }
}
