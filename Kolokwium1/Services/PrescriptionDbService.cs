using Kolokwium1.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1.Services
{
    public class PrescriptionDbService : IPrescriptionService
    {
        public Prescription InputNewReceipt(ReceipeRequest request)
        {

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18923;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
         

                com.Connection = con;
                con.Open();
            

                DateTime data1 = DateTime.ParseExact(request.Date, "d", CultureInfo.InvariantCulture);
                DateTime data2 = DateTime.ParseExact(request.DueDate, "d", CultureInfo.InvariantCulture);

                com.CommandText = "Insert into Prescription (Date, DueDate, IdPatient, IdDoctor)" +
                    "values ( @Date, @DueDate, @IdPatient, @IdDoctor); Select Scope_Identity()";
               

                com.Parameters.AddWithValue("Date", data1);
                com.Parameters.AddWithValue("DueDate",data2);
                com.Parameters.AddWithValue("IdPatient", request.IdPatient);
                com.Parameters.AddWithValue("IdDoctor", request.IdDoctor);

                int id = Convert.ToInt32(com.ExecuteScalar());
               

                var response = new Prescription
                {
                    IdPrescription = id,
                    Date = data1,
                    DueDate = data2,
                    IdPatient = request.IdPatient,
                    IdDoctor = request.IdDoctor
                };
                return response;
            }
        }

        //------------------

        public List<ReceiptResponse> GetPrescription(int id)
        {
            using (var client = new SqlConnection("Data Source = db-mssql; Initial Catalog=s18923; Integrated Security=True"))
            using (var com = new SqlCommand())


            {
                client.Open();
                com.Connection = client;
                

                List<ReceiptResponse> responses = new List<ReceiptResponse>();
                List<string> meds = new List<string>();


                com.Parameters.AddWithValue("IdPrescription", id);
                com.CommandText = "Select IdPatient, IdDoctor, Date, DueDate, Dose, Details, Description, Name, Type from Prescription " +
                    "Inner Join Prescription_Medicament on Prescription_Medicament.IdPrescription = Prescription.IdPrescription " +
                    "Inner Join Medicament on Medicament.IdMedicament = Prescription_Medicament.IdMedicament " +
                    "Where Prescription.IdPrescription = @id";

                com.Parameters.AddWithValue("id", id);
                var reader = com.ExecuteReader();
                if (!reader.Read())
                {
                    reader.Close();
                    
                }
                else
                {
                    while (reader.Read())
                    {
                        var response = new ReceiptResponse();

                        response.Details = reader["Details"].ToString();
                        {
                            response.MedName = reader["Name"].ToString();
                            response.MedDescription = reader["Description"].ToString();
                            response.MedType = reader["Type"].ToString();
                            response.Meds = meds;
                            meds.Add(response.MedName);
                            meds.Add(response.MedDescription);
                            meds.Add(response.MedType);
                        }
                        response.IdPrescription = id;
                        response.Date = DateTime.Parse(reader["Date"].ToString());
                        response.DueDate = DateTime.Parse(reader["DueDate"].ToString());
                        response.IdPatient = (int)reader["IdPatient"];
                        response.IdDoctor = (int)reader["IdDoctor"];
                        response.Dose = (int)reader["Dose"];
                        
                        responses.Add(response);
                    }
                }
                reader.Close();
              
                return responses;
            }
        }

    }
}
