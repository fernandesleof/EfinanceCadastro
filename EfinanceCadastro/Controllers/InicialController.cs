using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EfinanceCadastro.Controllers
{
    public class InicialController : Controller
    {
        // GET: Inicial
        public ActionResult Index()
        {
            return View();
        }
    }
}