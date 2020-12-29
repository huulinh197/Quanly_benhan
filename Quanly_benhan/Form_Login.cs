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
    public partial class Form_Login : Form
    {
        DataDataContext db = new DataDataContext();
        
        public Form_Login()
        {
            InitializeComponent();
        }

        private void From_Login_Load(object sender, EventArgs e)
        {

        }

       
        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            if (cb_BanLa.Text == "Bệnh nhân")
            {
                var check = (from p in db.BenhNhans where p.CCCD == txt_TaiKhoan.Text && p.MK == txt_MatKhau.Text select p).SingleOrDefault();
                if (check != null)
                {
                    Hide();
                    Form_BenhNhan frm = new Form_BenhNhan(txt_TaiKhoan.Text);
                    //frm.HoTen = BenhNhan.FindBenhNhanByCCCD(txt_TaiKhoan.Text).Rows[0][2].ToString();
                    frm.ShowDialog();
                    Show();
                }
                else
                {
                    MessageBox.Show("Bạn nhập sai mật khẩu hoặc tài khoản không tồn tại", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else if (cb_BanLa.Text == "Bác sĩ")
            {
                var check = (from p in db.BacSis where p.MaBacSi == txt_TaiKhoan.Text && p.MK == txt_MatKhau.Text select p).SingleOrDefault();
                if (check != null)
                {
                    Hide();
                    Form_BacSi frm = new Form_BacSi();
                    frm.ShowDialog();
                    Show();
                }
                else
                {
                    MessageBox.Show("Bạn nhập sai mật khẩu hoặc tài khoản không tồn tại", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var check = (from p in db.Admins where p.TaiKhoan == txt_TaiKhoan.Text && p.MK == txt_MatKhau.Text select p).SingleOrDefault();
                if (check != null)
                {
                    Hide();
                    FormAdmin frm = new FormAdmin();
                    frm.ShowDialog();
                    Show();
                }
                else
                {
                    MessageBox.Show("Bạn nhập sai mật khẩu hoặc tài khoản không tồn tại", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Sau khi ấn nút đăng nhập thì xóa ô mật khẩu
            txt_MatKhau.Text = string.Empty;
        }
    }
}
