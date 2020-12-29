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
    public partial class Form_QuanLyBenhAn : Form
    {
        DataDataContext db = new DataDataContext();
        BenhAn bn = new BenhAn();
        public Form_QuanLyBenhAn()
        {
            InitializeComponent();
        }

        private void Form_QuanLyBenhNhan_Load(object sender, EventArgs e)
        {
            var list = from p in db.BenhAns
                       select new
                       {
                           p.MaBenhAn,
                           p.CCCD,
                           p.NgayVaoVien,
                           p.NgayRaVien,
                           p.ChanDoanLucVaoVien,
                           p.ChanDoanLucRaVien,
                           p.PhuongAnDieuTri,
                           p.MaBacSi

                       };

            dgv_BenhAn.DataSource = list;
            cb_TimKiemTheo.SelectedItem = "Họ tên";
            FormBorderStyle = FormBorderStyle.None;
            load();
        }
        public void load()
        {
            var list = (from p in db.BacSis select p).ToList();
            cb_BacSi.DataSource = list;
            cb_BacSi.DisplayMember = "TenBacSi";
            cb_BacSi.ValueMember = "MaBacSi";
            //comboBox1.Enabled = false;
            var combo = from p in db.BenhNhans select p.CCCD;
            comboBox1.DataSource = combo;
            comboBox1.DisplayMember = "CCCD";
        }
        private void btn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                int result = DateTime.Compare(dtp_NgayVaoVien.Value, dtp_NgayRaVien.Value);
                if (comboBox1.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập căn cước công dân");
                }
                if (txt_MaBenhAn.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập mã bệnh án");
                }
                if (txt_ChanDoanLucVaoVien.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập chuẩn đoán lúc vào viện");

                }
                if (txt_ChanDoanLucRaVien.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập chuẩn đoán lúc ra viện");

                }
                if (result > 0)
                {
                    throw new Exception("Vui lòng chọn đúng ngày ra vào viện");
                }

                bn.CCCD = comboBox1.Text;
                bn.MaBenhAn = txt_MaBenhAn.Text;
                bn.NgayVaoVien = dtp_NgayVaoVien.Value;
                bn.NgayRaVien = dtp_NgayRaVien.Value;
                bn.ChanDoanLucVaoVien = txt_ChanDoanLucVaoVien.Text;
                bn.ChanDoanLucRaVien = txt_ChanDoanLucRaVien.Text;
                bn.PhuongAnDieuTri = txt_PhuongAnDieuTri.Text;
                bn.MaBacSi = cb_BacSi.SelectedValue.ToString();

                db.BenhAns.InsertOnSubmit(bn);
                db.SubmitChanges();
                clear();
                Form_QuanLyBenhNhan_Load(sender, e);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Tồn tại bệnh án", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            try
            {
                int result = DateTime.Compare(dtp_NgayVaoVien.Value, dtp_NgayRaVien.Value);
                if (comboBox1.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập căn cước công dân");
                }
                if (txt_MaBenhAn.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập mã bệnh án");
                }
                if (txt_ChanDoanLucVaoVien.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập chuẩn đoán lúc vào viện");

                }
                if (txt_ChanDoanLucRaVien.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập chuẩn đoán lúc ra viện");

                }
                if (result > 0)
                {
                    throw new Exception("Vui lòng chọn đúng ngày ra vào viện");
                }
                bn = db.BenhAns.Where(s => s.MaBenhAn == dgv_BenhAn.Rows[row].Cells[0].Value.ToString()).Single();
                bn.MaBenhAn = txt_MaBenhAn.Text;
                bn.NgayVaoVien = dtp_NgayVaoVien.Value;
                bn.NgayRaVien = dtp_NgayRaVien.Value;
                bn.ChanDoanLucVaoVien = txt_ChanDoanLucVaoVien.Text;
                bn.ChanDoanLucRaVien = txt_ChanDoanLucRaVien.Text;
                bn.PhuongAnDieuTri = txt_PhuongAnDieuTri.Text;
                bn.MaBacSi = cb_BacSi.SelectedValue.ToString();
                db.SubmitChanges();
                Form_QuanLyBenhNhan_Load(sender, e);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Tồn tại bệnh án", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                bn = db.BenhAns.Where(s => s.MaBenhAn == dgv_BenhAn.Rows[row].Cells[0].Value.ToString()).Single();
                db.BenhAns.DeleteOnSubmit(bn);
                db.SubmitChanges();
                Form_QuanLyBenhNhan_Load(sender, e);
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
        private void clear()
        {
            comboBox1.Text = "";
            txt_MaBenhAn.Text = "";
            dtp_NgayVaoVien.Text = DateTime.Today.ToString();
            dtp_NgayRaVien.Text = DateTime.Today.ToString();
            txt_ChanDoanLucVaoVien.Text = "";
            txt_ChanDoanLucRaVien.Text = "";
            txt_PhuongAnDieuTri.Text = "";

        }
        int row;

        private void dgv_BenhAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex;
            comboBox1.Text = dgv_BenhAn.Rows[row].Cells[1].Value.ToString();
            txt_MaBenhAn.Text = dgv_BenhAn.Rows[row].Cells[0].Value.ToString();
            dtp_NgayVaoVien.Text = dgv_BenhAn.Rows[row].Cells[2].Value.ToString();
            dtp_NgayRaVien.Text = dgv_BenhAn.Rows[row].Cells[3].Value.ToString();
            txt_ChanDoanLucVaoVien.Text = dgv_BenhAn.Rows[row].Cells[4].Value.ToString();
            txt_ChanDoanLucRaVien.Text = dgv_BenhAn.Rows[row].Cells[5].Value.ToString();
            txt_PhuongAnDieuTri.Text = dgv_BenhAn.Rows[row].Cells[6].Value.ToString();
            cb_BacSi.SelectedValue = dgv_BenhAn.Rows[row].Cells[7].Value.ToString();
            //comboBox1.Enabled = false;
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnlammoi_Click(object sender, EventArgs e)
        {
            clear();
           
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            String key = txt_TimKiem.Text;
            if (cb_TimKiemTheo.SelectedItem.ToString() == "CCCD")
            {
                if (key != null)
                {
                    var tim = (from p in db.BenhAns where p.CCCD.Contains(key) select new
                    {

                        p.MaBenhAn,
                        p.CCCD,
                        p.NgayVaoVien,
                        p.NgayRaVien,
                        p.ChanDoanLucVaoVien,
                        p.ChanDoanLucRaVien,
                        p.PhuongAnDieuTri,
                        p.MaBacSi

                    });
                    dgv_BenhAn.DataSource = tim;
                    clear();
                    
                }
            }
            else if (cb_TimKiemTheo.Text == "Mã bác sĩ")
            {
                if (key != null)
                {
                    var tim = (from p in db.BenhAns
                               where p.MaBacSi.Contains(key)
                               select new
                               {

                                   p.MaBenhAn,
                                   p.CCCD,
                                   p.NgayVaoVien,
                                   p.NgayRaVien,
                                   p.ChanDoanLucVaoVien,
                                   p.ChanDoanLucRaVien,
                                   p.PhuongAnDieuTri,
                                   p.MaBacSi

                               });
                    dgv_BenhAn.DataSource = tim;
                    clear();
                }
            }
            else if (cb_TimKiemTheo.SelectedItem.ToString() == "Mã bệnh án")
            {
                if (key != null)
                {
                    var tim = (from p in db.BenhAns
                               where p.MaBenhAn.Contains(key)
                               select new
                               {

                                   p.MaBenhAn,
                                   p.CCCD,
                                   p.NgayVaoVien,
                                   p.NgayRaVien,
                                   p.ChanDoanLucVaoVien,
                                   p.ChanDoanLucRaVien,
                                   p.PhuongAnDieuTri,
                                   p.MaBacSi

                               });
                    dgv_BenhAn.DataSource = tim;
                    clear();
                }
            }
        }
    }
}
