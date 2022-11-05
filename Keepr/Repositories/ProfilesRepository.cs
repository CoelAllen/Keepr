namespace Keepr.Repositories;
public class ProfilesRepository
{
  private readonly IDbConnection _db;

  public ProfilesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Profile GetProfile(string profileId)
  {
    var sql = @"
    SELECT 
    a.*
    FROM accounts a
    WHERE a.id = @profileId
    ;";
    return _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();
  }
}