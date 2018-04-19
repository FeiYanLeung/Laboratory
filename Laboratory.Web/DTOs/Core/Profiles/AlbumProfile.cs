
using AutoMapper;
namespace Laboratory.Web.Dto
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile()
        {
            base.CreateMap<Album, AlbumDto>()
                .ForMember(dto => dto.GenreName, opts => opts.MapFrom(src => src.Genre.Name))
                .ForMember(dto => dto.ArtistName, opts => opts.MapFrom(src => src.Artist.Name))
                .SkipVirtualProperties()
                .ReverseMap();

        }
    }
}
