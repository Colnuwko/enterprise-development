﻿using HotelBookingDetails.Domain;

namespace HotelBookingDetails.WebApi.Dto;

/// <summary>
/// Это кастомный класс для возвращаемого значения аналитического запроса
/// </summary>
/// <remarks>
/// Структура {Минимальная цена, Максимальная цена, Средняя цена, Отель}
/// </remarks>
public class ComposeDataHotelDto(IEnumerable<Hotel> hotel, int min, int max, double avg)
{
    /// <summary>
    /// Отель
    /// </summary>
    public IEnumerable<Hotel> Hotel { get; set; } = hotel;

    /// <summary>
    /// Минимальная цена
    /// </summary>
    public int Min { get; set; } = min;
    /// <summary>
    /// Максимальная цена
    /// </summary>
    public int Max { get; set; } = max;

    /// <summary>
    /// Средняя цена
    /// </summary>
    public double Avg { get; set; } = avg;
}