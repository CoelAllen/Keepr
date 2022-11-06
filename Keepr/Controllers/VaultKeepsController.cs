namespace Keepr.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  private readonly VaultKeepsService _vks;
  private readonly Auth0Provider _ap;


  [HttpPost]
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep data)
  {
    try
    {
      var userInfo = await _ap.GetUserInfoAsync<Account>(HttpContext);
      data.CreatorId = userInfo?.Id;
      var vKeep = _vks.CreateVaultKeep(data, userInfo?.Id);
      return Ok(vKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{id}")]

  public async Task<ActionResult<string>> DeleteVaultKeep(int id)
  {
    try
    {
      var userInfo = await _ap.GetUserInfoAsync<Account>(HttpContext);
      _vks.DeleteVaultKeep(id, userInfo?.Id);
      return Ok("Deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }








  public VaultKeepsController(VaultKeepsService vks, Auth0Provider ap)
  {
    _vks = vks;
    _ap = ap;
  }
}