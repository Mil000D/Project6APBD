namespace Zadanie7.DAL;

public partial class ClientTrip
{
    public int IdClient { get; set; }

    public int IdTrip { get; set; }

    public DateTime RegisteredAt { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual Client Clients { get; set; } = null!;

    public virtual Trip Trips { get; set; } = null!;
}
