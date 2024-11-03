using AutoMapper;
using HotelBookingDetails.Domain;
using WebApi.Dto;


namespace WebApi;

public class Mapping: Profile
{
    public Mapping()
    {
        CreateMap<Client, ClientDto>().ReverseMap();
    }

}
