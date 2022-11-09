namespace Keepr.Repositories;

public class VaultsRepository : BaseRepository
{


  public VaultsRepository(IDbConnection db) : base(db)
  {
  }

  internal Vault CreateVault(Vault data)
  {
    var sql = @"
    INSERT INTO vaults(
      name,
      description,
      img,
      isPrivate,
      creatorId
    ) VALUES (
      @Name,
      @Description,
      @Img,
      @IsPrivate,
      @CreatorId
    );
    SELECT LAST_INSERT_ID()
    ;";
    data.CreatedAt = DateTime.Now;
    data.UpdatedAt = DateTime.Now;
    data.Id = _db.ExecuteScalar<int>(sql, data);
    return data;
  }

  internal List<Vault> GetMyVaults(string id)
  {
    var sql = @"
    SELECT
    v.*,
    a.*
    FROM vaults v
    JOIN accounts a ON a.id = v.creatorId
    WHERE a.id = @id
    ;";
    return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
    {
      v.Creator = a;
      return v;
    }, new { id }).ToList();
  }

  internal List<Vault> GetVaults(string profileId)
  {
    var sql = @"
    SELECT 
    v.*,
    a.*
    FROM vaults v
    JOIN accounts a ON a.id = v.creatorId
    WHERE v.creatorId = @profileId AND v.isPrivate = 0
    ;";
    return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
    {
      v.Creator = p;
      return v;
    }, new { profileId }).ToList();
  }

  internal Vault GetVault(int id)
  {
    var sql = @"
    SELECT
    v.*,
    a.*
    FROM vaults v
    JOIN accounts a ON a.id = v.creatorId
    WHERE v.id = @id
    ;";
    return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
    {
      v.Creator = p;
      return v;
    }, new { id }).FirstOrDefault();
  }

  internal Vault UpdateVault(Vault original)
  {
    var sql = @"
    UPDATE vaults SET
      name = @Name,
      description = @Description,
      img = @Img,
      isPrivate = @IsPrivate
    WHERE id = @Id
    ;";
    original.UpdatedAt = DateTime.Now;
    _db.Execute(sql, original);

    return original;
  }

  internal void Delete(int id)
  {
    var sql = @"
    DELETE FROM vaults
    WHERE id = @id LIMIT 1
    ;";
    _db.Execute(sql, new { id });
  }
}