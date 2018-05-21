
using MediatR;
using TestMediaTR.DTOs;

namespace TestMediaTR.Domain.Commands
{
    public class CreateUpdateConceptosCommand : BaseCommand , IRequest<CommandResult> 
    {
        public ConceptoDTO Concepto { get; set; }

    }
}