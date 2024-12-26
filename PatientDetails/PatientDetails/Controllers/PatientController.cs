using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PatientDetails_Entities;
using System.Web.Mvc;
using PatientDetails_BLL;

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
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid data. Please check the form inputs.";
                return View(pdEntities);
            }

            try
            {
                // Delegate business logic to the BLL
                var createService = new CreateBLL();
                var result = createService.CreatePatient(pdEntities);

                if (result != null)
                {
                    TempData["Success"] = "Patient record created successfully.";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Failed to create patient record. Please try again.";
            }
            catch (Exception ex)
            {
                // Log exception

                TempData["Error"] = $"An error occurred: {ex.Message}";
            }

            return View(pdEntities);
        }

        public ActionResult Edit()
        {
            return View();
        }
    }
}