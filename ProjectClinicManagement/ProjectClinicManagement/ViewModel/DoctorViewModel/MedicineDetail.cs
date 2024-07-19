namespace ProjectClinicManagement.ViewModel.DoctorViewModel
{
    public class MedicineDetail
    {
        public int MedicineId { get; set; }
        public string GenericName { get; set; }
        public string ATCCode { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
    }
}
