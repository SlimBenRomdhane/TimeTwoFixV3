namespace TimeTwoFix.Web.Models.InterventionSparePartModel
{
    public class CreateInterventionSparePartViewModel
    {
        public int InterventionId { get; set; }
        public int SparePartId { get; set; }
        public int Quantity { get; set; }
        //Bon de sortie       
        public string DeliveryNote { get; set; }
    }
}
