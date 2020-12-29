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
    public partial class Form_QuanlyKhoa : Form
    {
        public Form_QuanlyKhoa()
        {
            InitializeComponent();
        }
        DataDataContext db = new DataDataContext();
        Khoa khoa = new Khoa();
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form_QuanlyKhoa_Load(object sender, EventArgs e)
        {
            var list = from p in db.Khoas select p;
            dgv_Khoa.DataSource = list;
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaKhoa.Text.Trim() == string.Empty)
                {
                    throw new Exception("Mã khoa không được để trống!");
                }
                if (txt_TenKhoa.Text.Trim() == string.Empty)
                {
                    throw new Exception("Tên khoa không được để trống!");
                }
               
                khoa.MaKhoa = txt_MaKhoa.Text;
                khoa.TenKhoa = txt_TenKhoa.Text;
                db.Khoas.InsertOnSubmit(khoa);
                db.SubmitChanges();
                Form_QuanlyKhoa_Load(sender, e);
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
                if (txt_MaKhoa.Text.Trim() == string.Empty)
                {
                    throw new Exception("Mã khoa không được để trống!");
                }
                if (txt_TenKhoa.Text.Trim() == string.Empty)
                {
                    throw new Exception("Tên khoa không được để trống!");
                }
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
            khoa = db.Khoas.Where(s => s.MaKhoa == txt_MaKhoa.Text).Single();
            
            khoa.TenKhoa = txt_TenKhoa.Text;

            db.SubmitChanges();

            Form_QuanlyKhoa_Load(sender, e);
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                khoa = db.Khoas.Where(s => s.MaKhoa == txt_MaKhoa.Text).Single();
                db.Khoas.DeleteOnSubmit(khoa);
                db.SubmitChanges();
                Form_QuanlyKhoa_Load(sender, e);
            }
            catch (SqlException sql)
            {
                if (sql.Message.Contains("DELETE statement conflicted with the REFERENCE"))
                {
                    MessageBox.Show("Hiện đang tồn tại bác sĩ trong khoa này, chỉ có thể xóa khoa không có bác sĩ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgv_Khoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row;
            row = e.RowIndex;
            txt_MaKhoa.Text = dgv_Khoa.Rows[row].Cells[0].Value.ToString();
            txt_TenKhoa.Text = dgv_Khoa.Rows[row].Cells[1].Value.ToString();

        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
