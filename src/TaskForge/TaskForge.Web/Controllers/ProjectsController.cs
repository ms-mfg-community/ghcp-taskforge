using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskForge.Core.DTOs;
using TaskForge.Core.Interfaces;
using TaskForge.Data.Models;

namespace TaskForge.Web.Controllers;

[Authorize]
public class ProjectsController : Controller
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var projects = await _projectService.GetProjectsByUserAsync(userId);
        return View(projects);
    }

    public async Task<IActionResult> Details(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
            return NotFound();

        return View(project);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProjectDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        await _projectService.CreateProjectAsync(dto, userId);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
            return NotFound();

        var dto = new UpdateProjectDto
        {
            Name = project.Name,
            Description = project.Description,
            Status = project.Status
        };

        ViewBag.ProjectId = id;
        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateProjectDto dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ProjectId = id;
            return View(dto);
        }

        var result = await _projectService.UpdateProjectAsync(id, dto);
        if (result == null)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
            return NotFound();

        return View(project);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _projectService.DeleteProjectAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
