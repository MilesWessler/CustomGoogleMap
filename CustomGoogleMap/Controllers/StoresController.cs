using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomGoogleMap.Models;
using GoogleMaps.LocationServices;

namespace CustomGoogleMap.Controllers
{
    public class StoresController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Stores
        public ActionResult Index()
        {
            return View(_context.Stores.ToList());
        }

        // GET: Stores/Details/5
        public ActionResult ViewMap(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = _context.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // GET: Stores/Create
        public ActionResult AddStoreLocation()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStoreLocation([Bind(Include = "Id,Address,Latitude,Longitude,Content")] Store store)
        {
            if (ModelState.IsValid)
            {
                var locationService = new GoogleLocationService();
                var point = locationService.GetLatLongFromAddress(store.Address);

                Store storeToAdd = new Store
                {
                    Address = store.Address,
                    Latitude = point.Latitude,
                    Longitude = point.Longitude,
                    Content = store.Content
                };

                _context.Stores.Add(storeToAdd);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(store);
        }

        // GET: Stores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = _context.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Address,Latitude,Longitude,Content")] Store store)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(store).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(store);
        }

        // GET: Stores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = _context.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Store store = _context.Stores.Find(id);
            _context.Stores.Remove(store);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
