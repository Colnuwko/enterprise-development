namespace HotelBookingDetails.Domain.Repositories;

internal interface IRepositoryPassport
{
    public IEnumerable<Passport> GetPassports();

    public Passport? GetPassportById(int id);

    public bool PostPassport(Passport passport);

    public bool PutPassport(int id, Passport passport);
    public bool DeletePassport(int id);
}