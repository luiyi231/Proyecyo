using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoIIIç
{
    internal class Pila
    {
        private class Node
        {
            public string data;
            public Node Link;
        }
        private Node top;
        private int maxsize;
        private int currentsize;

        private Node Top { get => top; set => top = value; }
        public int Maxsize { get => maxsize; set => maxsize = value; }
        public int Currentsize { get => currentsize; set => currentsize = value; }

        public Pila(int max)
        {
            this.Top = null;
            this.Maxsize = max;
            this.Currentsize = 0;
        }
        public void push(string x)
        {
            if (currentsize < maxsize)
            {
                Node temp = new Node();
                if (temp == null)
                {
                    return;
                }
                temp.data = x;
                temp.Link = top;
                top = temp;
                currentsize++;
            }
            else
            {
                return;
            }
        }
        public string Pop()
        {
            if (top == null)
            {
                Console.WriteLine("Stack Underflow");
                throw new InvalidOperationException("La pila está vacía.");
            }

            string valor = TopElement();
            top = top.Link;
            currentsize--;

            return valor;
        }


        public bool IsEmpty()
        {
            return top == null;
        }
        public bool IsFull()
        {
            if (currentsize > maxsize)
                return false;
            else
                return true;
        }
        public string TopElement()
        {
            if (!IsEmpty())
            {
                return top.data.ToString();
            }
            else
            {
                return "Pila Vacia";
            }
        }
        public void clearPila()
        {
            top = null;
            currentsize = 0;
        }
        public int MaxSize()
        {
            return maxsize;
        }
    }
}
