﻿namespace Stock_Back.BLL.Models.MaterialModelDTO
{
    public class MaterialInsertDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? MetricUnit { get; set; }
        public string? Vendor { get; set; }
        public string? VendorUnitType { get; set; }
        public double CostPerUnit { get; set; }
        public string? ProviderName { get; set; }
        public int UnitsPerSinglePurchase { get; set; }
        public string? PricingPerSinglePurchase { get; set; }
    }
}