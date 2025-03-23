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
    /// Lógica de interacción para Calculadora.xaml
    /// </summary>
    public partial class Calculadora : UserControl
    {
        private PostFija calculadora;
        public Calculadora()
        {
            InitializeComponent();
            calculadora = new PostFija();
            AsignarEventos();
        }

        private void AsignarEventos()
        {
            foreach (UIElement elemento in GridCalculadora.Children)
            {
                if (elemento is Button boton)
                {
                    boton.Click += Boton_Click;
                }
            }
        }

        private void Boton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button boton)
            {
                string contenido = boton.Content.ToString();

                if (contenido == "=")
                {
                    EvaluarExpresion();
                }
                else if (contenido == "Borrar")
                {
                    BorrarUltimo();
                }
                else if (contenido == "Limpiar")
                {
                    ColaInput.Text = "";
                }
                else
                {
                    ColaInput.Text += contenido;
                }
            }
        }

        private void EvaluarExpresion()
        {
            try
            {
                string expresionPostfija = ColaInput.Text;

                if (string.IsNullOrWhiteSpace(expresionPostfija) || !expresionPostfija.All(c => Char.IsDigit(c) || "+-*/()".Contains(c)))
                {
                    throw new Exception("La expresión contiene caracteres no válidos.");
                }

                int resultado = calculadora.EvaluarExpresionPostFija(expresionPostfija, out expresionPostfija);
                ColaInput.Text = resultado.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la expresión: " + ex.Message);
            }
        }


        private void BorrarUltimo()
        {
            if (!string.IsNullOrEmpty(ColaInput.Text))
            {
                ColaInput.Text = ColaInput.Text.Substring(0, ColaInput.Text.Length - 1);
            }
        }
    }
}
