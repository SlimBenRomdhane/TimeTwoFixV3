using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using TimeTwoFix.Web.Models.InterventionModels;
using TimeTwoFix.Web.Models.WorkOrderModels;

public static class WorkOrderPdfGenerator
{
    public static byte[] Generate(ReadWorkOrderViewModel model)
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);

                // Header
                page.Header().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("TimeTwoFix Workshop").FontSize(18).Bold();
                        col.Item().Text("Tunis, Tunisia");
                        col.Item().Text("Phone: +216 XX XXX XXX");
                        col.Item().Text("Email: contact@timetwofix.tn");
                    });

                    row.ConstantItem(150).Column(col =>
                    {
                        col.Item().Text($"Invoice #WO-{model.Id}").FontSize(14).Bold();
                        col.Item().Text($"Date: {DateTime.Now:yyyy-MM-dd}");
                    });
                });

                // Content
                page.Content().Column(col =>
                {
                    // Customer & Vehicle Info
                    col.Item().PaddingVertical(10).Row(row =>
                    {
                        row.RelativeItem().Column(c =>
                        {
                            c.Item().Text("Customer Information").Bold();
                            c.Item().Text($"{model.CustomerName} {model.CustomerLastName}");
                            c.Item().Text($"Phone: {model.CustomerPhone}");
                            c.Item().Text($"Email: {model.CustomerEmail}");
                        });

                        row.RelativeItem().Column(c =>
                        {
                            c.Item().Text("Vehicle Information").Bold();
                            c.Item().Text($"Brand: {model.VehicleViewModel?.Brand}");
                            c.Item().Text($"Model: {model.VehicleViewModel?.Model}");
                            c.Item().Text($"License Plate: {model.VehicleViewModel?.LicensePlate}");
                            c.Item().Text($"VIN: {model.VehicleViewModel?.Vin}");
                        });
                    });

                    // Work Order Info
                    col.Item().PaddingBottom(10).Column(c =>
                    {
                        c.Item().Text("Work Order Details").Bold();
                        c.Item().Text($"Mileage: {model.Mileage} km");
                        //c.Item().Text($"Labor Cost: {model.TolalLaborCost:N3} TND");
                        //c.Item().Text($"Notes: {model.Notes ?? "—"}");
                    });

                    // Interventions Table
                    col.Item().Text("Interventions").FontSize(14).Bold().Underline();
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(c =>
                        {
                            c.ConstantColumn(30); // #
                            c.RelativeColumn();   // Service
                            c.ConstantColumn(80); // Cost
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("#").Bold();
                            header.Cell().Element(CellStyle).Text("Service").Bold();
                            header.Cell().Element(CellStyle).Text("Cost").Bold();
                        });

                        int index = 1;
                        foreach (var iv in model.InterventionViewModels)
                        {
                            table.Cell().Element(CellStyle).Text(index++.ToString());
                            table.Cell().Element(CellStyle).Text(iv.ProvidedService?.Name ?? "[no service]");
                            table.Cell().Element(CellStyle).Text($"{iv.InterventionPrice:N3} TND");
                        }
                    });

                    // Spare Parts Table (Flat List)
                    var allSpareParts = model.InterventionViewModels
                        .SelectMany(iv => iv.SparePartsUsed ?? Enumerable.Empty<InterventionSparePartDisplayViewModel>())
                        .ToList();

                    if (allSpareParts.Any())
                    {
                        col.Item().PaddingTop(20).Text("Spare Parts Used").FontSize(14).Bold().Underline();

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(); // Part
                                c.ConstantColumn(50); // Qty
                                c.ConstantColumn(80); // Unit
                                c.ConstantColumn(80); // Total
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Part").Bold();
                                header.Cell().Element(CellStyle).Text("Qty").Bold();
                                header.Cell().Element(CellStyle).Text("Unit").Bold();
                                header.Cell().Element(CellStyle).Text("Total").Bold();
                            });

                            foreach (var part in allSpareParts)
                            {
                                table.Cell().Element(CellStyle).Text(part.SparePartName);
                                table.Cell().Element(CellStyle).Text(part.Quantity.ToString());
                                table.Cell().Element(CellStyle).Text($"{part.UnitPrice:N3}");
                                table.Cell().Element(CellStyle).Text($"{(part.UnitPrice * part.Quantity):N3}");
                            }
                        });
                    }

                    // Totals
                    var totalServices = model.InterventionViewModels.Sum(iv => iv.InterventionPrice);
                    var totalSpareParts = allSpareParts.Sum(p => p.UnitPrice * p.Quantity);
                    var grandTotal = totalServices + totalSpareParts;

                    col.Item().PaddingTop(20).AlignRight().Column(c =>
                    {
                        c.Item().Text($"Intervention Subtotal: {totalServices:N3} TND").FontSize(12);
                        c.Item().Text($"Spare Parts Subtotal: {totalSpareParts:N3} TND").FontSize(12);
                        c.Item().Text($"Grand Total: {grandTotal:N3} TND").FontSize(14).Bold();
                    });
                });

                // Footer
                page.Footer().AlignCenter().Text("Thank you for choosing TimeTwoFix.").Italic();
            });
        }).GeneratePdf();
    }

    // Table Cell Styling with Borders
    private static IContainer CellStyle(IContainer container)
    {
        return container
            .Border(1)
            .Padding(4)
            .AlignMiddle()
            .ShowOnce();
    }
}