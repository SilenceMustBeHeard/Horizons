using Horizons.Services.Core.Interfaces;
using Horizons.Web.ViewModels.Destination;

namespace Horizons.Services.Core.Admin.Interfaces;

public interface IDestinationServiceManagement
{
    // Create
    Task<bool> AddDestinationAsync(DestinationAddInputModel model, string userId);

    // Read (Admin can see all including deleted)
    Task<IEnumerable<DestinationIndexViewModel>> GetAllDestinationsForAdminAsync();
    Task<DestinationDetailsViewModel?> GetDestinationDetailsForAdminAsync(Guid id);

    // Update
    Task<DestinationEditInputModel?> GetDestinationForEditAsync(string? userId, Guid id);
    Task<bool> EditDestinationAsync(DestinationEditInputModel model, string userId);

    // Delete (Soft delete)
    Task<DestinationDeleteInputModel?> GetDestinationForDeleteAsync(string? userId, Guid id);
    Task<bool> DeleteDestinationAsync(Guid id, string userId);

    // Permanent delete (Hard delete - Admin only)
    Task<bool> PermanentDeleteDestinationAsync(Guid id);
    Task<bool> RestoreDestinationAsync(Guid id);
}