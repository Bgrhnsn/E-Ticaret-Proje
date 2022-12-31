using B1RKOD.Data;
using B1RKOD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace B1RKOD.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public OrderDetailsVM OrderVM { get; set; }
        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpPost]
        [Authorize(Roles =Diger.Role_Admin)]
        public IActionResult Onaylandi()
        {
            OrderHeader orderHeader = _db.OrderHeaders.FirstOrDefault(i => i.Id == OrderVM.OrderHeader.Id);
            orderHeader.OrderStatus = Diger.Durum_Onaylandı;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = Diger.Role_Admin)]
        public IActionResult KargoyaVer()
        {
            OrderHeader orderHeader = _db.OrderHeaders.FirstOrDefault(i => i.Id == OrderVM.OrderHeader.Id);
            orderHeader.OrderStatus = Diger.Durum_Kargoda;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            OrderVM = new OrderDetailsVM
            {

                OrderHeader = _db.OrderHeaders.FirstOrDefault(i => i.Id == id),
                OderDetails = _db.OderDetails.Where(x => x.OrderId == id).Include(x => x.Product)
            };
           
            return View(OrderVM);
        }

        public IActionResult Index()
        {

            var claimsIdentitiy = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentitiy.FindFirst(ClaimTypes.NameIdentifier);
            IEnumerable<OrderHeader> orderHeaderList;
            if(User.IsInRole(Diger.Role_Admin))
            {
                orderHeaderList = _db.OrderHeaders.ToList();

            }
            else
            {
                orderHeaderList = _db.OrderHeaders.Where(i => i.ApplicationUserId == claim.Value).Include(i => i.ApplicationUser);
            }

            return View(orderHeaderList);
        }
        public IActionResult Beklenen()
        {

            var claimsIdentitiy = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentitiy.FindFirst(ClaimTypes.NameIdentifier);
            IEnumerable<OrderHeader> orderHeaderList;
            if (User.IsInRole(Diger.Role_Admin))
            {
                orderHeaderList = _db.OrderHeaders.Where(i => i.OrderStatus == Diger.Durum_Beklemede);

            }
            else
            {
                orderHeaderList = _db.OrderHeaders.Where(i => i.ApplicationUserId == claim.Value&&i.OrderStatus==Diger.Durum_Beklemede)
                    .Include(i => i.ApplicationUser);
            }

            return View(orderHeaderList);
        }
        public IActionResult Onaylanan()
        {

            var claimsIdentitiy = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentitiy.FindFirst(ClaimTypes.NameIdentifier);
            IEnumerable<OrderHeader> orderHeaderList;
            if (User.IsInRole(Diger.Role_Admin))
            {
                orderHeaderList = _db.OrderHeaders.Where(i => i.OrderStatus == Diger.Durum_Onaylandı);

            }
            else
            {
                orderHeaderList = _db.OrderHeaders.Where(i => i.ApplicationUserId == claim.Value && i.OrderStatus == Diger.Durum_Onaylandı)
                    .Include(i => i.ApplicationUser);
            }

            return View(orderHeaderList);
        }
        public IActionResult Kargolanan()
        {

            var claimsIdentitiy = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentitiy.FindFirst(ClaimTypes.NameIdentifier);
            IEnumerable<OrderHeader> orderHeaderList;
            if (User.IsInRole(Diger.Role_Admin))
            {
                orderHeaderList = _db.OrderHeaders.Where(i => i.OrderStatus == Diger.Durum_Kargoda);

            }
            else
            {
                orderHeaderList = _db.OrderHeaders.Where(i => i.ApplicationUserId == claim.Value && i.OrderStatus == Diger.Durum_Kargoda)
                    .Include(i => i.ApplicationUser);
            }

            return View(orderHeaderList);
        }
    }
}
