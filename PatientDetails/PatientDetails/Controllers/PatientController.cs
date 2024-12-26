using System;
using PatientDetails_Entities;
using System.Web.Mvc;
using PatientDetails_BLL;

namespace PatientDetails.Controllers
{
    public class PatientController : Controller
    {
        private readonly CreateBLL _createBll;

        public PatientController()
        {
            _createBll = new CreateBLL();
        }

        // GET: Patient
        public ActionResult Index()
        {
            var bll = new CreateBLL();
            var patients = bll.GetPatients();

            if (patients == null)
            {
                TempData["Error"] = "Failed to retrieve patient records.";
            }
            return View(patients);
        }

        // GET: Create Patient
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
                var result = _createBll.CreatePatient(pdEntities);

                if (result != null)
                {
                    TempData["Success"] = "Patient record created successfully.";
                    return RedirectToAction("Index");
                }

                TempData["Error"] = "Failed to create patient record. Please try again.";
            }
            catch (Exception ex)
            {
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
