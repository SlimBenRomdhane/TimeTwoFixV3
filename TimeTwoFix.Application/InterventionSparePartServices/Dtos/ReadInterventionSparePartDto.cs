namespace TimeTwoFix.Application.InterventionSparePartServices.Dtos
{
    public class ReadInterventionSparePartDto
    {
        public int Id { get; set; }
        public int InterventionId { get; set; }
        public int SparePartId { get; set; }
        public int Quantity { get; set; }

        //Bon de sortie
        public string DeliveryNote { get; set; }
        public string SparePartName { get; set; }
        public decimal UnitPrice { get; set; }

    }
}