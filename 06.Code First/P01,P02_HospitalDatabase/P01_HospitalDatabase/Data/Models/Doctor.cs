using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    //added-->
    public class Doctor
    {
        public Doctor()
        {
            DoctorsVisitaions = new List<Visitation>();
        }
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }

        public ICollection<Visitation> DoctorsVisitaions { get; set; }
    }
    //<--
}
