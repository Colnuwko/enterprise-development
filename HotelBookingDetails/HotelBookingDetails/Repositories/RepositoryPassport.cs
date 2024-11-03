
namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryPassport : IRepositoryPassport
{

    private readonly List<Passport> _passports = [];
    private int _passportId = 1;

    public bool PutPassport(int id, Passport passport)
    {
        var oldValue = GetPassportById(id);

        if (oldValue != null) { return false; }
        return true;
    }

    public bool DeletePassport(int id)
    {
        var passport = GetPassportById(id);
        if (passport != null) { return false; }
        _passports.Remove(passport);
        return true;
    }

    public bool PostPassport(Passport passport)
    {
        passport.Id = _passportId++;
        _passports.Add(passport);
        return true;
    }

    public Passport? GetPassportById(int id) => _passports.Find(p => p.Id == id);

    public IEnumerable<Passport> GetPassports() => _passports;

}
