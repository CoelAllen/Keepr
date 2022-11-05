namespace Keepr.Repositories;

public class KeepsRepository : BaseRepository
{





  public KeepsRepository(IDbConnection db) : base(db)
  {
  }

  internal Keep CreateKeep(Keep data)
  {
    var sql = @"
    INSERT INTO keeps(
      name,
      description,
      img,
      views,
      kept,
      creatorId
      )
      VALUES(
        @Name,
        @Description,
        @Img,
        @Views,
        @Kept,
        @CreatorId
      );
      SELECT LAST_INSERT_ID()
    ;";
    data.CreatedAt = DateTime.Now;
    data.UpdatedAt = DateTime.Now;
    data.Id = _db.ExecuteScalar<int>(sql, data);
    return GetKeep(data.Id);
  }

  internal List<Keep> GetKeeps()
  {
    var sql = @"
    SELECT
    k.*,
    a.*
    FROM keeps k
    JOIN accounts a ON a.id = k.creatorId
    ;";
    return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
    {
      k.Creator = p;
      return k;
    }).ToList();
  }

  internal Keep UpdateKeep(Keep original)
  {
    var sql = @"
    UPDATE keeps SET
    name = @Name,
    description = @Description,
    img = @Img,
    views = @Views,
    kept = @Kept
    WHERE id = @Id
    ;";
    original.UpdatedAt = DateTime.Now;
    _db.Execute(sql, original);
    return original;
  }

  internal void DeleteKeep(int id)
  {
    var sql = @"
    DELETE FROM keeps
    WHERE id = @Id
    ;";
    _db.Execute(sql, new { id });
  }

  internal Keep GetKeep(int id)
  {
    var sql = @"
     SELECT
     k.*,
     a.*
     FROM keeps k
     JOIN accounts a ON a.id = k.creatorID
     WHERE k.id =@id
    ;";
    return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
    {
      k.Creator = p;
      return k;
    }, new { id }).FirstOrDefault();
  }
}