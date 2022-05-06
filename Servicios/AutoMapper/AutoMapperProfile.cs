using AutoMapper;
using Migrantes.Models.Entities;
using Migrantes.ViewModels;

namespace Migrantes.Servicios.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IdentidadPersona, DocumentosViewModel>(); 
        }
    }
}
