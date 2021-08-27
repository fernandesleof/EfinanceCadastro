using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EfinanceCadastro.Models;

namespace EfinanceCadastro.Controllers
{
    public class CidadeController : Controller
    {
        // GET: Cidade
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Listar()
        {
            using (CidadeModel model = new CidadeModel())
            {
                List<Cidade> lista = model.Listar();
                return View(lista);
            }
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            using (EstadoModel model = new EstadoModel())
            {
                List<Estado> lista = model.Listar();
                return View(lista);
            }
            //return View();
        }

        [HttpPost]

        public ActionResult Create(FormCollection form)
        {
            Cidade cidade = new Cidade();
            CidadeModel CidadeModel = new CidadeModel();

            cidade.idEstado = Convert.ToInt32(form["idestado"]);
            cidade.nomeCidade = form["nome"];
            cidade.codigoIbgeCidade = Convert.ToInt32(form["codigoibge"]);

            CidadeModel model = new CidadeModel();
            model.CadastroCidade(cidade);

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
            CidadeModel model = new CidadeModel();
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
            CidadeModel model = new CidadeModel();
            Cidade cidade = model.GetCidade(id);

            return View(cidade);

        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form, [Bind] Cidade cidade)
        {
            CidadeModel cidadeModel = new CidadeModel();
            cidade.idCidade = Convert.ToInt32(form["id"]);
            cidade.idEstado = Convert.ToInt32(form["idestado"]);
            cidade.nomeCidade = form["nome"];
            cidade.codigoIbgeCidade = Convert.ToInt32(form["codigoibge"]);

            cidadeModel.Alterar(cidade);
            return RedirectToAction("Listar");

        }
    }
}