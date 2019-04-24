using System;  
using System.Collections.Generic;  
using System.Diagnostics;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using AgendaSqlAspVue.Models; 

namespace AgendaSqlAspVue.Controllers{
    public class EmployeeController : Controller
    {
        EmployeeDataAccessLayer Objemployee = new EmployeeDataAccessLayer();
        public IActionResult Index()
        {
            List<Employee> lstEmployee = new List<Employee>();
            lstEmployee=Objemployee.GetAllEmployees().ToList();
            return View(lstEmployee);
        }
        public IActionResult Create(){
            return View();
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind]Employee Employee){
            if(ModelState.IsValid){
                Objemployee.AddEmployee(Employee);
                return RedirectToAction("Index");
            }
            return View(Employee);
        }
        [HttpGet]
        public IActionResult Edit(int? id){
            if(id==null){
                return NotFound();
            }
            Employee employee= Objemployee.GetEmployeeData(id);
            if(employee == null){
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind]Employee employee){
            if(id!=employee.ID){
                return NotFound();
            }
            if(ModelState.IsValid){
                Objemployee.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        [HttpGet]
        public IActionResult Detalis(int? id){
            if(id== null){
                return NotFound(); 
            }
            Employee employee = Objemployee.GetEmployeeData(id);
            if(employee== null){
                return NotFound();
            }
            return View(employee);
        }
        [HttpGet]
        public IActionResult Delete(int? id){
            if(id==null){
                return NotFound();
            }
            Employee employee = Objemployee.GetEmployeeData(id);
            if(employee==null){
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id){
            Objemployee.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

    }
}