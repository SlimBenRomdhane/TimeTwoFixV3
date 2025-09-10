namespace TimeTwoFix.Web.Models.SparePartModels
{
    public class CategoryDropdownItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UsageCount { get; set; }

        // Display format for dropdown/list
        public string DisplayText => UsageCount > 0
            ? $"{Name} ({UsageCount} parts)"
            : Name;

        public string PopularityBadge => UsageCount switch
        {
            > 50 => "🔥 Popular",
            > 20 => "⭐ Common",
            > 0 => "✓ Used",
            _ => "🆕 New"
        };
    }
}
