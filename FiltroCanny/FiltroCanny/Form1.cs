using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace FiltroCanny
{
    public partial class Form1 : Form
    {

        private Bitmap Image1; //Cria Bitmap para Imagem 1

        //Variaveis para Isolar Canais Imagem 1
        private byte[,] img1RED;
        private byte[,] img1GREEN;
        private byte[,] img1BLUE;

        private Bitmap Image3; //Cria Bitmap para Imagem 3 (RESULTADO)

        //Variaveis para Isolar Canais Imagem 3 (RESULTADO)
        private byte[,] img3RED;
        private byte[,] img3GREEN;
        private byte[,] img3BLUE;


        public Form1()
        {
            InitializeComponent();
        }

        private void Vizinhaca(Bitmap imagem, byte[,] r, byte[,] g, byte[,] b)
        {
            //Função para o calculo das funçoes de Vizinhaça

            for (int i = 0; i < imagem.Width; i++)
            {
                for (int j = 0; j < imagem.Height; j++)
                {
                    byte[,] aux = new byte[3, 3];


                    if ((i > 0 && j > 0) && (i < imagem.Width - 1 && j < imagem.Height -1))
                    {
                        //RED
                        aux[0, 0] = r[i - 1, j - 1];
                        aux[0, 1] = r[i - 1, j];
                        aux[0, 2] = r[i - 1, j + 1];
                        aux[1, 0] = r[i, j - 1];
                        aux[1, 1] = r[i, j];
                        aux[1, 2] = r[i, j + 1];
                        aux[2, 0] = r[i + 1, j - 1];
                        aux[2, 1] = r[i + 1, j];
                        aux[2, 2] = r[i + 1, j + 1];

                        byte[] auxArrays = {
                        aux[0, 0],
                        aux[0, 1],
                        aux[0, 2],
                        aux[1, 0],
                        aux[1, 1],
                        aux[1, 2],
                        aux[2, 0],
                        aux[2, 1],
                        aux[2, 2]
                        };

                        int auxMedia = (
                        Convert.ToInt32(aux[0, 0])+
                        Convert.ToInt32(aux[0, 1])+
                        Convert.ToInt32(aux[0, 2])+
                        Convert.ToInt32(aux[1, 0])+
                        Convert.ToInt32(aux[1, 1])+
                        Convert.ToInt32(aux[1, 2])+
                        Convert.ToInt32(aux[2, 0])+
                        Convert.ToInt32(aux[2, 1])+
                        Convert.ToInt32(aux[2, 2])) /9;

                        img3RED[i, j] = Convert.ToByte(auxMedia);
                    }

                    if ((i > 0 && j > 0) && (i < imagem.Width - 1 && j < imagem.Height -1))
                    {
                        //GREEN
                        aux[0, 0] = g[i - 1, j - 1];
                        aux[0, 1] = g[i - 1, j];
                        aux[0, 2] = g[i - 1, j + 1];
                        aux[1, 0] = g[i, j - 1];
                        aux[1, 1] = g[i, j];
                        aux[1, 2] = g[i, j + 1];
                        aux[2, 0] = g[i + 1, j - 1];
                        aux[2, 1] = g[i + 1, j];
                        aux[2, 2] = g[i + 1, j + 1];

                        byte[] auxArrays = {
                        aux[0, 0],
                        aux[0, 1],
                        aux[0, 2],
                        aux[1, 0],
                        aux[1, 1],
                        aux[1, 2],
                        aux[2, 0],
                        aux[2, 1],
                        aux[2, 2]
                        };

                        int auxMedia = (
                        Convert.ToInt32(aux[0, 0])+
                        Convert.ToInt32(aux[0, 1])+
                        Convert.ToInt32(aux[0, 2])+
                        Convert.ToInt32(aux[1, 0])+
                        Convert.ToInt32(aux[1, 1])+
                        Convert.ToInt32(aux[1, 2])+
                        Convert.ToInt32(aux[2, 0])+
                        Convert.ToInt32(aux[2, 1])+
                        Convert.ToInt32(aux[2, 2])) /9;

                        img3GREEN[i, j] = Convert.ToByte(auxMedia);
                    }

                    if ((i > 0 && j > 0) && (i < imagem.Width - 1 && j < imagem.Height -1))
                    {
                        //BLUE
                        aux[0, 0] = b[i - 1, j - 1];
                        aux[0, 1] = b[i - 1, j];
                        aux[0, 2] = b[i - 1, j + 1];
                        aux[1, 0] = b[i, j - 1];
                        aux[1, 1] = b[i, j];
                        aux[1, 2] = b[i, j + 1];
                        aux[2, 0] = b[i + 1, j - 1];
                        aux[2, 1] = b[i + 1, j];
                        aux[2, 2] = b[i + 1, j + 1];


                        byte[] auxArrays = {
                        aux[0, 0],
                        aux[0, 1],
                        aux[0, 2],
                        aux[1, 0],
                        aux[1, 1],
                        aux[1, 2],
                        aux[2, 0],
                        aux[2, 1],
                        aux[2, 2]
                        };

                        int auxMedia = (
                        Convert.ToInt32(aux[0, 0])+
                        Convert.ToInt32(aux[0, 1])+
                        Convert.ToInt32(aux[0, 2])+
                        Convert.ToInt32(aux[1, 0])+
                        Convert.ToInt32(aux[1, 1])+
                        Convert.ToInt32(aux[1, 2])+
                        Convert.ToInt32(aux[2, 0])+
                        Convert.ToInt32(aux[2, 1])+
                        Convert.ToInt32(aux[2, 2])) /9;

                       img3BLUE[i, j] = Convert.ToByte(auxMedia);
                    }
                }
            }
        }


        private void IsolaCanalImg(Bitmap imagem, byte[,] r, byte[,] g, byte[,] b) //Isola Canais RGB para Imagem
        {
            //Isola Canais RGB
            for (int i = 0; i < imagem.Width; i++)
            {
                for (int j = 0; j < imagem.Height; j++)
                {
                    Color px = imagem.GetPixel(i, j);
                    r[i, j] = px.R;
                    g[i, j] = px.G;
                    b[i, j] = px.B;
                }
            }
        }

        private void GeracaoImagem(Bitmap Imagem)
        {

            //Gera Imagens a partir da Imagem
            for (int i = 0; i < Imagem.Width; i++)
            {
                for (int j = 0; j < Imagem.Height; j++)
                {
                    Color cor = Color.FromArgb(255, img3RED[i, j], img3GREEN[i, j], img3BLUE[i, j]);
                    Image3.SetPixel(i, j, cor);
                }
            }

            pictureBox3.Image = Image3;
        }

        private void button1_Click(object sender, EventArgs e) //Abrir Imagem
        {
            openFileDialog1.InitialDirectory = "C:\\Processamento de imagem\\Matlab";
            openFileDialog1.Filter = "*.jpg, *.bmp, *.png, *.tif)| *.jpg; *.bmp; *.png; *.tif; ";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image1 = new Bitmap(openFileDialog1.FileName);
                button2.Enabled = true;
                pictureBox1.Image = Image1;

                img1RED   = new byte[Image1.Width, Image1.Height];
                img1GREEN = new byte[Image1.Width, Image1.Height];
                img1BLUE  = new byte[Image1.Width, Image1.Height];
            }
        }

        private void button7_Click(object sender, EventArgs e) //Media
        {
            //Inicia Variavies
            img1RED   = new byte[Image1.Width, Image1.Height];
            img1GREEN = new byte[Image1.Width, Image1.Height];
            img1BLUE  = new byte[Image1.Width, Image1.Height];

            img3RED   = new byte[Image1.Width, Image1.Height];
            img3GREEN = new byte[Image1.Width, Image1.Height];
            img3BLUE  = new byte[Image1.Width, Image1.Height];

            Image3 = new Bitmap(Image1);
            IsolaCanalImg(Image1, img1RED, img1GREEN, img1BLUE);
            Vizinhaca(Image1, img1RED, img1GREEN, img1BLUE);
            GeracaoImagem(Image1);
            button4.Visible = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Image1 = Image3;

            pictureBox1.Image = Image1;
            pictureBox3.Image = null;
            button4.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap Imagem = new Bitmap(pictureBox1.Image);
            int Largura = Imagem.Width;
            int Altura = Imagem.Height;
            int[,] Cannys = new int[,] { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
            int todosPixelsR = 0;
            int todosPixelsG = 0;
            int todosPixelsB = 0;
            int i;
            int j;
            int aux, aux2;
            Bitmap NovaImagem = new Bitmap(Largura, Altura); //para trabalhar com a imagem, novo bmp com o mesmo tamanho da img original
            for (i = 0; i < Altura - 2; i++)
            {
                for (j = 0; j < Largura - 2; j++)
                {
                    for (aux = 0; aux < 3; aux++)
                    {
                        for (aux2 = 0; aux2 < 3; aux2++)
                        {
                            todosPixelsR += Imagem.GetPixel(j + aux, i + aux2).R * Cannys[aux2, aux];
                            todosPixelsG += Imagem.GetPixel(j + aux, i + aux2).G * Cannys[aux2, aux];
                            todosPixelsB += Imagem.GetPixel(j + aux, i + aux2).B * Cannys[aux2, aux];
                        }

                    }

                    /*   if ((todosPixelsR < 255) && (todosPixelsR > 0))
                            {
                                todosPixelsR = 255;
                            }                    
                        else
                         {
                            todosPixelsR = 0;
                         }

                        if ((todosPixelsG < 255) && (todosPixelsG > 0))
                        {
                            todosPixelsG = 255;
                        }
                        else
                        {
                            todosPixelsG = 0;
                        }

                        if ((todosPixelsB < 255) && (todosPixelsB > 0))
                        {
                            todosPixelsB = 255;
                        }
                        else
                        {
                            todosPixelsB = 0;
                        }*/
                    if (todosPixelsR < 0)
                    {
                        todosPixelsR = 0;
                    }
                    else
                    {
                        if (todosPixelsR > 255)
                        {
                            todosPixelsR = 255;
                        }
                    }
                    if (todosPixelsG < 0)
                    {
                        todosPixelsG = 0;
                    }
                    else
                    {
                        if (todosPixelsG > 255)
                        {
                            todosPixelsG = 255;
                        }
                    }
                    if (todosPixelsB < 0)
                    {
                        todosPixelsB = 0;
                    }
                    else
                    {
                        if (todosPixelsB > 255)
                        {
                            todosPixelsB = 255;
                        }

                    }
                    int cinza = (todosPixelsR + todosPixelsG + todosPixelsB) / 3;

                    // NovaImagem.SetPixel(j + 1, i + 1, Color.FromArgb(255, todosPixelsR, todosPixelsG, todosPixelsB));
                    NovaImagem.SetPixel(j + 1, i + 1, Color.FromArgb(255, cinza, cinza, cinza));
                    todosPixelsR = 0;
                    todosPixelsG = 0;
                    todosPixelsB = 0;

                }
            }
            pictureBox3.Image = NovaImagem;
        }
        int globalValue2 = 0;
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox2.Text, out int value))
            {
                globalValue2 = value;
            }
            else
            {
                MessageBox.Show("O valor inserido não é válido. Insira um número válido.", "Erro de entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // LAPLACIANO + IMAGEM ORIGINAL

                int Largura = Image1.Width;
                int Altura = Image1.Height;
                int[,] Laplace1 = new int[,] { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } }; // MÁSCARA DE REALCE 1
                int[,] Laplace2 = new int[,] { { 1, 1, 1 }, { 1, -8, 1 }, { 1, 1, 1 } }; // MÁSCARA DE REALCE 2

                int[,] laplaceAtual = (globalValue2 == 1) ? Laplace1 : Laplace2; // Selecionar a máscara adequada com base no valor de globalValue2

                Bitmap NovaImagem = new Bitmap(Largura, Altura);

                for (int i = 1; i < Altura - 1; i++)
                {
                    for (int j = 1; j < Largura - 1; j++)
                    {
                        int todosPixelsR = 0;
                        int todosPixelsG = 0;
                        int todosPixelsB = 0;

                        for (int aux = -1; aux <= 1; aux++)
                        {
                            for (int aux2 = -1; aux2 <= 1; aux2++)
                            {
                                Color pixel = Image1.GetPixel(j + aux, i + aux2);
                                todosPixelsR += pixel.R * laplaceAtual[aux + 1, aux2 + 1];
                                todosPixelsG += pixel.G * laplaceAtual[aux + 1, aux2 + 1];
                                todosPixelsB += pixel.B * laplaceAtual[aux + 1, aux2 + 1];
                            }
                        }

                        todosPixelsR = Math.Max(0, Math.Min(255, todosPixelsR));
                        todosPixelsG = Math.Max(0, Math.Min(255, todosPixelsG));
                        todosPixelsB = Math.Max(0, Math.Min(255, todosPixelsB));

                        Color pixelOriginal = Image1.GetPixel(j, i);

                        int novoValorR = Math.Max(0, Math.Min(255, pixelOriginal.R - todosPixelsR));
                        int novoValorG = Math.Max(0, Math.Min(255, pixelOriginal.G - todosPixelsG));
                        int novoValorB = Math.Max(0, Math.Min(255, pixelOriginal.B - todosPixelsB));

                        NovaImagem.SetPixel(j, i, Color.FromArgb(255, novoValorR, novoValorG, novoValorB));
                    }
                }

                pictureBox3.Image = NovaImagem;
            
        }


        int globalValue = 0;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox1.Text, out int value))
            {
                globalValue = value;
            }
            else
            {
                MessageBox.Show("O valor inserido não é válido. Insira um número válido.", "Erro de entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int Largura = Image1.Width;
            int Altura = Image1.Height;
            int[,] Mascara1 = new int[,] { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } }; // MASCARA APLICADA
            int[,] Mascara2 = new int[,] { { 1, 1, 1 }, { 1, -8, 1 }, { 1, 1, 1 } }; // MASCARA 2 APLICADA

            int[,] mascaraAtual = (globalValue == 1) ? Mascara1 : Mascara2; // Selecionar a máscara adequada com base no valor de globalValue

            Bitmap NovaImagem = new Bitmap(Largura, Altura);

            for (int i = 1; i < Altura - 1; i++)
            {
                for (int j = 1; j < Largura - 1; j++)
                {
                    int todosPixelsR = 0;
                    int todosPixelsG = 0;
                    int todosPixelsB = 0;

                    for (int aux = -1; aux <= 1; aux++)
                    {
                        for (int aux2 = -1; aux2 <= 1; aux2++)
                        {
                            Color pixel = Image1.GetPixel(j + aux, i + aux2); // pega posição do pixel atual
                            todosPixelsR += pixel.R * mascaraAtual[aux + 1, aux2 + 1];
                            todosPixelsG += pixel.G * mascaraAtual[aux + 1, aux2 + 1];
                            todosPixelsB += pixel.B * mascaraAtual[aux + 1, aux2 + 1];
                        }
                    }

                    todosPixelsR = Math.Max(0, Math.Min(255, todosPixelsR));
                    todosPixelsG = Math.Max(0, Math.Min(255, todosPixelsG));
                    todosPixelsB = Math.Max(0, Math.Min(255, todosPixelsB));

                    NovaImagem.SetPixel(j, i, Color.FromArgb(255, todosPixelsR, todosPixelsG, todosPixelsB));
                }
            }

            pictureBox3.Image = NovaImagem;

        }
    }
}
