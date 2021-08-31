using NBS;
using System;
using System.IO;
using NBS.Models;
using System.Net;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections.Generic;

namespace NBS.Controllers
{
    public class UsersController : Controller
    {
        private DbCon db = new DbCon();

        // GET: /Users/
        public ActionResult Index(string sName, string sCell, string sUser, string sRole, string sArea)
        {
            if (Session["role"] == null) return HttpNotFound();
            else if (Session["role"].ToString() == "Admin")
            {
                var oUser = db.Tb_Users.Include(t => t.Tb_Area).Include(t => t.Tb_Roles);
                if (!string.IsNullOrEmpty(sUser)) oUser = oUser.Where(x => x.User.Contains(sUser));
                if (!string.IsNullOrEmpty(sCell)) oUser = oUser.Where(x => x.Cell.Contains(sCell.Trim()));
                if (!string.IsNullOrEmpty(sArea)) oUser = oUser.Where(x => x.Tb_Area.Area.Contains(sArea));
                if (!string.IsNullOrEmpty(sRole)) oUser = oUser.Where(x => x.Tb_Roles.Role.Contains(sRole));
                if (!string.IsNullOrEmpty(sName)) oUser = oUser.Where(x => x.Name.ToLower().Contains(sName.Trim().ToLower()));
                return View(oUser.ToList());
            }
            else return RedirectToAction("Index", "Auth");
        }

        // GET: /Users/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Users oUser = db.Tb_Users.Find(id);
            if (oUser == null) return HttpNotFound();
            return View(oUser);
        }

        // GET: /Users/Create
        public ActionResult Create()
        {
            ViewBag.AreaId = new SelectList(db.Tb_Area, "Id", "Area");
            ViewBag.RoleId = new SelectList(db.Tb_Roles, "Id", "Role");
            return View();
        }

        // POST: /Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Cell,User,Pwd,AreaId,RoleId,Status,Address,Photo")] Tb_Users oUser, HttpPostedFileBase hpfBase)
        {
            //getting the extension(ex-.jpg)  
            var ext = Path.GetExtension(hpfBase.FileName);
            //getting only file name(ex-sms.jpg)  
            var oFile = Path.GetFileName(hpfBase.FileName);
            var oExt = new[] { ".bmp", ".jpg", ".jpeg", ".png" };
            if (oExt.Contains(ext)) //check what type of extension  
            {
                string name = Path.GetFileNameWithoutExtension(oFile); //getting file name without extension
                string myfile = DateTime.Now.ToString("yyyyMMdd_HHmmssFF2_") + HomeViewModel.GetRandomStr(10) + ext; //appending the name with id
                var oPath = Path.Combine(Server.MapPath("~/images/site/"), myfile); //store the file inside ~/project folder(images/site)  
                //file.SaveAs(Server.MapPath("~/images/site/" + myfile));
                if (ModelState.IsValid)
                {
                    oUser.Photo = myfile;
                    db.Tb_Users.Add(oUser);
                    db.SaveChanges();
                    hpfBase.SaveAs(oPath);
                    return RedirectToAction("Index");
                }
            }
            else ViewBag.message = "Please choose only Image file";

            ViewBag.AreaId = new SelectList(db.Tb_Area, "Id", "Area", oUser.AreaId);
            ViewBag.RoleId = new SelectList(db.Tb_Roles, "Id", "Role", oUser.RoleId);
            return View(oUser);
        }

        // GET: /Users/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Users oUser = db.Tb_Users.Find(id);
            if (oUser == null) return HttpNotFound();
            ViewBag.AreaId = new SelectList(db.Tb_Area, "Id", "Area", oUser.AreaId);
            ViewBag.RoleId = new SelectList(db.Tb_Roles, "Id", "Role", oUser.RoleId);
            return View(oUser);
        }

        // POST: /Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Cell,User,Pwd,AreaId,RoleId,Status,Address,Photo")] Tb_Users oUser, HttpPostedFileBase hpfBase)
        {
            var ext = Path.GetExtension(hpfBase.FileName);
            var oFile = Path.GetFileName(hpfBase.FileName);
            var oExt = new[] { ".bmp", ".jpg", ".jpeg", ".png" };
            if (oExt.Contains(ext))
            {
                string name = Path.GetFileNameWithoutExtension(oFile);
                string myfile = DateTime.Now.ToString("yyyyMMdd_HHmmssFF2_") + HomeViewModel.GetRandomStr(10) + ext;
                var oPath = Path.Combine(Server.MapPath("~/images/site/"), myfile);
                if (ModelState.IsValid)
                {
                    oUser.Photo = myfile;
                    db.Entry(oUser).State = EntityState.Modified;
                    db.SaveChanges();
                    hpfBase.SaveAs(oPath);
                    return RedirectToAction("Index");
                }
            }
            else ViewBag.message = "Please choose only Image file";

            
            ViewBag.AreaId = new SelectList(db.Tb_Area, "Id", "Area", oUser.AreaId);
            ViewBag.RoleId = new SelectList(db.Tb_Roles, "Id", "Role", oUser.RoleId);
            return View(oUser);
        }

        // GET: /Users/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Users oUser = db.Tb_Users.Find(id);
            if (oUser == null) return HttpNotFound();
            return View(oUser);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Tb_Users oUser = db.Tb_Users.Find(id);
            db.Tb_Users.Remove(oUser);
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
