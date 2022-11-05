namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProfilesController : ControllerBase
{
  private readonly ProfilesService _ps;
  private readonly Auth0Provider _ap;
  private readonly KeepsService _ks;
  private readonly VaultsService _vs;

  public ProfilesController(ProfilesService ps, Auth0Provider ap, KeepsService ks, VaultsService vs)
  {
    _ps = ps;
    _ap = ap;
    _ks = ks;
    _vs = vs;
  }



  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetProfile(string profileId)
  {
    try
    {
      var profile = _ps.GetProfile(profileId);
      return Ok(profile);

    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/keeps")]

  public ActionResult<List<Keep>> GetProfileKeeps(string profileId)
  {
    try
    {
      var keeps = _ks.GetKeeps(profileId);
      return keeps;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/vaults")]
  public ActionResult<List<Vault>> GetProfileVaults(string profileId)
  {
    try
    {
      var vaults = _vs.GetVaults(profileId);
      return vaults;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}