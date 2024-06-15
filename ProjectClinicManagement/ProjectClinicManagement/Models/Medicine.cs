using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClinicManagement.Models
{
    public class Medicine
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string ATCCode { get; set; }
        [Required] public string GenericName { get; set; }
        public string Description { get; set; }
        [Required] public string Manufacturer { get; set; }
        [Required] public string Type { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        [Required] public float Price { get; set; }
        [Required] public int Quantity { get; set; }
        public StatusType Status { get; set; }

        public virtual ICollection<Prescription>? Prescriptions { get; set; }
        public enum StatusType
        {
            Active,
            Inactive
        }
    }
}
