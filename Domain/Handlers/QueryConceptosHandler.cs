using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestMediaTR.Domain.Commands;
using TestMediaTR.DTOs;
using TestMediaTR.Persistence;
using TestMediaTR.Persistence.Models;

namespace TestMediaTR.Domain.Handlers
{
    public class QueryConceptosHandler
        : IRequestHandler<QueryConceptosCommand, CommandResult>
    {
        private readonly ConceptosContext _dbContext;
        private readonly IMapper _mapper;

        public QueryConceptosHandler(ConceptosContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }    

        public async Task<CommandResult> Handle(QueryConceptosCommand request, 
                                            CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id == 0){
                    return CommandResult.Success(
                        data: _mapper.Map<List<PrcConceptos_Model>, List<ConceptoDTO>>(await LoadByType(request.Type))
                    );
                }
                else
                {
                    return CommandResult.Success(
                        data: _mapper.Map<PrcConceptos_Model, ConceptoDTO>(await LoadById(request.Id))
                    );
                }
            }
            catch (Exception ex)
            {
                return CommandResult.Fail(ex.Message);
            }
        }

        #region "Data Access Methods"
        private async Task<List<PrcConceptos_Model>> LoadByType(string type)
        {
            var parameters = new SqlParameter[] 
            { 
                Parameters.CreateParameter("@TIPO", type, ParameterDirection.Input, SqlDbType.VarChar)
            };

            return await _dbContext.Conceptos.FromSql($"EXECUTE prcCargarConceptos @TIPO", parameters).ToListAsync();
        }

        private async Task<PrcConceptos_Model> LoadById(int id)
        {
            var parameters = new SqlParameter[] 
            { 
                Parameters.CreateParameter("@ID", id, ParameterDirection.Input, SqlDbType.Int)
            };

            return await _dbContext.Conceptos.FromSql($"EXECUTE prcCargarConceptos @Id={id}", parameters).FirstAsync();
        }

        #endregion
    }
}