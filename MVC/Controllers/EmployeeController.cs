using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using System.Net.Http;
using Newtonsoft.Json;
using PagedList.Mvc;
using PagedList;

namespace MVC.Controllers
{
    [Filters.AuthorizeEmployee] //Customized Authorization Filter
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        public ActionResult Index(int? i, string search)
        {
            //Get Employees from Web API
            IEnumerable<mvcEmployeeModel> emplist;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employee").Result;
            emplist = response.Content.ReadAsAsync<IEnumerable<mvcEmployeeModel>>().Result;

            //If a search string exists filter employees list with search string
            if (search != null)
            {
                var result = emplist.Where(x => x.name.ToUpper().Contains(search.ToUpper())).ToList();

                //Return result in a paged list
                return View(result.ToPagedList(i ?? 1, 5));
            }

            //Return full employees list if there's no search string
            else
                return View(emplist.ToPagedList(i ?? 1, 5));

        }

        //GET: /Employee/Create
        //Return Create from view
        public ActionResult Create()
        {
            return View(new mvcEmployeeModel());
        }

        //POST: /Employee/Create
        [HttpPost]
        public ActionResult Create(mvcEmployeeModel dep)
        {
            //Set message to be alerted after successful creation 
            TempData["successMessage"] = "Saved Successfully";

            //Post created employee record with API and redirect to Index
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Employee", dep).Result;
            return RedirectToAction("Index");
        }

        //GET: /Employee/Edit/id
        public ActionResult Edit(int id)
        {
            //Get Required employee record with API using ID
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employee" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<mvcEmployeeModel>().Result);
        }

        //POST: /Employee/Edit/{id}
        [HttpPost]
        public ActionResult Edit(mvcEmployeeModel emp)
        {
            //Set message to be alerted after successful update 
            TempData["successMessage"] = "Updated Successfully";

            //Post updated data with API
            HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Employee/" + emp.Id, emp).Result;
            return RedirectToAction("Index");
        }

        //DELETE: /Employee/Delete/{id}
        public ActionResult Delete(int id)
        {
            //Delete the selected employee 
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Employee/" + id.ToString()).Result;

            //Set message to be alerted after successful delete
            TempData["successMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }

        //Action method to get all departments
        //GET: /Employee/getdeps
        public ActionResult getdeps(int? i)
        {
            //Get Departments from Web API
            IEnumerable<mvcDepartmentModel> deplist;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Department").Result;
            deplist = response.Content.ReadAsAsync<IEnumerable<mvcDepartmentModel>>().Result;

            //Return result in a paged list
            return View(deplist.ToPagedList(i ?? 1, 5));
        }


        //Action method to get all employees in a selected department
        //GET: /Employee/Showemps
        public ActionResult ShowEmps(int id, int? i)
        {
            //Get all employees from Web API
            IEnumerable<mvcEmployeeModel> emplist;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employee").Result;
            emplist = response.Content.ReadAsAsync<IEnumerable<mvcEmployeeModel>>().Result;

            //Get employees with department id equals to that of selected department
            var emps = emplist.Where(x => x.depID.Equals(id));

            //Return result in a paged list
            return View(emps.ToPagedList(i ?? 1, 5));

        }


    }
}