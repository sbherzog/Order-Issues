using OrderIssues.Data;
using OrderIssues.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderIssues.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            OrderRepository reps = new OrderRepository(Properties.Settings.Default.ConStr);
            IndexViewModel vm = new IndexViewModel();            
            vm.order = reps.GetOrders();
            return View(vm);
        }

        public ActionResult AddnewOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddnewOrder(Order ord)
        {
            OrderRepository reps = new OrderRepository(Properties.Settings.Default.ConStr);
            reps.AddNewOrder(ord);
            TempData["success"] = "Your order was successfully added"; 
            return Redirect("/");
        }
        public  ActionResult MarkCompleted(int orderId)
        {
            OrderRepository reps = new OrderRepository(Properties.Settings.Default.ConStr);
            reps.MarkCompleted(orderId);
            return Redirect("/");
        }

        public ActionResult MarkResolved(int issueId)
        {
            OrderRepository reps = new OrderRepository(Properties.Settings.Default.ConStr);
            reps.MarkResolved(issueId);
            return Redirect("/Home/IssuesByOrderId?orderId="+issueId);
        }
              

        public ActionResult IssuesByOrderId(int orderId)
        {
            OrderRepository reps = new OrderRepository(Properties.Settings.Default.ConStr);
            IssuesByOrderIdModelView vm = new IssuesByOrderIdModelView();
            vm.issue = reps.IssuesByOrderId(orderId);
            vm.order = reps.GetOrderById(orderId);
            return View(vm);
        }

        public ActionResult AddNewIssue(int orderId)
        {
            OrderRepository reps = new OrderRepository(Properties.Settings.Default.ConStr);
            IssuesByOrderIdModelView vm = new IssuesByOrderIdModelView();
            vm.order = reps.GetOrderById(orderId);
            return View(vm);
        }
        [HttpPost]
        public ActionResult AddNewIssue(Issue issue)
        {
            OrderRepository reps = new OrderRepository(Properties.Settings.Default.ConStr);
            reps.AddNewIssue(issue);
            return Redirect("/Home/IssuesByOrderId?orderId="+issue.OrderId);
        }
    }
}