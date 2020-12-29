
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
    public partial class Form_QuanlyBenhNhan : Form
    {
        DataDataContext db = new DataDataContext();
        BenhNhan bn = new BenhNhan();

        public Form_QuanlyBenhNhan()
        {
            InitializeComponent();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (txt_CCCD.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập căn cước công dân");
                }
                else if (txt_HoTen.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập họ tên bệnh nhân");
                }
                else if (txt_DiaChi.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập địa chỉ bệnh nhân");
                }
                else if (txt_MaBHYT.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập địa mã bảo hiểm y tế");
                }
                else if (txt_MatKhau.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập mật khẩu");
                }
                else if (txt_SDT.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập số điện thoại");
                }

                bn.HoTen = txt_HoTen.Text;
                bn.MK = txt_MatKhau.Text;
                bn.NgaySinh = dtp_NgaySinh.Value;
                bn.SDT = txt_SDT.Text;
                bn.CCCD = txt_CCCD.Text;
                bn.DiaChi = txt_DiaChi.Text;
                bn.MaBHYT = txt_MaBHYT.Text;
                if (rdb_Nam.Checked)
                {
                    bn.GioiTinh = "Nam";
                }
                else
                {
                    bn.GioiTinh = "Nữ";

                }

                db.BenhNhans.InsertOnSubmit(bn);
                db.SubmitChanges();
                Form_QuanlyBenhNhan_Load(sender, e);
            }
            catch (SqlException er)
            {
                if (er.Message.Contains("DELETE statement conflicted with the REFERENCE constraint"))
                {
                    MessageBox.Show("Tồn tại bệnh án vui lòng kiểm tra lại", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(er.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {

            try
            {
                if (txt_CCCD.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập căn cước công dân");
                }
                if (txt_HoTen.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập họ tên bệnh nhân");
                }
                if (txt_DiaChi.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập địa chỉ bệnh nhân");
                }
                if (txt_MaBHYT.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập địa mã bảo hiểm y tế");
                }
                if (txt_MatKhau.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập mật khẩu");
                }
                if (txt_SDT.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập số điện thoại");
                }
                bn = db.BenhNhans.Where(s => s.CCCD == dgv_BenhNhan.Rows[row].Cells[0].Value.ToString()).Single();
                bn.HoTen = txt_HoTen.Text;
                bn.MaBHYT = txt_MaBHYT.Text;
                bn.MK = txt_MatKhau.Text;
                bn.SDT = txt_MatKhau.Text;
                bn.NgaySinh = dtp_NgaySinh.Value;
                bn.DiaChi = txt_DiaChi.Text;
                if (rdb_Nam.Checked)
                {
                    bn.GioiTinh = "Nam";
                }
                else
                {
                    bn.GioiTinh = "Nữ";

                }

                db.SubmitChanges();
                Form_QuanlyBenhNhan_Load(sender, e);
            }
            catch (SqlException sql)
            {
                if (sql.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Bệnh nhân đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                bn = db.BenhNhans.Where(s => s.CCCD == dgv_BenhNhan.Rows[row].Cells[0].Value.ToString()).Single();
                db.BenhNhans.DeleteOnSubmit(bn);
                db.SubmitChanges();

                Form_QuanlyBenhNhan_Load(sender, e);
            }
            catch (SqlException er)
            {
                if (er.Message.Contains("DELETE statement conflicted with the REFERENCE constraint"))
                {
                    MessageBox.Show("Tồn tại bệnh án vui lòng kiểm tra lại", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(er.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnlammoi_Click(object sender, EventArgs e)
        {
            ClearALL();
            txt_CCCD.Enabled = true;
        }
        private void ClearALL()
        {
            txt_CCCD.Text = string.Empty;
            txt_HoTen.Text = string.Empty;
            txt_SDT.Text = string.Empty;
            dtp_NgaySinh.Value = DateTime.Today;
            txt_MaBHYT.Text = string.Empty;
            txt_MatKhau.Text = string.Empty;
            txt_DiaChi.Text = string.Empty;
            rdb_Nam.Checked = true;

        }

        private void Form_QuanlyBenhNhan_Load(object sender, EventArgs e)
        {
            
            var data = from p in db.BenhNhans select p;
            dgv_BenhNhan.DataSource = data;
            cb_TimKiemTheo.SelectedItem = "Họ tên";
            FormBorderStyle = FormBorderStyle.None;
            ClearALL();

        }
        int row;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex;
            if (row >= 0)
            {  
                txt_CCCD.Text = dgv_BenhNhan.Rows[row].Cells[0].Value.ToString();
                txt_MatKhau.Text = dgv_BenhNhan.Rows[row].Cells[1].Value.ToString();
                txt_HoTen.Text = dgv_BenhNhan.Rows[row].Cells[2].Value.ToString();
                if (dgv_BenhNhan.Rows[row].Cells[3].Value.ToString() == "Nam")
                {
                    rdb_Nam.Checked = true;
                }
                else
                {
                    rdb_Nu.Checked = true;
                }
                dtp_NgaySinh.Text = dgv_BenhNhan.Rows[row].Cells[4].Value.ToString();
                txt_MaBHYT.Text = dgv_BenhNhan.Rows[row].Cells[5].Value.ToString();
                txt_DiaChi.Text = dgv_BenhNhan.Rows[row].Cells[6].Value.ToString();
                txt_SDT.Text = dgv_BenhNhan.Rows[row].Cells[7].Value.ToString();
            }
            txt_CCCD.Enabled = false;
            txt_CCCD.Focus();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_XemBenhAn_Click(object sender, EventArgs e)
        {
            Hide();
            //Quanly_benhan.BacSi.Form_XemBenhAn frm = new Quanly_benhan.BacSi.Form_XemBenhAn(txt_CCCD.Text);
            Form_XemBenhAn frm = new Form_XemBenhAn(txt_CCCD.Text);
            frm.ShowDialog();
            Show();
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            String key = txt_TimKiem.Text;
            if (cb_TimKiemTheo.SelectedItem.ToString() == "Họ tên")
            {
                if (key != null)
                {
                    var tim = (from p in db.BenhNhans
                               where p.HoTen.Contains(key)
                               select new
                               {
                                   p.CCCD,
                                   p.MK,
                                   p.HoTen,
                                   p.GioiTinh,
                                   p.NgaySinh,
                                   p.MaBHYT,
                                   p.DiaChi,
                                   p.SDT
                               });
                    dgv_BenhNhan.DataSource = tim;
                    ClearALL();
                }
            }
            else if (cb_TimKiemTheo.SelectedItem.ToString() == "Căn cước công dân")
            {
                if (key != null)
                {
                    var tim = (from p in db.BenhNhans
                               where p.HoTen.Contains(key)
                               select new
                               {

                                   p.CCCD,
                                   p.MK,
                                   p.HoTen,
                                   p.GioiTinh,
                                   p.NgaySinh,
                                   p.MaBHYT,
                                   p.DiaChi,
                                   p.SDT

                               });
                    dgv_BenhNhan.DataSource = tim;
                    ClearALL();
                }
            }
            else if (cb_TimKiemTheo.SelectedItem.ToString() == "Địa chỉ")
            {
                if (key != null)
                {
                    var tim = (from p in db.BenhNhans
                               where p.HoTen.Contains(key)
                               select new
                               {
                                   p.CCCD,
                                   p.MK,
                                   p.HoTen,
                                   p.GioiTinh,
                                   p.NgaySinh,
                                   p.MaBHYT,
                                   p.DiaChi,
                                   p.SDT

                               });
                    dgv_BenhNhan.DataSource = tim;
                    ClearALL();
                }
            }
        }
    }
}
