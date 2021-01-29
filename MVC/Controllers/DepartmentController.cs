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
    public class DepartmentController : Controller
    {
        //
        // GET: /Department/
        public ActionResult Index(string search, int?i )
        {
            //Get Departments from Web API
            IEnumerable<mvcDepartmentModel> deplist;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Department").Result;
            deplist = response.Content.ReadAsAsync<IEnumerable<mvcDepartmentModel>>().Result;

            //If a search string exists filter Departments list with search string
            if (search != null)
            {
                var result = deplist.Where(x => x.name.ToUpper().Contains(search.ToUpper())).ToList();

                //Return result in a paged list
                return View(result.ToPagedList(i ?? 1, 5));
            }

            //Return full departments list if there's no search string
            else
            return View(deplist.ToPagedList(i ?? 1, 5));

        }

        //GET: /Department/Create
        //Return Create from view
        public ActionResult Create()
        {
            return View(new mvcDepartmentModel());
        }

        //POST: /Department/Create
        [HttpPost]
        public ActionResult Create(mvcDepartmentModel dep)
        {
            //Set message to be alerted after successful creation 
            TempData["successMessage"] = "Saved Successfully";

            //Post created department with API and redirect to Index
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Department", dep).Result;
            return RedirectToAction("Index");
        }

        //GET: /Department/Edit/id
        public ActionResult Edit(int id)
        {
            //Get Required department with API using ID
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Department"+ id.ToString()).Result;
            return View(response.Content.ReadAsAsync<mvcDepartmentModel>().Result);
        }

        //POST: /Department/Edit/{id}
        [HttpPost]
        public ActionResult Edit(mvcDepartmentModel dep)
        {
            //Set message to be alerted after successful update 
            TempData["successMessage"] = "Updated Successfully";

            //Post updated data with API
            HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Department/"+dep.Id, dep).Result;
            return RedirectToAction("Index");
        }

        //DELETE: /Department/Delete/{id}
        public ActionResult Delete(int id)
        {
            //Delete the selected department 
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Department/" + id.ToString()).Result;

            //Set message to be alerted after successful delete 
            TempData["successMessage"] = "Deleted Successfully";


            return RedirectToAction("Index");
        }





    }
}