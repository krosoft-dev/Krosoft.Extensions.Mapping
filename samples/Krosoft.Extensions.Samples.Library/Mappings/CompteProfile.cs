using AutoMapper;
using Krosoft.Extensions.Mapping.Extensions;
using Krosoft.Extensions.Samples.Library.Models;
using Krosoft.Extensions.Samples.Library.Models.Dto;

namespace Krosoft.Extensions.Samples.Library.Mappings;

public class CompteProfile : Profile
{
    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="CompteProfile" />.
    /// </summary>
    public CompteProfile()
    {
        CreateMap<Compte, CompteDto>()
            .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
            .ForAllOtherMembers(m => m.Ignore());
    }
}