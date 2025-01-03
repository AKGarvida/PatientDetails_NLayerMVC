using System;
using PatientDetails_Entities;
using System.Web.Mvc;
using PatientDetails_BLL;
using System.Collections.Generic;
using System.Linq;

namespace PatientDetails.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientDetailBLL _pdBLL;

        public PatientController()
        {
            _pdBLL = new PatientDetailBLL();
        }

        // GET: Patient
        public ActionResult Index()
        {
            var patients = _pdBLL.GetPatients();

            if (patients == null)
            {
                TempData["Error"] = "Failed to retrieve patient records.";
            }
            return View(patients);
        }

        [HttpGet]
        public JsonResult FilterPatients(string date, string dosage, string drug, string patient)
        {
            try
            {
                DateTime? modifiedDate = string.IsNullOrWhiteSpace(date) ? (DateTime?)null : DateTime.Parse(date);
                decimal? dosageValue = string.IsNullOrWhiteSpace(dosage) ? (decimal?)null : decimal.Parse(dosage);
                var patients = _pdBLL.GetPatients(patient, drug, dosageValue, modifiedDate);
                return Json(patients.Select(p => new
                {
                    p.ID,
                    p.Dosage,
                    p.Drug,
                    p.Patient,
                    ModifiedDate = p.ModifiedDate.ToString("yyyy-MM-dd")
                }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return Json(new List<object>(), JsonRequestBehavior.AllowGet);
            }
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
                var result = _pdBLL.CreatePatient(pdEntities);

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

        // GET: Edit
        public ActionResult Edit(int id)
        {
            var patient = _pdBLL.GetPatients().Find(p => p.ID == id);
            if (patient == null)
            {
                TempData["Error"] = "Patient not found.";
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        [HttpPost]
        public ActionResult Edit(PatientDetailEntities pdEntities)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid data. Please check the form inputs.";
                return View(pdEntities);
            }

            try
            {
                var result = _pdBLL.UpdatePatient(pdEntities);

                if (result != null)
                {
                    TempData["Success"] = "Patient record updated successfully.";
                    return RedirectToAction("Index");
                }

                TempData["Error"] = "Failed to update patient record. Please try again.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.Message}";
            }

            return View(pdEntities);
        }


        [HttpPost]
        public JsonResult DeletePatient(int id)
        {
            try
            {
                bool isDeleted = _pdBLL.DeletePatient(id);
                if (isDeleted)
                {
                    return Json(new { success = true, message = "Record deleted successfully." });
                }
                return Json(new { success = false, message = "Failed to delete record. Please try again." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }

    }
}
