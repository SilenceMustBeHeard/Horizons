namespace Horizons.Web.ViewModels.Destination
{
    public class DestinationDeleteInputModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public string Publisher { get; set; } = null!;
    }
}
