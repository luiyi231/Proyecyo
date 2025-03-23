using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoIIIç
{
    internal class PostFija
    {
        public List<string> operadores = new List<string> { "*", "/", "+", "-" };
        public Pila pila = new Pila(15);

        public int calcularOperacion(int x, int y, string operador)
        {
            switch (operador)
            {
                case "+": return x + y;
                case "-": return x - y;
                case "*": return x * y;
                case "/": return x / y;
                default: throw new InvalidOperationException("Operador desconocido: " + operador);
            }
        }

        public List<string> StringToList(string expresion)
        {
            var listaExpresion = new List<string>();
            int indiceListaExpresion = 0;
            listaExpresion.Add(expresion[0].ToString());
            for (int i = 1; i < expresion.Length; i++)
            {
                var valor = expresion[i];
                if (char.IsDigit(valor) && operadores.Concat(new string[2] { "(", ")" }).Contains(listaExpresion.ElementAt(indiceListaExpresion)))
                {
                    listaExpresion.Add(valor.ToString());
                    indiceListaExpresion++;
                }
                else if (operadores.Concat(new string[2] { "(", ")" }).Contains(valor.ToString()))
                {
                    listaExpresion.Add(valor.ToString());
                    indiceListaExpresion++;
                }
                else
                {
                    var numero = listaExpresion.ElementAt(indiceListaExpresion) + valor;
                    listaExpresion.RemoveAt(indiceListaExpresion);
                    listaExpresion.Add(numero);
                }
            }
            return listaExpresion;
        }

        public int PrecedenciaOperador(string operador)
        {
            if (operador == "*" || operador == "/") return 1;
            if (operador == "+" || operador == "-") return 2;
            return 0;
        }

        public string InFijaToPostFija(string expresionInfija)
        {
            string exprecionPostFija = "";
            int numero = 0;
            expresionInfija = expresionInfija.Replace(" ", "");
            var listaExpresionInfija = StringToList(expresionInfija);
            pila.push("(");
            listaExpresionInfija.Add(")");

            foreach (var x in listaExpresionInfija)
            {
                if (Int32.TryParse(x, out numero))
                    exprecionPostFija += x + " ";
                else if (x == "(")
                    pila.push(x);
                else if (operadores.Contains(x))
                {
                    var operadorPila = pila.TopElement();
                    int precedenciaOperadorPila = PrecedenciaOperador(operadorPila);
                    int precedenciaOperadorExpresion = PrecedenciaOperador(x);
                    while (precedenciaOperadorPila <= precedenciaOperadorExpresion && operadores.Contains(operadorPila))
                    {
                        exprecionPostFija += pila.Pop() + " ";
                        operadorPila = pila.TopElement();
                        precedenciaOperadorPila = PrecedenciaOperador(operadorPila);
                    }
                    pila.push(x);
                }
                else if (x == ")")
                {
                    var top = pila.TopElement();
                    while (top != "(")
                    {
                        exprecionPostFija += pila.Pop() + " ";
                        top = pila.TopElement();
                    }
                    pila.Pop();
                }
            }
            return exprecionPostFija.Trim();
        }

        public int EvaluarExpresionPostFija(string expresionInfija, out string expresionPostFija)
        {
            expresionPostFija = InFijaToPostFija(expresionInfija);
            var listaPostFija = expresionPostFija.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            int numero = 0;

            foreach (var x in listaPostFija)
            {
                if (Int32.TryParse(x, out numero))
                {
                    pila.push(x);
                }
                else if (operadores.Contains(x))
                {
                    int op2 = Convert.ToInt32(pila.Pop());
                    int op1 = Convert.ToInt32(pila.Pop());
                    pila.push(calcularOperacion(op1, op2, x).ToString());
                }
            }
            return Convert.ToInt32(pila.Pop());
        }
    }
}
