namespace CrudMvc.PlayerController;

using System;
using CrudMvc.Data;
using CrudMvc.Mappers;
using CrudMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class PlayerController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<PlayerController> _logger;

    public PlayerController(
        ILogger<PlayerController> logger,
        AppDbContext context
    )
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Get()
    {
        var list = _context.Players!.Include(a => a.Team).Select(p => p.ToModel()).ToList();
        return View(list);
    }

   public IActionResult Post() => View();
   [HttpPost]
   public IActionResult Post(Player createPlayer)
   {
        // if(!ModelState.IsValid) return View();
        // if (_context.Players!.Include(x => x.Team).All(p => p.TeamId != createPlayer.Id))
        // {
        //     return RedirectToAction("Edit");
        // }
        // else
        // {
        //     _context.Players?.Add(createPlayer.ToEntity());
        //     _context.SaveChanges();
        //     return RedirectToAction("Get");
        // }


        try
        {
            _context.Players?.Add(createPlayer.ToEntity());
            _context.SaveChanges();
            return RedirectToAction("Get");
        }
        catch (Exception e)
        {
            return RedirectToAction("Post");
        }
    }

    [Authorize(Roles = "admin")]
    public IActionResult Delete(int id) => View(new Player(){Id = id});
    [HttpPost]
    public IActionResult Delete(Player player)
    {

        if(!_context.Players!.Any(x => x.Id == player.Id)) return RedirectToAction("Delete");
        var a=_context.Players!.FirstOrDefault(x => x.Id == player.Id);

        _context.Players!.Remove(a!);
        _context.SaveChanges();
        return RedirectToAction("Get");

    }
    [Authorize]
    public IActionResult Edit(int id) => View(new Player(){Id = id});
    [HttpPost]
    public IActionResult Edit(Player player)
    {
        if(!_context.Players!.Any(x => x.Id == player.Id)) return RedirectToAction("Edit");
        var a=_context.Players!.FirstOrDefault(x => x.Id == player.Id);

        a!.Name = player.Name;
        a.Position = player.Position;
        _context.Players?.Update(a);
        _context.SaveChanges();
        return LocalRedirect($"/player/get");
    }
}