namespace Keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vkr;
  private readonly VaultsRepository _vr;

  public VaultKeepsService(VaultKeepsRepository vkr, VaultsRepository vr)
  {
    _vkr = vkr;
    _vr = vr;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep data, string userId)
  {
    var vaultKeep = _vkr.CreateVaultKeep(data);
    var vault = _vr.GetVault(data.VaultId);
    if (vault.CreatorId != userId)
    {
      throw new Exception("Not your vault");
    }
    if (vaultKeep.CreatorId != userId)
    {
      throw new Exception("Incorrect Login credentials");
    }

    return vaultKeep;
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