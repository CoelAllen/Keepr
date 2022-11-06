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
    WHERE id = @id
    ;";
    return _db.Query<VaultKeep>(sql, new { id }).FirstOrDefault();
  }

  // TODO This still doesn't work and I need to check if vault.isPrivate is false for auth checks

  // NOTE Ask about bearer token issue-incognito gives same token as regular window.
  internal List<VaultedKeep> GetKeepsByVaultId(int id)
  {
    var sql = @"
    SELECT
    k.*,
    vk.id AS vaultKeepId,
    a.*
    FROM vaultKeeps vk
    JOIN keeps k ON k.id = vk.keepId
    JOIN accounts a ON a.id = vk.creatorId
    WHERE vk.vaultId = @id
    ;";
    return _db.Query<VaultedKeep, Profile, VaultedKeep>(sql, (v, p) =>
    {
      v.Creator = p;
      return v;
    }, new { id }).ToList();

  }

  internal void DeleteVaultKeep(int id)
  {
    var sql = @"
    DELETE FROM vaultKeeps
    WHERE id = @id LIMIT 1
    ;";
    _db.Execute(sql, new { id });

  }
}

// NOTE this is another try at the dirty keeps thing
// SELECT
// vk.*,
// k.*,
// a.*,
// v.*
// FROM vaultKeeps vk

// JOIN keeps k ON k.id = vk.keepId

// JOIN accounts a ON a.id = vk.creatorId

// JOIN vaults v ON v.id = vk.vaultId

// WHERE vk.vaultId = @id AND v.isPrivate = 0