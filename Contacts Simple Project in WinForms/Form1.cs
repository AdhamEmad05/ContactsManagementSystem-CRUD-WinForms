using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsBusinessLayer;

namespace ContactsWinFormPresentaionLayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.KeyPreview = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddNewContact frm = new frmAddNewContact(-1);
            frm.ShowDialog();

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            dataGridView1.DataSource = clsContact.GetAllContacts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Contact with ContactID = " + dataGridView1.CurrentRow.Cells[0].Value, "Deleting Contact", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (clsContact.DeleteContact((int)dataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Contact Deleted Successfully!");
                }
                else
                    MessageBox.Show("Deleting Failed!");


                RefreshGrid();
            }

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewContact frm = new frmAddNewContact((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            RefreshGrid();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
