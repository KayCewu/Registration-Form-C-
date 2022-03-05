using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
//Import the SqlClient
using System.Data.SqlClient;

namespace CRegistrationForm
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        // Declare the connection string
        string SQL;
        public string ConStrr = "Data Source=LAPTOP-4IS5CG1L;Initial Catalog=CeeRegTable;Integrated Security=True";
        SqlConnection Con=new SqlConnection();

        private void Form1_Load(object sender, EventArgs e)
        {
            //When the form loads, load the following info to the comboboxes
            cmbNationality.Items.Add("South African");
            cmbNationality.Items.Add("Zimbabwean");
            cmbNationality.Items.Add("Lesotho");
            cmbNationality.Items.Add("Nigerian");
            cmbNationality.Items.Add("Swati");
            cmbNationality.Items.Add("Other");
            cmbTitle.Items.Add("Mr");
            cmbTitle.Items.Add("Mrs");
            cmbTitle.Items.Add("Ms");
            cmbTitle.Items.Add("Dr");
            this.cmbTitle.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbNationality.DropDownStyle = ComboBoxStyle.DropDownList;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Input validations
            if ("".Equals(txtName.Text))
            {
                MessageBox.Show("Name left blank", "Error", 0, MessageBoxIcon.Error);
                return;
            }
            if ("".Equals(txtID.Text))
            {
                MessageBox.Show("ID Number left blank", "Error", 0, MessageBoxIcon.Error);
                return;
            }
            if ("".Equals(txtSur.Text))
            {
                MessageBox.Show("Surname left blank", "Error", 0, MessageBoxIcon.Error);
                return;
            }
            if ("".Equals(cmbTitle.Text))
            {
                MessageBox.Show("Title left blank", "Error", 0, MessageBoxIcon.Error);
                return;
            }
            if ("".Equals(cmbNationality.Text))
            {
                MessageBox.Show("Nationality left blank", "Error", 0, MessageBoxIcon.Error);
                return;
            }
            if ("".Equals(MsktxtNumber.Text))
            {
                MessageBox.Show("Phone number left blank", "Error", 0, MessageBoxIcon.Error);
                return;
            }
            //Create a regular expression to check for any incorrect input
            Regex reg = new Regex("[^a-zA-Z]");
            bool hasSpeChar = reg.IsMatch(txtName.Text.ToString()) || reg.IsMatch(txtSur.Text.ToString());
            if (hasSpeChar == true)
            {
                MessageBox.Show("Cannot contain special characters or numbers", "Error", 0, MessageBoxIcon.Error);
                return;
            }
            if (txtID.Text.Length != 13)
            {
                MessageBox.Show("ID number has to be 13 characters long", "Error", 0, MessageBoxIcon.Error);
                txtID.Clear();
                txtID.Focus();
                return;
            }
            //We now prepare for database connection
            SqlCommand cmd = new SqlCommand();
            try
            {
                SQL = "Insert into regdetails(FName, SName, IdentityNo, Title, Nationality, PhoneNo) values ('" + txtName.Text + "','" + txtSur.Text + "','" + txtID.Text + "','" + cmbTitle.Text + "','" + cmbNationality.Text + "','" + MsktxtNumber.Text + "')";
                Con = new SqlConnection(ConStrr);
                cmd = new SqlCommand(SQL, Con);
                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }
                cmd.CommandText = SQL;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registered Successfully", "Congratulations", 0, MessageBoxIcon.Information);
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Record not added", "Error", 0, MessageBoxIcon.Error);

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
