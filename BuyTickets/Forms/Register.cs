using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using System.Data.SQLite;

namespace BuyTickets
{
    public partial class Register : MaterialForm
    {
        public void Registration (string Mail, string Login, string Pass, string Name, string Surname, string Phone)
        {
            const string databaseName = @"..\..\DB\BT.sqlite";
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            connection.Open();
                SQLiteCommand cmd = new SQLiteCommand("INSERT INTO 'Users' (Login, Password, Name, Surname, Phone, Mail, isAdmin)"
                + "VALUES (@LoginParam, @PasswordParam, @NameParam, @SurnameParam, @PhoneParam, @MailParam, @isAdmin)", connection);
                cmd.Parameters.AddWithValue("@LoginParam", Login);
                cmd.Parameters.AddWithValue("@PasswordParam", Pass);
                cmd.Parameters.AddWithValue("@NameParam", Name);
                cmd.Parameters.AddWithValue("@SurnameParam", Surname);
                cmd.Parameters.AddWithValue("@PhoneParam", Phone);
                cmd.Parameters.AddWithValue("@MailParam", Mail);
                cmd.Parameters.AddWithValue("@isAdmin", false);
                cmd.ExecuteNonQuery();
            connection.Close();
        }
        public Register()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            string err = "";
            if (materialSingleLineTextField1.TextLength < 4)
                err += "E-mail должен состоять минимум из 4 символов" + Environment.NewLine;
            if (materialSingleLineTextField2.TextLength < 5)
                err += "Логин должен состоять минимум из 5 символов" + Environment.NewLine;
            if (materialSingleLineTextField3.TextLength < 6)
                err += "Пароль должен состоять минимум из 6 символов" + Environment.NewLine;
            if (materialSingleLineTextField4.TextLength < 2)
                err += "Имя должно состоять минимум из 2 символов" + Environment.NewLine;
            if (materialSingleLineTextField5.TextLength < 2)
                err += "Фамилия должна состоять минимум из 2 символов" + Environment.NewLine;
            if (materialSingleLineTextField6.TextLength < 2)
                err += "Телефон должен состоять минимум из 5 символов" + Environment.NewLine;

            if (materialCheckBox1.Checked == true)
            {
                if (err == "")
                {
                    bool canRegister = false;
                    const string databaseName = @"..\..\DB\BT.sqlite";
                    SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
                    connection.Open();
                    SQLiteCommand TestForDup = new SQLiteCommand("SELECT COUNT(*) FROM Users WHERE Login = '" + materialSingleLineTextField2.Text + "' OR Mail = '" + materialSingleLineTextField1.Text + "'", connection);
                    SQLiteDataReader reader = TestForDup.ExecuteReader();
                    while (reader.Read())
                    {
                        if (Convert.ToInt16(reader[0]) == 0)
                            canRegister = true;
                    }
                    connection.Close();

                    if (canRegister)
                    {
                        Registration(materialSingleLineTextField2.Text,
                            materialSingleLineTextField1.Text,
                            materialSingleLineTextField3.Text,
                            materialSingleLineTextField4.Text,
                            materialSingleLineTextField5.Text,
                            materialSingleLineTextField6.Text);
                        Main form = new Main();
                        form.Show();
                        this.Visible = false;
                    }
                    else MessageBox.Show("Логин или E-mail уже зарегистрированы.");
                }
                else MessageBox.Show(err);
            }
            else MessageBox.Show("Хорошая попытка, робот!");
        }


        private void Register_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
