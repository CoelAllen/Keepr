namespace Keepr.Repositories;

public class VaultKeepsRepository : BaseRepository
{
  public VaultKeepsRepository(IDbConnection db) : base(db)
  {
  }

  internal VaultKeep CreateVaultKeep(VaultKeep data)
  {
    var sql = @"
    INSERT INTO vaultKeeps(
      creatorId,
      vaultId,
      keepId
    ) VALUES (
      @CreatorId,
      @VaultId,
      @KeepId
    );
    SELECT LAST_INSERT_ID()
    ;";
    data.Id = _db.ExecuteScalar<int>(sql, data);
    return data;
  }

  internal VaultKeep GetVaultKeep(int id)
  {
    var sql = @"
    SELECT
    *

    FROM vaultKeeps
    WHERE vaultKeeps.id = @id
    ;";
    return _db.Query<VaultKeep>(sql, new { id }).FirstOrDefault();
  }

  internal List<Keep> GetKeepsByVaultId(int id)
  {
    var sql = @"
    SELECT 
    k.*,
    vk.*,
    a.*
    FROM keeps k
    JOIN vaultKeeps vk ON vk.keepId = k.id
    JOIN accounts a ON a.id = k.creatorId
    WHERE vk.vaultId = @id
    ;";
    return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
    {
      k.Creator = p;
      return k;
    }, new { id }).ToList();

  }

  internal object DeleteVaultKeep(int id)
  {
    var sql = @"
    DELETE FROM vaultKeeps
    WHERE id = @id LIMIT 1
    ;";
    return _db.Execute(sql, new { id });
  }
}