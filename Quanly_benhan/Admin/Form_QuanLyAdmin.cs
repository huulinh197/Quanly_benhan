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

namespace Quanly_benhan
{
    public partial class Form_QuanLyAdmin : Form
    {
        public Form_QuanLyAdmin()
        {
            InitializeComponent();
        }
        DataDataContext db = new DataDataContext();
        Admin admin = new Admin();
        private void Form_QuanLyAdmin_Load(object sender, EventArgs e)
        {
            var list = from p in db.Admins select p;
            dgv_Admin.DataSource = list;
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_TaiKhoan.Text.Trim() == string.Empty)
                {
                    throw new Exception("Tài khoản không được để trống!");
                }
                if (txt_MatKhau.Text.Trim() == string.Empty)
                {
                    throw new Exception("Mật khẩu không được để trống!");
                }
                
                admin.TaiKhoan = txt_TaiKhoan.Text;
                admin.MK = txt_MatKhau.Text;
                db.Admins.InsertOnSubmit(admin);
                db.SubmitChanges();


                Form_QuanLyAdmin_Load(sender, e);
            }
            catch (SqlException sql)
            {
                if (sql.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Tài khoản đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(sql.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            try
            {

                if (txt_TaiKhoan.Text.Trim() == string.Empty)
                {
                    throw new Exception("Tài khoản không được để trống!");
                }
                if (txt_MatKhau.Text.Trim() == string.Empty)
                {
                    throw new Exception("Mật khẩu không được để trống!");
                }
                admin = db.Admins.Where(s => s.TaiKhoan == txt_TaiKhoan.Text).Single();
              
                admin.MK = txt_MatKhau.Text;
                Form_QuanLyAdmin_Load(sender, e);
            }
            catch (SqlException sql)
            {
                if (sql.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Tài khoản đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(sql.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (dgv_Admin.Rows.Count == 1)
            {
                MessageBox.Show("Đây là tải khoản cuối cùng, nếu xóa bạn sẽ mất quyền truy cập admin, không thể xóa tài khoản admin cuối cùng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                admin = db.Admins.Where(s => s.TaiKhoan == txt_TaiKhoan.Text).Single();
                db.Admins.DeleteOnSubmit(admin);

                db.SubmitChanges();
                Form_QuanLyAdmin_Load(sender, e);
            }
        }

      

        private void dgv_Admin_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int row;
            row = e.RowIndex;
            txt_TaiKhoan.Text = dgv_Admin.Rows[row].Cells[0].Value.ToString();
            txt_MatKhau.Text = dgv_Admin.Rows[row].Cells[1].Value.ToString();
        }
    }
}
