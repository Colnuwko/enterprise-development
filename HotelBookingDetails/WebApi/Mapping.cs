using AutoMapper;
using HotelBookingDetails.Domain.Entity;
using HotelBookingDetails.WebApi.Dto;
using HotelBookingDetails.Domain.Repositories;
namespace HotelBookingDetails.WebApi;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Client, ClientDto>().ReverseMap()
            .ForMember("Birthday", opt => opt.MapFrom(c => new DateOnly(c.BirthdayYear, c.BirthdayMonth, c.BirthdayDay)));
        CreateMap<Hotel, HotelDto>().ReverseMap();
        CreateMap<Room, RoomDto>().ReverseMap();
        CreateMap<ReservedRooms, ReservedRoomsDto>().ReverseMap()
            .ForMember("DateArrival", opt => opt.MapFrom(r => DateOnly.ParseExact(r.DateArrival, "yyyy-mm-dd")))
            .ForMember("DateDeparture", opt => opt.MapFrom(r => Converter(r.DateDeparture)));
        CreateMap<TypeRoom, TypeRoomDto>().ReverseMap();
        CreateMap<Passport, PassportDto>().ReverseMap();
    }

    private static DateOnly? Converter(string? date)
    {
        if (date != null) { return DateOnly.ParseExact(date, "yyyy-mm-dd"); }
        DateOnly? finalDate = null;
        return finalDate;
    }
}