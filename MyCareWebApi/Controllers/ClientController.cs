using MyCareWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyCareWebApi.Controllers
{
    public class ClientController : ApiController
    {
        public ClientController()
        { }

        public IHttpActionResult GetAllClients()
        {
            IList<ClientViewModel> Clients = null;

            using (var ctx = new TestDbContext())
            {
                Clients = ctx.Clients.Select(s => new ClientViewModel()
                {
                    ClientId = s.ClientId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Address = s.Address,
                    DOB = s.DOB.Value
                }).ToList<ClientViewModel>();
            }

            if (Clients == null)
            {
                return NotFound();
            }

            return Ok(Clients);
        }

        public IHttpActionResult GetClientById(int id)
        {
            ClientViewModel Client = null;

            using (var ctx = new TestDbContext())
            {
                Client = ctx.Clients
                    .Where(s => s.ClientId == id)
                    .Select(s => new ClientViewModel()
                    {
                        ClientId = s.ClientId,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Address = s.Address,
                        DOB = s.DOB.Value
                    }).FirstOrDefault<ClientViewModel>();
            }

            if (Client == null)
            {
                return NotFound();
            }

            return Ok(Client);
        }

        public IHttpActionResult GetAllClients(string name)
        {
            IList<ClientViewModel> Clients = null;

            using (var ctx = new TestDbContext())
            {
                Clients = ctx.Clients
                    .Where(s => s.FirstName.ToLower() == name.ToLower())
                    .Select(s => new ClientViewModel()
                    {
                        ClientId = s.ClientId,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Address = s.Address,
                        DOB = s.DOB.Value
                    }).ToList<ClientViewModel>();
            }

            if (Clients.Count == 0)
            {
                return NotFound();
            }

            return Ok(Clients);
        }

        public IHttpActionResult PostNewClient(ClientViewModel Client)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new TestDbContext())
            {
                ctx.Clients.Add(new Client()
                {
                    ClientId = Client.ClientId,
                    FirstName = Client.FirstName,
                    LastName = Client.LastName,
                    Address = Client.Address,
                    DOB = Client.DOB
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult Put(ClientViewModel Client)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new TestDbContext())
            {
                var existingClient = ctx.Clients.Where(s => s.ClientId == Client.ClientId)
                                                        .FirstOrDefault<Client>();

                if (existingClient != null)
                {
                    existingClient.FirstName = Client.FirstName;
                    existingClient.LastName = Client.LastName;
                    existingClient.Address = Client.Address;
                    existingClient.DOB = Client.DOB;

                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Client id");

            using (var ctx = new TestDbContext())
            {
                var Client = ctx.Clients
                    .Where(s => s.ClientId == id)
                    .FirstOrDefault();

                ctx.Entry(Client).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
