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
            using (CidadeModel model = new CidadeModel())
            {
                List<Cidade> lista = model.Listar();
                return View(lista);
            }
        }

        [HttpPost]

        public ActionResult Create(FormCollection form)
        {
            Cliente cliente = new Cliente();
            ClienteModel ClienteModel = new ClienteModel();
                    
            cliente.nomeCliente = form["nome"];
            cliente.telefoneCliente = String.Join("", System.Text.RegularExpressions.Regex.Split(form["telefone"], @"[^\d]"));
            cliente.cpfCnpjCliente = String.Join("", System.Text.RegularExpressions.Regex.Split(form["cpfcnpj"], @"[^\d]"));
            cliente.idCidade = Convert.ToInt32(form["idcidade"]);

            ClienteModel model = new ClienteModel();
            model.CadastroCliente(cliente);

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

            using (CidadeModel modelCidade = new CidadeModel())
            {
                List<Cidade> listacidade = modelCidade.Listar();
                ViewBag.DadosCidade = listacidade;
            }

            ViewBag.DadosCliente = cliente;
            return View();

        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form, [Bind] Cliente cliente)
        {
            ClienteModel ClienteModel = new ClienteModel();
            cliente.idCliente = Convert.ToInt32(form["id"]);
            cliente.nomeCliente = form["nome"];
            cliente.telefoneCliente = String.Join("", System.Text.RegularExpressions.Regex.Split(form["telefone"], @"[^\d]"));
            cliente.cpfCnpjCliente = String.Join("", System.Text.RegularExpressions.Regex.Split(form["cpfcnpj"], @"[^\d]")); 
            cliente.idCidade = Convert.ToInt32(form["idcidade"]);

            ClienteModel.Alterar(cliente);
            return RedirectToAction("Listar");

        }
    }
}
