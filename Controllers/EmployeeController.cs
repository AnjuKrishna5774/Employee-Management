using Employee_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Employee_Management_System.Controllers
{
    public class EmployeeController : Controller
    {
        Db_Operation op = new Db_Operation();
        
        // GET: EmployeeController
        public ActionResult Index()
        {
            
            List<EmployeeModel> list = new List<EmployeeModel>();
            list = op.GetAllEmployee();
            return View(list);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            EmployeeModel model = new EmployeeModel();
            model = op.GetOneEmployee(id);
            return View(model);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeModel model)
        {
            try
            {
                string reg1 = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";
                if (!Regex.Match(model.empl_password, reg1).Success)
                {
                    ModelState.AddModelError("empl_password", "Please Enter Valid password");

                }
                var findage = DateTime.Now.Year - model.empl_dob.Year;
                if(findage<18)
                {

                    ModelState.AddModelError("empl_dob", "You Are Not Eligible for Register ");
                }
                var count = op.CheckEmailUniqueness(0, model.empl_email);
                if(count>0)
                {
                    ModelState.AddModelError("empl_email", "This email exist ");
                }
                if (ModelState.IsValid)
                {
                    model.empl_id = 0;
                    op.InsertEmployee(model);
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    return View(model);
                }
               
                
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            EmployeeModel model = new EmployeeModel();
            
            model = op.GetOneEmployee(id);
            return View(model);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeModel model)
        {
            try
            {
                model.empl_id = id;
                op.UpdateEmployee(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            EmployeeModel model = new EmployeeModel();
            model = op.GetOneEmployee(id);
            return View(model);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, EmployeeModel model)
        {
            try
            {
                op.DeleteEmployee(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public JsonResult CheckEmployeeEmailUnqnes(int empl_id, string empl_gmail)
        {
            int count = op.CheckEmailUniqueness(empl_id, empl_gmail);
            return Json(count);
        }
    }
}
