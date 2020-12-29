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
    public partial class Form_QuanlyBacSi : Form
    {
        public Form_QuanlyBacSi()
        {
            InitializeComponent();
        }
        DataDataContext db = new DataDataContext();
        BacSi bacsi = new BacSi();
        private void btn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_DiaChi.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Địa chỉ không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (txt_Email.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Email không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (txt_MaBacSi.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Mã bác sĩ không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (txt_MatKhau.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Mật khẩu không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (txt_TenBacSi.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Tên bác sĩ không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                bacsi.MaBacSi = txt_MaBacSi.Text;
                bacsi.TenBacSi = txt_TenBacSi.Text;
                if (rdb_Nam.Checked == true)
                {
                    bacsi.GioiTinh = "Nam";
                }
                else
                {
                    bacsi.GioiTinh = "Nữ";
                }
                bacsi.NgaySinh = dtp_NgaySinh.Value;
                bacsi.DiaChi = txt_DiaChi.Text;
                bacsi.Email = txt_Email.Text;
                bacsi.MaKhoa = cb_MaKhoa.SelectedValue.ToString();
                bacsi.MK = txt_MatKhau.Text;
                db.BacSis.InsertOnSubmit(bacsi);
                db.SubmitChanges();
                Form_QuanlyBacSi_Load(sender, e);
            }
            catch (SqlException sql)
            {
                if (sql.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Bác sĩ đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Form_QuanlyBacSi_Load(object sender, EventArgs e)
        {
            var data = from p in db.BacSis
                       select new
                       {
                           p.MaBacSi,
                           p.TenBacSi,
                           p.GioiTinh,
                           p.NgaySinh,
                           p.DiaChi,
                           p.Email,
                           p.MK,
                           p.MaKhoa,
                       };
            
            dgv_BacSi.DataSource = data;
            var load = (from b in db.Khoas select b).ToList();
            cb_MaKhoa.DataSource = load;
            cb_MaKhoa.DisplayMember = "TenKhoa";
            cb_MaKhoa.ValueMember = "MaKhoa";
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                bacsi = db.BacSis.Where(s => s.MaBacSi == txt_MaBacSi.Text).Single();
                db.BacSis.DeleteOnSubmit(bacsi);
                db.SubmitChanges();

                Form_QuanlyBacSi_Load(sender, e);
            }
            catch (SqlException sql)
            {
                if (sql.Message.Contains("DELETE statement conflicted with the REFERENCE"))
                {
                    MessageBox.Show("Hiện đang tồn tại bệnh án của bác sĩ này, chỉ có thể xóa bác sĩ không có bệnh án", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_DiaChi.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Địa chỉ không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (txt_Email.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Email không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (txt_MaBacSi.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Mã bác sĩ không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (txt_MatKhau.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Mật khẩu không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (txt_TenBacSi.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Tên bác sĩ không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                bacsi = db.BacSis.Where(s => s.MaBacSi == txt_MaBacSi.Text).Single();

                bacsi.TenBacSi = txt_TenBacSi.Text;
                if (rdb_Nam.Checked == true)
                {
                    bacsi.GioiTinh = "Nam";
                }
                else
                {
                    bacsi.GioiTinh = "Nữ";
                }
                bacsi.NgaySinh = dtp_NgaySinh.Value;
                bacsi.DiaChi = txt_DiaChi.Text;
                bacsi.Email = txt_Email.Text;
                bacsi.MaKhoa = cb_MaKhoa.SelectedValue.ToString();
                bacsi.MK = txt_MatKhau.Text;
                db.SubmitChanges();

                Form_QuanlyBacSi_Load(sender, e);
            }
            catch (SqlException sql)
            {
                if (sql.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Bác sĩ đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        int row;


        private void dgv_BacSi_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex;
            if (row >= 0)
            {
                txt_MaBacSi.Text = dgv_BacSi.Rows[row].Cells[0].Value.ToString();
                txt_TenBacSi.Text = dgv_BacSi.Rows[row].Cells[1].Value.ToString();
                if (dgv_BacSi.Rows[row].Cells[2].Value.ToString() == "Nam")
                {
                    rdb_Nam.Checked = true;
                }
                else
                {
                    rdb_Nu.Checked = true;
                }
                dtp_NgaySinh.Text = dgv_BacSi.Rows[row].Cells[3].Value.ToString();
                txt_DiaChi.Text = dgv_BacSi.Rows[row].Cells[4].Value.ToString();
                txt_Email.Text = dgv_BacSi.Rows[row].Cells[5].Value.ToString();
                cb_MaKhoa.SelectedValue = dgv_BacSi.Rows[row].Cells[7].Value.ToString();
                txt_MatKhau.Text = dgv_BacSi.Rows[row].Cells[6].Value.ToString();
            }
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            if (cb_TimKiemTheo.Text == "Tên bác sĩ")
            {
                var tim = from p in db.BacSis where p.TenBacSi.Contains(txt_TimKiem.Text)
                          select new
                          {
                              p.MaBacSi,
                              p.TenBacSi,
                              p.GioiTinh,
                              p.NgaySinh,
                              p.DiaChi,
                              p.Email,
                              p.MK,
                              p.MaKhoa
                          };
                dgv_BacSi.DataSource = tim;
                
            }
            else
            {
                var tim = from p in db.BacSis
                          where p.MaBacSi.Contains(txt_TimKiem.Text)
                          select new
                          {
                              p.MaBacSi,
                              p.TenBacSi,
                              p.GioiTinh,
                              p.NgaySinh,
                              p.DiaChi,
                              p.Email,
                              p.MK,
                              p.MaKhoa
                          };
                dgv_BacSi.DataSource = tim;
            }

            ClearAll();
        }
        private void ClearAll()
        {
            //txt_TimKiem.Text = string.Empty;
            txt_MaBacSi.Text = string.Empty;
            txt_TenBacSi.Text = string.Empty;
            txt_Email.Text = string.Empty;
            txt_MatKhau.Text = string.Empty;
            rdb_Nam.Checked = true;
            txt_DiaChi.Text = string.Empty;
            dtp_NgaySinh.Text = DateTime.Now.ToString();
        }
    }
}
