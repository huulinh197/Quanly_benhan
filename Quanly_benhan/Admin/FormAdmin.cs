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
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }
        private void CloseOtherForm()//Hàm đóng tất cả các form hiện tại trên panel container rồi mới mở form mới
        {
            while (PanelContainer.Controls.Count > 0)
            {
                PanelContainer.Controls[0].Dispose();
            }
        }
        private void btn_QuanLyBacSi_Click(object sender, EventArgs e)
        {
            CloseOtherForm();
            Form_QuanlyBacSi frm = new Form_QuanlyBacSi() { TopLevel = false };
            this.PanelContainer.Controls.Add(frm);
            frm.Show();
        }

        private void btn_QuanLyBenhNhan_Click(object sender, EventArgs e)
        {
            CloseOtherForm();
            Form_QuanlyBenhNhan frm = new Form_QuanlyBenhNhan() { TopLevel = false };
            this.PanelContainer.Controls.Add(frm);
            frm.Show();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            lb_ChaoMungUser.Text = "Chào mừng\n" ;
        }

        private void btn_QuanLyBenhAn_Click(object sender, EventArgs e)
        {
            CloseOtherForm();
            //Form_AminQuanlyBenhAn frm = new Form_AminQuanlyBenhAn() { TopLevel = false };
            //this.PanelContainer.Controls.Add(frm);
            //frm.Show();
        }

        private void btn_QuanLyKhoa_Click(object sender, EventArgs e)
        {
            CloseOtherForm();
             Form_QuanlyKhoa frm = new Form_QuanlyKhoa() { TopLevel = false };
            this.PanelContainer.Controls.Add(frm);
            frm.Show();
        }

        private void btn_QuanLyTaiKhoanAdmin_Click(object sender, EventArgs e)
        {
            CloseOtherForm();
            Form_QuanLyAdmin frm = new Form_QuanLyAdmin() { TopLevel = false };
            this.PanelContainer.Controls.Add(frm);
            frm.Show();
        }

        private void btn_DangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
            Form_Login frm = new Form_Login();
            frm.ShowDialog();
        }
    }
}
