using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void DrawGraph(double coefficient)
        {
            // Очистити малюнок
            pictureBox1.Image = null;
            pictureBox1.Refresh();

            // Створити бітмап для малювання
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bitmap);

            // Встановити систему координат
            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;

            // Вісь X
            g.DrawLine(Pens.Black, 0, centerY, pictureBox1.Width, centerY);
            for (int x = centerX + 20; x < pictureBox1.Width; x += 20)
            {
                g.DrawLine(Pens.Black, x, centerY - 3, x, centerY + 3);
                g.DrawString((x - centerX) / 20 + "", DefaultFont, Brushes.Black, new PointF(x - 5, centerY + 5));
            }
            for (int x = centerX - 20; x > 0; x -= 20)
            {
                g.DrawLine(Pens.Black, x, centerY - 3, x, centerY + 3);
                g.DrawString((x - centerX) / 20 + "", DefaultFont, Brushes.Black, new PointF(x - 5, centerY + 5));
            }

            // Вісь Y
            g.DrawLine(Pens.Black, centerX, 0, centerX, pictureBox1.Height);
            for (int y = centerY + 20; y < pictureBox1.Height; y += 20)
            {
                g.DrawLine(Pens.Black, centerX - 3, y, centerX + 3, y);
                g.DrawString(-(y - centerY) / 20 + "", DefaultFont, Brushes.Black, new PointF(centerX - 20, y - 5));
            }
            for (int y = centerY - 20; y > 0; y -= 20)
            {
                g.DrawLine(Pens.Black, centerX - 3, y, centerX + 3, y);
                g.DrawString(-(y - centerY) / 20 + "", DefaultFont, Brushes.Black, new PointF(centerX - 20, y - 5));
            }

            // Написи на осях
            g.DrawString("X", DefaultFont, Brushes.Black, new PointF(pictureBox1.Width - 20, centerY - 20));
            g.DrawString("Y", DefaultFont, Brushes.Black, new PointF(centerX + 10, 0));

            // Малювати графік
            double scaleX = 20; // Масштаб по X
            double scaleY = 20; // Масштаб по Y
            double step = 0.1; // Крок
            Point prevPoint = new Point();
            for (double t = 0; t <= 2 * Math.PI; t += step)
            {
                double x = coefficient * (t - Math.Sin(t) * Math.Sin(t)) * scaleX;
                double y = coefficient * (t - Math.Cos(t) * Math.Cos(t)) * scaleY;
                Point point = new Point((int)(centerX + x), (int)(centerY - y)); // Перевернути Y, тому що ось Y внизу
                if (t != 0)
                    g.DrawLine(Pens.Blue, prevPoint, point);
                prevPoint = point;
            }

            // Відобразити малюнок на PictureBox
            pictureBox1.Image = bitmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double coefficient))
            {
                DrawGraph(coefficient);
            }
            else
            {
                MessageBox.Show("Неправильний формат введених даних!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}