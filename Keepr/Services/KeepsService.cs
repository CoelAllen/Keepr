namespace Keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _kr;

  public KeepsService(KeepsRepository kr)
  {
    _kr = kr;
  }

  internal Keep CreateKeep(Keep data)
  {
    return _kr.CreateKeep(data);
  }

  internal Keep GetKeep(int id)
  {
    var keep = _kr.GetKeep(id);
    if (keep == null)
    {
      throw new Exception("Not a valid Keep");
    }
    // TODO Add increasing views here
    return keep;

  }

  internal List<Keep> GetKeeps()
  {
    return _kr.GetKeeps();
  }

  internal Keep UpdateKeep(Keep keep, string userId)
  {
    var original = GetKeep(keep.Id);
    if (original.CreatorId != userId)
    {
      throw new Exception("You cannot edit others Keeps");
    }
    original.Name = keep.Name;
    original.Description = keep.Description;
    original.Img = keep.Img ?? original.Img;
    original.Views = keep.Views;
    original.Kept = keep.Kept;

    var updated = _kr.UpdateKeep(original);
    return updated;
  }

  internal void DeleteKeep(int keepId, string userId)
  {
    var keep = GetKeep(keepId);
    if (keep.CreatorId != userId)
    {
      throw new Exception("You cannot delete this Keep");
    }
    _kr.DeleteKeep(keepId);
  }
  // internal void UpdateKeep(Keep k)
  // {
  //   // TODO this will link to the views update, METHOD OVERLOAD BABY!
  // }
}