using Horizons.Services.Core.Contracts;
using Horizons.Web.ViewModels.Destination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Horizons.Web.Controllers
{
    public class DestinationController : BaseController
    {

        private readonly IDestinationService destinationService;
        private readonly ITerrainService terrainService;


        public DestinationController(IDestinationService destinationService,
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
              
              
                string? userId = GetUserId();
                IEnumerable<DestinationIndexViewModel> destinations =
                    await destinationService.GetAllDestinationsAsync(userId);


                return View(destinations);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            try

            {
                string? userId = GetUserId();
                DestinationDetailsViewModel? destination =
                    await destinationService.GetDestinationDetailsByIdAsync(id, userId);
                if (destination == null)
                {
                    return RedirectToAction("Index", "Home");
                }

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
            try
            {
                var terrains = await terrainService.GetAllTerrainsDropdownAsync();
                var viewModel = new DestinationAddInputModel
                {
                    PublishedOn = DateTime.UtcNow,
                    Terrains = terrains
                };
                return View(viewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(DestinationAddInputModel inputModel)
        {
            try
            {
                string? userId = GetUserId();
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (!ModelState.IsValid)
                {
                    inputModel.Terrains = await terrainService
                        .GetAllTerrainsDropdownAsync();

                    return View(inputModel);
                }

                bool addResult = await destinationService
                    .AddDestinationAsync(inputModel, userId);

                if (!addResult)
                {
                    ModelState.AddModelError("", "Something went wrong while saving the destination.");
                    inputModel.Terrains = await terrainService.GetAllTerrainsDropdownAsync();
                    return View(inputModel);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            string? userId = GetUserId();
            bool isPublisher = await destinationService.IsUserPublisherAsync(id, userId);

            if (!isPublisher)
                return RedirectToAction("Index");

            var destination = await destinationService.GetDestinationForEditAsync(userId, id);
            if (destination == null)
                return RedirectToAction("Index");


            destination.Terrains = await terrainService.GetAllTerrainsDropdownAsync();

            return View(destination);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(DestinationEditInputModel model)
        {
            
          

            string? userId = GetUserId();
            if (userId == null)
              
           return RedirectToAction("Login", "Account");

            bool isPublisher = await destinationService.IsUserPublisherAsync(model.Id, userId);
            if (!isPublisher)
              
             return RedirectToAction("Index");

            if (!ModelState.IsValid)
            {
                model.Terrains = await terrainService.GetAllTerrainsDropdownAsync();
                return View(model);
            }

            bool result = await destinationService.EditDestinationAsync(model, userId);
            if (!result)
                return RedirectToAction("Index");


            return RedirectToAction(nameof(Details), new { id = model.Id });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var destination = await destinationService.GetDestinationForDeleteAsync(GetUserId(), id.Value);
            if (destination == null)
            {
                return RedirectToAction("Details");
            }

            return View(destination);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            string? userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            bool isPublisher = await destinationService.IsUserPublisherAsync(id, userId);
            if (!isPublisher)
            {
                return Forbid(); 
            }

            bool result = await destinationService.DeleteDestinationAsync(id, userId);
            if (!result)
            {
                return RedirectToAction("Index");
            }

            TempData["SuccessMessage"] = "Destination successfully deleted.";
            return RedirectToAction("Index");

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Favorites()
        {
            string? userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            try
            {
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
        public async Task<IActionResult> AddToFavorites(int id)
        {
            string? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            await destinationService.AddToFavoritesAsync(userId, id);

            return RedirectToAction(nameof(Details), new { id });
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveFromFavorites(int id)
        {
            string? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            bool result = await destinationService.RemoveFromFavoritesAsync(userId, id);

          
            return RedirectToAction(nameof(Favorites));
        }


    }
}