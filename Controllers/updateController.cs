using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pkopkop.Models;
using System.Data.Entity;

namespace pkopkop.Controllers
{
    public class updateController : Controller
    {
        dbconnect conn = new dbconnect();
        // GET: update
        public ActionResult Index()
        {
            var x = conn.updates.ToList();
            var updates = updateviews();
            return View(updates);
        }
        public List<updatesview> updateviews()
        {
            var v = conn.updates.ToList();
            var l = v.Select(x => new updatesview {
                updateid = x.updateid,
                clientname = x.client.clientname,
                issuesod = x.issuesod,
                Responsibilities = x.Responsibilities,
                issueeod = x.issueeod,
                createdby = x.createdby,
                createdon = x.createdon,

            }).Where(c=>c.createdby==User.Identity.Name);
            return l.ToList();
        }
        // GET: update/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: update/Create
        public ActionResult Create()
        {
            var cols = conn.clients.Select(s => new { Text = s.clientname, Value = s.clientid }).ToList();
            ViewBag.clientsColection = new SelectList(cols, "Value", "Text").Take(5);
            return View();
        }

        // POST: update/Create
        [HttpPost]
        public ActionResult Create(pkopkop.update update)
        {
            try
            {
                var contName = update.clientid;
                if (contName <=0)
                {

                }
                update.createdby = User.Identity.Name;
                conn.updates.Add(update);
                conn.SaveChanges();
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: update/Edit/5
        public ActionResult Edit1(int id)
        {
            var cols = conn.clients.Select(s => new { Text = s.clientname, Value = s.clientid }).ToList();
            ViewBag.clientsColection = new SelectList(cols, "Value", "Text");
            var v = conn.updates.Where(x => x.updateid == id).FirstOrDefault();
            return View(v);
        }
        public ActionResult Edit(int id)
        {
            var cols = conn.clients.Select(s => new { Text = s.clientname, Value = s.clientid }).ToList();
            ViewBag.clientsColection = new SelectList(cols, "Value", "Text");
            var v = conn.updates.Where(x => x.updateid == id).FirstOrDefault();
           // TODO: Add update logic here
            return View(v);
        }

        // POST: update/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, update update)
        {
            try
            {
                update.modifiedon = DateTime.Now;
                update.createdby= User.Identity.Name;
                conn.Entry(update).State = EntityState.Modified;
                conn.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: update/Delete/5
        public ActionResult Delete(int id)
        {
var up= conn.updates.Where(x => x.updateid == id).FirstOrDefault();
            return View(up);
        }

        // POST: update/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, update update)
        {
            try
            {
                conn.updates.Remove(conn.updates.Where(x => x.updateid == id).FirstOrDefault());
                conn.SaveChanges();
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
