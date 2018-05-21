using MediatR;

namespace TestMediaTR.Domain.Commands
{
    public class QueryConceptosCommand : BaseCommand , IRequest<CommandResult> 
    {
        public int Id{ get; set; }

        public string Type { get; set; }

    }
}