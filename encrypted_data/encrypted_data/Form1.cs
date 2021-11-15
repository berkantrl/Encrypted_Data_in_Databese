using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace encrypted_data
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-67U2HEQ\SQLEXPRESS;Initial Catalog=encryptdata; Integrated Security = True");
        void list()
        {
            SqlDataAdapter data = new SqlDataAdapter("Select * From TBLDATAS", connection);
            DataTable dataTable = new DataTable();
            data.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void buttonsave_Click(object sender, EventArgs e)
        {
            string name = txtname.Text;
            string encrypted_name = encrypt(name);
            
            string surname = txtsurname.Text;
            string encrypted_surname = encrypt(surname);

            string mail = txtmail.Text;
            string encrypted_mail = encrypt(mail);

            string password = txtpassword.Text;
            string encrypted_password = encrypt(password);

            string Anumber = txtnumber.Text;
            string encrypted_Anumber = encrypt(Anumber);


            connection.Open();
            SqlCommand command = new SqlCommand("Insert Into TBLDATAS (NAME, SURNAME, MAIL, PASSWORD, ACCOUNTNUMBER) values (@p1,@p2,@p3,@p4,@p5)", connection);
            command.Parameters.AddWithValue("@p1", encrypted_name);
            command.Parameters.AddWithValue("@p2", encrypted_surname);
            command.Parameters.AddWithValue("@p3", encrypted_mail);
            command.Parameters.AddWithValue("@p4", encrypted_password);
            command.Parameters.AddWithValue("@p5", encrypted_Anumber);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Data Added");

        }

        public string encrypt(string data)
        {

            char[] data_array = data.ToCharArray();
            string characters = "ABCÇDEFGHJKLIİJKLMNOÖPRSŞTUÜVYZWXQabcdefghıijklmnoöprsştuüvyzwxq!'+-.,";
            char[] character = characters.ToCharArray();
            string[] elementsymbols = {"He","Li","Be","Ne","Na", "Mg", "Al","Si","Cl","Ar","Ca","Sc","Ti","Cr", "Mn","Fe","Co","Ni","Cu","Zn","Ga","Ge","As","Se","Br","Kr","Rb",
                "Sr","Zr","Nb","Mo","Tc","Ru","Rh","Pd","Ag","Cd","In","Sn","Sb","Te","Xe","Cs","Ba","La","Ce","Pr","Nd","Pm","Sm","Eu","Gd","Tb","Dy","Ho","Er","Tm","Yb","Lu",
                "Hf","Ta","Re","Os","Ir","Pt","Au","Hg","Tl","Pb","Bi"};
            string encrytpdata = "";
            for(int i = 0; i < data_array.Length; i++) { 
                for (int j = 0; j < characters.Length; j++)
                {
                    if (data_array[i] == characters[j]){
                        encrytpdata += elementsymbols[j];


                    }
                }
                data = encrytpdata;
            }
            return data;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            list();
        }
    }
}
