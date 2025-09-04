using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.WorkOrderModels
{
    public enum WorkOrderStatus
    {
        //Normal workflow states for a work order, automated
        Pending, // Initial state, waiting for approval or scheduling
        InProgress, // Work is currently being done on the order
        Completed, // Work has been finished and is awaiting final review or payment



        Paused, // Temporarily stopped, but not cancelled, can be resumed later
        Canceled,    // Generally by teh customer or admin 
    }
}
