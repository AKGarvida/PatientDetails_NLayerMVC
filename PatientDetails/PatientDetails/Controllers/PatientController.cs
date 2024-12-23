using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PatientDetails_Entities;
using System.Web.Mvc;

namespace PatientDetails.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(PatientDetailEntities pdEntities)
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
    }
}