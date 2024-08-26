using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SİFRE_COZME
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-BC3LOP2\SQLEXPRESS;Initial Catalog=sifre;Integrated Security=True;Connect Timeout=30;");
        private void Btn_kaydet_Click(object sender, EventArgs e)
        {
            string ad = Txt_ad.Text;
            //veri şifreleme
            byte[] addizi = ASCIIEncoding.ASCII.GetBytes(ad);

            //md,base64,encoding
            string  adsifreliveri = Convert.ToBase64String(addizi);
          


            string soyad = Txt_Soyad.Text;
            //veri şifreleme
            byte[] soyaddizi = ASCIIEncoding.ASCII.GetBytes(soyad);

            //md,base64,encoding
            string soyadsifreliveri = Convert.ToBase64String(soyaddizi);

            string mail = Txt_mail .Text;
            //veri şifreleme
            byte[] maildizi = ASCIIEncoding.ASCII.GetBytes(mail);

            //md,base64,encoding
            string mailsifreliveri = Convert.ToBase64String(maildizi);


            string sifre = Txt_sifre .Text;
            //veri şifreleme
            byte[] sifredizi = ASCIIEncoding.ASCII.GetBytes(sifre);

            //md,base64,encoding
            string sifresifreliveri = Convert.ToBase64String(sifredizi);

            string hesapno = Txt_hesapno.Text;
            //veri şifreleme
            byte[] hesapnodizi = ASCIIEncoding.ASCII.GetBytes(hesapno);

            //md,base64,encoding
            string hesapnosifreliveri = Convert.ToBase64String(hesapnodizi);


            bgl.Open();
            SqlCommand komut = new SqlCommand("insert into TBLVERİLER(AD,SOYAD,MAİL,SIFRE,HESAPNO) values(@p1,@p2,@p3,@p4,@p5)", bgl);
            komut.Parameters.AddWithValue("@p1", adsifreliveri);
            komut.Parameters.AddWithValue("@p2", soyadsifreliveri);
            komut.Parameters.AddWithValue("@p3", mailsifreliveri);
            komut.Parameters.AddWithValue("@p4", sifresifreliveri);
            komut.Parameters.AddWithValue("@p5", hesapnosifreliveri);
            komut.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Veriler Eklendi");
            listeleSifreli();
        }

        void listeleSifreli()
        {
            
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBLVERİLER", bgl);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listeleSifreli();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string adcozum = Txt_ad.Text;
            byte[] adcozumdizi = Convert.FromBase64String(adcozum); 
            string adverisi = ASCIIEncoding.ASCII.GetString(adcozumdizi);
            label6.Text = adverisi;
            listele();
        }
        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select AD,SOYAD,MAİL,SIFRE,HESAPNO from TBLVERİLER", bgl);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows) {

                dr["AD"] = Coz(dr["AD"] as string);
                dr["SOYAD"] = Coz(dr["SOYAD"] as string);
                dr["MAİL"] = Coz(dr["MAİL"] as string);
                dr["SIFRE"] = Coz(dr["SIFRE"] as string);
                dr["HESAPNO"] = Coz(dr["HESAPNO"] as string);


            }
            dataGridView1.DataSource = ds.Tables[0];
        }
        private object Coz(string v1)
        {
            byte[] adcozumdizi = Convert.FromBase64String(v1);
            string adverisi = ASCIIEncoding.ASCII.GetString(adcozumdizi);
            return adverisi;
        }
    }
}
