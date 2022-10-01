
namespace CrudMvc.TeamController;
using CrudMvc.Data;
using CrudMvc.Mappers;
using CrudMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class TeamController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<TeamController> _logger;

    public TeamController(
        ILogger<TeamController> logger,
        AppDbContext context
    )
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Get()
    {
        var list = _context.Teams!.Include(a => a.Players).Select(p => p.ToModel()).ToList();
        return View(list);
    }

   public IActionResult Post() => View();
   [HttpPost]
   public IActionResult Post(Team createTeam)
   {
        if(!ModelState.IsValid) return View();
        _context.Teams?.Add(createTeam.ToEntity());
        _context.SaveChanges();
        return RedirectToAction("Get");
   }

    [Authorize(Roles = "admin")]
    public IActionResult Delete(int id) => View(new Team(){Id = id});
    [HttpPost]
    public IActionResult Delete(Player player)
    {

        if(!_context.Teams!.Any(x => x.Id == player.Id)) return RedirectToAction("Delete");
        var a=_context.Teams!.FirstOrDefault(x => x.Id == player.Id);

        _context.Teams!.Remove(a!);
        _context.SaveChanges();
        return RedirectToAction("Get");
    }
    [Authorize]
    public IActionResult Edit(int id) => View(new Team(){Id = id});
    [HttpPost]
    public IActionResult Edit(Team team)
    {
        if(!_context.Teams!.Any(x => x.Id == team.Id)) return RedirectToAction("Edit");
        var a=_context.Teams!.FirstOrDefault(x => x.Id == team.Id);

        a!.Name = team.Name;
        _context.Teams?.Update(a);
        _context.SaveChanges();
        return LocalRedirect($"/team/get");
    }
}