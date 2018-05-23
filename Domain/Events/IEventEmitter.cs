namespace TestMediaTR.Domain.Events
{
    public interface IEventEmitter
    {
        void EmitConceptoCreatedEvent(ConceptoCreatedEvent message);
    }
}