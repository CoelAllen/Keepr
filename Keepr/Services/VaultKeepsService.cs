namespace Keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vkr;

  public VaultKeepsService(VaultKeepsRepository vkr)
  {
    _vkr = vkr;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep data)
  {
    return _vkr.CreateVaultKeep(data);
  }

  internal List<Keep> GetKeepsByVaultId(int id)
  {
    return _vkr.GetKeepsByVaultId(id);
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

  internal string DeleteVaultKeep(int id, string userId)
  {
    var keep = GetVaultKeep(id);
    if (keep.CreatorId != userId)
    {
      throw new Exception("This is not Yours!");
    }
    var removed = _vkr.DeleteVaultKeep(id);
    // if (removed = null)
    // {
    //   throw new Exception("Unable to delete");
    // }
    return "Successfully Deleted";
  }
}