namespace Keepr.Services;

public class VaultsService
{
  private readonly VaultsRepository _vr;


  public VaultsService(VaultsRepository vr)
  {
    _vr = vr;
  }

  internal Vault CreateVault(Vault data)
  {
    return _vr.CreateVault(data);
  }

  internal List<Vault> GetVaults(string profileId)
  {
    var vaults = _vr.GetVaults(profileId);
    return vaults;
  }
  internal List<Vault> GetMyVaults(string id)
  {
    return _vr.GetMyVaults(id);
  }
  internal Vault GetVault(int id)
  {
    var vault = _vr.GetVault(id);
    if (vault == null)
    {
      throw new Exception("Bad Vault Id");
    }
    if (vault.IsPrivate == true)
    {
      throw new Exception("This vault is Private");
    }
    return vault;
  }

  internal Vault UpdateVault(Vault vault, string userId)
  {
    var original = GetVault(vault.Id);
    if (original.CreatorId != userId)
    {
      throw new Exception("Cannot edit, not your Vault");
    }
    original.Name = vault.Name ?? original.Name;
    original.Description = vault.Description ?? original.Description;
    original.Img = vault.Img ?? original.Img;
    original.IsPrivate = vault.IsPrivate || original.IsPrivate;

    var updated = _vr.UpdateVault(original);
    return updated;
  }


  internal void DeleteRestaurant(int id, string userId)
  {
    var vault = GetVault(id);
    if (vault.CreatorId != userId)
    {
      throw new Exception("Unauthorized");
    }
    _vr.Delete(id);
  }

}
