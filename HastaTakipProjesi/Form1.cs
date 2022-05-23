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
// hasta takip programı 
// Mucit Yazılım- Sadık ŞAHİN
namespace HastaTakipProjesi
{
    public partial class Form1 : Form
    {
        OleDbConnection con = new OleDbConnection("provider = microsoft.ace.oledb.12.0; data source = hastaKayitDB.accdb  ");
        OleDbCommand cmd;
        OleDbDataAdapter da;

        void listele()
        {

            da = new OleDbDataAdapter("select * from hastalar ", con);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;

            temizle();
        }
        void temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            pictureBox1.ImageLocation = "";
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new OleDbCommand(" insert into hastalar ( TC, AD, SOYAD, ADRES, TELEFON, TESHIS, UCRET, GTARIH, CTARIH, RESIM   ) values ( @tc, @ad, @soyad, @adres, @telefon, @teshis, @ucret, @gtarih, @ctarih, @resim )  ", con);
                cmd.Parameters.AddWithValue("@tc", textBox2.Text);
                cmd.Parameters.AddWithValue("@ad", textBox3.Text);
                cmd.Parameters.AddWithValue("@soyad", textBox4.Text);
                cmd.Parameters.AddWithValue("@adres", textBox5.Text);
                cmd.Parameters.AddWithValue("@telefon", textBox6.Text);
                cmd.Parameters.AddWithValue("@teshis", textBox7.Text);
                cmd.Parameters.AddWithValue("@ucret", textBox8.Text);
                cmd.Parameters.AddWithValue("@gtarih", dateTimePicker1.Value.ToString());
                cmd.Parameters.AddWithValue("@ctarih", dateTimePicker2.Value.ToString());
                cmd.Parameters.AddWithValue("@resim", textBox9.Text);
                if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("Lütfen tüm alanları doldurunuz");
                }
                else
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Yeni Kayıt Tamam ");
                }





            }
            catch (Exception)
            {

                MessageBox.Show("Hatalı işlem !");
            }



            listele();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyalari :|*.jpg; *.png ";
            dosya.ShowDialog();
            string dosyayolu = dosya.FileName;
            textBox9.Text = dosyayolu;
            pictureBox1.ImageLocation = dosyayolu;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();

            textBox9.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();

            pictureBox1.ImageLocation = textBox9.Text;


        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                cmd = new OleDbCommand(" delete from hastalar   where TC = @tc   ", con);
                cmd.Parameters.AddWithValue("@tc", textBox2.Text);

                if (textBox2.Text == "")
                {
                    MessageBox.Show("Silmek istediğiniz kişinin TC sini  giriniz ");
                }
                else
                {
                    con.Open();
                    int sonuc = cmd.ExecuteNonQuery();
                    con.Close();
                    if (sonuc == 1)
                        MessageBox.Show("Silme işlemi tamam ");
                    else
                        MessageBox.Show("Silmek istediginiz kişinin TC sini doğru giriniz ! ");
                    listele();
                }



            }
            catch (Exception)
            {
                MessageBox.Show("Hatılı işlem !");

            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new OleDbCommand(" update hastalar set  AD = '" + textBox3.Text + "' , SOYAD = '" + textBox4.Text + "', ADRES ='" + textBox5.Text + "', TELEFON = '" + textBox6.Text + "', TESHIS = '" + textBox7.Text + "' , UCRET = '" + textBox8.Text + "', GTARIH = '" + dateTimePicker1.Value.ToString() + "', CTARIH = '" + dateTimePicker2.Value.ToString() + "', RESIM = '" + textBox9.Text + "'   where TC = '" + textBox2.Text + "' ", con);

                if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("Lütfen tüm alanları doldurunuz");
                }
                else
                {

                    con.Open();
                    int sonuc = cmd.ExecuteNonQuery();
                    con.Close();
                    if (sonuc == 1)
                        MessageBox.Show("Güncelleme işlemi tamam ");
                    else
                        MessageBox.Show("Güncelleme işleminde hata oluştu ! ");
                    listele();

                }

            }
            catch (Exception)
            {
                MessageBox.Show("Hatalı işlem ! ");
            }


        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            da = new OleDbDataAdapter("select * from hastalar where tc  like '" + textBox10.Text + "%' ", con);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            temizle();


        }
    }
}
