namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Marker interface to identify the root of an Aggregate.
    /// An Aggregate Root is the only Entity in the Aggregate that can be retrieved directly from a Repository.
    /// It acts as a guardian and ensures transactional consistency within its Aggregate.
    /// </summary>
    public interface IAggregateRoot
    {
        // No members defined. Serves only as a Marker.
    }
}
