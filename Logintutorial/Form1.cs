using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;

namespace C__Login_tutorial
{


    public partial class Tutorial : Form

    {
        SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-7IEOM2U\SQLEXPRESS;Initial Catalog=employee;Integrated Security=True;Connect Timeout=30");

        public Tutorial()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void password_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            password_textbox.PasswordChar = password_checkbox.Checked ? '0' : '*';
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_Click(object sender, EventArgs e)
        {
        }

        private void username_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void register_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void register_button_Click(object sender, EventArgs e)
        {

            if (username_textbox.Text == ""
               || password_textbox.Text == "")
            {
                MessageBox.Show("Please fill all blank fields"
                    , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State != ConnectionState.Open)
                {
                    try
                    {
                        connect.Open();
                        // TO CHECK IF THE USER IS EXISTING ALREADY
                        string selectUsername = "SELECT COUNT(id) FROM users WHERE username = @users";

                        using (SqlCommand checkUser = new SqlCommand(selectUsername, connect))
                        {
                            checkUser.Parameters.AddWithValue("@users", username_textbox.Text.Trim());
                            int count = (int)checkUser.ExecuteScalar();

                            if (count >= 1)
                            {
                                MessageBox.Show(username_textbox.Text.Trim() + " is already taken"
                                   , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                DateTime today = DateTime.Today;

                                string insertData = "INSERT INTO users " +
                                    "(username, password, date_register) " +
                                    "VALUES(@username, @password, @dateReg)";

                                using (SqlCommand cmd = new SqlCommand(insertData, connect))
                                {
                                    cmd.Parameters.AddWithValue("@username", username_textbox.Text.Trim());
                                    cmd.Parameters.AddWithValue("@password", password_textbox.Text.Trim());
                                    cmd.Parameters.AddWithValue("@dateReg", today);

                                    cmd.ExecuteNonQuery();

                                    MessageBox.Show("Registered successfully!"
                                        , "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex
                    , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }
    }
}




