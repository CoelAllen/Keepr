namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
  private readonly VaultsService _vs;
  private readonly Auth0Provider _ap;
  private readonly VaultKeepsService _vks;

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault data)
  {

    try
    {
      var userInfo = await _ap.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("Bad Login Info");
      }
      data.CreatorId = userInfo?.Id;
      var vault = _vs.CreateVault(data);
      vault.Creator = userInfo;
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{id}")]
  public async Task<ActionResult<Vault>> GetVault(int id)
  {
    try
    {
      var userInfo = await _ap.GetUserInfoAsync<Account>(HttpContext);
      var vault = _vs.GetVault(id, userInfo?.Id);
      return vault;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPut("{id}")]
  public async Task<ActionResult<Vault>> UpdateVault([FromBody] Vault data, int id)
  {
    try
    {
      var userInfo = await _ap.GetUserInfoAsync<Account>(HttpContext);
      data.Id = id;
      var vault = _vs.UpdateVault(data, userInfo?.Id);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{id}")]

  public async Task<ActionResult<string>> DeleteVault(int id)
  {
    try
    {
      var userInfo = await _ap.GetUserInfoAsync<Account>(HttpContext);
      _vs.DeleteVault(id, userInfo?.Id);
      return Ok("Vault Deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }



  [HttpGet("{vaultId}/keeps")]
  public async Task<ActionResult<List<VaultedKeep>>> GetKeepsByVaultId(int vaultId)
  {
    try
    {
      var userInfo = await _ap.GetUserInfoAsync<Account>(HttpContext);
      var keeps = _vks.GetKeepsByVaultId(vaultId, userInfo);

      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  public VaultsController(VaultsService vs, Auth0Provider ap, VaultKeepsService vks)
  {
    _vs = vs;
    _ap = ap;
    _vks = vks;

  }
}