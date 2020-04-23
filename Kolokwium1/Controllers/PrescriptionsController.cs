using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Kolokwium1.DTOs;
using Kolokwium1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium1.Controllers
{
    [Route("api/prescriptions")]
    [ApiController]


    public class PrescriptionsController : ControllerBase
    {
        IPrescriptionService service;

        public PrescriptionsController(IPrescriptionService services)
        {
            this.service = services;
        }
        [HttpPost]
        public IActionResult Receipt(ReceipeRequest request)
        {
            DateTime date = DateTime.ParseExact(request.Date, "d", CultureInfo.InvariantCulture);
            DateTime dueDate = DateTime.ParseExact(request.DueDate, "d", CultureInfo.InvariantCulture);

            if (date < dueDate)
            {
                return BadRequest("DueDate nie jest starsza niż Date! ");
            }

            var result = service.InputNewReceipt(request);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Prescription(int id)
        {
            var result = service.GetPrescription(id);
            return Ok(result);
        }
    }
}