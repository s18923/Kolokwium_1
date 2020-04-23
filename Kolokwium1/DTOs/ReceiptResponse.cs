using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1.DTOs
{
    public class ReceiptResponse
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }

        public int Dose { get; set; }
  
        public string Details { get; set; }
        public string MedName { get; set; }
        public string MedDescription { get; set; }
        public string MedType { get; set; }

        public List<string> Meds { get; set; }

    }
}
