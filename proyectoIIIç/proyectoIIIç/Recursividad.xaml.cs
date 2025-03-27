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
    /// Lógica de interacción para Recursividad.xaml
    /// </summary>
    public partial class Recursividad : UserControl
    {
        private List<int> _numeros = new List<int>();
        public Recursividad()
        {
            InitializeComponent();
            cmbEjercicios.SelectedIndex = 0;
        }

        private void cmbEjercicios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LimpiarTodo();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtValor.Text, out int val))
            {
                _numeros.Add(val);
                ActualizarGrid();
                txtValor.Clear();
            }
            else
            {
                MessageBox.Show("Ingrese un número válido.");
            }
        }

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            if (cmbEjercicios.SelectedItem is ComboBoxItem item)
            {
                string opcion = item.Content.ToString();
                txtResultado.Clear();
                if (opcion.Contains("Capicúa"))
                {
                    int numero = ObtenerNumeroUnico();
                    bool esCapicua = EsCapicua(numero);
                    txtResultado.Text = esCapicua ? numero + " ES capicúa" : numero + " NO es capicúa";
                }
                else if (opcion.Contains("Sumar elementos"))
                {
                    int suma = SumarVector(_numeros, 0);
                    txtResultado.Text = "Suma = " + suma;
                }
                else if (opcion.Contains("Multiplicar elementos"))
                {
                    if (_numeros.Count == 0)
                    {
                        txtResultado.Text = "No hay datos en el vector.";
                    }
                    else
                    {
                        long prod = MultiplicarVector(_numeros, 0);
                        txtResultado.Text = "Producto = " + prod;
                    }
                }
                else if (opcion.Contains("Menor de un vector"))
                {
                    if (_numeros.Count == 0)
                    {
                        txtResultado.Text = "No hay datos en el vector.";
                    }
                    else
                    {
                        int menor = EncontrarMenor(_numeros, 0, _numeros[0]);
                        txtResultado.Text = "Mínimo = " + menor;
                    }
                }
                else if (opcion.Contains("Mayor de un vector"))
                {
                    if (_numeros.Count == 0)
                    {
                        txtResultado.Text = "No hay datos en el vector.";
                    }
                    else
                    {
                        int mayor = EncontrarMayor(_numeros, 0, _numeros[0]);
                        txtResultado.Text = "Máximo = " + mayor;
                    }
                }
                else if (opcion.Contains("Factorial"))
                {
                    int numero = ObtenerNumeroUnico();
                    if (numero < 0)
                    {
                        txtResultado.Text = "Factorial no definido para negativos.";
                    }
                    else
                    {
                        long fact = Factorial(numero);
                        txtResultado.Text = "Factorial(" + numero + ") = " + fact;
                    }
                }
                else if (opcion.Contains("Fibonacci"))
                {
                    int numero = ObtenerNumeroUnico();
                    if (numero < 0)
                    {
                        txtResultado.Text = "No existe Fibonacci de negativos.";
                    }
                    else
                    {
                        long fib = Fibonacci(numero);
                        txtResultado.Text = "Fibonacci(" + numero + ") = " + fib;
                    }
                }
                else if (opcion.Contains("Invertir un número"))
                {
                    int numero = ObtenerNumeroUnico();
                    long invertido = InvertirNumero(numero);
                    txtResultado.Text = "Invertido de " + numero + " = " + invertido;
                }
                else if (opcion.Contains("Sumar dígitos"))
                {
                    int numero = ObtenerNumeroUnico();
                    int sumaDig = SumaDigitos(Math.Abs(numero));
                    txtResultado.Text = "Suma de dígitos de " + numero + " = " + sumaDig;
                }
                else if (opcion.Contains("Suma desde 1 hasta n"))
                {
                    int numero = ObtenerNumeroUnico();
                    if (numero < 1)
                    {
                        txtResultado.Text = "Debe ser un número >= 1";
                    }
                    else
                    {
                        int suma1n = Suma1aN(numero);
                        txtResultado.Text = "Suma 1.." + numero + " = " + suma1n;
                    }
                }
                else if (opcion.Contains("Par o impar"))
                {
                    int numero = ObtenerNumeroUnico();
                    bool esPar = EsPar(numero);
                    txtResultado.Text = esPar ? numero + " es PAR" : numero + " es IMPAR";
                }
                else if (opcion.Contains("Positivo o negativo"))
                {
                    int numero = ObtenerNumeroUnico();
                    if (numero > 0) txtResultado.Text = numero + " es POSITIVO";
                    else if (numero < 0) txtResultado.Text = numero + " es NEGATIVO";
                    else txtResultado.Text = "El número es CERO (ni positivo ni negativo)";
                }
                else if (opcion.Contains("Torres de Hanói"))
                {
                    int discos = ObtenerNumeroUnico();
                    if (discos < 1)
                    {
                        txtResultado.Text = "Debe tener al menos 1 disco.";
                    }
                    else
                    {
                        dgDatos.ItemsSource = null;
                        var pasos = new List<string>();
                        Hanoi(discos, 'A', 'C', 'B', pasos);
                        txtResultado.Text = "Movimientos totales: " + pasos.Count;
                        dgDatos.ItemsSource = pasos;
                    }
                }
                else
                {
                    txtResultado.Text = "Selecciona un ejercicio válido.";
                }
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarTodo();
        }

        private void LimpiarTodo()
        {
            _numeros.Clear();
            ActualizarGrid();
            txtValor.Clear();
            txtResultado.Clear();
        }

        private void ActualizarGrid()
        {
            dgDatos.ItemsSource = null;
            dgDatos.ItemsSource = _numeros;
        }

        private int ObtenerNumeroUnico()
        {
            if (_numeros.Count > 0)
            {
                return _numeros[0];
            }
            else
            {
                int.TryParse(txtValor.Text, out int num);
                return num;
            }
        }

        private bool EsCapicua(int n)
        {
            string s = Math.Abs(n).ToString();
            return EsCapicuaCadena(s, 0, s.Length - 1);
        }

        private bool EsCapicuaCadena(string s, int ini, int fin)
        {
            if (ini >= fin) return true;
            if (s[ini] != s[fin]) return false;
            return EsCapicuaCadena(s, ini + 1, fin - 1);
        }

        private int SumarVector(List<int> vec, int i)
        {
            if (i >= vec.Count) return 0;
            return vec[i] + SumarVector(vec, i + 1);
        }

        private long MultiplicarVector(List<int> vec, int i)
        {
            if (i >= vec.Count) return 1;
            return vec[i] * MultiplicarVector(vec, i + 1);
        }

        private int EncontrarMenor(List<int> vec, int i, int menorActual)
        {
            if (i >= vec.Count) return menorActual;
            if (vec[i] < menorActual) menorActual = vec[i];
            return EncontrarMenor(vec, i + 1, menorActual);
        }

        private int EncontrarMayor(List<int> vec, int i, int mayorActual)
        {
            if (i >= vec.Count) return mayorActual;
            if (vec[i] > mayorActual) mayorActual = vec[i];
            return EncontrarMayor(vec, i + 1, mayorActual);
        }

        private long Factorial(int n)
        {
            if (n <= 1) return 1;
            return n * Factorial(n - 1);
        }

        private long Fibonacci(int n)
        {
            if (n < 2) return n;
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        private long InvertirNumero(int n)
        {
            n = Math.Abs(n);
            return InvertirNumeroRec(n, 0);
        }

        private long InvertirNumeroRec(long n, long rev)
        {
            if (n == 0) return rev;
            long digit = n % 10;
            return InvertirNumeroRec(n / 10, rev * 10 + digit);
        }

        private int SumaDigitos(int n)
        {
            if (n < 10) return n;
            return (n % 10) + SumaDigitos(n / 10);
        }

        private int Suma1aN(int n)
        {
            if (n <= 1) return n;
            return n + Suma1aN(n - 1);
        }

        private bool EsPar(int n)
        {
            if (n == 0) return true;
            if (n == 1 || n == -1) return false;
            return EsPar(Math.Abs(n) - 2);
        }

        private void Hanoi(int n, char origen, char destino, char auxiliar, List<string> pasos)
        {
            if (n == 1)
            {
                pasos.Add("Mover disco de " + origen + " a " + destino);
            }
            else
            {
                Hanoi(n - 1, origen, auxiliar, destino, pasos);
                pasos.Add("Mover disco de " + origen + " a " + destino);
                Hanoi(n - 1, auxiliar, destino, origen, pasos);
            }
        }
    }
}
