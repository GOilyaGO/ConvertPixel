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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MassivOut();
            this.KeyUp += new KeyEventHandler(PRESS);
        }

        private Graphics graph;

        
        public static bool timerOfOn;
        public static string Key1 = "Left", Key2 = "Right";
        public static int otstupX = 0, otstupY = 0, rangeMeshduCells = 21, cellSize = 20, historyLength = 20, tInterval = 50;
        public static int cellsY = 10, cellsX = 10, useLayer;
        bool pain ,autoSize = true;
        public static int[,,] massiv = new int[105, 105, 21];
        int color1 = 1, color2 = 0;
        string text, openFile, saveFile, exportName, importStringMassiv, importOutput;
        int curX, curY, curHisX1 = 1, curHisY1 = 1, nextLayer, historyIndex;
        int counter = 0;
        string[] massivImport;
        int importNum ;

        private void Painting(Graphics g)
        {
            SolidBrush s = new SolidBrush(Color.Blue); //Если пригодится

            for (int x = 0; x < cellsX; x++)
            {
                for (int y = 0; y < cellsY; y++)
                {
                    if (massiv[x, y, useLayer] == 0)
                    {
                        s.Color = Color.Gray;
                        g.FillRectangle(s, x * rangeMeshduCells + otstupX, y * rangeMeshduCells + otstupY, cellSize, cellSize);

                    }
                    else if (massiv[x, y, useLayer] == 1)
                    {
                        s.Color = Color.Red;
                        g.FillRectangle(s, x * rangeMeshduCells + otstupX, y * rangeMeshduCells + otstupY, cellSize, cellSize);
                    }
                    else if (massiv[x, y, useLayer] == 2)
                    {
                        s.Color = Color.DarkOrange;
                        g.FillRectangle(s, x * rangeMeshduCells + otstupX, y * rangeMeshduCells + otstupY, cellSize, cellSize);
                    }
                    else if (massiv[x, y, useLayer] == 3)
                    {
                        s.Color = Color.Yellow;
                        g.FillRectangle(s, x * rangeMeshduCells + otstupX, y * rangeMeshduCells + otstupY, cellSize, cellSize);
                    }
                    else if (massiv[x, y, useLayer] == 4)
                    {
                        s.Color = Color.Lime;
                        g.FillRectangle(s, x * rangeMeshduCells + otstupX, y * rangeMeshduCells + otstupY, cellSize, cellSize);
                    }
                    else if (massiv[x, y, useLayer] == 5)
                    {
                        s.Color = Color.Aqua;
                        g.FillRectangle(s, x * rangeMeshduCells + otstupX, y * rangeMeshduCells + otstupY, cellSize, cellSize);
                    }

                    else if (massiv[x, y, useLayer] == 6)
                    {
                        s.Color = Color.Blue;
                        g.FillRectangle(s, x * rangeMeshduCells + otstupX, y * rangeMeshduCells + otstupY, cellSize, cellSize);
                    }
                    else if (massiv[x, y, useLayer] == 7)
                    {
                        s.Color = Color.Fuchsia;
                        g.FillRectangle(s, x * rangeMeshduCells + otstupX, y * rangeMeshduCells + otstupY, cellSize, cellSize);
                    }
                    else { }
                }
            }
        }

        private void AutoSize()
        {
            if (autoSize)
            {
                if (38 + otstupX + panel1.Width + cellsX * rangeMeshduCells > 560)
                {
                    this.Width = 50 + otstupX + panel1.Width + cellsX * rangeMeshduCells;
               
                }
                else { this.Width = 560; }

                if (50 + otstupY + cellsY * rangeMeshduCells > 486)
                {
                    this.Height = 50 + otstupY + cellsY * rangeMeshduCells;
                    
                }
                else { this.Height = 480; }

                //numericUpDown1.Value = 50 - otstupY - this.Height / rangeMeshduCells;
            }
            pictureBoxScreen.Width = otstupX + cellsX * rangeMeshduCells;
            pictureBoxScreen.Height = otstupY + cellsY * rangeMeshduCells;
            pictureBoxScreen.Refresh();
        }

        private void HistoryUndo()
        {
            if (historyIndex > 0)
            {
                if (useLayer <= 0)
                {
                    useLayer = historyLength;
                }
                else
                {
                    useLayer--;
                }
                historyIndex--;
            }
            pictureBoxScreen.Refresh();
        }

        private void History()
        {
            if (historyIndex < historyLength)
            {
                historyIndex++;
            }

            if (useLayer >= historyLength)
            {
                nextLayer = 0;
            }
            else
            {
                nextLayer = useLayer + 1;
            }

            for (int x = 0; x <= cellsX; x++)
            {
                for (int y = 0; y <= cellsY; y++)
                {
                    massiv[x, y, nextLayer] = massiv[x, y, useLayer];
                }
            }

            if (useLayer >= historyLength)
            {
                useLayer = 0;
            }
            else
            {
                useLayer++;
            }


        }

        private void PressedButton(MouseEventArgs e)
        {
            try
            {
                curX = (e.X - otstupX) / rangeMeshduCells;
                curY = (e.Y - otstupY) / rangeMeshduCells;
                label2.Text = "x:" + curX;
                label3.Text = "y:" + curY;

                if (pain)
                {
                    if (curX <= cellsX - 1 && curY <= cellsY && curX >= 0 && curY >= 0)
                    {
                        if (e.Button.ToString() == Key1)
                        {
                            massiv[curX, curY, useLayer] = color1;
                        }
                        else if (e.Button.ToString() == Key2)
                        {
                            massiv[curX, curY, useLayer] = color2;
                        }
                    }
                    MassivOut();
                }
            }
            catch { }
        }

        private void MassivFill()
        {
            for (int x = 0; x < cellsX; x++)
            {
                for (int y = 0; y < cellsY; y++)
                {
                    massiv[x, y, useLayer] = color1;
                }
            }
        }

        private void MassivOut()
        {
            if (!checkBox1.Checked)
            {
                if (curHisX1 != curX || curHisY1 != curY)
                {
                    counter++;
                    label4.Text = counter + "";
                    pictureBoxScreen.Refresh();
                }
            }
            curHisX1 = curX;
            curHisY1 = curY;


        }

        private void MassivConvert()
        {
            text = "";
            for (int y = 0; y < cellsY; y++)
            {
                for (int x = 0; x < cellsX; x++)
                {
                    if (massiv[x, y, useLayer] == 0)
                    {
                        text = text + comboBox8.Text;
                    }
                    else if (massiv[x, y, useLayer] == 1)
                    {
                        text = text + comboBox1.Text;
                    }
                    else if (massiv[x, y, useLayer] == 2)
                    {
                        text = text + comboBox2.Text;
                    }
                    else if (massiv[x, y, useLayer] == 3)
                    {
                        text = text + comboBox3.Text;
                    }
                    else if (massiv[x, y, useLayer] == 4)
                    {
                        text = text + comboBox4.Text;
                    }
                    else if (massiv[x, y, useLayer] == 5)
                    {
                        text = text + comboBox5.Text;
                    }
                    else if (massiv[x, y, useLayer] == 6)
                    {
                        text = text + comboBox6.Text;
                    }
                    else if (massiv[x, y, useLayer] == 7)
                    {
                        text = text + comboBox7.Text;
                    }
                    else { }
                }

                text = text + "\r\n";
            }
        }

        private void SaveFile()
        {
            saveFile = "";
            saveFile += "cellsY:" + cellsY + " cellsX:" + cellsX + " Massiv:";

            for (int y = 0; y < cellsY; y++)
            {
                for (int x = 0; x < cellsX; x++)
                {
                    saveFile += massiv[x, y, useLayer];

                }
            }
        }

        private void OpenFile()
        {
            try
            {
                massivImport = openFile.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch { MessageBox.Show("Ошибка открытия"); }
            try
            {
                for (int i = 0; i <= massivImport.Length - 1; i++)
                {
                    importNum = massivImport[i].IndexOf(":");
                    try { importOutput = Convert.ToString(massivImport[i].Substring(importNum + 1)); } catch { }
                    exportName = massivImport[i].Substring(0, importNum);
                    if (exportName == "cellsY")
                    {
                        cellsY = Convert.ToInt32(importOutput);
                    }
                    else if (exportName == "cellsX")
                    {
                        cellsX = Convert.ToInt32(importOutput);
                    }
                    else if (exportName == "Massiv")
                    {
                        importStringMassiv = importOutput;
                    }
                }
                int cNum1 = 0;

                for (int y = 0; y < cellsY; y++)
                {
                    for (int x = 0; x < cellsX; x++)
                    {
                        massiv[x, y, useLayer] = Convert.ToInt32(importStringMassiv.Substring(cNum1, 1));
                        cNum1++;
                    }
                }
            }
            catch { MessageBox.Show("Импортируемое сохранение повреждено"); }
            numericUpDown1.Value = cellsX;
            numericUpDown2.Value = cellsY;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MassivConvert();
            richTextBox1.Text = text;
            MassivOut();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            History();
            MassivFill();
            MassivOut();
            pictureBoxScreen.Refresh();
        }

        private void PRESS(object sender, KeyEventArgs e)
        {

            //              MessageBox.Show(e.KeyCode.ToString(), "Pressed");


        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString() == Key1)
            {
                color1 = 1;
            }
            else if (e.Button.ToString() == Key2)
            {
                color2 = 1;
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString() == Key1)
            {
                color1 = 2;
            }
            else if (e.Button.ToString() == Key2)
            {
                color2 = 2;
            }
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString() == Key1)
            {
                color1 = 3;
            }
            else if (e.Button.ToString() == Key2)
            {
                color2 = 3;
            }
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString() == Key1)
            {
                color1 = 4;
            }
            else if (e.Button.ToString() == Key2)
            {
                color2 = 4;
            }
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                pictureBoxScreen.Refresh();

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Interval = tInterval;
            if (checkBox1.Checked)
            {
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                cellsX = Convert.ToInt32(numericUpDown1.Value);
            }
            catch
            {
                MessageBox.Show("Значение не возможно установить");
            }
            AutoSize();
            pictureBoxScreen.Refresh();

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                cellsY = Convert.ToInt32(numericUpDown2.Value);
            }
            catch
            {
                MessageBox.Show("Значение не возможно установить");
            }
            AutoSize();
            pictureBoxScreen.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //autoSize = true;
            AutoSize();
            //autoSize = false;
        }

        private void загрузитьИзБуфераToolStripMenuItem_Click(object sender, EventArgs e)
        {
            History();
            try
            {
                openFile = Clipboard.GetText();
                OpenFile();
                AutoSize();
                pictureBoxScreen.Refresh();
            }
            catch { MessageBox.Show("Невозможно прочитать сохранение"); }
        }

        private void скопироватьВБуферToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
            try
            {
                Clipboard.SetText(saveFile, TextDataFormat.UnicodeText);
            }
            catch { Clipboard.SetText(" ", TextDataFormat.Text); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            HistoryUndo();
        }

        private void писатьБаквамиИзСмайловToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void письмоБуквамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            autoSize = checkBox2.Checked;
            AutoSize();
        }

        private void созданиеШрифтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Зайдите в Файл>Письмо Emoji, включите галочку \"Создание шрифта\"\n" +
                "Введите в поле \"Буква\" букву или символ который хотите добавить в шрифт. \n" +
                "Далее в форме рисования задайте размер буквы в шрифте (Горизонталь и Вертикаль).\n " +
                "Нарисуйте букву в поле рисования основной формы для письма используйте красный для буквы и серый для фона.\n" +
                "Нажмите \"Добавить\" после добавления всех желаемых букв нажмите \"Сохранить\"\n" +
                "После установите созданый шрифт.\n" +
                "Создание шрифта удалит стандартный шрифт\n " +
                "но после перезапуска програмы он восстановится. ", "Создание шрифта");
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString() == Key1 || e.Button.ToString() == Key2)
            {
                pain = true;
                History();
            }

            PressedButton(e);
            pictureBoxScreen.Refresh();
        }

        private void pictureBoxScreen_MouseMove(object sender, MouseEventArgs e)
        {
            PressedButton(e);
        }

        private void pictureBoxScreen_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString() == Key1 || e.Button.ToString() == Key2)
            {
                pain = false;
            }
            MassivOut();
        }

        private void pictureBoxScreen_Paint(object sender, PaintEventArgs e)
        {
            Painting(e.Graphics);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(richTextBox1.Text, TextDataFormat.UnicodeText);
            }
            catch { Clipboard.SetText(" ", TextDataFormat.Text); }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Text = "Символов: " + richTextBox1.Text.Length;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            History();
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            string fileText = System.IO.File.ReadAllText(filename);
            openFile = fileText;
            OpenFile();
            AutoSize();
            pictureBoxScreen.Refresh();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
            
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(filename, saveFile);
        }

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show("В этой программе вы сможите делать изображения из символов или Emoji\n\n" +
               "Рисовать можно двумя цветами используя две удобные кнопки мыши которые можно настроить.\n" +
               "\n Сетка рисования\nМожно выбрать удобный для вас размер сетки рисования." +
               "\nДля этого в нижней части есть два поля" +
               " X - Горизонталь Y - Вертикаль впишите нужные вам размеры сетки и нажмите клавишу Enter" +
               " Удобный размер для ВК 10х10 Emoji.\n" +
               "Если сделать больше по горизонтали то в мобильной версии ВК будет каша." +
               "\n Для использования сетки больше чем 10х10 раздвиньте форму." +
               "\n Можно изменить размер клеток.\nДля этого перейдите в" +
               " Файл>Настройки измените размер клетки и расстояние между клетками." +
               " Нажмите \"Применить\" и нажмите на поле с клетками для перерисовки" +
               "\nДля конвертации вы можите использовать свои Emoji." +
               "\n\n Заполнение всего поля одним цветом\n Заполнения всего поля выполняется после нажатия" +
               " \"Полное заполнение\" цветом выбраным в  настройках \"Цвет1\"" +
               " обычно он стоит на ЛКМ - левая кнопка мыши. \n\n" +
               " Извените за исчезновение сетки на краткое время при рисовании у меня не получилось это исправить. " +
               "\n\nСоздатель: ilya_GO \nОбратная связь смотрите в настройках","Помощь");

        }

        private void измененияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("v1.0\n Создана программа\n" +
                "\n v1.1\n Исправлен баг при конвертации\n убрана возможность делать не симетричное поле\n" +
                "\n v1.2\n Добавленые функции:\n   Рисования с зажатой кнопкой мыши\n   Изменение кнопки рисования\n   Выбор Emoji из имеющегося списка\n   Счётчик символов\n" +
                "\n v1.3\n Переделана отрисовка поверхности рисования\n Добавленые функции:\n   Не симетричное изменение размера поверхности рисования\n   Настройки:\n      Управления\n      Размера клеток\n      Расстояния между ними\n   Рисование двумя цветами\n   Максимальные размеры поля рисования увеличены до 50х50\n" +
                "\n v1.4\n Изменения:\n   Настройка размера сетки\n Исправлены баги:\n    При изменении настроек они не сразу применялись\n    При рисовании пикселя нельзя было сразуже изменить цвет\n Добавленые функции: \n   CTRL + Z для отмены действия\n   В настройках кнопка \"По умолчанию\"\n   Настройка длинны истории\n   Переключить отрисовку в режим таймер\n   Настройка интервала таймера\n   Изменения вертикали/горизонтали колесом мыши\n   Кнопка очистки поля конвертации" +
                "\n v1.5\n Добавленые функции:\n   Сохранение и открытие шаблона в файл или в буфер обмена\n   Авто изменение размера\n   Письмо буквами из символов\n   Создатель шрифтов для письма\n " +
                "\n v1.6\n Изменения:\n   Оптимизирован интерфейс и поле клеток ", "Изменения");
        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString() == Key1)
            {
                color1 = 5;
            }
            else if (e.Button.ToString() == Key2)
            {
                color2 = 5;
            }
        }

        private void pictureBox6_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString() == Key1)
            {
                color1 = 6;
            }
            else if (e.Button.ToString() == Key2)
            {
                color2 = 6;
            }
        }


        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            panel1.Left = this.Size.Width - panel1.Size.Width - 28;

        }

        private void pictureBox7_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString() == Key1)
            {
                color1 = 7;
            }
            else if (e.Button.ToString() == Key2)
            {
                color2 = 7;
            }
        }

        private void pictureBox8_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString() == Key1)
            {
                color1 = 0;
            }
            else if (e.Button.ToString() == Key2)
            {
                color2 = 0;
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode.ToString() == "Z")
            {
                HistoryUndo();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            graph = this.CreateGraphics();
            timer1.Interval = tInterval;
            timer1.Start();
            openFileDialog1.Filter = "Шаблон ConvertPixel(*.ConPixPattern)|*.ConPixPattern|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Шаблон ConvertPixel(*.ConPixPattern)|*.ConPixPattern|All files(*.*)|*.*";
            //openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //saveFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void настройкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            Form2 Form = new Form2(this);
            Form.ShowDialog();
            
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        public static void Peredacha()
        {
            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            

        }

        public static string[] bluePrint = {
            "A<rd>000000000111000100010010001001000100111110010001001000100000000",
            "B<rd>000000001111000100010010001001111000100010010001001111000000000",
            "C<rd>000000000111000100010010000001000000100000010001000111000000000",
            "D<rd>000000001111000100010010001001000100100010010001001111000000000",
            "E<rd>000000001111100100000010000001111000100000010000001111100000000",
            "F<rd>000000001111100100000010000001111000100000010000001000000000000",
            "G<rd>000000000111000100010010000001011100101010010001000111000000000",
            "H<rd>000000001000100100010010001001111100100010010001001000100000000",
            "I<rd>000000000010000001000000100000010000001000000100000010000000000",
            "J<rd>000000000001000000100000010000001000000100010010000110000000000",
            "K<rd>000000001000100100010010010001110000100100010001001000100000000",
            "L<rd>000000001000000100000010000001000000100000010000001111100000000",
            "M<rd>000000001000100110110010101001010100100010010001001000100000000",
            "N<rd>000000001000100100010011001001010100100110010001001000100000000",
            "O<rd>000000000111000100010010001001000100100010010001000111000000000",
            "P<rd>000000001111000100010010001001111000100000010000001000000000000",
            "Q<rd>000000000111000100010010001001000100101010010011000111100000000",
            "R<rd>000000001111000100010010001001111000100010010001001000100000000",
            "S<rd>000000000111100100000010000000111000000010000001001111000000000",
            "T<rd>000000001111100001000000100000010000001000000100000000000000000",
            "U<rd>000000001000100100010010001001000100100010010001000111000000000",
            "V<rd>000000001000100100010010001001000100100010001010000010000000000",
            "W<rd>000000001000100100010010001001010100101010011011001000100000000",
            "X<rd>000000001000100100010001010000010000010100010001001000100000000",
            "Y<rd>000000001000100100010001010000010000001000000100000010000000000",
            "Z<rd>000000001111100000010000010000010000010000010000001111100000000",
            "a<rd>000000000000000000000001110000000100011110010001000111100000000",
            "b<rd>000000001000000100000010000001111000100010010001001111000000000",
            "c<rd>000000000000000000000001110001000100100000010001000111000000000",
            "d<rd>000000000000100000010000001000111100100010010001000111100000000",
            "e<rd>000000000000000000000001110001000100111110010000000111100000000",
            "f<rd>000000000011000010010001000001110000010000001000000100000000000",
            "g<rd>000000000000000000000001110001000100011110000001000111000000000",
            "h<rd>000000001000000100000010000001111000100010010001001000100000000",
            "i<rd>000000000010000000000001100000010000001000000100000111000000000",
            "j<rd>000000000001000000000000110000001000000100010010000110000000000",
            "k<rd>000000001000000100000010001001001000111000010010001000100000000",
            "l<rd>000000000110000001000000100000010000001000000100000111000000000",
            "m<rd>000000000000000000000011110001010100101010010101001010100000000",
            "n<rd>000000000000000000000011110001000100100010010001001000100000000",
            "o<rd>000000000000000000000001110001000100100010010001000111000000000",
            "p<rd>000000000000000000000011100001001000111000010000001000000000000",
            "q<rd>000000000000000000000000111000100100001110000001000000100000000",
            "r<rd>000000000000000000000010111001100000100000010000001000000000000",
            "s<rd>000000000000000000000001111001000000011100000001001111000000000",
            "t<rd>000000000010000001000001110000010000001000000100000001100000000",
            "u<rd>000000000000000000000010001001000100100010010011000110100000000",
            "v<rd>000000000000000000000010001001000100100010001010000010000000000",
            "w<rd>000000000000000000000010001001000100101010011111001010100000000",
            "x<rd>000000000000000000000010001000101000001000001010001000100000000",
            "y<rd>000000000000000000000010001001000100011110000001001111000000000",
            "z<rd>000000000000000000000011111000001000001000001000001111100000000",
            "0<rd>000000000111000100010010011001010100110010010001000111000000000",
            "1<rd>000000000010000011000010100000010000001000000100001111100000000",
            "2<rd>000000000111000100010000001000011000010000010000001111100000000",
            "3<rd>000000000111000100010000001000011000000010010001000111000000000",
            "4<rd>000000000001000001100001010001001000111110000010000001000000000",
            "5<rd>000000001111100100000011110000000100000010010001000111000000000",
            "6<rd>000000000111000100010010000001111000100010010001000111000000000",
            "7<rd>000000001111100000010000010000010000001000000100000010000000000",
            "8<rd>000000000111000100010010001000111000100010010001000111000000000",
            "9<rd>000000000111000100010010001000111100000010010001000111000000000",
            "&<rd>000000000100000101000010100000100000101010010010000110100000000",
            "`<rd>000000000010000001000000000000000000000000000000000000000000000",
            "(<rd>000000000001000001000000100000010000001000000100000001000000000",
            ")<rd>000000000100000001000000100000010000001000000100000100000000000",
            "*<rd>000000000000000101010001110001111100011100010101000000000000000",
            "+<rd>000000000000000001000000100001111100001000000100000000000000000",
            "-<rd>000000000000000000000000000000111000000000000000000000000000000",
            "=<rd>000000000000000000000001110000000000011100000000000000000000000",
            ".<rd>000000000000000000000000000000000000000000000000000010000000000",
            "!<rd>000000000010000001000000100000010000001000000000000010000000000",
            "\"<rd>000000000101000010100000000000000000000000000000000000000000000",
            "#<rd>000000000101000010100011111000101000111110001010000101000000000",
            "$<rd>000000000010000011110010100000111000001010011110000010000000000",
            "%<rd>000000001100000110010000010000010000010000010011000001100000000",
            "^<rd>000000000010000010100010001000000000000000000000000000000000000",
            ",<rd>000000000000000000000000000000000000001000000100000100000000000",
            ":<rd>000000000000000000000000100000000000001000000000000000000000000",
            ";<rd>000000000000000000000000100000000000001000001000000000000000000",
            "?<rd>000000000111000100010000001000001000001000000000000010000000000",
            "@<rd>000000000111000100010010111001010100101100010000000111100000000",
            "/<rd>000000000000000000010000010000010000010000010000000000000000000",
            "<<rd>000000000001000001000001000001000000010000000100000001000000000",
            "><rd>000000000100000001000000010000000100000100000100000100000000000",
            "|<rd>000000000010000001000000100000010000001000000100000010000000000",
            "\\<rd>000000000000000100000001000000010000000100000001000000000000000",
            "€<rd>000000000011100010000011110000100000111100001000000011100000000",
            "£<rd>000000000011000010010001000001111000010000001000001111100000000",
            "[<rd>000000000011000001000000100000010000001000000100000011000000000",
            "]<rd>000000000110000001000000100000010000001000000100000110000000000",
            "{<rd>000000000001000001000000100000100000001000000100000001000000000",
            "}<rd>000000000100000001000000100000001000001000000100000100000000000",
            "_<rd>000000000000000000000000000000000000000000000000001111100000000",
            "█<rd>000000001111100111110011111001111100111110011111001111100000000",
            "░<rd>000000000000000000000000000000000000000000000000000000000000000" };
    }
}
