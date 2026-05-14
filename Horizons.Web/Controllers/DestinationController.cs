using Horizons.Services.Core.Interfaces;
using Horizons.Web.ViewModels.Destination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Horizons.Web.Controllers
{
    public class DestinationController : BaseController
    {
        private readonly IDestinationService destinationService;
        private readonly ITerrainService terrainService;

        public DestinationController(
            IDestinationService destinationService,
            ITerrainService terrainService)
        {
            this.destinationService = destinationService;
            this.terrainService = terrainService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                string? userId = GetUserId(); var destinations = await destinationService.GetAllDestinationsAsync(userId);
                return View(destinations);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet("api/destinations/map-data")]
        public async Task<IActionResult> GetMapData()
        {
            try
            {
                var destinations = await destinationService.GetMapDataAsync();
                return Ok(destinations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to retrieve map data" });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            try
            {
                if (id == null)
                    return RedirectToAction("Index", "Home");

                string? userId = GetUserId(); var destination = await destinationService.GetDestinationDetailsByIdAsync(id, userId);

                if (destination == null)
                    return RedirectToAction("Index", "Home");

                return View(destination);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Add()
        {
          
                var terrains = await terrainService.GetAllTerrainsDropdownAsync();
                var viewModel = new DestinationAddInputModel
                {
                    PublishedOn = DateTime.UtcNow,
                    Terrains = terrains
                };
                return View(viewModel);
       
            
        }

        [HttpPost]

        public async Task<IActionResult> Add(DestinationAddInputModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Terrains = await terrainService.GetAllTerrainsDropdownAsync();
                    return View(model);
                }
                string? userId = GetUserId(); if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Account");
                await destinationService.AddDestinationAsync(model, userId);
                TempData["SuccessMessage"] = "Destination added successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }









        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Favorites()
        {
            try
            {
                string? userId = GetUserId(); if (string.IsNullOrEmpty(userId))


                    return RedirectToAction("Login", "Account");

                var favorites = await destinationService.GetUserFavoriteDestinationsAsync(userId);
                return View(favorites);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToFavorites(Guid id)
        {
            try
            {
                string? userId = GetUserId(); if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Account");

                await destinationService.AddToFavoritesAsync(userId, id);
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveFromFavorites(Guid id)
        {
            try
            {
                string? userId = GetUserId(); if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Account");

                bool result = await destinationService.RemoveFromFavoritesAsync(userId, id);
                return RedirectToAction(nameof(Favorites));
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Map()
        {
            return View();
        }
    }
}