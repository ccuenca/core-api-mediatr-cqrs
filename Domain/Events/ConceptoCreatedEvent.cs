using Newtonsoft.Json;

namespace TestMediaTR.Domain.Events
{
    public class ConceptoCreatedEvent : BaseEvent
    {
        public int Id { get; set; }

        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public string Tipo { get; set; }

        public string Parametro { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
