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
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{id}")]
  public ActionResult<Vault> GetVault(int id)
  {
    try
    {
      var vault = _vs.GetVault(id);
      if (vault.IsPrivate == true)
      {
        throw new Exception("This vault is private");
      }
      return Ok(vault);
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
      _vs.DeleteRestaurant(id, userInfo?.Id);
      return Ok("Vault Deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{vaultId}/keeps")]
  public async Task<ActionResult<List<VaultedKeep>>> GetKeepsByVaultId(int id)
  {
    try
    {
      var userInfo = await _ap.GetUserInfoAsync<Account>(HttpContext);
      var keeps = _vks.GetKeepsByVaultId(id);
      // var vault = _vs.GetVault(id);

      // if (vault.IsPrivate == true && userInfo?.Id != vault.CreatorId)
      // {
      //   throw new Exception("Access Denied");
      // }
      // else if (vault.IsPrivate == true && userInfo?.Id == vault.CreatorId)
      // {
      //   return Ok(keeps);
      // }
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