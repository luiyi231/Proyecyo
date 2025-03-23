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
    /// Lógica de interacción para Colas.xaml
    /// </summary>
    public partial class Colas : UserControl
    {
        // Cola representada por una lista
        private Queue<int> cola = new Queue<int>();
        private const int TamañoMaximo = 10;  // Tamaño máximo de la cola

        public Colas()
        {
            InitializeComponent();
            // Establecer el tamaño máximo en el TextBox
            txtColaMax.Text = TamañoMaximo.ToString();
        }

        // Función para insertar un número en la cola
        private void InsertarCola_Click(object sender, RoutedEventArgs e)
        {
            // Verifica que el valor del TextBox sea un número
            if (int.TryParse(txtCola.Text, out int valor))
            {
                // Verifica si la cola no está llena
                if (cola.Count < TamañoMaximo)
                {
                    cola.Enqueue(valor); // Inserta el valor en la cola
                    lstCola.Items.Add(valor); // Agrega el valor a la lista visual
                    txtCola.Clear(); // Limpia el TextBox
                }
                else
                {
                    MessageBox.Show("La cola está llena.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Por favor ingrese un número válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ActualizarEstadoCola();
        }

        // Función para quitar el valor del frente de la cola
        private void QuitarCola_Click(object sender, RoutedEventArgs e)
        {
            if (cola.Count > 0)
            {
                cola.Dequeue(); // Elimina el valor del frente
                lstCola.Items.RemoveAt(0); // Elimina el primer elemento de la lista visual
            }
            else
            {
                MessageBox.Show("La cola está vacía.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            ActualizarEstadoCola();
        }

        // Función para limpiar la cola
        private void LimpiarCola_Click(object sender, RoutedEventArgs e)
        {
            cola.Clear(); // Limpia la cola
            lstCola.Items.Clear(); // Limpia la lista visual

            ActualizarEstadoCola();
        }

        // Función para actualizar los estados de la cola
        private void ActualizarEstadoCola()
        {
            // Verifica si la cola está vacía
            txtColaVacia.Text = cola.Count == 0 ? "Sí" : "No";

            // Verifica si la cola está llena
            txtColaLlena.Text = cola.Count == TamañoMaximo ? "Sí" : "No";

            // Muestra el valor del frente de la cola
            txtColaFrente.Text = cola.Count > 0 ? cola.Peek().ToString() : "N/A";
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this);

            // Verificar que sea el tipo correcto de ventana
            if (parentWindow is MainWindow mainWindow)
            {
                // Cambiar el contenido del ContentControl para cargar el UserControl de Colas
                mainWindow.Contenido.Content = new PilasColasControl();
            }
        }
    }
}
