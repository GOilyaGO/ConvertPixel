using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertPixelToSymbol
{
    public partial class Form2 : Form
    {
        Form f1;
        public Form2(Form1 form1)
        {
            InitializeComponent();
            f1 = form1;

        }
        
        string pressKey1 = "Left",pressKey2 = "Right";

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        { 
            pressKey1 = e.Button.ToString();
            label1.Text = "Цвет1:" + pressKey1;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            pressKey2 = e.Button.ToString();
            label6.Text = "Цвет2:" + pressKey2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.Key1 = pressKey1;
            Form1.Key2 = pressKey2;
            Form1.rangeMeshduCells = Convert.ToInt32(textBox1.Text);
            Form1.cellSize = Convert.ToInt32(textBox2.Text);
            Form1.otstupX = Convert.ToInt32(textBox3.Text);
            Form1.otstupY = Convert.ToInt32(textBox4.Text);
            Form1.historyLength = Convert.ToInt32(textBox6.Text);
            Form1.tInterval = Convert.ToInt32(textBox7.Text);
            f1.Refresh();
            
            Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = Form1.rangeMeshduCells + "";
            textBox2.Text = Form1.cellSize + "";
            textBox3.Text = Form1.otstupX + "";
            textBox4.Text = Form1.otstupY + "";
            textBox6.Text = Form1.historyLength + "";
            textBox7.Text = Form1.tInterval + "";
            label1.Text = "Цвет1:" + Form1.Key1;
            label6.Text = "Цвет2:" + Form1.Key2;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Исходное значение:21\nДля того чтобы между клетками небыло линий установите значение равное размеру клетки");
        }

        private void label5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Исходное значение:20\nРазмер клетки сменится после нажатия \"Применить\" и любого действия с полем");
        }

        private void label3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Исходное значение:20\nОтступ от краёв формы по горизонтали");
        }

        private void label8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Исходное значение:20\nМаксимальное значение:20\nДлинна истории - максимальное количество действий которые можно отменить");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "21";
            textBox2.Text = "20";
            textBox3.Text = "20";
            textBox4.Text = "30";
            textBox6.Text = "20";
            textBox7.Text = "50";
        }

        private void label9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Исходное значение:50\nС каким интервалом будут происходить обновления если включена \"Прописовка по таймеру\"");
        }

        private void label4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Исходное значение:20\nОтступ от краёв формы по вертикали");
            
        }
    }
}
