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
    /// Lógica de interacción para PilasColasControl.xaml
    /// </summary>
    public partial class PilasColasControl : UserControl
    {
        // Pila representada por una lista
        private Stack<int> pila = new Stack<int>();
        private const int TamañoMaximo = 10;  // Tamaño máximo de la pila

        public PilasColasControl()
        {
            InitializeComponent();
            // Establecer el tamaño máximo en el TextBox
            txtPilaMax.Text = TamañoMaximo.ToString();
        }

        // Función para insertar un número en la pila
        private void InsertarPila_Click(object sender, RoutedEventArgs e)
        {
            // Verifica que el valor del TextBox sea un número
            if (int.TryParse(txtPila.Text, out int valor))
            {
                // Verifica si la pila no está llena
                if (pila.Count < TamañoMaximo)
                {
                    pila.Push(valor); // Inserta el valor en la pila
                    lstPila.Items.Add(valor); // Agrega el valor a la lista visual
                    txtPila.Clear(); // Limpia el TextBox
                }
                else
                {
                    MessageBox.Show("La pila está llena.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Por favor ingrese un número válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ActualizarEstadoPila();
        }

        // Función para quitar el valor de la cima de la pila
        private void QuitarPila_Click(object sender, RoutedEventArgs e)
        {
            if (pila.Count > 0)
            {
                pila.Pop(); // Elimina el valor de la cima
                lstPila.Items.RemoveAt(lstPila.Items.Count - 1); // Elimina el último elemento de la lista visual
            }
            else
            {
                MessageBox.Show("La pila está vacía.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            ActualizarEstadoPila();
        }

        // Función para limpiar la pila
        private void LimpiarPila_Click(object sender, RoutedEventArgs e)
        {
            pila.Clear(); // Limpia la pila
            lstPila.Items.Clear(); // Limpia la lista visual

            ActualizarEstadoPila();
        }

        // Función para actualizar los estados de la pila
        private void ActualizarEstadoPila()
        {
            // Verifica si la pila está vacía
            txtPilaVacia.Text = pila.Count == 0 ? "Sí" : "No";

            // Verifica si la pila está llena
            txtPilaLlena.Text = pila.Count == TamañoMaximo ? "Sí" : "No";

            // Muestra el valor de la cima de la pila
            txtPilaCima.Text = pila.Count > 0 ? pila.Peek().ToString() : "N/A";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this);

            // Verificar que sea el tipo correcto de ventana
            if (parentWindow is MainWindow mainWindow)
            {
                // Cambiar el contenido del ContentControl para cargar el UserControl de Colas
                mainWindow.Contenido.Content = new Colas();
            }
        }
    }
}

