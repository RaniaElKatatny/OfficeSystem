using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace MVC.Controllers
{
	public class LoginController : Controller
	{
		
		// GET: /Login/
		//Return Login page view
		public ActionResult Login()
		{
			return View();
		}

		//POST: /Login/
		[HttpPost]
		public ActionResult Login(string email, string password)
		{
			//Get all employees using Web API
			IEnumerable<mvcEmployeeModel> emplist;
			HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employee").Result;
			emplist = response.Content.ReadAsAsync<IEnumerable<mvcEmployeeModel>>().Result;

			//Get the record in which name and password matches input
			var result = emplist.Where(x => x.email.Contains(email)).Where(x => x.password.Contains(password)).ToList();

			//If the record matches
			if (result.Any())
			{
				//Activate the user session
				 Session["LoggedIn"] = true;

				//Redirect to homepage
				return RedirectToAction("Index", "Home", new { area = "" });

			}

			//If credentials does not match go back to login page
			else return RedirectToAction("Login");
		}

		//GET: /Login/Logout
		public ActionResult Logout()
		{
			//Deactivate the user session
			Session["LoggedIn"] = null;

			//Redirect to login page
			return RedirectToAction("Login", "Login");
		}
	}
}