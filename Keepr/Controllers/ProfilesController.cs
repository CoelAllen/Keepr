namespace Keepr.Controllers;

[ApiController]
[Route("[controller]")]

public class ProfilesController : ControllerBase
{
  private readonly ProfilesService _ps;
  private readonly Auth0Provider _ap;

  public ProfilesController(ProfilesService ps, Auth0Provider ap)
  {
    _ps = ps;
    _ap = ap;
  }

  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetProfile(string profileId)
  {
    var profile = _ps.GetProfile(profileId);
    return Ok(profile);
  }
}