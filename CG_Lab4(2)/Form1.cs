using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace CG_Lab4_2_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int[,] kv = new int[4, 3]; // матрица тела 
        int[,] fg = new int[5, 3]; // матрица тела 
        int[,] fg1 = new int[5, 3];
        int[,] osi = new int[4, 3]; // матрица координат осей 
        int[,] matr_sdv = new int[3, 3]; //матрица преобразования
        int[,] matr_otr = new int[3, 3]; //матрица отражения
        int[,] matr_mash = new int[3, 3]; //матрица масштабирования
        int[,] matr_pov = new int[3, 3]; //матрица отражения

        int k, l, k0, l0, b, c; // элементы матрицы сдвига
        int a = 1;
        int d = -1;
        int fig_number, click_number;// элементы выбора для сдвига
        int m, m1;//элементы матрицы масштаба
        bool f = true; // значеие для таймера

        Pen myPen;
        Color current_color = Color.Blue;

        private void Form1_Load(object sender, EventArgs e)
        {
            matr_otr[0, 0] = 1; matr_otr[0, 1] = 0; matr_otr[0, 2] = 0;
            matr_otr[1, 0] = 0; matr_otr[1, 1] = 1; matr_otr[1, 2] = 0;
            matr_otr[2, 0] = 0; matr_otr[2, 1] = 0; matr_otr[2, 2] = 1;

            matr_mash[0, 0] = 1; matr_mash[0, 1] = 0; matr_mash[0, 2] = 0;
            matr_mash[1, 0] = 0; matr_mash[1, 1] = 1; matr_mash[1, 2] = 0;
            matr_mash[2, 0] = 0; matr_mash[2, 1] = 0; matr_mash[2, 2] = 1;

            matr_pov[0, 0] = 1; matr_pov[0, 1] = 0; matr_pov[0, 2] = 0;
            matr_pov[1, 0] = 0; matr_pov[1, 1] = 1; matr_pov[1, 2] = 0;
            matr_pov[2, 0] = 0; matr_pov[2, 1] = 0; matr_pov[2, 2] = 1;
        }

        //Вывод осей
        private void button1_Click(object sender, EventArgs e)
        {
            k0 = pictureBox1.Width / 2;
            l0 = pictureBox1.Height / 2;

            k = pictureBox1.Width / 2;
            l = pictureBox1.Height / 2;

            Draw_osi(k0, l0);
        }

        //Вывод квадрата
        private void button2_Click(object sender, EventArgs e)
        {
            fig_number = 1;
            Draw_Kv(current_color);
        }
        
        //Вывод фигуры
        private void button3_Click(object sender, EventArgs e)
        {
            fig_number = 2;
            Draw_Fig(current_color);
        }

        //Выбор цвета
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == System.Windows.Forms.DialogResult.OK)
            {
                current_color = colorDialog1.Color;
            }
        }

        //Сдвиг вверх
        private void button5_Click(object sender, EventArgs e)
        {
            switch (fig_number)
            {
                case 1:
                    Draw_Kv(pictureBox1.BackColor);
                    Draw_osi(k0, l0);
                    l -= 5;
                    Draw_Kv(current_color);
                    break;
                case 2:
                    Draw_Fig(pictureBox1.BackColor);
                    Draw_osi(k0, l0);
                    l -= 5;
                    Draw_Fig(current_color);
                    break;
            }
            click_number = 1;
        }

        //Сдвиг влево
        private void button6_Click(object sender, EventArgs e)
        {
            switch (fig_number)
            {
                case 1:
                    Draw_Kv(pictureBox1.BackColor);
                    Draw_osi(k0, l0);
                    k -= 5;
                    Draw_Kv(current_color);
                    break;
                case 2:
                    Draw_Fig(pictureBox1.BackColor);
                    Draw_osi(k0, l0);
                    k -= 5;
                    Draw_Fig(current_color);
                    break;
            }
            click_number = 2;
        }

        //Сдвиг вправо
        private void button7_Click(object sender, EventArgs e)
        {
            switch (fig_number)
            {
                case 1:
                    Draw_Kv(pictureBox1.BackColor);
                    Draw_osi(k0, l0);
                    k += 5;
                    Draw_Kv(current_color);
                    break;
                case 2:
                    Draw_Fig(pictureBox1.BackColor);
                    Draw_osi(k0, l0);
                    k += 5;
                    Draw_Fig(current_color);
                    break;
            }
            click_number = 3;
        }

        //Сдвиг вниз
        private void button8_Click(object sender, EventArgs e)
        {
            switch (fig_number)
            {
                case 1:
                    Draw_Kv(pictureBox1.BackColor);
                    Draw_osi(k0, l0);
                    l += 5;
                    Draw_Kv(current_color);
                    break;
                case 2:
                    Draw_Fig(pictureBox1.BackColor);
                    Draw_osi(k0, l0);
                    l += 5;
                    Draw_Fig(current_color);
                    break;
            }
            click_number = 4;
        }

        //Старт
        private void button9_Click(object sender, EventArgs e)
        {

            timer1.Interval = 50;
            button9.Text = "Стоп";

            if (f == true) timer1.Start();
            else
            {
                timer1.Stop();
                button9.Text = "Старт";
            }
            f = !f;
        }

        //Отражение
        private void button10_Click(object sender, EventArgs e)
        {
            switch (fig_number)
            {
                case 1:
                    Draw_Kv(pictureBox1.BackColor);
                    Init_matr_otr(a, d);
                    Draw_Kv(current_color);
                    break;
                case 2:
                    Draw_Fig(pictureBox1.BackColor);
                    Init_matr_otr(a, -d);
                    Draw_Fig(current_color);
                    break;
            }

            a *= -1;
            d *= -1;
        }

        //Масштаб -
        private void button11_Click(object sender, EventArgs e)
        {
            m -= 1;
            m1 -= 1;

            switch (fig_number)
            {
                case 1:
                    Draw_Kv(pictureBox1.BackColor);
                    Init_matr_mash(m, m1);
                    Draw_Kv(current_color);
                    break;
                case 2:
                    Draw_Fig(pictureBox1.BackColor);
                    Init_matr_mash(m, m1);
                    Draw_Fig(current_color);
                    break;
            }
        }
        
        //Масштаб +
        private void button12_Click(object sender, EventArgs e)
        {
            m += 1;
            m1 += 1;

            switch (fig_number)
            {
                case 1:
                    Draw_Kv(pictureBox1.BackColor);
                    Init_matr_mash(m, m1);
                    Draw_Kv(current_color);
                    break;
                case 2:
                    Draw_Fig(pictureBox1.BackColor);
                    Init_matr_mash(m, m1);
                    Draw_Fig(current_color);
                    break;
            }
        }

        //Поворот -
        private void button13_Click(object sender, EventArgs e)
        {
            b = 1;
            c = -1;

            switch (fig_number)
            {
                case 1:
                    Draw_Kv(pictureBox1.BackColor);
                    Init_matr_pov(0, b, c, 0);
                    Draw_Kv(current_color);
                    break;
                case 2:
                    Draw_Fig(pictureBox1.BackColor);
                    Init_matr_pov(0, b, c, 0);
                    Draw_Fig(current_color);
                    break;
            }
        }

        //Поворот +
        private void button14_Click(object sender, EventArgs e)
        {
            b = -1;
            c = 1;

            switch (fig_number)
            {
                case 1:
                    Draw_Kv(pictureBox1.BackColor);
                    Init_matr_pov(1, b, c, 1);
                    Draw_Kv(current_color);
                    break;
                case 2:
                    Draw_Fig(pictureBox1.BackColor);
                    Init_matr_pov(1, b, c, 1);
                    Draw_Fig(current_color);
                    break;
            }
        }

        //Очистить
        private void button15_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        //Настройка линии
        private Pen ThisPen(Color curentcolor, float width)
        {
            Pen myPen = new Pen(curentcolor, width);
            myPen.Color = curentcolor;
            myPen.Width = width;
            if (radioButton2.Checked == true)
            {
                myPen.DashPattern = new float[] { 2f, 1f };
            }
            return myPen;
        }

        //Инициализация матрицы сдвига 
        private void Init_matr_sdvig(int k, int l)
        {
            matr_sdv[0, 0] = 1; matr_sdv[0, 1] = 0;  matr_sdv[0, 2] = 0;
            matr_sdv[1, 0] = 0; matr_sdv[1, 1] = 1; matr_sdv[1, 2] = 0;
            matr_sdv[2, 0] = k; matr_sdv[2, 1] = l;  matr_sdv[2, 2] = 1;
        }

        //Инициализация матрицы отражения
        private void Init_matr_otr(int a, int d)
        {
            matr_otr[0, 0] = a; matr_otr[0, 1] = 0; matr_otr[0, 2] = 0;
            matr_otr[1, 0] = 0; matr_otr[1, 1] = d; matr_otr[1, 2] = 0;
            matr_otr[2, 0] = 0; matr_otr[2, 1] = 0; matr_otr[2, 2] = 1;
        }

        //Инициализация матрицы масштабирования
        private void Init_matr_mash(int m, int m1)
        {
            matr_mash[0, 0] = m; matr_mash[0, 1] = 0; matr_mash[0, 2] = 0;
            matr_mash[1, 0] = 0; matr_mash[1, 1] = m1; matr_mash[1, 2] = 0;
            matr_mash[2, 0] = 0; matr_mash[2, 1] = 0; matr_mash[2, 2] = 1;
        }

        //Инициализация матрицы поворота 
        private void Init_matr_pov(int a, int b, int c, int d)
        {
            matr_pov[0, 0] = a; matr_pov[0, 1] = b; matr_pov[0, 2] = 0;
            matr_pov[1, 0] = c; matr_pov[1, 1] = d; matr_pov[1, 2] = 0;
            matr_pov[2, 0] = 0; matr_pov[2, 1] = 0; matr_pov[2, 2] = 1;
        }

        //Инициализация матрицы осей 
        private void Init_osi()
        {
            osi[0, 0] = -200; osi[0, 1] = 0;    osi[0, 2] = 1;
            osi[1, 0] = 200;  osi[1, 1] = 0;    osi[1, 2] = 1;
            osi[2, 0] = 0;    osi[2, 1] = 200;  osi[2, 2] = 1;
            osi[3, 0] = 0;    osi[3, 1] = -200; osi[3, 2] = 1;
        }

        //Инициализация квадрата
        private void Init_kvadrat()
        {
            kv[0, 0] = -50; kv[0, 1] = 0;   kv[0, 2] = 1;
            kv[1, 0] = 0;   kv[1, 1] = 50;  kv[1, 2] = 1;
            kv[2, 0] = 50;  kv[2, 1] = 0;   kv[2, 2] = 1;
            kv[3, 0] = 0;   kv[3, 1] = -50; kv[3, 2] = 1;
        }

        //Инициализация фигуры
        private void Init_Fig()
        {
            fg[0, 0] = -15; fg[0, 1] = 50;  fg[0, 2] = 1;
            fg[1, 0] = 20;  fg[1, 1] = 20;  fg[1, 2] = 1;
            fg[2, 0] = 5;   fg[2, 1] = 0;   fg[2, 2] = 1;
            fg[3, 0] = 20;  fg[3, 1] = -20; fg[3, 2] = 1;
            fg[4, 0] = -15; fg[4, 1] = -50; fg[4, 2] = 1;
        }

        //Умножение матриц
        private int[,] Multiply_matr(int[,] a, int[,] b)
        {
            int n = a.GetLength(0);
            int m = a.GetLength(1);

            int[,] r = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    r[i, j] = 0;
                    for (int ii = 0; ii < m; ii++)
                    {
                        r[i, j] += a[i, ii] * b[ii, j];
                    }
                }
            }
            return r;
        }

        //Отрисовка осей 
        private void Draw_osi(int k, int l)
        {
            Init_osi();
            Init_matr_sdvig(k, l);
            int[,] osi1 = Multiply_matr(osi, matr_sdv);

            myPen = ThisPen(Color.Red, 1);

            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            // рисуем ось ОХ 
            g.DrawLine(myPen, osi1[0, 0], osi1[0, 1], osi1[1, 0], osi1[1, 1]);
            // рисуем ось ОУ 
            g.DrawLine(myPen, osi1[2, 0], osi1[2, 1], osi1[3, 0], osi1[3, 1]);

            g.Dispose();
            myPen.Dispose();
        }

        //Отрисовка квадрата
        private void Draw_Kv(Color color2)
        {
            Init_kvadrat(); //инициализация матрицы тела 
            Init_matr_sdvig(k, l); //инициализация матрицы преобразования 

            int[,] kv4 = Multiply_matr(kv, matr_otr);
            int[,] kv3 = Multiply_matr(kv4, matr_mash);
            int[,] kv2 = Multiply_matr(kv3, matr_pov);
            int[,] kv1 = Multiply_matr(kv2, matr_sdv); //перемножение матриц

            myPen = ThisPen(color2, 2);

            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            g.DrawLine(myPen, kv1[0, 0], kv1[0, 1], kv1[1, 0], kv1[1, 1]);
            g.DrawLine(myPen, kv1[1, 0], kv1[1, 1], kv1[2, 0], kv1[2, 1]);
            g.DrawLine(myPen, kv1[2, 0], kv1[2, 1], kv1[3, 0], kv1[3, 1]);
            g.DrawLine(myPen, kv1[3, 0], kv1[3, 1], kv1[0, 0], kv1[0, 1]);

            g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой 
            myPen.Dispose(); //освобождвем ресурсы, связанные с Pen
        }

        //Отрисовка фигуры
        private void Draw_Fig(Color color2)
        {
            Init_Fig();
            Init_matr_sdvig(k, l);

            int[,] fg4 = Multiply_matr(fg, matr_otr);
            int[,] fg3 = Multiply_matr(fg4, matr_mash);
            int[,] fg2 = Multiply_matr(fg3, matr_pov); //перемножение матриц
            int[,] fg1 = Multiply_matr(fg2, matr_sdv);

            myPen = ThisPen(color2, 2);

            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            g.DrawLine(myPen, fg1[0, 0], fg1[0, 1], fg1[1, 0], fg1[1, 1]);
            g.DrawLine(myPen, fg1[1, 0], fg1[1, 1], fg1[2, 0], fg1[2, 1]);
            g.DrawLine(myPen, fg1[2, 0], fg1[2, 1], fg1[3, 0], fg1[3, 1]);
            g.DrawLine(myPen, fg1[3, 0], fg1[3, 1], fg1[4, 0], fg1[4, 1]);
            g.DrawLine(myPen, fg1[4, 0], fg1[4, 1], fg1[0, 0], fg1[0, 1]);

            g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой 
            myPen.Dispose(); //освобождвем ресурсы, связанные с Pen
        }

        //таймер
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (click_number)
            {
                case 1:
                    if (fig_number == 1)
                    {
                        Draw_Kv(pictureBox1.BackColor);
                        Draw_osi(k0, l0);
                        l -= 5;
                        Draw_Kv(current_color);
                        Thread.Sleep(10);
                    }
                    if (fig_number == 2)
                    {
                        Draw_Fig(pictureBox1.BackColor);
                        Draw_osi(k0, l0);
                        l -= 5;
                        Draw_Fig(current_color);
                        Thread.Sleep(10);
                    }
                    break;

                case 2:
                    if (fig_number == 1)
                    {
                        Draw_Kv(pictureBox1.BackColor);
                        Draw_osi(k0, l0);
                        k -= 5;
                        Draw_Kv(current_color);
                        Thread.Sleep(10);
                    }
                    if (fig_number == 2)
                    {
                        Draw_Fig(pictureBox1.BackColor);
                        Draw_osi(k0, l0);
                        k -= 5;
                        Draw_Fig(current_color);
                        Thread.Sleep(10);
                    }
                    break;

                case 3:
                    if (fig_number == 1)
                    {
                        Draw_Kv(pictureBox1.BackColor);
                        Draw_osi(k0, l0);
                        k += 5;
                        Draw_Kv(current_color);
                        Thread.Sleep(10);
                    }
                    if (fig_number == 2)
                    {
                        Draw_Fig(pictureBox1.BackColor);
                        Draw_osi(k0, l0);
                        k += 5;
                        Draw_Fig(current_color);
                        Thread.Sleep(10);
                    }
                    break;

                 case 4:
                    if (fig_number == 1)
                    {
                        Draw_Kv(pictureBox1.BackColor);
                        Draw_osi(k0, l0);
                        l += 5;
                        Draw_Kv(current_color);
                        Thread.Sleep(10);
                    }
                    if (fig_number == 2)
                    {
                        Draw_Fig(pictureBox1.BackColor);
                        Draw_osi(k0, l0);
                        l += 5;
                        Draw_Fig(current_color);
                        Thread.Sleep(10);
                    }
                    break;
            }
        }
    }
}
