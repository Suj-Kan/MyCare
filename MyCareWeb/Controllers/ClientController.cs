using MyCareWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MyCareWeb.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            IEnumerable<ClientViewModel> Clients = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44321/api/");
                //HTTP GET
                var responseTask = client.GetAsync("client");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ClientViewModel>>();
                    readTask.Wait();

                    Clients = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    Clients = Enumerable.Empty<ClientViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(Clients);
        }
         
       

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(ClientViewModel client)
        {
            try
            {
                using (var httpclient = new HttpClient())
                {
                    httpclient.BaseAddress = new Uri("https://localhost:44321/api/client");

                    //HTTP POST
                    var postTask = httpclient.PostAsJsonAsync<ClientViewModel>("client", client);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                return View(client);
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            ClientViewModel Client = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44321/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Client?Id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ClientViewModel>();
                    readTask.Wait();

                    Client = readTask.Result;
                }
            }

            return View(Client);
        }

        [HttpPost]
        public ActionResult Edit(ClientViewModel Client)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44321/api/Client");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<ClientViewModel>("Client", Client);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(Client);
        }

      

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44321/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Client/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}
