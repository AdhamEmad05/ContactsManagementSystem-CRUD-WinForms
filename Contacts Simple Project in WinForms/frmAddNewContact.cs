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
    public partial class frmAddNewContact : Form
    {
        private enum enMode { AddNewContact = 0, UpdateContact = 1 }
        private enMode _Mode;

        private int _ContactID;
        private clsContact contact;

        public frmAddNewContact(int ContactID)
        {
            InitializeComponent();
            
            _ContactID = ContactID;

            this.KeyPreview = true;

            if (_ContactID == -1)
                _Mode = enMode.AddNewContact;
            else
                _Mode = enMode.UpdateContact;
        }
   

        private void frmAddNewContact_Load(object sender, EventArgs e)
        {
            // Loading Data to Form

            LoadData();
        }

        private void LoadData()
        {
            DataTable DT = clsCountries.GetAllCountries();
            foreach (DataRow Row in DT.Rows)
            {
                combobxCountry.Items.Add(Row["CountryName"]);
            }


            if (_Mode == enMode.UpdateContact)
            {
                contact = clsContact.Find(_ContactID);

                label9.Text = "   Edit Contact";
                txtID.Text = contact.ContactID.ToString();
                txtFirstName.Text = contact.FirstName;
                txtLastName.Text = contact.LastName;
                txtEmail.Text = contact.Email;
                txtPhone.Text = contact.Phone;
                txtAddress.Text = contact.Address;
                dateTimePicker1.Value = contact.DateOfBirth;
                combobxCountry.SelectedIndex = combobxCountry.FindString(clsCountries.Find(contact.countryID).CountryName);

                try
                {
                    pictureBox1.Load(contact.ImagePath);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message, "Error");
                    pictureBox1.ImageLocation = "";
                }

                linkLabel2.Visible = !(string.IsNullOrEmpty(pictureBox1.ImageLocation));

            }
            else
            {
                contact = new clsContact();

                label9.Text = "Add New Contact";

                linkLabel2.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // "Save" Button

            contact.FirstName = txtFirstName.Text;
            contact.LastName = txtLastName.Text;
            contact.Email = txtEmail.Text;
            contact.Phone = txtPhone.Text;
            contact.Address = txtAddress.Text;
            contact.DateOfBirth = dateTimePicker1.Value;
            contact.countryID = clsCountries.Find(combobxCountry.Text).CountryID;

            if (pictureBox1.ImageLocation == null)
                contact.ImagePath = "";
            else
                contact.ImagePath = pictureBox1.ImageLocation;

            if (contact.Save())
            {
                MessageBox.Show("Contact Saved Successfully", "Saving Contact");

                txtID.Text = contact.ContactID.ToString();
                txtFirstName.Text = contact.FirstName;
                txtLastName.Text = contact.LastName;
                txtEmail.Text = contact.Email;
                txtPhone.Text = contact.Phone;
                txtAddress.Text = contact.Address;

                label9.Text = "   Edit Contact";

                _Mode = enMode.UpdateContact;
            }
            else
                MessageBox.Show("Failed Saving Contact", "Saving Contact");

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Multiselect = false;

            openFileDialog1.InitialDirectory = "C:\\Users\\Adham\\Desktop";

            string Filter = "All files (*.*)|*.*|" + 
                            "jpeg files (*.jpeg)|*.jpeg|" +
                            "jpg files (*.jpg)|*.jpg";

            openFileDialog1.Filter = Filter;


            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;
                pictureBox1.ImageLocation = selectedFilePath;
                
                //pictureBox1.Load(selectedFilePath);
            }

            linkLabel2.Visible = !(string.IsNullOrEmpty(pictureBox1.ImageLocation));

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.ImageLocation = null;

            linkLabel2.Visible = false;
        }

        private void frmAddNewContact_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
