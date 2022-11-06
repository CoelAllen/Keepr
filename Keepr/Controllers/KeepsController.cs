namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{
  private readonly Auth0Provider _ap;
  private readonly KeepsService _ks;

  [HttpGet("{id}")]
  public ActionResult<Keep> GetKeep(int id)
  {
    try
    {
      var keep = _ks.GetKeep(id);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Keep>> GetKeeps()
  {
    try
    {
      var keeps = _ks.GetKeeps();
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep data)
  {
    try
    {
      var userInfo = await _ap.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null)
      {
        throw new Exception("You must be logged in");
      }
      data.CreatorId = userInfo?.Id;
      data.Creator = userInfo;
      var keep = _ks.CreateKeep(data);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPut("{id}")]
  public async Task<ActionResult<Keep>> UpdateKeep([FromBody] Keep data, int id)
  {
    try
    {
      var userInfo = await _ap.GetUserInfoAsync<Account>(HttpContext);
      data.Id = id;
      var keep = _ks.UpdateKeep(data, userInfo?.Id);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{id}")]
  public async Task<ActionResult<string>> DeleteKeep(int id)
  {
    try
    {
      var userInfo = await _ap.GetUserInfoAsync<Account>(HttpContext);
      _ks.DeleteKeep(id, userInfo?.Id);
      return Ok("Keep Deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }






  public KeepsController(Auth0Provider ap, KeepsService ks)
  {
    _ap = ap;
    _ks = ks;
  }
}