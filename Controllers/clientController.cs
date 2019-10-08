using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pkopkop.Controllers
{
    public class clientController : Controller
    {
        dbconnect conn = new dbconnect();
        // GET: client
        public ActionResult Index()
        {
            var x = conn.clients.ToList();
            return View(x);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: client/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: client/Create
        [HttpPost]
        public ActionResult Create(client client)
        {
            try
            {
            conn.clients.Add(client);
            conn.SaveChanges();
            return RedirectToAction("Index");
            }
            catch
            {
            return View();
            }
        }

        // GET: client/Edit/5
        public ActionResult Edit(int id)
        {
            var client = conn.clients.Where(c => c.clientid == id).FirstOrDefault();
            return View(client);
        }

        // POST: client/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, client client)
        {
            try
            {
                client.modifiedon = DateTime.Now;
                conn.Entry(client).State = EntityState.Modified;
                conn.SaveChanges();// TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: client/Delete/5
        public ActionResult Delete(int id)
        {
            var cl = conn.clients.Where(x => x.clientid == id).FirstOrDefault();
            return View(cl);
        }

        // POST: client/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, client client)
        {
            try
            {
                conn.clients.Remove(conn.clients.Where(x => x.clientid == id).FirstOrDefault());
                conn.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
