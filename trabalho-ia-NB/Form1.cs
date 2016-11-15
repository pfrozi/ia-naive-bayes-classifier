using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IA.TrabalhoNB.UI
{
    public partial class Form1 : Form
    {

        List<FileClass> listFiles;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listFiles = new List<FileClass>();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnAddClass_Click(object sender, EventArgs e)
        {
            lstClasses.Items.Add(txtClassName.Text);

            string[] items = new string[lstClasses.Items.Count];
            lstClasses.Items.CopyTo(items, 0);

            cmbClass.Items.Clear();
            cmbClass.Items.AddRange(items);

            txtClassName.Text = "";
        }

        private void btnRemoveClass_Click(object sender, EventArgs e)
        {
            lstClasses.Items.Remove(lstClasses.SelectedItem);

            string[] items = new string[lstClasses.Items.Count];
            lstClasses.Items.CopyTo(items, 0);

            cmbClass.Items.Clear();
            cmbClass.Items.AddRange(items);
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            listFiles.Add(new FileClass { Path = txtFile.Text  , MyClass = cmbClass.Text });

            lvFiles.DataSource = null;
            lvFiles.DataSource = listFiles;
            
        }

    }
}
