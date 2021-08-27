using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EfinanceCadastro.Models;

namespace EfinanceCadastro.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Listar()
        {
            using (ClienteModel model = new ClienteModel())
            {
                List<Cliente> lista = model.Listar();
                return View(lista);
            }
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(FormCollection form)
        {
            int idCliente = 0;
            Cliente cliente = new Cliente();
            ClienteModel ClienteModel = new ClienteModel();

            cliente.idCidade = Convert.ToInt32(form["codigcidade"]);          
            cliente.nomeCliente = form["nome"];
            cliente.telefoneCliente = form["telefone"];
            cliente.cpfCnpjCliente = form["cpfcnpj"];

            ClienteModel model = new ClienteModel();
            idCliente = model.CadastroCliente(cliente);

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
            ClienteModel model = new ClienteModel();
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
            ClienteModel model = new ClienteModel();
            Cliente cliente = model.GetCliente(id);

            ClienteModel modelCliente = new ClienteModel();

            return View(cliente);

        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form, [Bind] Cliente cliente)
        {
            ClienteModel ClienteModel = new ClienteModel();
            cliente.idCidade = Convert.ToInt32(form["codigcidade"]);
            cliente.nomeCliente = form["nome"];
            cliente.telefoneCliente = form["telefone"];
            cliente.cpfCnpjCliente = form["cpfcnpj"];

            ClienteModel.Alterar(cliente);
            return RedirectToAction("Listar");

        }
    }
}
