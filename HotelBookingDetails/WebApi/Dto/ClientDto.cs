using HotelBookingDetails.Domain;

namespace WebApi.Dto
{
    public class ClientDto
    {    
        /// <summary>
         /// Имя
         /// </summary>
        public required string FullName { get; set; }

        /// <summary>
        /// Пасспортные данные
        /// </summary>
        public required Passport PassportData { get; set; }

        /// <summary>
        /// День рождения
        /// </summary>
        public required DateOnly Birthday { get; set; }
    }
}
