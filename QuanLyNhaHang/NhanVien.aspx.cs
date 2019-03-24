using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyNhaHang
{
    public partial class NhanVien : System.Web.UI.Page
    {
        DataSet1TableAdapters.NhanVienTableAdapter nv = new DataSet1TableAdapters.NhanVienTableAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadNgayThangNam();
                LoadData();
            }
        }

        public void LoadData()
        {
            GridView1.DataSource = nv.GetData();
            GridView1.DataBind();
        }
        #region load ngày tháng năm cơ bản
        public void LoadNgayThangNam()
        {
            ddlNgay.Items.Clear();
            //ddlNgay.Items.Add("-Ngày-");
            for (int i = 1; i <= 31; i++)
            {
                ddlNgay.Items.Add(i.ToString());
            }
            ddlThang.Items.Clear();
            //ddlThang.Items.Add("-Tháng-");
            for (int i = 1; i <= 12; i++)
            {
                ddlThang.Items.Add(i.ToString());
            }
            ddlNam.Items.Clear();
            //ddlNam.Items.Add("-Năm-");
            for (int i = 1900; i <= DateTime.Now.Year; i++)
            {
                ddlNam.Items.Add(i.ToString());
            }
        }
        #endregion
        #region xử lý ngày tạm bỏ cái này nha, nếu muốn dùng thì tắt chú thích đi :))
        protected void ddlThang_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    int ngay;
        //    int thang = int.Parse(ddlThang.SelectedItem.Value);
        //    switch (thang)
        //    {
        //        case 2:

        //            if (ddlNam.SelectedIndex != 0)
        //            {
        //                ddlNgay.Items.Clear();
        //                int nam = int.Parse(ddlNam.SelectedItem.Value);
        //                //if ((nam % 4 == 0) && ((nam % 100 != 0) || (nam % 400 == 0)))
        //                if (nam % 4 == 0)
        //                {
        //                    ngay = 29;
        //                }
        //                else
        //                {
        //                    ngay = 28;
        //                }
        //                //ddlNgay.Items.Add("-Ngày-");
        //                for (int i = 1; i <= ngay; i++)
        //                {
        //                    ddlNgay.Items.Add(i.ToString());
        //                }
        //            }
        //            else
        //            {
        //                ddlNgay.Items.Clear();
        //                //ddlNgay.Items.Add("-Ngày-");
        //            }
        //            break;
        //        case 1:
        //        case 3:
        //        case 5:
        //        case 7:
        //        case 8:
        //        case 10:
        //        case 12:
        //            ddlNgay.Items.Clear();
        //            //ddlNgay.Items.Add("-Ngày-");
        //            for (int i = 1; i <= 31; i++)
        //            {
        //                ddlNgay.Items.Add(i.ToString());
        //            }
        //            break;
        //        case 4:
        //        case 6:
        //        case 9:
        //        case 11:
        //            ddlNgay.Items.Clear();
        //            //ddlNgay.Items.Add("-Ngày-");
        //            for (int i = 1; i <= 30; i++)
        //            {
        //                ddlNgay.Items.Add(i.ToString());
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        }
        #endregion
        #region thêm nhân viên
        protected void btnThem_Click(object sender, EventArgs e)
        {
            string gt;
            if (rbGioiTinh.SelectedValue == "Nam")
            {
                gt = rbGioiTinh.SelectedValue.ToString();
            }
            else
            {
                gt = rbGioiTinh.SelectedValue.ToString();
            }
            string layngay = ddlNgay.SelectedItem.Value;
            string laythang = ddlThang.SelectedItem.Value;
            string laynam = ddlNam.SelectedItem.Value;
            string date = laythang + "/" + layngay + "/" + laynam;

            if (txtMaNV.Text == "" || txtUser.Text == "" || txtPass.Text == "" || txtHoTen.Text == "" || txtDiaChi.Text == "" || txtSDT.Text == "")
            {
                lblTBLoi.Text = "Vui lòng nhập đầy đủ thông tin";
            }
            else
                if (txtMaNV.Text.Length < 6)
                {
                    lblTBLoi.Text = "Mã nhân viên phải đủ 6 số trở lên";
                }
                else
                    if (txtSDT.Text.Length < 9)
                    {
                        lblTBLoi.Text = "Số điện thoại phải đủ 10 số trở lên";
                    }
                    else
                        {
                            try
                            {
                                nv.ThemNV(Int32.Parse(txtMaNV.Text), txtUser.Text, txtPass.Text, txtHoTen.Text, date, gt, txtDiaChi.Text, Int32.Parse(txtSDT.Text));
                                lblTBLoi.Text = "Thêm nhân viên thành công";
                                LoadData();
                                KhoaTruong();
                            }
                            catch
                            {
                                lblTBLoi.Text = "Mã nhân viên không hợp lệ";
                            }
                        }
        }
        #endregion
        #region load gridview
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "xoa")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RemoveAt = gvr.RowIndex;
                nv.XoaNV(int.Parse(GridView1.Rows[RemoveAt].Cells[0].Text));
                LoadData();
            }
        }
        #endregion
        #region nút xóa
        protected void btnHuy_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrangChu.aspx");
        }
        #endregion
        #region khóa trường lại
        public void KhoaTruong()
        {
            txtMaNV.Text = "";
            txtUser.Text = "";
            txtPass.Text = "";
            txtHoTen.Text = "";
            LoadNgayThangNam();
            txtDiaChi.Text = "";
            txtSDT.Text = "";
        }
        #endregion
    }
}