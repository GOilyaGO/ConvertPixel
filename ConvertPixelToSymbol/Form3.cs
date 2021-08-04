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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        
        string[] strMass, latterChars, massivImport;
        string strIN, rm = "<rm>", rd = "<rd>", later, print, text, openFile, importOutput, exportName, openFileStrimgMassiv;
        string saveFile, fileText, proOutTextTwo;
        int rdLocation, xlatterValue = 7, yLatterValue = 9, importNum;
        bool openMessege;

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            label7.Text = "Символы:" + richTextBox1.Text.Length;
        }

        private void OpenFile()
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = openFileDialog1.FileName;
                fileText = System.IO.File.ReadAllText(filename);
            }
            catch { MessageBox.Show("Ошибка открытия файла"); }
        }

        private void SaveFile()
        {
            try
            {
                proOutTextTwo = richTextBox3.Text;
                richTextBox3.Text.Remove(richTextBox3.Text.Length - 4);
                saveFile = "";
                saveFile += "yLatterValue<:>" + Form1.cellsY + " xlatterValue<:>" + Form1.cellsX + " Massiv<:>" + richTextBox3.Text;
            }
            catch { MessageBox.Show("Вы сохраните пустой файл"); }
        }

        private void BluePrintCreating()
        {
            try
            {

                Form1.bluePrint = openFileStrimgMassiv.Split(new string[] { rm }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch { MessageBox.Show("Сортировка не удалась"); }
        }

        private void Sorting()
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
                    importNum = massivImport[i].IndexOf("<:>");
                    try { importOutput = Convert.ToString(massivImport[i].Substring(importNum + 3)); } catch { }
                    exportName = massivImport[i].Substring(0, importNum);
                    if (exportName == "yLatterValue")
                    {
                        yLatterValue = Convert.ToInt32(importOutput);
                    }
                    else if (exportName == "xlatterValue")
                    {
                        xlatterValue = Convert.ToInt32(importOutput);
                    }
                    else if (exportName == "Massiv")
                    {
                        openFileStrimgMassiv = importOutput;
                    }
                }
            }
            catch { MessageBox.Show("Ошибка сортировки"); }
        }

        private void Convertion()
        {
            richTextBox1.Text = "";
            int t = 0, laterTime;
            strMass = new string[strIN.Length];
            latterChars = new string[strIN.Length];
            try
            {
                //Превращение строки в массив
                for (int i = 0; i < strIN.Length; i++)
                {
                    strMass[i] = strIN.Substring(i, 1);
                }

                //Сравнивание надписи с шаблоном и вывод шаблона
                for (int i = 0; i < strMass.Length; i++)
                {
                    for (int u = 0; u < Form1.bluePrint.Length; u++)
                    {
                        text = "";
                        rdLocation = Form1.bluePrint[u].IndexOf(rd);
                        later = Form1.bluePrint[u].Substring(0, 1);
                        print = Form1.bluePrint[u].Substring(rdLocation + rd.Length);
                        if (strMass[i] == later)
                        {
                            t = 0;
                            for (int y = 0; y < yLatterValue; y++)
                            {
                                for (int x = 0; x < xlatterValue; x++)
                                {
                                    laterTime = Convert.ToInt32(print.Substring(t, 1));
                                    if (laterTime == 0)
                                    {
                                        text += comboBox2.Text;
                                    }
                                    else if (laterTime == 1)
                                    {
                                        text += comboBox1.Text;
                                    }
                                    t++;
                                }
                                text += "\n";
                            }

                            latterChars[i] = text;

                        }
                        
                    }
                }

                for(int f = 0; f < latterChars.Length; f++)
                {
                    richTextBox1.Text += latterChars[f];

                    if (checkBox1.Checked)
                    {
                        richTextBox1.Text += "\n";
                    }
                }
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = openFileDialog1.FileName;
                fileText = System.IO.File.ReadAllText(filename);
                richTextBox3.Text = fileText.Substring(fileText.IndexOf("Massiv<:>") + 9);
                richTextBox3.Text = richTextBox3.Text + "<rm>";
            }
            catch { MessageBox.Show("Не удалось открыть, возможно не указан путь к объекту"); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveFile();
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(filename, saveFile);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int rmIndex, latterIndex;
                latterIndex = richTextBox3.Text.IndexOf(textBox2.Text);
                rmIndex = richTextBox3.Text.IndexOf(rm, latterIndex);
                richTextBox3.Text = richTextBox3.Text.Remove(latterIndex, rmIndex - latterIndex + rm.Length);
            }
            catch { MessageBox.Show("Нечего удалять"); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox3.Text += textBox2.Text + "<rd>";

            for (int y = 0; y < Form1.cellsY; y++)
            {
                for (int x = 0; x < Form1.cellsX; x++)
                {
                    richTextBox3.Text += Form1.massiv[x, y, Form1.useLayer];

                }
            }
            richTextBox3.Text += "<rm>";
            richTextBox3.Text = richTextBox3.Text;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            label4.Visible = checkBox2.Checked;
            label5.Visible = checkBox2.Checked;
            textBox2.Visible = checkBox2.Checked;
            button5.Visible = checkBox2.Checked;
            button6.Visible = checkBox2.Checked;
            button7.Visible = checkBox2.Checked;
            button8.Visible = checkBox2.Checked;
            richTextBox2.Visible = checkBox2.Checked;
            richTextBox3.Visible = checkBox2.Checked;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = openFileDialog1.FileName;
                fileText = System.IO.File.ReadAllText(filename);
                openFile = fileText;
                Sorting();
                BluePrintCreating();
            }
            catch { MessageBox.Show("Невозможно открыть файл"); }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //  bluePrint = Form1.defaultblueprintMass;
            openFileDialog1.Filter = "Text files(*.ConPixLetter)| *.ConPixLetter|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.ConPixLetter)|*.ConPixLetter|All files(*.*)|*.*";
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Text = latterChars[Convert.ToInt32(numericUpDown1.Value) - 1];
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Text = "";
                for (int f = 0; f < latterChars.Length; f++)
                {
                    richTextBox1.Text += latterChars[f];

                    if (checkBox1.Checked)
                    {
                        richTextBox1.Text += "\n";
                    }
                }
            }
            catch { MessageBox.Show("Нечего отображать"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(richTextBox1.Text, TextDataFormat.UnicodeText);
            }
            catch { MessageBox.Show("Копирование не удалось"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            strIN = textBox1.Text.Replace(" ", "░");
            Convertion();
            try
            {
                numericUpDown1.Maximum = latterChars.Length;
                numericUpDown1.Value = 1;
            }
            catch { MessageBox.Show("Окно текст не может быть пустым"); }
        }
    }
}
