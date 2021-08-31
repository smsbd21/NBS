using NBS;
using System.Net;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections.Generic;

namespace NBS.Controllers
{
    public class AreaController : Controller
    {
        private DbCon db = new DbCon();

        // GET: /Area/
        public ActionResult Index(string sArea)
        {
            if (Session["role"] == null) return HttpNotFound();
            else if (Session["role"].ToString() == "Admin")
            {
                var oArea = db.Tb_Area.AsQueryable();
                if (!string.IsNullOrEmpty(sArea)) oArea = oArea.Where(x => x.Area.Contains(sArea.Trim()));
                return View(oArea.ToList());
            }
            else return RedirectToAction("Index", "Auth");
        }

        // GET: /Area/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Area oArea = db.Tb_Area.Find(id);
            if (oArea == null)
            {
                return HttpNotFound();
            }
            return View(oArea);
        }

        // GET: /Area/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Area/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Area,Status")] Tb_Area oArea)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Area.Add(oArea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oArea);
        }

        // GET: /Area/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Area oArea = db.Tb_Area.Find(id);
            if (oArea == null) return HttpNotFound();
            return View(oArea);
        }

        // POST: /Area/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Area,Status")] Tb_Area oArea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oArea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oArea);
        }

        // GET: /Area/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Area oArea = db.Tb_Area.Find(id);
            if (oArea == null) return HttpNotFound();
            return View(oArea);
        }

        // POST: /Area/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Tb_Area oArea = db.Tb_Area.Find(id);
            db.Tb_Area.Remove(oArea);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool oDisp)
        {
            if (oDisp) db.Dispose();
            base.Dispose(oDisp);
        }
    }
}
