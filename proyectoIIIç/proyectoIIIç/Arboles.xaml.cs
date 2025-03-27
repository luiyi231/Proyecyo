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
    /// Lógica de interacción para Arboles.xaml
    /// </summary>
    public partial class Arboles : UserControl
    {
        ArbolBinario arbol = new ArbolBinario();
        public Arboles()
        {
            InitializeComponent();
        }
        private void btnInsertar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtValor.Text, out int valor))
            {
                arbol.Insertar(valor);
                txtResultado.Text = $"Insertado {valor}";
                txtValor.Clear();
                DibujarArbol();
            }
            else
            {
                MessageBox.Show("Ingresa un número válido.");
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtValor.Text, out int valor))
            {
                arbol.Eliminar(valor);
                txtResultado.Text = $"Eliminado {valor} (si existía)";
                txtValor.Clear();
                DibujarArbol();
            }
            else
            {
                MessageBox.Show("Ingresa un número válido.");
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtValor.Text, out int valor))
            {
                bool encontrado = arbol.Buscar(valor);
                txtResultado.Text = encontrado ?
                    $"Se encontró {valor} en el árbol" :
                    $"No se encontró {valor}";
            }
            else
            {
                MessageBox.Show("Ingresa un número válido.");
            }
        }

        private void btnMinimo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int min = arbol.Minimo();
                txtResultado.Text = $"Mínimo: {min}";
            }
            catch (Exception ex)
            {
                txtResultado.Text = ex.Message;
            }
        }

        private void btnMaximo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int max = arbol.Maximo();
                txtResultado.Text = $"Máximo: {max}";
            }
            catch (Exception ex)
            {
                txtResultado.Text = ex.Message;
            }
        }

        private void btnPreorden_Click(object sender, RoutedEventArgs e)
        {
            var lista = arbol.Preorden();
            txtResultado.Text = "Preorden: " + string.Join(", ", lista);
        }

        private void btnInorden_Click(object sender, RoutedEventArgs e)
        {
            var lista = arbol.Inorden();
            txtResultado.Text = "Inorden: " + string.Join(", ", lista);
        }

        private void btnPostorden_Click(object sender, RoutedEventArgs e)
        {
            var lista = arbol.Postorden();
            txtResultado.Text = "Postorden: " + string.Join(", ", lista);
        }

        private void btnBalancear_Click(object sender, RoutedEventArgs e)
        {
            arbol.Balancear();
            txtResultado.Text = "Árbol balanceado.";
            DibujarArbol();
        }

        private void DibujarArbol()
        {
            canvasArbol.Children.Clear();
            if (arbol.Raiz == null) return;

            Queue<(ArbolBinario.Nodo nodo, int x, int y)> cola = new Queue<(ArbolBinario.Nodo, int, int)>();
            int startX = (int)(canvasArbol.Width / 2);
            int startY = 40;
            cola.Enqueue((arbol.Raiz, startX, startY));

            int horizontalOffset = 60;
            int verticalSpacing = 60;

            while (cola.Count > 0)
            {
                var (nodo, x, y) = cola.Dequeue();
                DibujarNodo(nodo.Valor, x, y);

                if (nodo.Izquierdo != null)
                {
                    int hijoX = x - horizontalOffset;
                    int hijoY = y + verticalSpacing;
                    DibujarLinea(x, y, hijoX, hijoY);
                    cola.Enqueue((nodo.Izquierdo, hijoX, hijoY));
                }
                if (nodo.Derecho != null)
                {
                    int hijoX = x + horizontalOffset;
                    int hijoY = y + verticalSpacing;
                    DibujarLinea(x, y, hijoX, hijoY);
                    cola.Enqueue((nodo.Derecho, hijoX, hijoY));
                }
            }
        }

        private void DibujarNodo(int valor, int x, int y)
        {
            int radio = 20;
            Ellipse circle = new Ellipse
            {
                Width = radio * 2,
                Height = radio * 2,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Fill = Brushes.LightYellow
            };
            Canvas.SetLeft(circle, x - radio);
            Canvas.SetTop(circle, y - radio);

            TextBlock label = new TextBlock
            {
                Text = valor.ToString(),
                FontWeight = FontWeights.Bold,
                FontSize = 14
            };
            Canvas.SetLeft(label, x - 8);
            Canvas.SetTop(label, y - 10);

            canvasArbol.Children.Add(circle);
            canvasArbol.Children.Add(label);
        }

        private void DibujarLinea(int x1, int y1, int x2, int y2)
        {
            Line line = new Line
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            canvasArbol.Children.Add(line);
        }
    }
    public class ArbolBinario
    {
        public class Nodo
        {
            public int Valor;
            public Nodo Izquierdo;
            public Nodo Derecho;

            public Nodo(int valor)
            {
                Valor = valor;
            }
        }

        public Nodo Raiz;

        public ArbolBinario()
        {
            Raiz = null;
        }

        public void Insertar(int valor)
        {
            Raiz = InsertarRec(Raiz, valor);
        }

        private Nodo InsertarRec(Nodo actual, int valor)
        {
            if (actual == null)
                return new Nodo(valor);

            if (valor < actual.Valor)
                actual.Izquierdo = InsertarRec(actual.Izquierdo, valor);
            else if (valor > actual.Valor)
                actual.Derecho = InsertarRec(actual.Derecho, valor);
            return actual;
        }
        public void Eliminar(int valor)
        {
            Raiz = EliminarRec(Raiz, valor);
        }

        private Nodo EliminarRec(Nodo actual, int valor)
        {
            if (actual == null) return null;

            if (valor < actual.Valor)
                actual.Izquierdo = EliminarRec(actual.Izquierdo, valor);
            else if (valor > actual.Valor)
                actual.Derecho = EliminarRec(actual.Derecho, valor);
            else
            {
                // caso sin hijos o un solo hijo
                if (actual.Izquierdo == null) return actual.Derecho;
                else if (actual.Derecho == null) return actual.Izquierdo;
                // caso dos hijos
                int min = EncontrarMin(actual.Derecho);
                actual.Valor = min;
                actual.Derecho = EliminarRec(actual.Derecho, min);
            }
            return actual;
        }
        public bool Buscar(int valor)
        {
            return BuscarRec(Raiz, valor);
        }

        private bool BuscarRec(Nodo actual, int valor)
        {
            if (actual == null) return false;
            if (actual.Valor == valor) return true;
            else if (valor < actual.Valor) return BuscarRec(actual.Izquierdo, valor);
            else return BuscarRec(actual.Derecho, valor);
        }

        public int Minimo()
        {
            if (Raiz == null) throw new InvalidOperationException("Árbol vacío");
            return EncontrarMin(Raiz);
        }

        private int EncontrarMin(Nodo actual)
        {
            while (actual.Izquierdo != null)
            {
                actual = actual.Izquierdo;
            }
            return actual.Valor;
        }

        public int Maximo()
        {
            if (Raiz == null) throw new InvalidOperationException("Árbol vacío");
            return EncontrarMax(Raiz);
        }

        private int EncontrarMax(Nodo actual)
        {
            while (actual.Derecho != null)
            {
                actual = actual.Derecho;
            }
            return actual.Valor;
        }

        public List<int> Preorden()
        {
            var lista = new List<int>();
            PreordenRec(Raiz, lista);
            return lista;
        }
        private void PreordenRec(Nodo actual, List<int> lista)
        {
            if (actual == null) return;
            lista.Add(actual.Valor);
            PreordenRec(actual.Izquierdo, lista);
            PreordenRec(actual.Derecho, lista);
        }

        public List<int> Inorden()
        {
            var lista = new List<int>();
            InordenRec(Raiz, lista);
            return lista;
        }
        private void InordenRec(Nodo actual, List<int> lista)
        {
            if (actual == null) return;
            InordenRec(actual.Izquierdo, lista);
            lista.Add(actual.Valor);
            InordenRec(actual.Derecho, lista);
        }

        public List<int> Postorden()
        {
            var lista = new List<int>();
            PostordenRec(Raiz, lista);
            return lista;
        }
        private void PostordenRec(Nodo actual, List<int> lista)
        {
            if (actual == null) return;
            PostordenRec(actual.Izquierdo, lista);
            PostordenRec(actual.Derecho, lista);
            lista.Add(actual.Valor);
        }
        public void Balancear()
        {
            var elementos = this.Inorden();
            Raiz = null;
            ConstruirBalanceado(elementos, 0, elementos.Count - 1);
        }

        private void ConstruirBalanceado(List<int> sortedList, int inicio, int fin)
        {
            if (inicio > fin) return;
            int mid = (inicio + fin) / 2;
            Insertar(sortedList[mid]);
            ConstruirBalanceado(sortedList, inicio, mid - 1);
            ConstruirBalanceado(sortedList, mid + 1, fin);
        }
    }
}