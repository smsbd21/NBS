using NBS;
using System.Net;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections.Generic;

namespace NBS.Controllers
{
    public class RolesController : Controller
    {
        private DbCon db = new DbCon();
        // GET: /Roles/
        public ActionResult Index(string sRole)
        {
            if (Session["role"] == null) return HttpNotFound();
            else if (Session["role"].ToString() == "Admin")
            {
                var oRole = db.Tb_Roles.AsQueryable();
                if (!string.IsNullOrEmpty(sRole)) oRole = oRole.Where(x => x.Role.Contains(sRole.Trim()));
                return View(oRole.ToList());
            }
            else return RedirectToAction("Index", "Auth");
        }

        // GET: /Roles/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Roles oRoles = db.Tb_Roles.Find(id);
            if (oRoles == null)
            {
                return HttpNotFound();
            }
            return View(oRoles);
        }

        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Role,Status")] Tb_Roles oRoles)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Roles.Add(oRoles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oRoles);
        }

        // GET: /Roles/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Roles oRoles = db.Tb_Roles.Find(id);
            if (oRoles == null)
            {
                return HttpNotFound();
            }
            return View(oRoles);
        }

        // POST: /Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Role,Status")] Tb_Roles oRoles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oRoles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oRoles);
        }

        // GET: /Roles/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Roles oRoles = db.Tb_Roles.Find(id);
            if (oRoles == null)
            {
                return HttpNotFound();
            }
            return View(oRoles);
        }

        // POST: /Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Tb_Roles oRoles = db.Tb_Roles.Find(id);
            db.Tb_Roles.Remove(oRoles);
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
