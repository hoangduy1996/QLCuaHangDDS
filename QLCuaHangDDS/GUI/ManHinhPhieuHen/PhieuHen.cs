﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLCuaHangDDS.BUS;
using QLCuaHangDDS.DAO;
namespace QLCuaHangDDS.GUI.ManHinhPhieuHen
{
    public partial class PhieuHen : DevExpress.XtraEditors.XtraForm
    {
        private int selectedMaPH;
        public PhieuHen()
        {
            
        InitializeComponent();
            // This line of code is generated by Data Source Configuration Wizard
            PhieuHen_table.DataSource = new QLCuaHangDDS.DAO.QLCuaHangDDSDBDataContext().PHIEUHENs;
                PhieuHen_gv.Columns[0].Caption = "Mã Phiếu Hẹn";
                PhieuHen_gv.Columns[1].Caption = "Ngày Lập";
                PhieuHen_gv.Columns[2].Caption = "Tên Khách Hàng";
                PhieuHen_gv.Columns[3].Caption = "Số Điện Thoại";
                PhieuHen_gv.Columns[4].Caption = "Tình Trạng Máy";

        }
        public void updateExternalBind()
        {
            // Init Phan Loai
            PhieuHen_table.DataSource = new QLCuaHangDDS.DAO.QLCuaHangDDSDBDataContext().PHIEUHENs;
        }
        private void refreshData()
        {
            // This line of code is generated by Data Source Configuration Wizard
            PhieuHen_table.DataSource = new QLCuaHangDDS.DAO.QLCuaHangDDSDBDataContext().PHIEUHENs;
            PhieuHen_gv.Columns[0].Caption = "Mã Phiếu Hẹn";
            PhieuHen_gv.Columns[1].Caption = "Ngày Lập";
            PhieuHen_gv.Columns[2].Caption = "Tên Khách Hàng";
            PhieuHen_gv.Columns[3].Caption = "Số Điện Thoại";
            PhieuHen_gv.Columns[4].Caption = "Tình Trạng Máy";
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {

            if (PhieuHenBUS.isValidNumber(edt_tenKH.Text.ToString()) || !PhieuHenBUS.isValidNumber(edt_SoDT.Text.ToString()) || PhieuHenBUS.isValidNumber(edt_TinhTrang.Text.ToString()))
            {
                XtraMessageBox.Show("Thông tin không hợp lệ!Vui lòng kiểm tra lại", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PHIEUHEN ph = new PHIEUHEN();
            
            ph.TenKH = edt_tenKH.Text.ToString();
            ph.NgayLap = DateTime.Now;
            ph.SoDT = edt_SoDT.Text.ToString();
            ph.TinhTrangMay = edt_TinhTrang.Text.ToString();

            //  ph.TenKH= edt_TenSC.Text.ToString();
            // ph.ChiPhiSuaChua = System.Convert.ToDecimal(edt_ChiPhiSC.Text.ToString());
            if (PhieuHenBUS.ThemPhieuHen(ph))
            {
                XtraMessageBox.Show("Thêm thành công!", "Succeed!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                refreshData();
                return;
            }
            else
            {
                XtraMessageBox.Show("Thêm thất bại! Vui lòng xem lại thông tin", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                refreshData();
                return;
            }
        }

        private void PhieuHen_gv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            btn_Sua.Enabled = true;
            btn_Xoa.Enabled = true;
            PhieuHen_gv.Columns[0].OptionsColumn.AllowEdit = false;
            PhieuHen_gv.Columns[1].OptionsColumn.AllowEdit = false;
            try
            {
                this.selectedMaPH = int.Parse(PhieuHen_gv.GetFocusedRowCellValue("MaPH").ToString());
                
            }
            catch (Exception) { }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (PhieuHenBUS.XoaPhieuHen(this.selectedMaPH))
            {
                XtraMessageBox.Show("Xoá thành công!", "Succeed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.refreshData();
                return;
            }
            else
            {
                XtraMessageBox.Show("Xoá thất bại!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.refreshData();
                return;

            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if (PhieuHenBUS.isValidNumber(PhieuHen_gv.GetRowCellValue(PhieuHen_gv.FocusedRowHandle, "TenKH")) || !PhieuHenBUS.isValidNumber(PhieuHen_gv.GetRowCellValue(PhieuHen_gv.FocusedRowHandle, "SoDT")) || PhieuHenBUS.isValidNumber(PhieuHen_gv.GetRowCellValue(PhieuHen_gv.FocusedRowHandle, "TinhTrangMay")))
            {
                XtraMessageBox.Show("Thông tin không hợp lệ!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PHIEUHEN ph = new PHIEUHEN();
            ph.NgayLap = DateTime.Now;
            ph.TenKH= PhieuHen_gv.GetRowCellValue(PhieuHen_gv.FocusedRowHandle, "TenKH").ToString();
            ph.SoDT= PhieuHen_gv.GetRowCellValue(PhieuHen_gv.FocusedRowHandle, "SoDT").ToString();
            ph.TinhTrangMay = PhieuHen_gv.GetRowCellValue(PhieuHen_gv.FocusedRowHandle, "TinhTrangMay").ToString();
            if (PhieuHenBUS.SuaPhieuHen(this.selectedMaPH, ph))
            {
                XtraMessageBox.Show("Cập nhật thành công!", "Succeed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.refreshData();
                return;
            }
            else
            {
                XtraMessageBox.Show("Cập nhật thất bại!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.refreshData();
                return;

            }
        }
    }
}