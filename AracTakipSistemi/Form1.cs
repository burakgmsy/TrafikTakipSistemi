using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AracTakipSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public string idctrl;
        public string passwordctrl;


        public void gecis()
        {
            main frm = new main();
            frm.Show();
            this.Hide();
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            iletisim frm = new iletisim();
            frm.Show();

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            string id = "admin";
            string password = "123";

            idctrl = textBox1.Text;
            passwordctrl = textBox2.Text;

            if (id == idctrl && password == passwordctrl)
            {
                gecis();

            }




            else
            {
                MessageBox.Show("Hatalı Giriş!...");
            }






        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }
    }
}

