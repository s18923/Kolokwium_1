using Kolokwium1.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1.Services
{
    public interface IPrescriptionService
    {
        public Prescription InputNewReceipt(ReceipeRequest request);

        public List<ReceiptResponse> GetPrescription(int id);
    }
}
