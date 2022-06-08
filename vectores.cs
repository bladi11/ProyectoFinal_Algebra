using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_Algebra
{
    internal class vectores
    {
        public string cambio { get; set; }
        List<double> vector1 = new List<double>();
        List<double> vector2 = new List<double>();
        List<double> vector3 = new List<double>();
        List<double> identidad1 = new List<double>();
        List<double> identidad2 = new List<double>();
        List<double> identidad3 = new List<double> ();

        public static List<vectores> listaCambios = new List<vectores>();
        public List<double> Vector1 { get => vector1; set => vector1 = value; }
        public List<double> Vector2 { get => vector2; set => vector2 = value; }
        public List<double> Vector3 { get => vector3; set => vector3 = value; }
        public List<double> Identidad1 { get => identidad1; set => identidad1 = value; }
        public List<double> Identidad2 { get => identidad2; set => identidad2 = value; }
        public List<double> Identidad3 { get => identidad3; set => identidad3 = value; }

        public static void guardarenJson(string archivo)
        {
            string json = JsonConvert.SerializeObject(listaCambios);
            System.IO.File.WriteAllText(archivo, json);
        }
        public static void leerJson(string archivo)
        {
            StreamReader jsonStream = File.OpenText(archivo);
            string json = jsonStream.ReadToEnd();
            jsonStream.Close();
            listaCambios = JsonConvert.DeserializeObject<List<vectores>>(json);
        }
    }
}
