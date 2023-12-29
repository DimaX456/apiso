using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using TicketSelling.Context.Contracts.Enums;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Services.Contracts.Enums;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ModelsRequest;

namespace TicketSelling.Services.AutoMappers
{
    /// <summary>
    /// Маппер
    /// </summary>
    public class ServiceMapper : Profile
    {
        public ServiceMapper() 
        {
            CreateMap<Post, PostModel>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();

            CreateMap<Hall, HallModel>(MemberList.Destination).ReverseMap();
            CreateMap<Film, FilmModel>(MemberList.Destination).ReverseMap();
            CreateMap<Client, ClientModel>(MemberList.Destination).ReverseMap();
            CreateMap<Cinema, CinemaModel>(MemberList.Destination).ReverseMap();
            CreateMap<Staff, StaffModel>(MemberList.Destination).ReverseMap();
            CreateMap<Ticket, TicketModel>(MemberList.Destination)
                .ForMember(x => x.Hall, opt => opt.Ignore())
                .ForMember(x => x.Cinema, opt => opt.Ignore())
                .ForMember(x => x.Client, opt => opt.Ignore())
                .ForMember(x => x.Film, opt => opt.Ignore())
                .ForMember(x => x.Staff, opt => opt.Ignore()).ReverseMap();

            CreateMap<TicketRequestModel, Ticket>(MemberList.Destination)
                .ForMember(x => x.Hall, opt => opt.Ignore())
                .ForMember(x => x.Cinema, opt => opt.Ignore())
                .ForMember(x => x.Client, opt => opt.Ignore())
                .ForMember(x => x.Film, opt => opt.Ignore())
                .ForMember(x => x.Staff, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.DeletedAt, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ForMember(x => x.UpdatedBy, opt => opt.Ignore());
        }
    }
}
