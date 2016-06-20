using BLibrary.Entity.Filters.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BLibrary.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DataTables()
        {
            return View(new FormFilter());
        }

        public JsonResult GetTableData(DataTablesFilter filter)
        {
            List<Form> data = new List<Form>() {
                new Form() {InstanceCode="0122313113",ApplyDate="2015-10-12",ApplyEmpCode="12121212" },
                new Form() {InstanceCode="0122313113",ApplyDate="2015-10-12",ApplyEmpCode="12121212" },
                new Form() {InstanceCode="0122313113",ApplyDate="2015-10-12",ApplyEmpCode="12121212" },
                new Form() {InstanceCode="0122313113",ApplyDate="2015-10-12",ApplyEmpCode="12121212" },
                new Form() {InstanceCode="0122313113",ApplyDate="2015-10-12",ApplyEmpCode="12121212" },
                new Form() {InstanceCode="0122313113",ApplyDate="2015-10-12",ApplyEmpCode="12121212" },
                new Form() {InstanceCode="0122313113",ApplyDate="2015-10-12",ApplyEmpCode="12121212" },
                new Form() {InstanceCode="0122313113",ApplyDate="2015-10-12",ApplyEmpCode="12121212" },
                new Form() {InstanceCode="0122313113",ApplyDate="2015-10-12",ApplyEmpCode="12121212" },
                new Form() {InstanceCode="0122313113",ApplyDate="2015-10-12",ApplyEmpCode="12121212" },
                new Form() {InstanceCode="0122313113",ApplyDate="2015-10-12",ApplyEmpCode="12121212" },
                new Form() {InstanceCode="0122313113",ApplyDate="2015-10-12",ApplyEmpCode="12121212" },
                new Form() {InstanceCode="0122313113",ApplyDate="2015-10-12",ApplyEmpCode="12121212" },
            };
            return Json(new
            {
                //draw = 1,
                recordsFiltered = data.Count,
                //error = 
                recordsTotal = data.Count,
                data = data.Skip(filter.iDisplayStart).Take(filter.PageSize).ToList()
            }, JsonRequestBehavior.AllowGet);
        }
    }
    public class Form
    {
        public string InstanceCode { get; set; }

        public string ApplyEmpCode { get; set; }

        public string ApplyDate { get; set; }

    }
}