using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string openedFilePath = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private List<(string Name, double Grade)> ParseStudentList()
        {
            var students = new List<(string Name, double Grade)>();
            foreach (var line in textBox1.Lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var parts = line.Split(' ');
                    if (parts.Length >= 3 && double.TryParse(parts[2], out double grade))
                    {
                        string fullName = $"{parts[0]} {parts[1]}";
                        students.Add((fullName, grade));
                    }
                }
            }
            return students;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "..\\",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
                openedFilePath = openFileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = "..\\",
                Filter = "Text files (*.txt)|*.txt"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, textBox1.Text, Encoding.UTF8);
                MessageBox.Show("Failas sėkmingai išsaugotas.", "Informacija", MessageBoxButtons.OK);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(openedFilePath))
            {
                File.WriteAllText(openedFilePath, textBox1.Text, Encoding.UTF8);
                MessageBox.Show("Failas sėkmingai išsaugotas.", "Informacija", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Nėra atidaryto failo. Naudokite 'Save As'.", "Klaida", MessageBoxButtons.OK);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var students = ParseStudentList();

            var kietiakiai = students.Where(s => s.Grade >= 5.0).OrderByDescending(s => s.Grade).ToList();

            textBox1.Clear();
            textBox1.AppendText("Kietiakiai:\r\n");
            foreach (var student in kietiakiai)
            {
                textBox1.AppendText($"{student.Name} {student.Grade}\r\n");
            }

            MessageBox.Show("Surūšiuota ir rodomi tik kietiakiai.", "Informacija", MessageBoxButtons.OK);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var students = ParseStudentList();

            var vargsiukai = students.Where(s => s.Grade < 5.0).OrderBy(s => s.Grade).ToList();

            textBox1.Clear();
            textBox1.AppendText("Vargšiukai:\r\n");
            foreach (var student in vargsiukai)
            {
                textBox1.AppendText($"{student.Name} {student.Grade}\r\n");
            }

            MessageBox.Show("Rodyti tik vargšiukai.", "Informacija", MessageBoxButtons.OK);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ši programa rūšiuoja ir filtruoja studentų duomenis.",
                            "Apie programą", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string[] firstNames = { "Jonas", "Ona", "Petras", "Marija", "Andrius", "Rūta", "Simas", "Lina", "Tomas", "Eglė" };
            string[] lastNames = { "Jonaitis", "Onaitė", "Petrauskas", "Mariutė", "Andriušis", "Rutkaitė", "Simaitis", "Linaitė", "Tomauskas", "Eglinskaitė" };

            Random random = new Random();
            textBox1.Clear();

            for (int i = 0; i < 100; i++)
            {
                string firstName = firstNames[random.Next(firstNames.Length)];
                string lastName = lastNames[random.Next(lastNames.Length)];
                double grade = Math.Round(random.NextDouble() * 10, 2);

                textBox1.AppendText($"{firstName} {lastName} {grade}\r\n");
            }

            MessageBox.Show("Sugeneruotas 100 studentų sąrašas!", "Informacija", MessageBoxButtons.OK);
        }

        private void SaveFile(string fileName, List<(string Name, double Grade)> students)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var student in students)
                {
                    writer.WriteLine($"{student.Name} {student.Grade}");
                }
            }
        }
    }
}
