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

namespace QLCuaHangDDS.GUI.ManHinhTraCuuHoaDonBanHang
{
    public partial class TraCuuHoaDonBanHang : DevExpress.XtraEditors.XtraForm
    {
        public TraCuuHoaDonBanHang()
        {
            InitializeComponent();
            // This line of code is generated by Data Source Configuration Wizard
            gridControl.DataSource = new QLCuaHangDDS.DAO.QLCuaHangDDSDBDataContext().HOADONBANHANGs;
            gridView.Columns[0].Visible = false;
            gridView.Columns[1].Caption = "Tên khách hàng";
            gridView.Columns[2].Caption = "Ngày lập hóa đơn";
            gridView.Columns[3].Caption = "Tổng tiền";
        }
    }
}