using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploArbol
{
    public class Medicamento
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string casaProductora { get; set; }
        public string descripcion { get; set; }
        public int cantidad { get; set; }
        public double precio { get; set; }
    }
    public class BNodo
    {
        public int nLlaes { get; set; }
        int grado;
        public Medicamento[] valores { get; set; }
        public int llavesDisponibles { get; set; }
        public BNodo padre { get; set; }
        public bool esHoja { get; set; }
        public BNodo[] subarboles { get; set; }
        public BNodo() { }
        public BNodo(int grado1)
        {
            this.grado = grado1;
            valores = new Medicamento[grado - 1];
            subarboles = new BNodo[grado];
        }
        bool existemedicamento = false;
        public bool nodoEstaVacio()
        {
            foreach(Medicamento medActual in valores)
            {
                if (medActual != null)
                {
                    existemedicamento = true;
                    break;
                }
            }
            return !existemedicamento; //Si regresa true es porque está vacio
        }
        bool ExisteEspacio = false;
        public bool existeEspacioEnNodo()
        {
            foreach (Medicamento medActual in valores)
            {
                if (medActual == null)
                {
                    ExisteEspacio = true;
                    break;
                }
            }
            return ExisteEspacio; // Si regresa null es que hay espacio en el nodo
        }
        public void insertarEnNodo(Medicamento nuevoValor)
        {
            if (this.nodoEstaVacio())
            {
                valores[0] = nuevoValor;
            }
            else if (existeEspacioEnNodo())
            {
                
                
                    //HCER UN CICLO PARA ENCONTRAR UN ESPACIO VACIO
                    //GUARDAR EL VALOR CUALDO ENCUENTRE EL NULO
                    //METODO ORDENAR 
                    var valoresTemp = valores.ToList();
                    valoresTemp.Sort(compararNombreMedicamentos);
                
            } 
            else 
            {
                Medicamento[] auxiliar = new Medicamento[grado];
                for(int i =0; i<grado-1;i++)
                {
                    auxiliar[i] = valores[i];
                }
                auxiliar[grado - 1] = nuevoValor;
                var auxiliarLista = auxiliar.ToList();
                auxiliarLista.Sort(compararNombreMedicamentos);
                auxiliar = auxiliarLista.ToArray();
            }

        }
        public Medicamento dividirNodo(Medicamento [] auxiliar )
        {
            //obtener el valor medio del areglo
            //recibe el arreglo auxiliar
            //1. crear un nuevo nodo solo con el valor medio
            //2. crear nodo con valores menores al medio
            //3. crear nodo con valores myores al medio
            //4. agregar a los subnodos que podemos tener 
            return auxiliar[0]; ///SOLO LO PUSE PARA QUE NO ME MARQUE ERROR! BORRAR!
        }
        public int compararNombreMedicamentos(Medicamento m1, Medicamento m2)
        {

            return m1.nombre.CompareTo(m2.nombre);// 
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
