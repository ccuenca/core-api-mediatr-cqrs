

using TestMediaTR.Domain.Commands;
using TestMediaTR.Domain.Events;

namespace TestMediaTR.Support.Converter
{
    public interface ICommandEventConverter
    {
        ConceptoCreatedEvent CommandToEvent(CreateUpdateConceptosCommand command);
    }
}