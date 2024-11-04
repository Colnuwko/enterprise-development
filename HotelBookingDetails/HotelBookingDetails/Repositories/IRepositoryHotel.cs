﻿namespace HotelBookingDetails.Domain.Repositories;

public interface IRepositoryHotel
{
    public IEnumerable<Hotel> GetHotels();

    public Hotel? GetHotelById(int id);

    public bool PostHotel(Hotel hotel);

    public bool PutHotel(int id, Hotel hotel);
    public bool DeleteHotel(int id);
    public int GetCountHotels();

    public int GetHotelIdByName(string name);

    public IEnumerable<Hotel> GetTopFiveHotelById(IEnumerable<int> id);
}