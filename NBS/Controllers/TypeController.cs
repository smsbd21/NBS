using NBS;
using System.Net;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections.Generic;

namespace NBS.Controllers
{
    public class TypeController : Controller
    {
        private DbCon db = new DbCon();

        // GET: /Type/
        public ActionResult Index(string sType)
        {
            if (Session["role"] == null) return HttpNotFound();
            else if (Session["role"].ToString() == "Admin")
            {
                var oType = db.Tb_Type.AsQueryable();
                if (!string.IsNullOrEmpty(sType)) oType = oType.Where(x => x.Type.Contains(sType.Trim()));
                return View(oType.ToList());
            }
            else return RedirectToAction("Index", "Auth");
        }

        // GET: /Type/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Type oType = db.Tb_Type.Find(id);
            if (oType == null) return HttpNotFound();
            return View(oType);
        }

        // GET: /Type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Type,MonAmt,Status")] Tb_Type oType)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Type.Add(oType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oType);
        }

        // GET: /Type/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Type oType = db.Tb_Type.Find(id);
            if (oType == null) return HttpNotFound();
            return View(oType);
        }

        // POST: /Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Type,MonAmt,Status")] Tb_Type oType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oType);
        }

        // GET: /Type/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Type oType = db.Tb_Type.Find(id);
            if (oType == null) return HttpNotFound();
            return View(oType);
        }

        // POST: /Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Tb_Type oType = db.Tb_Type.Find(id);
            db.Tb_Type.Remove(oType);
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
