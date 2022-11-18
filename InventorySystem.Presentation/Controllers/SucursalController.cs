using InventorySystem.Data.Entities;
using InventorySystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Presentation.Controllers
{
    [Route("[controller]/[action]")]
    public class SucursalController : Controller
    {
         SucursalService _sucursalService { get; }

         InventarioServices _inventarioService { get; }

        public SucursalController(SucursalService sucursalService, InventarioServices inventarioService)
        {
            _sucursalService = sucursalService;
            _inventarioService = inventarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var sucursales = await _sucursalService.GetAll();
            return View(sucursales);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            TempData["ExpectedRedirectAction"] = TempData["ExpectedRedirectAction"];
            TempData["ExpectedRedirectController"] = TempData["ExpectedRedirectController"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Sucursal sucursal)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("Sucursal.Nombre", "Debe Posee un nombre");
                return View(sucursal);
            }

            try
            {
                if(await _sucursalService.ExistSucursalByExpression(x=>x.Nombre.ToLower() == sucursal.Nombre.ToLower()))
                {
                    ModelState.AddModelError("Sucursal.Nombre", "Ya existe una sucursal con este nombre");
                    return View(sucursal);
                }
                await _sucursalService.CreateSucursal(sucursal);
                
                if(TempData["ExpectedRedirectAction"] !=null &&  TempData["ExpectedRedirectController"]!=null)
                {
                    return RedirectToAction(actionName: TempData["ExpectedRedirectAction"]!.ToString(),controllerName: TempData["ExpectedRedirectController"]!.ToString());
                }

                return RedirectToAction("Lista");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Inventario([FromQuery][Required]Guid? sucursalId)
        {
            ViewData["SucursalId"] = sucursalId;
            var result =await _inventarioService.GetInventarioListGroupBySucursal((Guid)sucursalId);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult>  Delete([FromQuery][Required]Guid? id)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("id", "Debe ser valido");
                return RedirectToAction("Lista");
            }
            try
            {
                await _sucursalService.DeleteSucursalById((Guid)id!);
                return RedirectToAction("Lista");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
