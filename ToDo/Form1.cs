using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDo
{
    public partial class FrmKezdo : Form
    {
        private List<string> todos = new List<string>();
        public FrmKezdo()
        {
            InitializeComponent();
            StreamReader sr = new StreamReader("todoitems.txt");

            while (!sr.EndOfStream)
            {
                todos.Add(sr.ReadLine());
            }

            sr.Close();
        }

        private void mKilepes_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmKezdo_Shown(object sender, EventArgs e)
        {
            textBox1.Text = "";
            listBox1.Items.Clear();
            textBox1.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnListabol_Click(object sender, EventArgs e)
        {
            frmLista formLista = new frmLista(todos);  //a frmLista konstruktor egy metódus

            var result = formLista.ShowDialog();

            if (result == DialogResult.OK)
            {
                textBox1.Text = formLista.SelectedTodo;
            }

            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.SelectionLength = 0;
        }

        private void btnFelvitel_Click(object sender, EventArgs e)
        {
            string todo = textBox1.Text.Trim();

            if (todo != "" && !listBox1.Items.Contains(todo))
            {
                listBox1.Items.Add(todo);
                textBox1.Text = "";
            }

            //if (textBox1.Text != "")
            //{
            //    listBox1.Items.Add(textBox1.Text);
            //    textBox1.Text = "";
            //}
        }

        private void btnEltavolit_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void btnKivesz_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;

            if (index > -1)
            {
                listBox1.Items.RemoveAt(index);
            }
            else
            {
                MessageBox.Show("Válassz ki egy elemet!", "Nincs kiválasztva!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void mbeolvas_Click(object sender, EventArgs e)
        {
            if (ofdMegnyitas.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();

                StreamReader sr = new StreamReader(ofdMegnyitas.FileName);

                while (!sr.EndOfStream)
                {
                    listBox1.Items.Add(sr.ReadLine());
                }

                sr.Close();

                MessageBox.Show("Sikeres beolvasás!", "Beolvasás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string FileNameDate()
        {
            var datum = DateTime.Now;
            string ev = datum.Year.ToString();
            string honap = "";

            if (datum.Month < 10)
            {
                honap = "0" + datum.Month.ToString();
            }
            else
            {
                honap = datum.Month.ToString();
            }

            string nap = "";
            if (datum.Day < 10) 
            {
                nap = "0" + datum.Day.ToString();
            }
            else
            {
                nap = datum.Day.ToString();
            }

            return ev + honap + nap;
        }

        private void mMentes_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                sfdMentes.FileName = FileNameDate();
                if (sfdMentes.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(sfdMentes.FileName);

                    foreach (var item in listBox1.Items)
                    {
                        sw.WriteLine(item);
                    }

                    sw.Close();

                    MessageBox.Show("Sikeres mentés!", "Mentés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Nincs mit menteni!", "Üres lista!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
