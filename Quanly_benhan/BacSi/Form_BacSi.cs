
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanly_benhan
{
    public partial class Form_BacSi : Form
    {
        DataDataContext db = new DataDataContext();
        public Form_BacSi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            CloseOtherForm();
            Form_QuanLyBenhAn frm = new Form_QuanLyBenhAn() { TopLevel = false };
            panelBacSi.Controls.Add(frm);
            frm.Show();

        }
        private void CloseOtherForm()//Hàm đóng tất cả các form hiện tại trên panel container rồi mới mở form mới
        {
            while (panelBacSi.Controls.Count > 0)
            {
                panelBacSi.Controls[0].Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CloseOtherForm();
            Form_QuanlyBenhNhan frm = new Form_QuanlyBenhNhan(){ TopLevel = false };
            panelBacSi.Controls.Add(frm);
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
