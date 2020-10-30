using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graphics3DS;

namespace Cubo3D_Traslazion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Graphics g;
        Graphics3D g3;
        //Escalar
        int esc = 5;
        //Tralsadar
        int tras = 5;
        Point3DF[] puntos = new Point3DF[8];
        //Varaibles para determinar el angulo que voy a rotar el objeto cubo
        int ang_x = 0; //Grados a rotar en el eje X
        int ang_y = 0;//Grados a rotar en el eje Y
        //Punto determinar la posicion del mouse
        Point posMouse;
        //Variable para determinar si el objeto se mueve o no
        bool mover;
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g3 = new Graphics3D(g);
            e.Graphics.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);
            Pen pluma = new Pen(Color.Black, 4);
            g3.DrawLine3D(pluma, puntos[0], puntos[1]);
            g3.DrawLine3D(pluma, puntos[1], puntos[2]);
            g3.DrawLine3D(pluma, puntos[2], puntos[3]);
            g3.DrawLine3D(pluma, puntos[3], puntos[0]);
            g3.DrawLine3D(pluma, puntos[4], puntos[5]);
            g3.DrawLine3D(pluma, puntos[5], puntos[6]);
            g3.DrawLine3D(pluma, puntos[6], puntos[7]);
            g3.DrawLine3D(pluma, puntos[7], puntos[4]);
            g3.DrawLine3D(pluma, puntos[0], puntos[4]);
            g3.DrawLine3D(pluma, puntos[1], puntos[5]);
            g3.DrawLine3D(pluma, puntos[2], puntos[6]);
            g3.DrawLine3D(pluma, puntos[3], puntos[7]);

            // Dibujar esquinas del cubo
            for (int i = 0; i < puntos.Length; i++)
            {
                PointF p = new PointF()

                {
                    //Calcular las esquinas de los puntos en el plano X,Y,Z
                    X = puntos[i].Z * (float)(Math.Cos(45 * (Math.PI / 180) + puntos[i].X)) - 3,
                    Y = puntos[i].Z * (float)(Math.Sin(45 * (Math.PI / 180) + puntos[i].Y) - 3)
                };

                //Dibujar los circulos que simulan los puntos del cubo  
                g.FillEllipse(new SolidBrush(Color.Yellow), new RectangleF(p, new SizeF(5F, 5F)));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Dibujar los punto del cubo en el form
            puntos[0] = new Point3DF(-50, -50, -50);
            puntos[1] = new Point3DF(-50, -50, 50);
            puntos[2] = new Point3DF(-50, 50, 50);
            puntos[3] = new Point3DF(-50, 50, -50);
            puntos[4] = new Point3DF(50, -50, -50);
            puntos[5] = new Point3DF(50, -50, 50);
            puntos[6] = new Point3DF(50, 50, 50);
            puntos[7] = new Point3DF(50, 50, -50);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // saber que teclas presiona el usuario para aumentar o disminuir el tamaño del cubo
            // en el eje X --- flecha lateral izquierda, derecha
            // en el eje Y --- flecha arriba, abajo
            // en el eje Z --- Q aumento, W disminuir
            switch (e.KeyCode)
            {
                case Keys.Left:
                    EscalarX(false);
                    break;
                case Keys.Right:
                    EscalarX(true);
                    break;
                case Keys.Down:
                    EscalarY(true);
                    break;
                case Keys.Up:
                    EscalarY(false);
                    break;
                case Keys.Q:
                    EscalarZ(true);
                    break;
                case Keys.E:
                    EscalarZ(false);
                    break;
                //Efecto de trazladar
                // A para mover el objeto a la derecha
                case Keys.D:
                    TrasladarX(true);
                    break;
                case Keys.A:
                    TrasladarX(false);
                    break;
                case Keys.S:
                    TrasladarY(true);
                    break;
                case Keys.W:
                    TrasladarY(false);
                    break;
            }
        }
        // Metodos para hacer las escalas
        // Escala en X = 4,5,6,7 --> Aumentar derecha, Disminuir izquierda
        private void EscalarX(bool aumentar_disminuir)
        {
            if (aumentar_disminuir)
            {
                for (int i = 4; i < puntos.Length; i++)
                {
                    puntos[i].X += esc;
                }
            }
            else {
                for (int i = 4; i < puntos.Length; i++)
                {
                    puntos[i].X -= esc;
                }
            }
            pictureBox1.Refresh();
        }
        // Escalar en Y = 2,3,6,7 -- Aumentar flecha arriba +, disminuir flecha abajo-
        private void EscalarY(bool aumentar_disminuir)
        {
            if (aumentar_disminuir)
            {
                puntos[2].Y += esc;
                puntos[3].Y += esc;
                puntos[6].Y += esc;
                puntos[7].Y += esc;
            }
            else
            {
                puntos[2].Y -= esc;
                puntos[3].Y -= esc;
                puntos[6].Y -= esc;
                puntos[7].Y -= esc;
            }
            pictureBox1.Refresh();
        }
        // Escalar en Z = 1,2,6,7 -- Q Aumentar +, W Disminuir -
        private void EscalarZ(bool aumentar_disminuir)
        {
            if (aumentar_disminuir)
            {
                puntos[1].Z += esc;
                puntos[2].Z += esc;
                puntos[5].Z += esc;
                puntos[6].Z += esc;
            }
            else
            {
                puntos[1].Z -= esc;
                puntos[2].Z -= esc;
                puntos[5].Z -= esc;
                puntos[6].Z -= esc;
            }
            pictureBox1.Refresh();
        }
        private void TrasladarX(bool izq_der) 
        {
            if (izq_der)
            {
                for (int i = 0; i < puntos.Length; i++)
                {
                    puntos[i].X += tras;
                }
            }
            else 
            {
                for (int i = 0; i < puntos.Length; i++)
                {
                    puntos[i].X -= tras;
                }
            }
            pictureBox1.Refresh();
        }

        private void TrasladarY(bool arriba_abajo)
        {
            if (arriba_abajo)
            {
                for (int i = 0; i < puntos.Length; i++)
                {
                    puntos[i].Y += tras;
                }
            }
            else
            {
                for (int i = 0; i < puntos.Length; i++)
                {
                    puntos[i].Y -= tras;
                }
            }
            pictureBox1.Refresh();
        }

        // Rotacion
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // Guardar los puntos en lo que se mueve elk mouse
            posMouse = e.Location;
            // Accionar la variable mover, significa que se esta ejecutando la rotacion
            mover = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // Se activa cuando el cursor se mueve por la pantalla
            if (mover)
            {
                // Determinar el angulo en el que se encuentra X y Y
                ang_x = e.Location.X + posMouse.X;
                ang_y = e.Location.Y + posMouse.Y;

                if (ang_x > 0)
                {
                    ang_x = 1; // rotando a la derecha
                } else if (ang_x < 0) 
                {
                    ang_x = -1;    // rotando a la izquierda
                }

                if (ang_y > 0)
                {
                    ang_y = -1; // rotando abajo
                }
                else if (ang_y < 0)
                {
                    ang_y = 1; // rotando arriba
                }

                puntos = Rotar(puntos, ang_x, ang_y);
                pictureBox1.Refresh();
            }
        }

        // metodo para aplicar rotacion al objeto
        private Point3DF[] Rotar(Point3DF[] points, double anguloX, double anguloY) 
        {
            Point3DF aux = new Point3DF();
            // Conversion de grados a radianes
            double gradosX = (anguloX * Math.PI) / 180;
            double gradosY = (anguloY * Math.PI) / 180;
            // Recorremos todos los puntos para aplicar el efecto de rotacion
            for (int i = 0; i < puntos.Length; i++)
            {
                //Rotacion en el eje Y
                //aplicar el coseno de los grados en radianes por la coordenada en X, Y, Z multiplicado por el seno de los grados
                aux.X = Convert.ToSingle(points[i].X * Math.Cos(gradosX) - points[i].Z * Math.Sin(gradosX));
                aux.Y = points[i].Y;
                aux.Z = Convert.ToSingle(points[i].Z * Math.Cos(gradosX) + points[i].X * Math.Sin(gradosX));
                // Rotacion x
                points[i].X = aux.X;
                points[i].Y = Convert.ToSingle(aux.Y * Math.Cos(gradosY) - aux.Z * Math.Sin(gradosY));
                points[i].Z = Convert.ToSingle(aux.Z * Math.Cos(gradosY) + aux.Y * Math.Sin(gradosY));
            }
            return points;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // Soltar el efecto de rotacion
            mover = false;
        }
    }
}
