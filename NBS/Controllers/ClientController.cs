using NBS;
using System.Net;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections.Generic;

namespace NBS.Controllers
{
    public class ClientController : Controller
    {
        private DbCon db = new DbCon();

        // GET: /Client/
        public ActionResult Index(string sCode, string sName, string sType, string sArea)
        {
            if (Session["role"] == null) return HttpNotFound();
            else if (Session["role"].ToString() == "Admin")
            {
                var oClient = db.Tb_Client.Include(t => t.Tb_Area).Include(t => t.Tb_Type);
                //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
                if (!string.IsNullOrEmpty(sArea)) oClient = oClient.Where(x => x.Tb_Area.Area.Contains(sArea));
                if (!string.IsNullOrEmpty(sType)) oClient = oClient.Where(x => x.Tb_Type.Type.Contains(sType));
                if (!string.IsNullOrEmpty(sCode)) oClient = oClient.Where(x => x.Code.ToLower().Contains(sCode.Trim().ToLower()));
                if (!string.IsNullOrEmpty(sName)) oClient = oClient.Where(x => x.Name.ToLower().Contains(sName.Trim().ToLower()));
                return View(oClient.ToList());
            }
            else return RedirectToAction("Index", "Auth");
        }

        // GET: /Client/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Client oClient = db.Tb_Client.Find(id);
            if (oClient == null) return HttpNotFound();
            return View(oClient);
        }
        // POST: /Client/Details/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "CustId,PayDate,PayMon,PayAmt,Remark,AddedBy,AddTime")] Tb_Collect oCol)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Collect.Add(oCol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oCol);
        }
        // GET: /Client/Create
        public ActionResult Create()
        {
            ViewBag.AreaId = new SelectList(db.Tb_Area, "Id", "Area");
            ViewBag.TypeId = new SelectList(db.Tb_Type, "Id", "Type");
            return View();
        }

        // POST: /Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Code,Name,AreaId,TypeId,Start,Close,MonAmt,SrvAmt,Status,Remark")] Tb_Client oClient)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Client.Add(oClient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AreaId = new SelectList(db.Tb_Area, "Id", "Area", oClient.AreaId);
            ViewBag.TypeId = new SelectList(db.Tb_Type, "Id", "Type", oClient.TypeId);
            return View(oClient);
        }

        // GET: /Client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Client oClient = db.Tb_Client.Find(id);
            if (oClient == null) return HttpNotFound();
            ViewBag.AreaId = new SelectList(db.Tb_Area, "Id", "Area", oClient.AreaId);
            ViewBag.TypeId = new SelectList(db.Tb_Type, "Id", "Type", oClient.TypeId);
            return View(oClient);
        }

        // POST: /Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Code,Name,AreaId,TypeId,Start,Close,MonAmt,SrvAmt,Status,Remark")] Tb_Client oClient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oClient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AreaId = new SelectList(db.Tb_Area, "Id", "Area", oClient.AreaId);
            ViewBag.TypeId = new SelectList(db.Tb_Type, "Id", "Type", oClient.TypeId);
            return View(oClient);
        }

        // GET: /Client/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Client oClient = db.Tb_Client.Find(id);
            if (oClient == null) return HttpNotFound();
            return View(oClient);
        }

        // POST: /Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_Client oClient = db.Tb_Client.Find(id);
            db.Tb_Client.Remove(oClient);
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
