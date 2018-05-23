

using System;
using TestMediaTR.Domain.Commands;
using TestMediaTR.Domain.Events;

namespace TestMediaTR.Support.Converter
{
    public class CommandEventConverter : ICommandEventConverter
    {
        //Esta clase puede existir si la conversión es muy compleja y requiere logica de negocio adicional
        public ConceptoCreatedEvent CommandToEvent(CreateUpdateConceptosCommand command)
        {
            var newEvent = new ConceptoCreatedEvent()
            {
                CreatedAt = DateTime.Now,
                Id = command.Concepto.Id,
                Codigo = command.Concepto.Codigo,
                Nombre = command.Concepto.Nombre,
                Parametro = command.Concepto.Parametro,
                Tipo = command.Concepto.Tipo
            };

            return newEvent;
        }
    }
}