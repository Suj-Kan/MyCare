using MyCareWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;

namespace MyCareWeb.Controllers
{
    [KnownType(typeof(ClientViewModel))]
    [KnownType(typeof(EmployeeViewModel))]
    [KnownType(typeof(TaskViewModel))]
    public class TimeSheetController : Controller
    {
        // GET: TimeSheet
        public ActionResult Index()
        {
            IEnumerable<TimeSheetViewModel> TimeSheets = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44321/api/");
                //HTTP GET
                var responseTask = client.GetAsync("timesheet");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TimeSheetViewModel>>();
                    readTask.Wait();

                    TimeSheets = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    TimeSheets = Enumerable.Empty<TimeSheetViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(TimeSheets);
        }

        // GET: TimeSheet/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TimeSheet/Create
        public ActionResult Create()
        {
            //IEnumerable<ClientViewModel> clients = null;
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://localhost:44321/api/");
            //    //HTTP GET
            //    var responseTask = client.GetAsync("client");
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var readTask = result.Content.ReadAsAsync<IList<ClientViewModel>>();
            //        readTask.Wait();

            //        clients = readTask.Result;
            //    }
            //    else //web api sent error response 
            //    {
            //        //log response status here..

            //        clients = Enumerable.Empty<ClientViewModel>();
            //    }

            //    TimeSheetViewModel viewModel = new TimeSheetViewModel
            //    {
            //        Clients = clients;
            //    }

            return View();
        }

        // POST: TimeSheet/Create
        [HttpPost]
        public ActionResult Create(TimeSheetViewModel timesheet)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TimeSheet/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TimeSheet/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TimeSheet/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TimeSheet/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
