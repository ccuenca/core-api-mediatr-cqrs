using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using TestMediaTR.Domain.Commands;
using TestMediaTR.Domain.Events;
using TestMediaTR.DTOs;
using TestMediaTR.Persistence;
using TestMediaTR.Support.Converter;

namespace TestMediaTR.Domain.Handlers
{
    public class CreateUpdateConceptoCommandHandler : IRequestHandler<CreateUpdateConceptosCommand, CommandResult>
    {
        private readonly ConceptosContext _dbContext;
        private readonly IEventEmitter _eventEmitter;
        private readonly ICommandEventConverter _converter;

        public CreateUpdateConceptoCommandHandler(ConceptosContext dbContext, IEventEmitter eventEmitter, ICommandEventConverter converter)
        {
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this._eventEmitter = eventEmitter ?? throw new ArgumentNullException(nameof(eventEmitter));
            this._converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public async Task<CommandResult> Handle(CreateUpdateConceptosCommand request,
                                                CancellationToken cancellationToken)
        {
            //Business Logic Here
            try
            {
                int Id = await CreateConcepto(request.Concepto, cancellationToken);

                Log.Information("Concepto creado {@concepto}", request.Concepto);

                _eventEmitter.EmitConceptoCreatedEvent(_converter.CommandToEvent(request));

                request.Concepto.Id = Id;

                return CommandResult.Success(request.Concepto);
            }
            catch (Exception ex)
            {
                return CommandResult.Fail(ex.Message);
            }
        }

        #region Data Acces Methods

        private async Task<int> CreateConcepto(ConceptoDTO concepto, CancellationToken cancellationToken)
        {
            var result = Parameters.CreateOutputParameter("@result", SqlDbType.Int);

            var parameters = new SqlParameter[]
            {
                Parameters.CreateParameter("@conCodigo", concepto.Codigo, ParameterDirection.Input, SqlDbType.VarChar),
                Parameters.CreateParameter("@conNombre", concepto.Nombre, ParameterDirection.Input, SqlDbType.VarChar),
                Parameters.CreateParameter("@conParametro", concepto.Parametro, ParameterDirection.Input, SqlDbType.VarChar),
                Parameters.CreateParameter("@conTipo", concepto.Tipo, ParameterDirection.Input, SqlDbType.VarChar),
                Parameters.CreateParameter("@conInactivo", false, ParameterDirection.Input, SqlDbType.Bit),
                result
            };

            await this._dbContext.Database.ExecuteSqlCommandAsync(
                    $@"EXECUTE {procs.CREATE_CONCEPTO}
                    @conCodigo,@conNombre,@conParametro,@conTipo,@conInactivo,@result OUTPUT",
                    parameters,
                    cancellationToken);

            return int.Parse(result.Value.ToString());
        }

        #endregion

    }
}