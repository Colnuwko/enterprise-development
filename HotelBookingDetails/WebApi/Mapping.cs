using AutoMapper;
using HotelBookingDetails.Domain;
using WebApi.Dto;


namespace WebApi;

public class Mapping: Profile
{
    public Mapping()
    {
        CreateMap<Passport, ClientDto>().ReverseMap();
        CreateMap<Hotel, HotelDto>().ReverseMap();
        CreateMap<Room, RoomDto>().ReverseMap();
        CreateMap<ReservedRooms, ReservedRoomsDto>().ReverseMap();
        CreateMap<TypeRoom, TypeRoomDto>().ReverseMap();
        CreateMap<Passport, PassportDto>().ReverseMap();

    }

}
