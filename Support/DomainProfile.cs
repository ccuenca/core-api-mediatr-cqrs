using AutoMapper;
using TestMediaTR.DTOs;
using TestMediaTR.Persistence.Models;

namespace TestMediaTR.Support
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<PrcConceptos_Model, ConceptoDTO>()
                .ForMember( dest => dest.Id , opt => opt.MapFrom( src => src.conNumero))
                .ForMember( dest => dest.Codigo , opt => opt.MapFrom( src => src.conCodigo.Trim()))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.conNombre.Trim()))
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.conTipo.Trim()))
                .ForMember(dest => dest.Parametro, opt => opt.MapFrom(src => src.conParametro.Trim()));
        }
    }
}