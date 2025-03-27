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
            cmbMetodosOrdenamiento.SelectedIndex = 0;
            cmbMetodosBusqueda.SelectedIndex = 0;
        }
        /// <summary>
        /// Agrega al DataGrid los números ingresados en 'txtNumeros'
        /// </summary>
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            List<int> nuevos = ParsearNumeros(txtNumeros.Text);
            datos.AddRange(nuevos);
            txtNumeros.Clear();
            ActualizarDataGrid();
        }
        private void btnOrdenar_Click(object sender, RoutedEventArgs e)
        {
            if (datos.Count == 0)
            {
                MessageBox.Show("La lista está vacía. Agrega datos primero.");
                return;
            }

            ComboBoxItem itemOrden = cmbMetodosOrdenamiento.SelectedItem as ComboBoxItem;
            string metodo = itemOrden?.Content.ToString() ?? "Selección";

            switch (metodo)
            {
                case "Selección":
                    SortSearchAlgorithms.Seleccion(datos);
                    break;
                case "Inserción":
                    SortSearchAlgorithms.Insercion(datos);
                    break;
                case "Burbuja":
                    SortSearchAlgorithms.Burbuja(datos);
                    break;
                case "QuickSort":
                    SortSearchAlgorithms.QuickSort(datos, 0, datos.Count - 1);
                    break;
                case "MergeSort":
                    datos = SortSearchAlgorithms.MergeSort(datos);
                    break;
                case "RadixSort":
                    SortSearchAlgorithms.RadixSort(datos);
                    break;
                case "BucketSort":
                    SortSearchAlgorithms.BucketSort(datos);
                    break;
                case "ShellSort":
                    SortSearchAlgorithms.ShellSort(datos);
                    break;
                case "HeapSort":
                    SortSearchAlgorithms.HeapSort(datos);
                    break;
            }

            if (rdbDesc.IsChecked == true)
            {
                datos.Reverse();
            }

            ActualizarDataGrid();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (datos.Count == 0)
            {
                MessageBox.Show("La lista está vacía. Agrega datos primero.");
                return;
            }

            if (!int.TryParse(txtElementoBuscar.Text, out int valorBuscar))
            {
                MessageBox.Show("Ingresa un número válido en 'Elemento'.");
                return;
            }

            ComboBoxItem itemBusqueda = cmbMetodosBusqueda.SelectedItem as ComboBoxItem;
            string metodoBusqueda = itemBusqueda?.Content.ToString() ?? "Secuencial";

            int indiceEncontrado = -1;

            switch (metodoBusqueda)
            {
                case "Secuencial":
                    indiceEncontrado = SortSearchAlgorithms.BusquedaSecuencial(datos, valorBuscar);
                    break;
                case "Binaria":
                    bool isAsc = (rdbAsc.IsChecked == true);
                    indiceEncontrado = SortSearchAlgorithms.BusquedaBinaria(datos, valorBuscar, isAsc);
                    break;
                case "Has":
                    indiceEncontrado = SortSearchAlgorithms.BusquedaHas(datos, valorBuscar);
                    break;
                case "Lineal":
                    indiceEncontrado = SortSearchAlgorithms.BusquedaLineal(datos, valorBuscar);
                    break;
                case "Indexada":
                    indiceEncontrado = SortSearchAlgorithms.BusquedaIndexada(datos, valorBuscar);
                    break;
                default:
                    indiceEncontrado = SortSearchAlgorithms.BusquedaSecuencial(datos, valorBuscar);
                    break;
            }

            if (indiceEncontrado >= 0)
                txtResultadoBusqueda.Text = $"Encontrado en índice {indiceEncontrado}";
            else
                txtResultadoBusqueda.Text = "No encontrado";
        }

        private List<int> ParsearNumeros(string texto)
        {
            var lista = new List<int>();
            if (string.IsNullOrWhiteSpace(texto))
                return lista;

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

        // Refrescar el DataGrid
        private void ActualizarDataGrid()
        {
            dgDatos.ItemsSource = null;
            dgDatos.ItemsSource = datos;
        }
        public static class SortSearchAlgorithms
        {
            #region ORDENAMIENTOS
            public static void Seleccion(List<int> arr)
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
                    (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);
                }
            }

            public static void Insercion(List<int> arr)
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

            public static void Burbuja(List<int> arr)
            {
                int n = arr.Count;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
                    {
                        if (arr[j] > arr[j + 1])
                        {
                            (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                        }
                    }
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
                        (arr[i], arr[j]) = (arr[j], arr[i]);
                    }
                }
                (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
                return (i + 1);
            }

            public static List<int> MergeSort(List<int> arr)
            {
                if (arr.Count <= 1) return arr;

                int mid = arr.Count / 2;
                var left = arr.Take(mid).ToList();
                var right = arr.Skip(mid).ToList();

                left = MergeSort(left);
                right = MergeSort(right);

                return Merge(left, right);
            }
            private static List<int> Merge(List<int> left, List<int> right)
            {
                var result = new List<int>();
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

            public static void RadixSort(List<int> arr)
            {
                if (arr.Count < 2) return;
                int maxVal = arr.Max();
                for (int exp = 1; maxVal / exp > 0; exp *= 10)
                {
                    CountingSortByDigit(arr, exp);
                }
            }
            private static void CountingSortByDigit(List<int> arr, int exp)
            {
                int n = arr.Count;
                int[] output = new int[n];
                int[] count = new int[10];

                for (int i = 0; i < n; i++)
                {
                    int index = (arr[i] / exp) % 10;
                    count[index]++;
                }
                for (int i = 1; i < 10; i++)
                {
                    count[i] += count[i - 1];
                }
                for (int i = n - 1; i >= 0; i--)
                {
                    int index = (arr[i] / exp) % 10;
                    output[count[index] - 1] = arr[i];
                    count[index]--;
                }
                for (int i = 0; i < n; i++)
                {
                    arr[i] = output[i];
                }
            }

            public static void BucketSort(List<int> arr)
            {
                if (arr.Count <= 1) return;
                int maxVal = arr.Max();
                int bucketCount = Math.Max(1, (int)Math.Sqrt(arr.Count));

                List<int>[] buckets = new List<int>[bucketCount];
                for (int i = 0; i < bucketCount; i++)
                    buckets[i] = new List<int>();

                // Distribuir
                foreach (var val in arr)
                {
                    int bi = (val * bucketCount) / (maxVal + 1);
                    buckets[bi].Add(val);
                }
                // Ordenar cada bucket (usar Inserción, por ejemplo)
                for (int i = 0; i < bucketCount; i++)
                {
                    Insercion(buckets[i]);
                }
                // Reconstruir
                int idx = 0;
                for (int i = 0; i < bucketCount; i++)
                {
                    foreach (var val in buckets[i])
                    {
                        arr[idx++] = val;
                    }
                }
            }

            public static void ShellSort(List<int> arr)
            {
                int n = arr.Count;
                for (int gap = n / 2; gap > 0; gap /= 2)
                {
                    for (int i = gap; i < n; i++)
                    {
                        int temp = arr[i];
                        int j = i;
                        while (j >= gap && arr[j - gap] > temp)
                        {
                            arr[j] = arr[j - gap];
                            j -= gap;
                        }
                        arr[j] = temp;
                    }
                }
            }

            public static void HeapSort(List<int> arr)
            {
                int n = arr.Count;

                // Build max heap
                for (int i = n / 2 - 1; i >= 0; i--)
                    Heapify(arr, n, i);

                // Extraer
                for (int i = n - 1; i > 0; i--)
                {
                    (arr[0], arr[i]) = (arr[i], arr[0]);
                    Heapify(arr, i, 0);
                }
            }
            private static void Heapify(List<int> arr, int n, int i)
            {
                int largest = i;
                int left = 2 * i + 1;
                int right = 2 * i + 2;

                if (left < n && arr[left] > arr[largest]) largest = left;
                if (right < n && arr[right] > arr[largest]) largest = right;

                if (largest != i)
                {
                    (arr[i], arr[largest]) = (arr[largest], arr[i]);
                    Heapify(arr, n, largest);
                }
            }

            #endregion

            #region BÚSQUEDAS

            /// <summary>
            /// Búsqueda secuencial (lineal). Retorna índice o -1.
            /// </summary>
            public static int BusquedaSecuencial(List<int> arr, int valor)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    if (arr[i] == valor)
                        return i;
                }
                return -1;
            }

            /// <summary>
            /// Búsqueda Binaria que funciona con listas ordenadas ascendentemente 
            /// o descendentemente (según 'ascending').
            /// Retorna índice o -1 si no se encuentra.
            /// </summary>
            /// <param name="arr">Lista de enteros, ya ordenada asc o desc</param>
            /// <param name="valor">Valor a buscar</param>
            /// <param name="ascending">true si la lista está en orden ascendente; false si está descendente</param>
            public static int BusquedaBinaria(List<int> arr, int valor, bool ascending)
            {
                int left = 0;
                int right = arr.Count - 1;

                while (left <= right)
                {
                    int mid = (left + right) / 2;
                    if (arr[mid] == valor)
                        return mid;

                    if (ascending)
                    {
                        // Búsqueda binaria ascendente
                        if (arr[mid] < valor)
                            left = mid + 1;
                        else
                            right = mid - 1;
                    }
                    else
                    {
                        // Búsqueda binaria descendente
                        if (arr[mid] < valor)
                            right = mid - 1;
                        else
                            left = mid + 1;
                    }
                }
                return -1;
            }

            public static int BusquedaHas(List<int> arr, int valor)
            {
                if (arr.Contains(valor))
                    return arr.IndexOf(valor);
                return -1;
            }

            /// <summary>
            /// Búsqueda Lineal: recorre la lista y retorna el índice del elemento si lo encuentra.
            /// </summary>
            public static int BusquedaLineal(List<int> arr, int valor)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    if (arr[i] == valor)
                        return i;
                }
                return -1;
            }

            /// <summary>
            /// Búsqueda Indexada: construye un diccionario de índices y retorna el índice del elemento.
            /// </summary>
            public static int BusquedaIndexada(List<int> arr, int valor)
            {
                Dictionary<int, int> indexMap = new Dictionary<int, int>();
                for (int i = 0; i < arr.Count; i++)
                {
                    if (!indexMap.ContainsKey(arr[i]))
                        indexMap[arr[i]] = i;
                }
                if (indexMap.ContainsKey(valor))
                    return indexMap[valor];
                return -1;
            }

            #endregion
        }
    }
}
