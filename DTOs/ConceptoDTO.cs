using System;
namespace TestMediaTR.DTOs
{
    public class ConceptoDTO
    {
        public ConceptoDTO()
        {
        }
        
        public int Id { get; set; }

        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public string Tipo { get; set; }

        public string Parametro { get; set; }
    }
}
