using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace AracTakipSistemi
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\database.mdb");
        OleDbCommand komut = new OleDbCommand();
        DataTable tablo = new DataTable();
        DateTime thisDay = DateTime.Today;



        public void listele()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";
            textBox10.Text = "";
            textBox6.Text = "";
            tablo.Clear();
            baglan.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * from sorgu", baglan);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            dataGridView1.Columns[0].Visible = false;
            baglan.Close();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            sınır();
            listele();
        }

        public void Ara()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";
            textBox6.Text = "";
            textBox10.Text = "";


            tablo.Clear();
            baglan.Open();
            OleDbCommand komut = new OleDbCommand("select * from sorgu where TCNO or Ruhsat like '%" + textBox10.Text + "%'", baglan);
            OleDbDataAdapter adtr = new OleDbDataAdapter(komut);
            DataSet ds = new DataSet();
            adtr.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
        }
        public void sınır()
        {
            textBox1.MaxLength = 11;
            textBox4.MaxLength = 10;
            textBox5.MaxLength = 260;

        }

        private void Button6_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
           
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            

            string tarih;
            tarih = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            DateTime oDate = Convert.ToDateTime(tarih);
            DateTime now = DateTime.Now;
            int kalan = (oDate.Date - now.Date).Days;
            string kalan2 = kalan.ToString();

            if (kalan > 30)
            {
                label8.ForeColor = Color.Green;
                label8.Text = "KALAN GÜN: " + kalan2 + " ZAMANI VAR";

            }
            if (kalan <= 30)
            {
                label8.ForeColor = Color.Blue;
                label8.Text = "KALAN GÜN: " + kalan2 + " DOLMAK ÜZERE";
            }
            if (kalan <= 0)
            {
                label8.ForeColor = Color.Red;
                label8.Text = "KALAN GÜN: " + kalan2 + " MUAYENE DOLDU!!";
            }


        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Kişi eklensin mi?", "EKLE", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                if (baglan.State == ConnectionState.Open)
                {

                    baglan.Close();
                }
                baglan.Open();

                OleDbCommand kmt;

                kmt = new OleDbCommand
                ("INSERT INTO sorgu(TCNO,Adi,Soyadi,Telefon,Plaka,Aractip,Ruhsat,BitisTarihi,Aciklama,KayıtTarihi) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text +  "','" + textBox5.Text + "','" + comboBox1.Text + "','" + textBox7.Text + "','" + dateTimePicker1.Value.ToShortDateString() + "','" + textBox6.Text + "','"+ thisDay.ToString() + "')", baglan);
                kmt.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Başarılı");
                listele();
            }
            else
            {
                MessageBox.Show("İŞLEM İPTAL EDİLDİ");
            }

        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Kişi Bilgileri Güncellensin mi?", "GÜNCELLE", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                OleDbCommand kmt;
                baglan.Open();
                kmt = new OleDbCommand("UPDATE sorgu set TCNO='" + textBox1.Text + "',Adi='" + textBox2.Text + "',Soyadi='" + textBox3.Text + "',Telefon='" + textBox4.Text +  "',Plaka='" + textBox5.Text + "',Aractip='" + comboBox1.Text + "',Ruhsat='" + textBox7.Text + "',BitisTarihi='" + dateTimePicker1.Value.ToShortDateString() + "',aciklama='" + textBox6.Text + "'where TCNO='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'", baglan);
                kmt.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("İşleminiz başarılı");
                listele();
            }
            else
            {
                MessageBox.Show("İŞLEM İPTAL EDİLDİ");
            }

        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Kişi silinsin mi?", "SİL", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                OleDbCommand kmt;
                baglan.Open();
                kmt = new OleDbCommand("Delete from sorgu where TCNO = '" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'", baglan);
                kmt.ExecuteNonQuery();
                kmt.Dispose();
                baglan.Close();
                MessageBox.Show("İşleminiz başarılı");
                listele();
            }
            else
            {
                MessageBox.Show("Çıkış yapılmadı");
            }
        }

        private void Button4_Click_1(object sender, EventArgs e)
        {
            listele();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";
            textBox6.Text = "";
            textBox10.Text = "";
        }

        private void Button5_Click_1(object sender, EventArgs e)
        {
            baglan.Open();
            OleDbCommand komut = new OleDbCommand("select * from sorgu where TCNO like'%" + textBox10.Text + "%' or Adi like'" + textBox10.Text + "%'", baglan);
            OleDbDataAdapter adtr = new OleDbDataAdapter(komut);
            DataSet ds = new DataSet();
            adtr.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
        }

        private void Button6_Click_1(object sender, EventArgs e)
        {
            string yedekKlasoru = "C:\\Yedek";
            System.IO.Directory.CreateDirectory(yedekKlasoru); // Klasör yoksa oluşturur, varsa bir şey yapmaz.
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(yedekKlasoru);
            System.IO.FileInfo[] fis = di.GetFiles("*..mdb");
            System.IO.File.Copy("database.mdb", System.IO.Path.Combine(yedekKlasoru, DateTime.Now.ToString("ddMMyyyy_hhmm_") + "Veriler.mdb"), true);
            MessageBox.Show("YEDEKLEME BAŞARILI");
        }                                                                                                   

        private void Button7_Click(object sender, EventArgs e)
        {
            raporcs frm = new raporcs();
            frm.Show();
        }
    }
}
