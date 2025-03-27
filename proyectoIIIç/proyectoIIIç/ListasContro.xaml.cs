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
    public partial class ListasContro : UserControl
    {
        ListaEnlazada listaSimple = new ListaEnlazada();
        ListaDobleEnlazada listaDoble = new ListaDobleEnlazada();

        public ListasContro()
        {
            InitializeComponent();
        }

        #region LISTA SIMPLE - EVENTOS
        private void btnAgregarInicioSimple_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtSimple.Text, out int valor))
            {
                listaSimple.AgregarAlInicio(valor);
                txtSimple.Clear();
                ActualizarDataGrid_Simple();
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un número válido (Lista Simple).");
            }
        }

        private void btnAgregarFinalSimple_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtSimple.Text, out int valor))
            {
                listaSimple.AgregarAlFinal(valor);
                txtSimple.Clear();
                ActualizarDataGrid_Simple();
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un número válido (Lista Simple).");
            }
        }

        private void btnEliminarInicioSimple_Click(object sender, RoutedEventArgs e)
        {
            listaSimple.EliminarInicio();
            ActualizarDataGrid_Simple();
        }

        private void btnEliminarFinalSimple_Click(object sender, RoutedEventArgs e)
        {
            listaSimple.EliminarFinal();
            ActualizarDataGrid_Simple();
        }

        private void btnEstaVacioSimple_Click(object sender, RoutedEventArgs e)
        {
            txtEstaVacioSimple.Text = listaSimple.EstaVacio() ? "Sí" : "No";
        }

        private void btnMayorSimple_Click(object sender, RoutedEventArgs e)
        {
            if (!listaSimple.EstaVacio())
                txtMayorSimple.Text = listaSimple.EncontrarMayor().ToString();
            else
                txtMayorSimple.Text = "Sin datos";
        }

        private void btnSumarSimple_Click(object sender, RoutedEventArgs e)
        {
            if (!listaSimple.EstaVacio())
                txtSumaSimple.Text = listaSimple.SumarElementos().ToString();
            else
                txtSumaSimple.Text = "0";
        }

        private void btnModaSimple_Click(object sender, RoutedEventArgs e)
        {
            if (!listaSimple.EstaVacio())
                txtModaSimple.Text = listaSimple.EncontrarModa().ToString();
            else
                txtModaSimple.Text = "Sin datos";
        }

        private void btnOrdenarDescSimple_Click(object sender, RoutedEventArgs e)
        {
            listaSimple.OrdenarDescendente();
            ActualizarDataGrid_Simple();
        }

        private void ActualizarDataGrid_Simple()
        {
            dgLista.ItemsSource = null;
            dgLista.ItemsSource = listaSimple.ObtenerElementos();
        }
        #endregion

        #region LISTA DOBLEMENTE ENLAZADA - EVENTOS
        private void btnAgregarInicioDoble_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtDoble.Text, out int valor))
            {
                listaDoble.AgregarAlInicio(valor);
                txtDoble.Clear();
                ActualizarDataGrid_Doble();
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un número válido (Lista Doble).");
            }
        }

        private void btnAgregarFinalDoble_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtDoble.Text, out int valor))
            {
                listaDoble.AgregarAlFinal(valor);
                txtDoble.Clear();
                ActualizarDataGrid_Doble();
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un número válido (Lista Doble).");
            }
        }

        private void btnEliminarInicioDoble_Click(object sender, RoutedEventArgs e)
        {
            listaDoble.EliminarInicio();
            ActualizarDataGrid_Doble();
        }

        private void btnEliminarFinalDoble_Click(object sender, RoutedEventArgs e)
        {
            listaDoble.EliminarFinal();
            ActualizarDataGrid_Doble();
        }

        private void btnEstaVacioDoble_Click(object sender, RoutedEventArgs e)
        {
            txtEstaVacioDoble.Text = listaDoble.EstaVacio() ? "Sí" : "No";
        }

        private void btnMayorDoble_Click(object sender, RoutedEventArgs e)
        {
            if (!listaDoble.EstaVacio())
                txtMayorDoble.Text = listaDoble.EncontrarMayor().ToString();
            else
                txtMayorDoble.Text = "Sin datos";
        }

        private void btnSumarDoble_Click(object sender, RoutedEventArgs e)
        {
            if (!listaDoble.EstaVacio())
                txtSumaDoble.Text = listaDoble.SumarElementos().ToString();
            else
                txtSumaDoble.Text = "0";
        }

        private void btnModaDoble_Click(object sender, RoutedEventArgs e)
        {
            if (!listaDoble.EstaVacio())
                txtModaDoble.Text = listaDoble.EncontrarModa().ToString();
            else
                txtModaDoble.Text = "Sin datos";
        }

        private void btnOrdenarDescDoble_Click(object sender, RoutedEventArgs e)
        {
            listaDoble.OrdenarDescendente();
            ActualizarDataGrid_Doble();
        }

        private void ActualizarDataGrid_Doble()
        {
            // Obtenemos todos los datos de la lista doble y los mostramos
            dgLista.ItemsSource = null;
            dgLista.ItemsSource = listaDoble.ObtenerElementos();
        }
        #endregion
        public class Nodo
        {
            public int Valor { get; set; }
            public Nodo Siguiente { get; set; }
            public Nodo(int valor)
            {
                Valor = valor;
                Siguiente = null;
            }
        }

        /// <summary>
        /// Lista Enlazada Simple
        /// </summary>
        public class ListaEnlazada
        {
            public Nodo Cabeza;

            public ListaEnlazada()
            {
                Cabeza = null;
            }

            public bool EstaVacio() => (Cabeza == null);

            public void AgregarAlInicio(int valor)
            {
                Nodo nuevo = new Nodo(valor);
                nuevo.Siguiente = Cabeza;
                Cabeza = nuevo;
            }

            public void AgregarAlFinal(int valor)
            {
                Nodo nuevo = new Nodo(valor);
                if (EstaVacio())
                {
                    Cabeza = nuevo;
                }
                else
                {
                    Nodo temp = Cabeza;
                    while (temp.Siguiente != null)
                    {
                        temp = temp.Siguiente;
                    }
                    temp.Siguiente = nuevo;
                }
            }

            public void EliminarInicio()
            {
                if (!EstaVacio())
                {
                    Cabeza = Cabeza.Siguiente;
                }
            }

            public void EliminarFinal()
            {
                if (!EstaVacio())
                {
                    if (Cabeza.Siguiente == null)
                    {
                        // Solo hay un elemento
                        Cabeza = null;
                    }
                    else
                    {
                        Nodo temp = Cabeza;
                        while (temp.Siguiente.Siguiente != null)
                        {
                            temp = temp.Siguiente;
                        }
                        temp.Siguiente = null;
                    }
                }
            }

            public int EncontrarMayor()
            {
                if (EstaVacio())
                    throw new System.InvalidOperationException("La lista está vacía.");

                int mayor = Cabeza.Valor;
                Nodo actual = Cabeza.Siguiente;
                while (actual != null)
                {
                    if (actual.Valor > mayor)
                        mayor = actual.Valor;
                    actual = actual.Siguiente;
                }
                return mayor;
            }

            public int SumarElementos()
            {
                int suma = 0;
                Nodo actual = Cabeza;
                while (actual != null)
                {
                    suma += actual.Valor;
                    actual = actual.Siguiente;
                }
                return suma;
            }

            public int EncontrarModa()
            {
                if (EstaVacio())
                    throw new System.InvalidOperationException("La lista está vacía.");

                var frecuencia = new System.Collections.Generic.Dictionary<int, int>();
                Nodo actual = Cabeza;
                while (actual != null)
                {
                    if (!frecuencia.ContainsKey(actual.Valor))
                        frecuencia[actual.Valor] = 0;
                    frecuencia[actual.Valor]++;
                    actual = actual.Siguiente;
                }

                // Obtener la clave con mayor frecuencia
                return frecuencia.OrderByDescending(x => x.Value).First().Key;
            }

            public void OrdenarDescendente()
            {
                if (Cabeza == null || Cabeza.Siguiente == null) return;

                var elementos = ObtenerElementos();
                elementos.Sort();        // Orden Asc
                elementos.Reverse();     // Ahora Desc

                // Reconstruir la lista
                Cabeza = null;
                foreach (var val in elementos)
                {
                    AgregarAlFinal(val);
                }
            }

            public System.Collections.Generic.List<int> ObtenerElementos()
            {
                var resultado = new System.Collections.Generic.List<int>();
                Nodo actual = Cabeza;
                while (actual != null)
                {
                    resultado.Add(actual.Valor);
                    actual = actual.Siguiente;
                }
                return resultado;
            }
        }


        /// <summary>
        /// Nodo para la lista doblemente enlazada
        /// </summary>
        public class NodoDoble
        {
            public int Valor { get; set; }
            public NodoDoble Siguiente { get; set; }
            public NodoDoble Anterior { get; set; }

            public NodoDoble(int valor)
            {
                Valor = valor;
                Siguiente = null;
                Anterior = null;
            }
        }

        /// <summary>
        /// Lista Doblemente Enlazada
        /// </summary>
        public class ListaDobleEnlazada
        {
            public NodoDoble Cabeza;
            public NodoDoble Cola;

            public ListaDobleEnlazada()
            {
                Cabeza = null;
                Cola = null;
            }

            public bool EstaVacio() => (Cabeza == null);

            public void AgregarAlInicio(int valor)
            {
                NodoDoble nuevo = new NodoDoble(valor);
                if (EstaVacio())
                {
                    Cabeza = nuevo;
                    Cola = nuevo;
                }
                else
                {
                    nuevo.Siguiente = Cabeza;
                    Cabeza.Anterior = nuevo;
                    Cabeza = nuevo;
                }
            }

            public void AgregarAlFinal(int valor)
            {
                NodoDoble nuevo = new NodoDoble(valor);
                if (EstaVacio())
                {
                    Cabeza = nuevo;
                    Cola = nuevo;
                }
                else
                {
                    Cola.Siguiente = nuevo;
                    nuevo.Anterior = Cola;
                    Cola = nuevo;
                }
            }

            public void EliminarInicio()
            {
                if (!EstaVacio())
                {
                    if (Cabeza == Cola)
                    {
                        // Solo un elemento
                        Cabeza = null;
                        Cola = null;
                    }
                    else
                    {
                        Cabeza = Cabeza.Siguiente;
                        Cabeza.Anterior = null;
                    }
                }
            }

            public void EliminarFinal()
            {
                if (!EstaVacio())
                {
                    if (Cabeza == Cola)
                    {
                        // Solo un elemento
                        Cabeza = null;
                        Cola = null;
                    }
                    else
                    {
                        Cola = Cola.Anterior;
                        Cola.Siguiente = null;
                    }
                }
            }

            public int EncontrarMayor()
            {
                if (EstaVacio())
                    throw new System.InvalidOperationException("La lista doble está vacía.");

                int mayor = Cabeza.Valor;
                NodoDoble actual = Cabeza.Siguiente;
                while (actual != null)
                {
                    if (actual.Valor > mayor)
                        mayor = actual.Valor;
                    actual = actual.Siguiente;
                }
                return mayor;
            }

            public int SumarElementos()
            {
                int suma = 0;
                NodoDoble actual = Cabeza;
                while (actual != null)
                {
                    suma += actual.Valor;
                    actual = actual.Siguiente;
                }
                return suma;
            }

            public int EncontrarModa()
            {
                if (EstaVacio())
                    throw new System.InvalidOperationException("La lista doble está vacía.");

                var frecuencia = new System.Collections.Generic.Dictionary<int, int>();
                NodoDoble actual = Cabeza;
                while (actual != null)
                {
                    if (!frecuencia.ContainsKey(actual.Valor))
                        frecuencia[actual.Valor] = 0;
                    frecuencia[actual.Valor]++;
                    actual = actual.Siguiente;
                }
                return frecuencia.OrderByDescending(x => x.Value).First().Key;
            }

            public void OrdenarDescendente()
            {
                if (Cabeza == null || Cabeza.Siguiente == null) return;
                var elementos = ObtenerElementos();
                elementos.Sort();
                elementos.Reverse();
                Cabeza = null;
                Cola = null;
                foreach (int val in elementos)
                {
                    AgregarAlFinal(val);
                }
            }
            public System.Collections.Generic.List<int> ObtenerElementos()
            {
                var resultado = new System.Collections.Generic.List<int>();
                NodoDoble actual = Cabeza;
                while (actual != null)
                {
                    resultado.Add(actual.Valor);
                    actual = actual.Siguiente;
                }
                return resultado;
            }
        }
    }
}
