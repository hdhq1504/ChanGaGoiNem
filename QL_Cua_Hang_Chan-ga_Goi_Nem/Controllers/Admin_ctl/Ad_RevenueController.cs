﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using QL_Cua_Hang_Chan_ga_Goi_Nem.Models;
using QL_Cua_Hang_Chan_ga_Goi_Nem.Models.Admin_Md;

namespace QL_Cua_Hang_Chan_ga_Goi_Nem.Controllers.Admin_ctl
{
    public class Ad_RevenueController : Controller
    {
        //
        // GET: /Ad_Revenue/
        DataDataContext db = new DataDataContext();
        public ActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            // Lọc các hóa đơn có trạng thái 'da_giao'
            var hoaDonsQuery = db.hoa_dons.Where(hd => hd.trang_thai == "da_giao");

            // Lọc thêm theo khoảng thời gian nếu có
            if (startDate.HasValue)
            {
                hoaDonsQuery = hoaDonsQuery.Where(hd => hd.ngay_lap >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                hoaDonsQuery = hoaDonsQuery.Where(hd => hd.ngay_lap <= endDate.Value);
            }

            // Lấy danh sách các hóa đơn và tính tổng tiền
            var hoaDons = hoaDonsQuery.Select(hd => new DoanhThuModel
            {
                HoaDonId = hd.hoa_don_id,
                NgayLap = hd.ngay_lap,
                TongTien = (decimal)hd.tong_tien,
                TrangThai = hd.trang_thai
            }).ToList();

            // Tính tổng tiền
            decimal tongTien = hoaDons.Sum(hd => hd.TongTien);

            // Truyền tổng tiền vào ViewBag để hiển thị
            ViewBag.TongTien = tongTien;

            // Trả về View với danh sách hóa đơn đã lọc
            return View(hoaDons);
        }
        
    }
}
