using BuyTickets.DB;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuyTickets.Models;

namespace BuyTickets
{
    public partial class Register : MaterialForm
    {
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
            string err = null;
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
                if (err == null)
                {
                    if (!Database.UserOnBase(materialSingleLineTextField2.Text,
                        materialSingleLineTextField1.Text))
                    {
                        User user = new User
                        {
                            Login = materialSingleLineTextField2.Text,
                            Mail = materialSingleLineTextField1.Text,
                            Password = materialSingleLineTextField3.Text,
                            Name = materialSingleLineTextField4.Text,
                            Surname = materialSingleLineTextField5.Text,
                            Phone = materialSingleLineTextField6.Text,
                            Balance = 0,
                            IsAdmin = false
                        };
                        Database.Registration(user);
                        Main form = new Main(user);
                        form.Show();
                        this.Visible = false;
                    }
                    else MessageBox.Show("Данный логин или e-mail занят.");
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