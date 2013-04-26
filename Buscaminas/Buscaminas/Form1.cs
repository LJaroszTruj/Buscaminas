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

namespace Buscaminas{

    public partial class Form1 : Form{

        //declaro el array de botones
        Button[,] matrizBotones;
        int filas = 15;
        int columnas = 20;
        int anchoBoton = 20;
        int minas = 40;




        // si el tag es 1 es que no hay bomba
        // si el tag es 2 es que sí hay bomba
        public Form1(){

            InitializeComponent();

            this.Height = filas * anchoBoton + 40;
            this.Width = columnas * anchoBoton + 20;

            matrizBotones = new Button[columnas, filas];
            //inicializo la matriz de botones
            for (int i = 0; i < filas; i++)
                for (int j = 0; j < columnas; j++){

                    Button boton = new Button();
                    //boton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    boton.Width = anchoBoton;
                    boton.Height = anchoBoton;
                    boton.Location = new Point(j * anchoBoton, i * anchoBoton);
                    boton.Click += chequeaBoton;
                    boton.Tag = "0";
                    matrizBotones[j, i] = boton;
                    panel1.Controls.Add(boton);
                }
            //Genera las minas y las coloca en el mapa de forma aleatoria
            poneMinas();
            //Según las minas que se tengan al lado, los botones se actualizan
            cuentaMinas();
        }

        private void cuentaMinas(){
            //Cambio los tag para que indiquen el nº de minas que hay alrededor
            //los dos for anidados externos recorren uno por uno los elementos de la matriz
            //los dos for anidados interiores recorren los 8 botones alrededor de una casilla 
            //y suman el número de minas
            for (int i = 0; i < filas; i++)
                for (int j = 0; j < columnas; j++){
                    int numeroBombas = 0;

                    for (int k = -1; k < 2; k++)
                        for (int l = -1; l < 2; l++){
                            int f = i + k;
                            int c = j + l;

                            if ((c < columnas) && (c >= 0) && (f < filas) && (f >= 0)){
                                if (matrizBotones[c, f].Tag == "B"){
                                    numeroBombas++;
                                }
                            }
                        }
                    if ((numeroBombas > 0) && (matrizBotones[j, i].Tag != "B")){

                        matrizBotones[j, i].Tag = numeroBombas;
                        matrizBotones[j, i].Text = numeroBombas.ToString();
                    }
                }
        }

        private void poneMinas(){

            Random aleatorio = new Random();
            int x = 0, y = 0;
            for (int i = 0; i < minas; i++){

                x = aleatorio.Next(filas);
                y = aleatorio.Next(columnas);

                while (!matrizBotones[y, x].Tag.Equals("0")){
                    x = aleatorio.Next(filas);
                    y = aleatorio.Next(columnas);
                }
                matrizBotones[y, x].Tag = "B";
                matrizBotones[y, x].Text = "B";
                matrizBotones[y, x].BackColor = Color.Sienna;
            }

        } 


        private void chequeaBoton(object sender, EventArgs e){

            //Chequea botón mira en las direcciones alrededor del botón pulsado
            //(sender as Button).Enabled = false;
            Button b = (sender as Button);
            int columna = b.Location.X / anchoBoton;
            int fila = b.Location.Y / anchoBoton;
            int numeroMinasAlrededor = 0;

            //Si el Tag es 0 es porque no hay ninguna mina alrededor. 
            //si fuera distinto de cero, no tengo que chequear nada mas
            if (matrizBotones[columna, fila].Tag == "0"){

                b.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                
                for (int i = -1; i < 2; i++){
                    for (int j = -1; j < 2; j++){

                        int f = fila + i;
                        int c = columna + j;

                        if ((c < columnas) && (c >= 0) && (f < filas) && (f >= 0)){

                            if (matrizBotones[c, f].FlatStyle != System.Windows.Forms.FlatStyle.Flat){

                                if (numeroMinasAlrededor == 0){
                                    chequeaBoton(matrizBotones[c, f], e);
                                }
                                else{
                                    numeroMinasAlrededor++;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}