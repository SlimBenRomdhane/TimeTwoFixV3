public class WorkOrderExportViewModel
{
    public int WorkOrderId { get; set; }

    // Customer Info
    public string CustomerName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerEmail { get; set; }

    // Vehicle Info
    public string LicensePlate { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Vin { get; set; }

    // Work Order Info
    public int Mileage { get; set; }
    public decimal LaborCost { get; set; }
    public string Notes { get; set; }

    // Interventions
    public List<ExportInterventionViewModel> Interventions { get; set; }

    public decimal TotalServices => Interventions.Sum(i => i.ServiceCost);
    public decimal TotalSpareParts => Interventions.SelectMany(i => i.SpareParts).Sum(sp => sp.Total);
    public decimal GrandTotal => TotalServices + TotalSpareParts;
}

public class ExportInterventionViewModel
{
    public string ServiceName { get; set; }
    public decimal ServiceCost { get; set; }
    public List<ExportSparePartViewModel> SpareParts { get; set; }
}

public class ExportSparePartViewModel
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total => UnitPrice * Quantity;
}