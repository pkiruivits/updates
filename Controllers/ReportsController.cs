using pkopkop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Data.Entity.Validation;

namespace pkopkop.Controllers
{
    public class ReportsController : Controller
    {
        dbconnect conn = new dbconnect();
        // GET: Reports
        public ActionResult Index(int page = 1, string sort = "clientname", string sortdir = "asc", string search = "", bool? usedaterange = false, DateTime? dates = null, DateTime? datef = null)
        {
            try
            {
                int pagesize = 50;
                int totalRecord = 0;
                if (page < 1) page = 1;
                int skip = (page * pagesize) - pagesize;
                var projectlisting = filters(search, sort, sortdir, skip, pagesize, out totalRecord, usedaterange, dates);
                ViewBag.TotalRows = totalRecord;
                ViewBag.search = search;
               
                ViewBag.dates = dates;
                ViewBag.datef = datef;

                //string usedaterange="";
                ViewBag.usedaterange = usedaterange;
                return View(projectlisting);
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }

            }
            return View();
        }
        public List<updatesview> updateviews(int page = 1, string sort = "clientname", string sortdir = "asc", string search = "") 
        {
            var v = conn.updates.ToList();
            var l = v.Select(x => new updatesview
            {
                updateid = x.updateid,
                clientname = x.client.clientname,
                issuesod = x.issuesod,
                Responsibilities = x.Responsibilities,
                issueeod = x.issueeod,
                createdby = x.createdby,
                createdon = x.createdon,

            });
            return l.ToList();
        }
        public ActionResult rpts2(int page = 1, string sort = "clientname", string sortdir = "asc", string search = "", bool? usedaterange = false, DateTime? dates = null, DateTime? datef = null)
        {
            try
            {
                int pagesize = 50;
                int totalRecord = 0;
                if (page < 1) page = 1;
                int skip = (page * pagesize) - pagesize;
                var projectlisting = filters(search,sort, sortdir, skip, pagesize, out totalRecord, usedaterange, dates,datef);
                ViewBag.TotalRows = totalRecord;
                ViewBag.search = search;
                ViewBag.dates = dates;
                ViewBag.datef = datef;
              
                //string usedaterange="";
                ViewBag.usedaterange = usedaterange;
                return View(projectlisting);
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }

            }
            return View();
        }
        public List<updatesview> filters(string search, string sort, string sortdir, int skip, int pagesize, out int totalRecord, bool? usedaterange = false, DateTime? dates = null, DateTime? datef = null)
        {
            var v = conn.updates.ToList();

            if (usedaterange == false)
            {
                if (search == "" && dates == null)
                {
                    var l = v.Select(x => new updatesview
                    {
                        updateid = x.updateid,
                        clientname = x.client.clientname,
                        issuesod = x.issuesod,
                        Responsibilities = x.Responsibilities,
                        issueeod = x.issueeod,
                        createdby = x.createdby,
                        createdon = x.createdon,
                    });
                    totalRecord = l.Count();
                  //  l = l.OrderBy(sort + " " + sortdir);
                    if (pagesize > 0)
                    {
                        l = l.Skip(skip).Take(pagesize);
                    }
                    return l.ToList();
                }
                else if (search == "" && dates!=null)
                {
                    var l = v.Select(x => new updatesview
                    {
                        updateid = x.updateid,
                        clientname = x.client.clientname,
                        issuesod = x.issuesod,
                        Responsibilities = x.Responsibilities,
                        issueeod = x.issueeod,
                        createdby = x.createdby,
                        createdon = x.createdon,
                    }).Where(s=>s.createdon==dates);
                    totalRecord = l.Count();
                    //  l = l.OrderBy(sort + " " + sortdir);
                    if (pagesize > 0)
                    {
                        l = l.Skip(skip).Take(pagesize);
                    }
                    return l.ToList();
                }


                else
                {
                    var l = v.Select(x => new updatesview
                    {
                        updateid = x.updateid,
                        clientname = x.client.clientname,
                        issuesod = x.issuesod,
                        Responsibilities = x.Responsibilities,
                        issueeod = x.issueeod,
                        createdby = x.createdby,
                        createdon = x.createdon,

                    }).Where(s => s.clientname.Contains(search));
                    totalRecord = l.Count();
                   // l = l.OrderBy(sort + " " + sortdir);
                    if (pagesize > 0)
                    {
                        l = l.Skip(skip).Take(pagesize);
                    }
                    return l.ToList();
                }
            }
            else
            {
                if (search == "")
                {
                    var l = v.Select(x => new updatesview
                    {
                        updateid = x.updateid,
                        clientname = x.client.clientname,
                        issuesod = x.issuesod,
                        Responsibilities = x.Responsibilities,
                        issueeod = x.issueeod,
                        createdby = x.createdby,
                        createdon = x.createdon,
                    }).Where(s=>s.createdon>=dates&& s.createdon<=datef);
                    totalRecord = l.Count();
                  //  l = l.OrderBy(sort + " " + sortdir);
                    if (pagesize > 0)
                    {
                        l = l.Skip(skip).Take(pagesize);
                    }
                    return l.ToList();
                }
              
               
                else
                {
                    var l = v.Select(x => new updatesview
                    {
                        updateid = x.updateid,
                        clientname = x.client.clientname,
                        issuesod = x.issuesod,
                        Responsibilities = x.Responsibilities,
                        issueeod = x.issueeod,
                        createdby = x.createdby,
                        createdon = x.createdon,
                    }).Where(s => s.clientname.Contains(search));
                    totalRecord = l.Count();
                  //  l = l.OrderBy(sort + " " + sortdir);
                    if (pagesize > 0)
                    {
                        l = l.Skip(skip).Take(pagesize);
                    }
                    return l.ToList();
                }
            }
        }
        // GET: Reports/Details/5
        public ActionResult Details(int id)
        {
            var update = conn.updates.Where(a => a.updateid == id).FirstOrDefault();
         
            var client = conn.clients.Where(c => c.clientid == update.clientid).FirstOrDefault();
            var updtview = new updatesview
            {
                updateid = id,
                clientname=client.clientname,
                issuesod=update.issueeod,
                issueeod=update.issueeod,
                createdon=update.createdon,
               // createdby=update.createdby,
               // comments=updatecomments.
            };

            return View();
        }

        // GET: Reports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reports/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        // GET: Reports/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reports/Edit/5
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

        // GET: Reports/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reports/Delete/5
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
