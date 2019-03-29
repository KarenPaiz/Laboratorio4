using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolBinario
{
    
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
            valores = new Medicamento[grado];
            subarboles = new BNodo[grado + 1];
        }
        bool existemedicamento = false;
        public bool nodoEstaVacio()
        {
            foreach (Medicamento medActual in valores)
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
            for (int i = 0; i < grado - 1; i++)
            {
                if (valores[i] == null)
                {
                    ExisteEspacio = true;
                    break;
                }
            }
            return ExisteEspacio; // Si regresa null es que hay espacio en el nodo
        }
        public BNodo insertarEnNodo(Medicamento nuevoValor)
        {
            BNodo AUXILIARDERETORNO = new BNodo(grado);
            if (this.nodoEstaVacio())
            {
                valores[0] = nuevoValor;
            }
            else if (existeEspacioEnNodo())
            {
                int recorre = 0;
                while (valores[recorre] != null)
                {
                    recorre++;
                }
                valores[recorre] = nuevoValor;

                var valoresTemp = valores.ToList();
                valoresTemp.Sort(compararNombreMedicamentos);
                valores = valoresTemp.ToArray();
            }
            else
            {
                Medicamento[] auxiliar = new Medicamento[grado];
                for (int i = 0; i < grado - 1; i++)
                {
                    auxiliar[i] = valores[i];
                }
                auxiliar[grado - 1] = nuevoValor;
                var auxiliarLista = auxiliar.ToList();
                auxiliarLista.Sort(compararNombreMedicamentos);
                auxiliar = auxiliarLista.ToArray();
                valores = auxiliar;
                AUXILIARDERETORNO = dividirNodo();
            }
            return AUXILIARDERETORNO;
        }
        public BNodo dividirNodo()
        {
            int cantidadValorMedio = (grado - 1) / 2;
            Medicamento Medicamentoauxiliar = valores[cantidadValorMedio - 1];
            BNodo nuevoNodo = new BNodo(grado);
            int contador = 0;
            for (int i = cantidadValorMedio; i < grado - 1; i++)//PARA GUARDAR LOS MEDICAMENTOS EN EL NUEVO NODO
            {
                nuevoNodo.valores[contador] = valores[i];
                contador++;
            }
            contador = 0;
            for (int i = cantidadValorMedio; i < grado + 1; i++) // para borrar los subnodos que no le corresponden
            {
                nuevoNodo.subarboles[contador] = subarboles[i];
                contador++;
            }
            contador = 0;
            nuevoNodo.padre = padre; // iguala padres
            while (padre.subarboles[contador] != null)//agrega el nuevo nodo como subnodo del padre
            {
                contador++;
            }
            padre.subarboles[contador] = nuevoNodo;
            //arreglar el nodo actual con la otra mitad de los datos
            for (int i = cantidadValorMedio - 1; i < grado - 1; i++) // para borrar los medicamentos que no le corresponden
            {
                valores[1] = null;
            }
            for (int i = cantidadValorMedio; i < grado + 1; i++) // para borrar los subnodos que no le corresponden
            {
                subarboles[i] = null;
            }
            BNodo nuevoPadre = new BNodo(grado);
            if (padre.valores == null)//SI EL PADRE ES NULO, SE CREA UN NUEVO NODO CON EL MEDICAMENTO INTERMEDIO Y LOS DOS SUBNODOS CREADOS
            {
                nuevoPadre.valores[0] = Medicamentoauxiliar;
                padre = nuevoPadre;
            }
            else
            {
                padre.valores[grado] = Medicamentoauxiliar;
            }
            nuevoPadre.subarboles[1] = nuevoNodo;
            return padre;
        }
        public int compararNombreMedicamentos(Medicamento m1, Medicamento m2)
        {

            return m1.Nombre.CompareTo(m2.Nombre);
        }
    }
    public class ArbolB
    {
        public int grado;
        public ArbolB(int grado1)
        {
            grado = grado1;
        }
        public BNodo Raiz;
        public void insertar(Medicamento medicamento)
        {

            BNodo aux = new BNodo(grado);
            BNodo aux2 = new BNodo(grado);
            if (Raiz == null)
            {
                Raiz = new BNodo(grado);
            }
            if (Raiz.subarboles[0] == null)
            {
                Raiz.insertarEnNodo(medicamento);
            }
            else
            {
                BNodo[] recorrido = new BNodo[6];
                int cont = 0;
                BNodo auxRecorre = Raiz;
                bool bandera = false;
                recorrido[0] = Raiz;
                int conta2 = 0;
                while (auxRecorre.subarboles != null)
                {
                    bandera = false;
                    while (bandera == false)
                    {
                        int comparison = String.Compare(medicamento.Nombre, auxRecorre.valores[cont].Nombre, comparisonType: StringComparison.OrdinalIgnoreCase);

                        if (comparison > 1)
                        {
                            cont++;
                        }
                        else
                        {
                            conta2++;
                            recorrido[conta2] = auxRecorre.subarboles[cont];
                            auxRecorre = auxRecorre.subarboles[cont];
                            bandera = true;
                        }
                        if (cont == grado - 1)
                        {
                            conta2++;
                            recorrido[conta2] = auxRecorre.subarboles[cont];
                            auxRecorre = auxRecorre.subarboles[cont];
                            bandera = true;
                        }
                    }
                    cont = 0;
                }
                aux = auxRecorre;
                aux2 = aux.insertarEnNodo(medicamento);
                aux2.subarboles[0] = aux;
                for (int i = 0; i < grado; i++)
                {
                    aux2.subarboles[i].padre = aux2;
                } // metodo para que todos los padres de sus subarboles sean el por si acaso

                int contadordeNodos = 0;
                if (aux2.valores[1] == null) //si el nodo regresado es la nueva raiz
                {
                    Raiz = aux2;
                }
                else //metodo para ordenar todo
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (recorrido[i] != null)
                        {
                            contadordeNodos++;
                        }
                    }
                    recorrido[contadordeNodos - 1] = aux2;
                    for (int i = contadordeNodos - 1; i < 0; i--)
                    {
                        if (recorrido[i].valores[grado - 1] != null)
                        {
                            recorrido[i - 1] = recorrido[i].dividirNodo();
                        }
                    }
                    Raiz = recorrido[0];
                    // PASRA POR TTODO!!! EL RECORRIDO DE ABAJO HACIA ARRIBA Y SI EL NODO LLEGÓ HASTA EL AUXILIAR EN VALORES LLAMAR EL DIVIDIR NODO. HACERLO TODO Y LUEGO IGUALAR LA RAIZ
                }
            }

        }
    }
}
