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
    /// Lógica de interacción para OrdenamientoControl.xaml
    /// </summary>
    public partial class OrdenamientoControl : UserControl
    {
     // Lista interna con los datos
        private List<int> datos = new List<int>();

        public OrdenamientoControl()
        {
            InitializeComponent();
            cmbMetodos.SelectedIndex = 0; // Seleccionar BubbleSort por defecto
        }

        /// <summary>
        /// Evento del botón "Agregar": parsea los números y los añade a 'datos'. 
        /// </summary>
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            List<int> nuevos = ParsearNumeros(txtNumeros.Text);
            datos.AddRange(nuevos);         // Agregar a la lista existente
            txtNumeros.Clear();             // Limpiar el TextBox
            ActualizarDataGrid();           // Mostrar en DataGrid
        }

        /// <summary>
        /// Evento del botón "Ordenar": ordena la lista 'datos' según el método 
        /// seleccionado y actualiza el DataGrid.
        /// </summary>
        private void btnOrdenar_Click(object sender, RoutedEventArgs e)
        {
            if (datos.Count == 0)
            {
                MessageBox.Show("La lista está vacía. Agrega datos primero.");
                return;
            }

            // Determinar el método seleccionado
            ComboBoxItem itemSeleccionado = cmbMetodos.SelectedItem as ComboBoxItem;
            string metodo = itemSeleccionado?.Content.ToString() ?? "BubbleSort";

            // Ordenar
            switch (metodo)
            {
                case "BubbleSort":
                    SortAlgorithms.BubbleSort(datos);
                    break;
                case "InsertionSort":
                    SortAlgorithms.InsertionSort(datos);
                    break;
                case "SelectionSort":
                    SortAlgorithms.SelectionSort(datos);
                    break;
                case "QuickSort":
                    SortAlgorithms.QuickSort(datos, 0, datos.Count - 1);
                    break;
                case "MergeSort":
                    datos = SortAlgorithms.MergeSort(datos);
                    break;
                default:
                    SortAlgorithms.BubbleSort(datos);
                    break;
            }

            // Si se quiere descendente, invertimos la lista
            if (rdbDesc.IsChecked == true)
            {
                datos.Reverse();
            }

            // Refrescar el DataGrid con la lista ya ordenada
            ActualizarDataGrid();
        }

        /// <summary>
        /// Convierte la cadena en una lista de enteros (aceptando comas y/o espacios).
        /// </summary>
        private List<int> ParsearNumeros(string texto)
        {
            List<int> lista = new List<int>();
            if (string.IsNullOrWhiteSpace(texto))
                return lista;

            // Separar por comas o espacios
            string[] partes = texto.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string p in partes)
            {
                if (int.TryParse(p, out int num))
                {
                    lista.Add(num);
                }
            }
            return lista;
        }

        /// <summary>
        /// Muestra la lista 'datos' en el DataGrid.
        /// </summary>
        private void ActualizarDataGrid()
        {
            dgDatos.ItemsSource = null;
            dgDatos.ItemsSource = datos;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            datos.Clear();
            dgDatos.ItemsSource = null; 
            dgDatos.ItemsSource = datos;
        }
    }
    public static class SortAlgorithms
    {
        public static void BubbleSort(List<int> arr)
        {
            int n = arr.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        // Intercambiar
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }

        public static void InsertionSort(List<int> arr)
        {
            for (int i = 1; i < arr.Count; i++)
            {
                int key = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
        }

        public static void SelectionSort(List<int> arr)
        {
            for (int i = 0; i < arr.Count - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < arr.Count; j++)
                {
                    if (arr[j] < arr[minIndex])
                        minIndex = j;
                }
                // Intercambiar
                int temp = arr[minIndex];
                arr[minIndex] = arr[i];
                arr[i] = temp;
            }
        }

        public static void QuickSort(List<int> arr, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(arr, low, high);
                QuickSort(arr, low, pi - 1);
                QuickSort(arr, pi + 1, high);
            }
        }

        private static int Partition(List<int> arr, int low, int high)
        {
            int pivot = arr[high];
            int i = (low - 1);

            for (int j = low; j <= high - 1; j++)
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
            int tempPivot = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = tempPivot;

            return (i + 1);
        }

        public static List<int> MergeSort(List<int> arr)
        {
            if (arr.Count <= 1)
                return arr;

            int mid = arr.Count / 2;
            var left = arr.Take(mid).ToList();
            var right = arr.Skip(mid).ToList();

            left = MergeSort(left);
            right = MergeSort(right);

            return Merge(left, right);
        }

        private static List<int> Merge(List<int> left, List<int> right)
        {
            List<int> result = new List<int>();
            int i = 0, j = 0;

            while (i < left.Count && j < right.Count)
            {
                if (left[i] <= right[j])
                {
                    result.Add(left[i]);
                    i++;
                }
                else
                {
                    result.Add(right[j]);
                    j++;
                }
            }
            while (i < left.Count)
            {
                result.Add(left[i]);
                i++;
            }
            while (j < right.Count)
            {
                result.Add(right[j]);
                j++;
            }
            return result;
        }
    }
}
