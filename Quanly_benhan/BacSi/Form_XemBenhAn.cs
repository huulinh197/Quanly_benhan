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
    public partial class Form_XemBenhAn : Form
    {
        DataDataContext db = new DataDataContext();
        public Form_XemBenhAn()
        {
            InitializeComponent();
        }

        private void Form_XemBenhAn_Load(object sender, EventArgs e)
        {
            lb_BenhAnCuaBenhNhan.Text = "Bệnh án của bệnh nhân ";
            var data = from p in db.BenhAns
                       where p.CCCD == txt_CCCD.Text
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

            dgv_BenhAn.DataSource = data;
            var load = from p in db.BacSis select p;
            cb_BacSi.DataSource = load;
            cb_BacSi.DisplayMember = "TenBacSi";
            cb_BacSi.ValueMember = "MaBacSi";
        }
        private string _message;
        
        public Form_XemBenhAn(string Message) :this()
        {
            _message = Message;
            txt_CCCD.Text = _message;
        }
        int row;
        private void dgv_BenhAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex;

            if (row >= 0)
            {
                txt_MaBenhAn.Text = dgv_BenhAn.Rows[row].Cells[0].Value.ToString();
                dtp_NgayVaoVien.Text = dgv_BenhAn.Rows[row].Cells[2].Value.ToString();
                dtp_NgayRaVien.Text = dgv_BenhAn.Rows[row].Cells[3].Value.ToString();
                txt_ChanDoanLucVaoVien.Text = dgv_BenhAn.Rows[row].Cells[4].Value.ToString();
                txt_ChanDoanLucRaVien.Text = dgv_BenhAn.Rows[row].Cells[5].Value.ToString();
                txt_PhuongAnDieuTri.Text = dgv_BenhAn.Rows[row].Cells[6].Value.ToString();
                cb_BacSi.SelectedValue = dgv_BenhAn.Rows[row].Cells[7].Value.ToString();
                grb_ThongTin.Text = "Thông tin bênh án thứ " + (row + 1);
            }
        }
    }
}
