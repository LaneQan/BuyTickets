using System;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using BuyTickets.DB;

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
                    Database DB = new Database();
                    if (DB.canRegisterOrNot(materialSingleLineTextField2.Text, materialSingleLineTextField1.Text))
                    {
                        DB.Registration(materialSingleLineTextField2.Text,
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
