

using CrudMvc.Models;

namespace CrudMvc.Mappers;

public static class Mapper
{
    public static Entities.Player ToEntity(this Player model) => new()
    {
        Name = model.Name,
        Position = model.Position,
        TeamId = model.TeamId
       
    };

    public static Player ToModel(this Entities.Player entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Position = entity.Position,
        TeamId = entity.Team.Id,
        Team = entity.Team.Name
    };
    public static Team ToModel(this Entities.Team entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        CountPlayer = entity.Players is null ? 0 : entity.Players.Count()
    };
    public static Entities.Team ToEntity(this Team model) => new()
    {
        Name = model.Name
    };
}