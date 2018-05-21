using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestMediaTR.Domain.Commands;
using TestMediaTR.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using TestMediaTR.DTOs;

namespace TestMediaTR.Domain.Handlers
{
    public class CreateUpdateConceptoCommandHandler :
        IRequestHandler<CreateUpdateConceptosCommand, CommandResult>
    {
        private readonly ConceptosContext _dbContext;

        public CreateUpdateConceptoCommandHandler(ConceptosContext dbContext)
        {
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<CommandResult> Handle(CreateUpdateConceptosCommand request, 
                                                CancellationToken cancellationToken)
        {
            //Business Logic Here

            try
            {
                int Id = await CreateConcepto(request.Concepto, cancellationToken);

                return CommandResult.Success(new
                {
                    Id = Id
                });

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