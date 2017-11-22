using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetoNuvem.Models;
using Microsoft.AspNet.Identity;

namespace ProjetoNuvem.Controllers
{
    public class ClienteController : Controller
    {
        private BancoNuvemContainer1 db = new BancoNuvemContainer1();

        // GET: Cliente
        public ActionResult Index()
        {
         
            var clientes = db.Clientes.Where(c =>  c.Fornecedor.Email == User.Identity.Name);
            return View(clientes.ToList());
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Email = User.Identity.GetUserName();

            fornecedor = db.Fornecedores.FirstOrDefault(f => f.Email == fornecedor.Email);
            ViewBag.FornecedorId = fornecedor.Id;
            return View();
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Cpf,FornecedorId")] Cliente cliente)
        {
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Email = User.Identity.GetUserName();

            fornecedor = db.Fornecedores.FirstOrDefault(f => f.Email == fornecedor.Email);
            cliente.FornecedorId = fornecedor.Id;
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "Id", "Nome", cliente.FornecedorId);
            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Email = User.Identity.GetUserName();

            fornecedor = db.Fornecedores.FirstOrDefault(f => f.Email == fornecedor.Email);
            ViewBag.FornecedorId = fornecedor.Id;
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Cpf,FornecedorId")] Cliente cliente)
        {
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Email = User.Identity.GetUserName();

            fornecedor = db.Fornecedores.FirstOrDefault(f => f.Email == fornecedor.Email);
            cliente.FornecedorId = fornecedor.Id;
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "Id", "Nome", cliente.FornecedorId);
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
