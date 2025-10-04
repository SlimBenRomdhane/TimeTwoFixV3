namespace TimeTwoFix.Core.Common.Constants
{
    /// <summary>
    /// Centralized role name constants to avoid magic strings throughout the application
    /// </summary>
    public static class RoleNames
    {
        public const string GeneralManager = "GeneralManager";
        public const string FrontDeskAssistant = "FrontDeskAssistant";
        public const string Mechanic = "Mechanic";
        public const string WareHouseManager = "WareHouseManager";
        public const string WorkshopManager = "WorkshopManager";

        /// <summary>
        /// Combined roles for common authorization scenarios
        /// </summary>
        public static class Combined
        {
            public const string FrontDeskAndGeneralManager = $"{FrontDeskAssistant},{GeneralManager}";
            public const string AllManagers = $"{GeneralManager},{WorkshopManager},{WareHouseManager}";
            public const string MechanicAndWorkshopManager = $"{Mechanic},{WorkshopManager}";
        }
    }
}
