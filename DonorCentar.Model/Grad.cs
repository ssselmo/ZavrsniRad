using System;
using System.Collections.Generic;
using System.Text;

namespace DonorCentar.Model
{
    public class Grad
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public Kanton Kanton { get; set; }
        public int KantonId { get; set; }

        public override string ToString()
        {
            return Naziv.ToString();
        }
    }
}
