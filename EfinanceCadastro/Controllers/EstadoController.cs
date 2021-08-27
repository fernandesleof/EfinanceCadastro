using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EfinanceCadastro.Models;
using System.Net;


namespace EfinanceCadastro.Controllers
{
    public class EstadoController : Controller
    {
        

        public ActionResult Listar()
        {
            using (EstadoModel estadoModel = new EstadoModel())
            {
                List<Estado> lista = model.Listar();
                return View(lista);
            }
        }

        // GET: Estado
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(FormCollection form)
        {
            int idestado = 0;
            Estado estado = new Estado();
            EstadoModel estadoModel = new EstadoModel();

            estado.codUfEstado = Convert.ToInt32(form["coduf"]);
            estado.nomeEstado = form["nome"];
            estado.ufEstado = form["uf"];

            EstadoModel model = new EstadoModel();
            idestado = model.CadastroEstado(estado);

            return RedirectToAction("Listar");
        }
        public ActionResult Sucesso()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            EstadoModel model = new EstadoModel();
            Estado estado = model.GetEstado(id);
            return View(estado);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            EstadoModel model = new EstadoModel();
            model.Excluir(Convert.ToInt32(id));
            return RedirectToAction("Listar");
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            EstadoModel model = new EstadoModel();
            Estado estado = model.GetEstado(id);

            EstadoModel modelEstado = new EstadoModel();

            return View(estado);

        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form, [Bind] Cliente cliente)
        {
            Estado estado = new Estado();
            EstadoModel estadoModel = new EstadoModel();
            estado.idEstado = Convert.ToInt32(form["id"]);
            estado.codUfEstado = Convert.ToInt32(form["coduf"]);
            estado.nomeEstado = form["nome"];
            estado.ufEstado = form["uf"];

            estadoModel.Alterar(estado);
            return RedirectToAction("Listar");

        }
    }
}