namespace Keepr.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _pr;

  public ProfilesService(ProfilesRepository pr)
  {
    _pr = pr;
  }

  internal Profile GetProfile(string profileId)
  {
    var profile = _pr.GetProfile(profileId);
    if (profile == null)
    {
      throw new Exception("Profile does not exist");
    }
    return profile;
  }
}