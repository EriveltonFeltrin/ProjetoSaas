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
    public class ProdutoController : Controller
    {
        private BancoNuvemContainer2 db = new BancoNuvemContainer2();

        // GET: Produto
        public ActionResult Index()
        {
            var produtos = db.Produtos.Where(p => p.Fornecedor.Email == User.Identity.Name);
            return View(produtos.ToList());
        }

        // GET: Produto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // GET: Produto/Create
        public ActionResult Create()
        {
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Email = User.Identity.GetUserName();

            fornecedor = db.Fornecedores.FirstOrDefault(f => f.Email == fornecedor.Email);
            ViewBag.FornecedorId = fornecedor.Id;
            return View();
        }

        // POST: Produto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Valor,Quantidade,FornecedorId")] Produto produto)
        {
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Email = User.Identity.GetUserName();

            fornecedor = db.Fornecedores.FirstOrDefault(f => f.Email == fornecedor.Email);
            produto.FornecedorId = fornecedor.Id;

            if (ModelState.IsValid)
            {
                db.Produtos.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "Id", "Nome", produto.FornecedorId);
            return View(produto);
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Email = User.Identity.GetUserName();

            fornecedor = db.Fornecedores.FirstOrDefault(f => f.Email == fornecedor.Email);
            ViewBag.FornecedorId = fornecedor.Id;
            return View(produto);
        }

        // POST: Produto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Valor,Quantidade,FornecedorId")] Produto produto)
        {
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Email = User.Identity.GetUserName();

            fornecedor = db.Fornecedores.FirstOrDefault(f => f.Email == fornecedor.Email);
            produto.FornecedorId = fornecedor.Id;

            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "Id", "Nome", produto.FornecedorId);
            return View(produto);
        }

        // GET: Produto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = db.Produtos.Find(id);
            db.Produtos.Remove(produto);
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
