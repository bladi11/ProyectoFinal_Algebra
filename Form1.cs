using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Grpc.Core;

namespace ProyectoFinal_Algebra
{
    public partial class Form1 : Form
    {
        string archivo = "Datos.json";
        vectores vector = new vectores();
        string texto;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            limpiar();
            CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vectores.listaCambios.Clear();
            vector = new vectores();
            vector.Vector1 = new List<double>() { Convert.ToDouble(textBoxingreso1.Text), Convert.ToDouble(textBoxIngreso2.Text), Convert.ToDouble(textBoxIngreso3.Text) };
            vector.Vector2 = new List<double>() { Convert.ToDouble(textBoxIngreso4.Text), Convert.ToDouble(textBoxIngreso5.Text), Convert.ToDouble(textBoxIngreso6.Text) };
            vector.Vector3 = new List<double>() { Convert.ToDouble(textBoxIngreso7.Text), Convert.ToDouble(textBoxIngreso8.Text), Convert.ToDouble(textBoxIngreso9.Text) };
            vector.Identidad1 = new List<double> { 1, 0, 0 };
            vector.Identidad2 = new List<double> { 0, 1, 0 };
            vector.Identidad3 = new List<double> { 0, 0, 1 };


            texto = "R1 x [ 1/" + vector.Vector1[0].ToString() + " ]";
            division(vector.Vector1, vector.Vector1[0], vector.Identidad1);
            cambios();

            texto = "R2 - [ " + vector.Vector2[0].ToString() + "R1 ]";
            RestarRenglones(vector.Vector2, vector.Vector1, vector.Vector2[0], vector.Identidad2, vector.Identidad1);
            cambios();

            texto = "R3 - [ " + vector.Vector3[0].ToString() + "R1 ]";
            RestarRenglones(vector.Vector3, vector.Vector1, vector.Vector3[0], vector.Identidad3, vector.Identidad1);
            cambios();

            texto = "R2 x [ 1/" + vector.Vector2[1].ToString() + " ]"; 
            division(vector.Vector2, vector.Vector2[1], vector.Identidad2);
            cambios();

            texto = "R3 - [ " + vector.Vector3[1].ToString() + "R2 ]";
            RestarRenglones(vector.Vector3, vector.Vector2, vector.Vector3[1], vector.Identidad3, vector.Identidad2);
            cambios();

            texto = "R3 x [ 1/" + vector.Vector3[2].ToString() + " ]";
            division(vector.Vector3, vector.Vector3[2], vector.Identidad3);
            cambios();

            texto = "R2 - [ " + vector.Vector2[2].ToString() + "R3 ]";
            RestarRenglones(vector.Vector2, vector.Vector3, vector.Vector2[2], vector.Identidad2, vector.Identidad3);
            cambios();

            texto = "R1 - [ " + vector.Vector1[2].ToString() + "R3 ]";
            RestarRenglones(vector.Vector1, vector.Vector3, vector.Vector1[2], vector.Identidad1, vector.Identidad3);
            cambios();

            texto = "R1 - [ " + vector.Vector1[1].ToString() + "R2 ]";
            RestarRenglones(vector.Vector1, vector.Vector2, vector.Vector1[1], vector.Identidad1, vector.Identidad2);
            cambios();


            

            txtReduccion(vector.Vector1, vector.Vector2, vector.Vector3);
            txtIdentidad(vector.Identidad1, vector.Identidad2, vector.Identidad3);

            vectores.guardarenJson(archivo);

            comboBox1.Items.Clear();
            foreach (var x in vectores.listaCambios)
            {
                comboBox1.Items.Add(x.cambio);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            vectores cambio = vectores.listaCambios.Find(x => x.cambio == comboBox1.SelectedItem.ToString());
            txtReduccion(cambio.Vector1, cambio.Vector2, cambio.Vector3);
            txtIdentidad(cambio.Identidad1, cambio.Identidad2, cambio.Identidad3);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        public void txtReduccion (List<double>vector1, List<double> vector2, List<double> vector3)
        {
            textBoxReduccion1.Text = vector1[0].ToString();
            textBoxReduccion2.Text = vector1[1].ToString();
            textBoxReduccion3.Text = vector1[2].ToString();
            textBoxReduccion4.Text = vector2[0].ToString();
            textBoxReduccion5.Text = vector2[1].ToString();
            textBoxReduccion6.Text = vector2[2].ToString();
            textBoxReduccion7.Text = vector3[0].ToString();
            textBoxReduccion8.Text = vector3[1].ToString();
            textBoxReduccion9.Text = vector3[2].ToString();
        }
        public void txtIdentidad(List<double> vector1, List<double> vector2, List<double> vector3)
        {
            textBoxIdentidad1.Text = vector1[0].ToString();
            textBoxIdentidad2.Text = vector1[1].ToString();
            textBoxIdentidad3.Text = vector1[2].ToString();
            textBoxIdentidad4.Text = vector2[0].ToString();
            textBoxIdentidad5.Text = vector2[1].ToString();
            textBoxIdentidad6.Text = vector2[2].ToString();
            textBoxIdentidad7.Text = vector3[0].ToString();
            textBoxIdentidad8.Text = vector3[1].ToString();
            textBoxIdentidad9.Text = vector3[2].ToString();
        }
        public void RestarRenglones(List<double>vectorAcambiar, List<double> vectorAux,double constante,List<double>Icambio,List<double>Iaux)
        {
            for(int x = 0; x<3; x++)
            {
                vectorAcambiar[x] = vectorAcambiar[x] - (constante * vectorAux[x]);
                Icambio[x] = Icambio[x] - (constante * Iaux[x]);
            }
        }
        public void division(List<double> vector, double constante, List<double> Identidad)
        {
            for (int x = 0; x < 3; x++)
            {
                vector[x] = vector[x] / constante;
                Identidad[x] = Identidad[x] / constante;
            }
        }
        public void cambios ()
        {
            for (int x = 0; x < 3; x++)
            {
                vector.Vector1[x] = Math.Round(vector.Vector1[x], 2);
                vector.Vector2[x] = Math.Round(vector.Vector2[x], 2);
                vector.Vector3[x] = Math.Round(vector.Vector3[x], 2);
                vector.Identidad1[x] = Math.Round(vector.Identidad1[x], 2);
                vector.Identidad2[x] = Math.Round(vector.Identidad2[x], 2);
                vector.Identidad3[x] = Math.Round(vector.Identidad3[x], 2);
            }
            vectores cambio = new vectores();
            cambio.cambio = texto;
            cambio.Vector1 = vector.Vector1.ToArray().ToList();
            cambio.Vector2 = vector.Vector2.ToArray().ToList();
            cambio.Vector3 = vector.Vector3.ToArray().ToList();
            cambio.Identidad1 = vector.Identidad1.ToArray().ToList();
            cambio.Identidad2 = vector.Identidad2.ToArray().ToList();
            cambio.Identidad3 = vector.Identidad3.ToArray().ToList();
            
            vectores.listaCambios.Add(cambio);
        }                
        private void limpiar()
        {
            List<double> I1 = new List<double> { 1, 0, 0 };
            List<double> I2 = new List<double> { 0, 1, 0 };
            List<double> I3 = new List<double> { 0, 0, 1 };
            List<double> ceros = new List<double> { 0, 0, 0 };
            txtReduccion(ceros, ceros, ceros);
            txtIdentidad(I1, I2, I3);

            textBoxingreso1.Clear();
            textBoxIngreso2.Clear();
            textBoxIngreso3.Clear();
            textBoxIngreso4.Clear();
            textBoxIngreso5.Clear();
            textBoxIngreso6.Clear();
            textBoxIngreso7.Clear();
            textBoxIngreso8.Clear();
            textBoxIngreso9.Clear();
            comboBox1.Text = "Operaciones Relizadas";
        }
    }
}
