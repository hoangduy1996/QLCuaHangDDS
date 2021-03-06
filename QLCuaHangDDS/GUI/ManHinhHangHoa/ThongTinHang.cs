﻿using DevExpress.XtraEditors;
using QLCuaHangDDS.BUS;
using QLCuaHangDDS.DAO;
using System;
using System.Windows.Forms;

namespace QLCuaHangDDS.GUI.ManHinhHangHoa
{
    public partial class ThongTinHang : Form
    {
        private int selectedMaMH;
        public ThongTinHang()
        {
            InitializeComponent();
            gridControl.DataSource = new QLCuaHangDDS.DAO.QLCuaHangDDSDBDataContext().MATHANGs;

            // Table decoration
            gridView.Columns[0].Visible = false;
            gridView.Columns[1].Caption = "Phân loại";
            gridView.Columns[2].Caption = "Tên mặt hàng";
            gridView.Columns[3].Caption = "Đơn giá";
            gridView.Columns[4].Caption = "Hãng sản xuất";
            gridView.Columns[5].Caption = "Năm sản xuất";
            gridView.Columns[6].Caption = "Số lượng";
            gridView.Columns[7].Caption = "Mô tả";
            gridView.Columns[8].Caption = "Kinh doanh";
            gridView.Columns[9].Visible = false;
            gridView.Columns[10].Visible = false;

            // Init Phan Loai
            cbb_PhanLoai.Properties.DataSource = new QLCuaHangDDS.DAO.QLCuaHangDDSDBDataContext().LOAIMATHANGs;
            cbb_PhanLoai.Properties.ShowHeader = false;

            // Init Hang san xuat
            cbb_HangSanXuat.Properties.DataSource = new QLCuaHangDDS.DAO.QLCuaHangDDSDBDataContext().HANGSANXUATs;
            cbb_HangSanXuat.Properties.ShowHeader = false;
        }

        public void updateExternalBind()
        {
            // Init Phan Loai
            cbb_PhanLoai.Properties.DataSource = new QLCuaHangDDS.DAO.QLCuaHangDDSDBDataContext().LOAIMATHANGs;
            cbb_PhanLoai.Properties.ShowHeader = false;

            // Init Hang san xuat
            cbb_HangSanXuat.Properties.DataSource = new QLCuaHangDDS.DAO.QLCuaHangDDSDBDataContext().HANGSANXUATs;
            cbb_HangSanXuat.Properties.ShowHeader = false;
        }

        private void refreshTableData()
        {
            gridControl.DataSource = new QLCuaHangDDS.DAO.QLCuaHangDDSDBDataContext().MATHANGs;
            edt_TenMatHang.Text = "";
            edt_SoLuong.Text = "";
            edt_NamSanXuat.Text = "";
            edt_MoTa.Text = "[chưa có]";
            edt_DonGia.Text = "";
            cbb_HangSanXuat.Text = "";
            cbb_PhanLoai.Text = "";
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            if (!MatHangBUS.isValidNumber(edt_SoLuong.Text.ToString()))
            {
                XtraMessageBox.Show("Số lượng không hợp lệ!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!MatHangBUS.isValidNumber(edt_NamSanXuat.Text.ToString()))
            {
                XtraMessageBox.Show("Năm không hợp lệ!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!MatHangBUS.isValidFloat(edt_DonGia.Text.ToString()))
            {
                XtraMessageBox.Show("Đơn giá không hợp lệ!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MATHANG mh = new MATHANG();
            mh.TenMH = edt_TenMatHang.Text.ToString();
            mh.TenLoaiMH = cbb_PhanLoai.Text.ToString();
            mh.TenHangSX = cbb_HangSanXuat.Text.ToString();
            mh.NamSX = int.Parse(edt_NamSanXuat.Text.ToString());
            mh.SoLuong = int.Parse(edt_SoLuong.Text.ToString());
            mh.DonGia = decimal.Parse(edt_DonGia.Text.ToString());
            mh.Mota = edt_MoTa.Text.ToString();
            mh.KinhDoanh = true;

            if (MatHangBUS.ThemMatHang(mh))
            {
                XtraMessageBox.Show("Thêm thành công!", "Succeed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.refreshTableData();
                return;
            }
            else
            {
                XtraMessageBox.Show("Thêm thất bại!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.refreshTableData();
                return;
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if (!MatHangBUS.isValidNumber(gridView.GetRowCellValue(gridView.FocusedRowHandle, "SoLuong")))
            {
                XtraMessageBox.Show("Số lượng không hợp lệ!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!MatHangBUS.isValidNumber(gridView.GetRowCellValue(gridView.FocusedRowHandle, "NamSX")))
            {
                XtraMessageBox.Show("Năm không hợp lệ!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!MatHangBUS.isValidFloat(gridView.GetRowCellValue(gridView.FocusedRowHandle, "DonGia")))
            {
                XtraMessageBox.Show("Đơn giá không hợp lệ!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            MATHANG mh = new MATHANG();
            mh.TenMH = gridView.GetRowCellValue(gridView.FocusedRowHandle, "TenMH").ToString();
            mh.TenLoaiMH = gridView.GetRowCellValue(gridView.FocusedRowHandle, "TenLoaiMH").ToString();
            mh.TenHangSX = gridView.GetRowCellValue(gridView.FocusedRowHandle, "TenHangSX").ToString();
            mh.NamSX = int.Parse(gridView.GetRowCellValue(gridView.FocusedRowHandle, "NamSX").ToString());
            mh.SoLuong = int.Parse(gridView.GetRowCellValue(gridView.FocusedRowHandle, "SoLuong").ToString());
            mh.DonGia = decimal.Parse(gridView.GetRowCellValue(gridView.FocusedRowHandle, "DonGia").ToString());
            mh.Mota = gridView.GetRowCellValue(gridView.FocusedRowHandle, "Mota").ToString();
            mh.KinhDoanh = bool.Parse(gridView.GetRowCellValue(gridView.FocusedRowHandle, "KinhDoanh").ToString());


            if (MatHangBUS.SuaMatHang(this.selectedMaMH, mh))
            {
                XtraMessageBox.Show("Cập nhật thành công!", "Succeed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.refreshTableData();
                return;
            }
            else
            {
                XtraMessageBox.Show("Cập nhật thất bại!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.refreshTableData();
                return;
            }
        }

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                this.selectedMaMH = int.Parse(gridView.GetFocusedRowCellValue("MaMH").ToString());
            }
            catch (Exception) { }
        }
    }
}
