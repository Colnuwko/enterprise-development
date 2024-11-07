
namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryPassport : IRepository<Passport>
{

    private readonly List<Passport> _passports = [];
    private int _passportId = 1;

    public bool Put(int id, Passport passport)
    {
        var oldValue = GetById(id);

        oldValue.Number = passport.Number;
        oldValue.Series = passport.Series;
        return true;
    }

    public bool Delete(int id)
    {
        var passport = GetById(id);
        if (passport == null) 
            return false; 
        _passports.Remove(passport);
        return true;
    }

    public bool Post(Passport passport)
    {
        passport.Id = _passportId++;
        _passports.Add(passport);
        return true;
    }

    public Passport? GetById(int id) => _passports.Find(p => p.Id == id);

    public IEnumerable<Passport> GetAll() => _passports;

}
