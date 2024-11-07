﻿using HotelBookingDetails.Domain;
namespace HotelBookingDetails.Tests;

public class Test(HotelBookingDetailsData dataProvider) : IClassFixture<HotelBookingDetailsData>
{
    private readonly HotelBookingDetailsData _dataProvider = dataProvider;

    [Fact]
    public void ReturnAllHotels()
    {
        var countHotels = _dataProvider.Hotels.Select(h => h.Name).ToList();
        var expectedNumber = 6;
        Assert.Equal(expectedNumber, countHotels.Count);
    }

    [Fact]
    public void ReturnAllClientInHotel()
    {
        var expecteClients = new List<Client>
        {
            _dataProvider.Clients[9],
            _dataProvider.Clients[10],
            _dataProvider.Clients[8],
            _dataProvider.Clients[11],
        };
        var hotelId = _dataProvider.Hotels.Where(h => h.Name == "Hilton").Select(h => h.Id).First();
        var clientInHotel = _dataProvider.ReservedRooms
            .OrderBy(r => r.Client.FullName)
            .Where(r => _dataProvider.Rooms
            .Where(r => r.HotelId == hotelId)
            .Select(r => r).ToList().Contains(r.Room))
            .Select(r => r.Client)
            .ToList();
        Assert.Equal(clientInHotel, expecteClients);
    }

    [Fact]
    public void ReturnTopFiveHotel()
    {
        var expecteHhotels = new List<Hotel>
        {
            _dataProvider.Hotels[0],
            _dataProvider.Hotels[1],
            _dataProvider.Hotels[2],
            _dataProvider.Hotels[3],
            _dataProvider.Hotels[5]
        };


        var topFiveHotel = _dataProvider.ReservedRooms
            .GroupBy(r => r.Room.HotelId)
            .Select(r => r.Key)
            .Take(5)
            .Join(_dataProvider.Hotels,
            roomId => roomId,
            hotel => hotel.Id,
            (roomId, hotel) => hotel)
            .OrderBy(r => r.Id)
            .ToList();
        Assert.Equal(expecteHhotels, topFiveHotel);

    }

    [Fact]
    public void ReturnFreeRooms()
    {
        var expectedRooms = new List<Room>
        {
            _dataProvider.Rooms[4],
            _dataProvider.Rooms[5],
            _dataProvider.Rooms[6],
            _dataProvider.Rooms[7]

        };
        var city = "New York";
        var freeRooms = _dataProvider.ReservedRooms
            .Where(r => !_dataProvider.ReservedRooms.Where(r => r.DateDeparture == DateOnly.ParseExact("0001-01-01", "yyyy-mm-dd"))
                .Select(r => r).ToList().Contains(r)
                &&
                _dataProvider.Rooms.Where(r => (_dataProvider.Hotels.Where(h => h.City == city).Select(h => h.Id).ToList()).Contains(r.HotelId))
                .Select(h => h).ToList().Contains(r.Room)
                ).Select(r => r.Room).ToList();
        Assert.Equal(expectedRooms, freeRooms);
    }

    [Fact]
    public void ReturnLongLiversHotel()
    {
        var expecteClients = new List<Client>
        {
            _dataProvider.Clients[14],
            _dataProvider.Clients[15],

        };

        var longerPeriods = _dataProvider.ReservedRooms
            .GroupBy(c => c.Client)
            .Select(c => new
            {
                Client = c.Key,
                Total = c.Sum(r => r.Period)
            }).Max(c => c.Total);

        var clientWithLongerPer = _dataProvider.ReservedRooms
            .GroupBy(c => c.Client)
            .Select(c => new
            {
                Client = c.Key,
                Total = c.Sum(r => r.Period)
            }).Where(c => c.Total == longerPeriods).Select(c => c.Client).ToList();

        Assert.Equal(expecteClients, clientWithLongerPer);
    }

    [Fact]
    public void MinAvgMaxCostInHotel()
    {
        var hotels = _dataProvider.Hotels.Select(h => h);

        var hotelCosts = hotels.Select(h => new
        {
            Hotel = (_dataProvider.Hotels.Where(hotel => hotel.Id == h.Id).Select(hotel => hotel)),
            Min = _dataProvider.Rooms.Where(r => r.HotelId == h.Id).Select(r => r).ToList().Min(rm => rm.Cost),
            Max = _dataProvider.Rooms.Where(r => r.HotelId == h.Id).Select(r => r).ToList().Max(rm => rm.Cost),
            Avg = _dataProvider.Rooms.Where(r => r.HotelId == h.Id).Select(r => r).ToList().Average(rm => rm.Cost)
        }).ToList();
        Assert.Equal(3000, hotelCosts[0].Min);
        Assert.Equal(4500.00, hotelCosts[0].Avg, 2);
        Assert.Equal(6000, hotelCosts[0].Max);
    }
}
