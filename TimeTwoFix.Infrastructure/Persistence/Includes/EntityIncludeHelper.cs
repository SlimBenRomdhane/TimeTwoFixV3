using System.Linq.Expressions;
using TimeTwoFix.Core.Entities.AppointmentManagement;
using TimeTwoFix.Core.Entities.BridgeManagement;
using TimeTwoFix.Core.Entities.ClientManagement;
using TimeTwoFix.Core.Entities.ServiceManagement;
using TimeTwoFix.Core.Entities.SparePartManagement;
using TimeTwoFix.Core.Entities.UserManagement;
using TimeTwoFix.Core.Entities.VehicleManagement;
using TimeTwoFix.Core.Entities.WorkOrderManagement;

namespace TimeTwoFix.Infrastructure.Persistence.Includes
{
    public static class EntityIncludeHelper
    {
        public static Expression<Func<TEntity, object>>[] GetIncludes<TEntity>() where TEntity : class
        {
            if (typeof(TEntity) == typeof(Client))
            {
                return new Expression<Func<TEntity, object>>[]
                {
                    c => ((Client)(object)c).Vehicles,
                };
            }
            else if (typeof(TEntity) == typeof(Vehicle))
            {
                return new Expression<Func<TEntity, object>>[]
                {
                    v => ((Vehicle)(object)v).Client,
                    v => ((Vehicle)(object)v).WorkOrders,
                    v => ((Vehicle)(object)v).Appointments
                };
            }
            else if (typeof(TEntity) == typeof(WorkOrder))
            {
                return new Expression<Func<TEntity, object>>[]
                {
                    w => ((WorkOrder)(object)w).Vehicle,
                    w => ((WorkOrder)(object)w).Interventions
                };
            }
            else if (typeof(TEntity) == typeof(Intervention))
            {
                return new Expression<Func<TEntity, object>>[]
                {
                    i => ((Intervention)(object)i).WorkOrder,
                    i => ((Intervention)(object)i).Service,
                    i => ((Intervention)(object)i).Mechanic,
                    i => ((Intervention)(object)i).LiftingBridge,
                    i => ((Intervention)(object)i).PauseRecords,
                    i => ((Intervention)(object)i).InterventionSpareParts
                };
            }
            else if (typeof(TEntity) == typeof(ProvidedService))
            {
                return new Expression<Func<TEntity, object>>[]
                {
                    s => ((ProvidedService)(object)s).Category,
                    s => ((ProvidedService)(object)s).Interventions
                };
            }
            else if (typeof(TEntity) == typeof(Mechanic))
            {
                return new Expression<Func<TEntity, object>>[]
                {
                    m => ((Mechanic)(object)m).Interventions
                };
            }
            else if (typeof(TEntity) == typeof(LiftingBridge))
            {
                return new Expression<Func<TEntity, object>>[]
                {
                    lb => ((LiftingBridge)(object)lb).Interventions
                };
            }
            else if (typeof(TEntity) == typeof(PauseRecord))
            {
                return new Expression<Func<TEntity, object>>[]
                {
                    pr => ((PauseRecord)(object)pr).Intervention
                };
            }
            else if (typeof(TEntity) == typeof(SparePart))
            {
                return new Expression<Func<TEntity, object>>[]
                {
                    sp => ((SparePart)(object)sp).InterventionSpareParts
                };
            }
            else if (typeof(TEntity) == typeof(InterventionSparePart))
            {
                return new Expression<Func<TEntity, object>>[]
                {
                    isp => ((InterventionSparePart)(object)isp).Intervention,
                    isp => ((InterventionSparePart)(object)isp).SparePart
                };
            }
            else if (typeof(TEntity) == typeof(Appointment))
            {
                return new Expression<Func<TEntity, object>>[]
                {
                    a => ((Appointment)(object)a).Vehicle
                };
            }
            else
            {
                return Array.Empty<Expression<Func<TEntity, object>>>();
            }
        }
    }
}