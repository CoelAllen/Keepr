namespace Keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vkr;

  public VaultKeepsService(VaultKeepsRepository vkr)
  {
    _vkr = vkr;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep data, string userId)
  {
    var vaultKeep = _vkr.CreateVaultKeep(data);
    if (vaultKeep.CreatorId != userId)
    {
      throw new Exception("Incorrect Login credentials");
    }
    else
    {

      return vaultKeep;
    }
  }

  internal List<VaultedKeep> GetKeepsByVaultId(int vaultId)
  {
    var keeps = _vkr.GetKeepsByVaultId(vaultId);
    return keeps;
  }
  internal VaultKeep GetVaultKeep(int id)
  {
    var keep = _vkr.GetVaultKeep(id);
    if (keep == null)
    {
      throw new Exception("Bad VaultKeep Id");
    }
    return keep;
  }

  internal void DeleteVaultKeep(int id, string userId)
  {
    var keep = GetVaultKeep(id);
    if (keep.CreatorId != userId)
    {
      throw new Exception("This is not Yours!");
    }
    _vkr.DeleteVaultKeep(id);
    // if (removed = null)
    // {
    //   throw new Exception("Unable to delete");
    // }
  }

}