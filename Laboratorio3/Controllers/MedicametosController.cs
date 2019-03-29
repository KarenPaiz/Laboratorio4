﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;



namespace Laboratorio3.Controllers
{
    public class MedicametosController : Controller
    {
        public static ArbolBinario.ArbolBinario ArbolMedicamentos = new ArbolBinario.ArbolBinario();
        public static ArbolBinario.Medicamento[] mostrar;
        public static List<ArbolBinario.Medicamento> medicamentos = new List<ArbolBinario.Medicamento>();
        public static int a = 0;
        public static int TAMANODELARBOL;
        // GET: Medicametos
        public void leerArchivo()
        {

            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/MEDICAMENTOS.csv";
            ArbolBinario.Medicamento aux;
            using (StreamReader sr = System.IO.File.OpenText(Path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    if (s != "")
                    {
                        aux = new ArbolBinario.Medicamento();
                        aux.Id = Convert.ToInt32(s.Split('|')[0]);
                        aux.Nombre = s.Split('|')[1];
                        aux.descripcion = s.Split('|')[2];
                        aux.Productora = s.Split('|')[3];
                        aux.Precio = Convert.ToDouble(s.Split('|')[4]);
                        aux.Cantidad = Convert.ToInt32(s.Split('|')[5]);
                        ArbolMedicamentos.AgregarElemento(aux);
                        medicamentos.Add(aux);
                    }
                }


            }
        }      //YA
        public ActionResult Index()
        {
            return View();
        }   //YA
        public void GuardarJson(string A)
        {
            string path;
            string pathJ;
            path = @"C:\LAB3\" + A + ".txt";
            pathJ = @"C:/LAB3/" + A + ".txt";
            string root = @"C:\LAB3";

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            if (!System.IO.File.Exists(path))
            {
                using (FileStream strm = System.IO.File.Create(path))
                using (StreamWriter sw = new StreamWriter(strm))
                {
                    sw.WriteLine("No_Info");
                    sw.Close();
                }
            }
            using (StreamWriter file = System.IO.File.CreateText(@"C:\LAB3\" + A + ".txt"))
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.Serialize(file, mostrar);
            }
        }         //YA

        
        public ActionResult MostrarIn()
        {
            if (ArbolMedicamentos.Raiz == null)
            {
                ingresoTamano();
                leerArchivo();
            }

            if (a == 0)
                a = medicamentos.Count;

            mostrar = ArbolMedicamentos.Mostrar(3, a);
            ViewBag.Matriz = mostrar;
            GuardarJson("MEDICAMENTOS InOrden");
            return View();
        }      //YA
        

        public ArbolBinario.Medicamento v1;
        public ActionResult NuevoMedicamento()
        {
            if (ArbolMedicamentos.Raiz == null)
            {
                ingresoTamano();
                leerArchivo();
            }

            return View();
        }          //YA
        public ActionResult Ingresa(string Nombre, int Id, string Descripcion, string Productora, double Precio)
        {
            if (ArbolMedicamentos.Raiz == null)
            {
                ingresoTamano();
                leerArchivo();
            }

            Random num = new Random();
            int Cantidad = num.Next(0, 15);
            v1 = new ArbolBinario.Medicamento { Nombre = Nombre, Id = Id, Precio = Precio, Cantidad = Cantidad, descripcion = Descripcion, Productora = Productora };
            ArbolMedicamentos.AgregarElemento(v1);
            a++;
            return View(v1);
        }         //YA


        public static ArbolBinario.Medicamento BuscarC;
        public ActionResult BuscarNombre()
        {
            if (ArbolMedicamentos.Raiz == null)
            {
                ingresoTamano();
                leerArchivo();
            }
            if (a == 0)
                a = medicamentos.Count;

            mostrar = ArbolMedicamentos.Mostrar(3, a);
            BuscarC = new ArbolBinario.Medicamento();
            var visi = mostrar;
            foreach (var item in visi)
            {
                if (item != null && (Request.Form["Nombre"]) == item.Nombre)
                {
                    BuscarC = new ArbolBinario.Medicamento { Id = item.Id, Nombre = item.Nombre, Cantidad = item.Cantidad, Precio = item.Precio, descripcion = item.descripcion, Productora = item.Productora};
                    ViewBag.Mostrar = BuscarC;
                }
            }
            return View();
        }      //YA
        public ActionResult BuscarNombre2()
        {
            if (ArbolMedicamentos.Raiz == null)
            {
                ingresoTamano();
                leerArchivo();
            }

            return View();
        }     //YA

        public ActionResult Pedir()
        {
            if (ArbolMedicamentos.Raiz == null)
            {
                ingresoTamano();
                leerArchivo();
            }

            ViewBag.Pedir = BuscarC;
            return View();
        }
        public ActionResult RealizarPedido(string Nombre, string Direccion, int Nit, int Cantidad)
        {
            if (ArbolMedicamentos.Raiz == null)
            {
                ingresoTamano();
                leerArchivo();
            }

            if (Cantidad == BuscarC.Cantidad)
            {
                ArbolMedicamentos.EliminarElemento(BuscarC);
                medicamentos.Remove( BuscarC );
            }
            else
            {
                BuscarC.Cantidad = BuscarC.Cantidad - Cantidad;
                ArbolMedicamentos.ActualizaDatos(BuscarC);
            }

            return View();
        }

        public ActionResult ingresoTamano()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ingresa2(int tamano)
        {
            TAMANODELARBOL = tamano;
            return View();
        }

    }
}