namespace DND.Domain.SharedKernel
{
    public record PermanentAttributeChangedEvent (
        Guid CreatureId
        ): IPermanentAttributeChangedEvent;
}
