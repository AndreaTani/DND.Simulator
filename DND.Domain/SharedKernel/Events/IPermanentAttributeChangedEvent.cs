namespace DND.Domain.SharedKernel
{
    public interface IPermanentAttributeChangedEvent : IDomainEvent
    {
        public Guid CreatureId { get; }
    }
}
