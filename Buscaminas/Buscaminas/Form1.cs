using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 *  NOMBRE: Luis 
 *  APELLIDOS: Jarosz Trujillo
 */

namespace Buscaminas
{
    public partial class Form1 : Form
    {
        //declaro el array de botones
        Button[,] matrizBotones;
        int filas = 20;
        int columnas = 20;
        int minas = 20;
        int anchoBoton = 20;


        //Si el Tag es 1 es que no hay bomba
        //Si el Tag es 2 es que hay bomba

        public Form1()
        {
            InitializeComponent();
            

            matrizBotones = new Button[filas, columnas];

            this.Height = columnas * anchoBoton + 40;
            this.Width = filas * anchoBoton + 20;
            
            for (int i = 0; i < filas; i++){
                for (int j = 0; j < columnas; j++){
                    Button boton = new Button();
                    //boton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    boton.Width = anchoBoton;
                    boton.Height = anchoBoton;
                    boton.Location = new Point(i * anchoBoton, j * anchoBoton);
                    boton.Click += chequeaBoton;
                    boton.Tag = "1";
                    matrizBotones[j, i] = boton;
                    panel1.Controls.Add(boton);

                    }
                poneMinas();
                }
        }

        private void poneMinas(){
            Random aleatorio = new Random();
            int x = 0, y = 0;
            for (int i = 0; i < minas; i++){
                x = aleatorio.Next(filas);
                y = aleatorio.Next(columnas);

                    while(!matrizBotones[y, x].Tag.Equals("1")) {
                        x = aleatorio.Next(filas);
                        y = aleatorio.Next(columnas);              
                    }
                    matrizBotones[y, x].Tag = "2";
                    matrizBotones[y, x].Text = "B";
            }
        }

        private void chequeaBoton(object sender, EventArgs e){
            Button b = (sender as Button);
            int columna = b.Location.X / anchoBoton;
            int fila = b.Location.Y / anchoBoton;
            //De esta manera sólo se consigen botones de 3x3
            
            for (int i = -1; i < 2; i++){
              for (int j = -1; j < 2; j++){
                  if ((columna + j < columnas) &&(columna + j >= 0)
                     &&(fila + i < filas) && (fila + i >= 0)){
                         if (matrizBotones[columna + j, fila + i].BackColor != Color.Tomato){

                             matrizBotones[columna + j, fila + i].BackColor = Color.Tomato;
                             chequeaBoton(matrizBotones[columna + j, fila + i], e);
                       }
                   }
                }
            }
        }
    }
}
