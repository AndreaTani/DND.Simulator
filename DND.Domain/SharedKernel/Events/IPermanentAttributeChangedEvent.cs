namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Marker interface to identify all domain events involved in permanently 
    /// change creature attributes
    /// </summary>
    public interface IPermanentAttributeChangedEvent : IDomainEvent
    {
        public Guid CreatureId { get; }
    }
}
