using InventorySystem.Data.Entities;
using InventorySystem.Data.Models;
using InventorySystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Presentation.Controllers;

[Route("[controller]/[action]")]
public class InventarioController : Controller
{
    private InventarioServices _inventarioServices;
    private ProductoServices _productoServices;
    private SucursalService _sucursalService;

    public InventarioController(InventarioServices inventarioServices,ProductoServices productoServices,SucursalService sucursalService )
    {
        _inventarioServices = inventarioServices;

        _productoServices = productoServices;

        _sucursalService = sucursalService;
    }
    [HttpGet]
    public async Task<IActionResult> Lista()
    {
        var result = await _inventarioServices.GetInventarioListGroupBySucursal(Guid.Empty);
        return View(result);

    }

     [HttpGet]
    public async Task<IActionResult> Crear([FromQuery]Guid? sucursalid=null)
    {
        var sucursales = await _sucursalService.GetAll();

        if (TempData["SucursalId"] == null)
            TempData["SucursalId"] = sucursalid;
        else
        {
            TempData["SucursalId"] = TempData["SucursalId"];

            if(sucursalid ==Guid.Empty || sucursalid  ==null)
                if(Guid.TryParse(TempData["SucursalId"].ToString(), out var sic))
                {
                    sucursalid = sic;
                }
        }
        
        var productos = await _productoServices.GetProductListNotRegisteredBySucuralId(sucursalid??Guid.Empty);
        return View(new CrearInventarioViewModel { Productos = productos, Sucursales = sucursales });
    }

    [HttpPost]
    public async Task<IActionResult> Crear(Inventario inventario, [FromQuery] Guid? sucursalId = null)
    {
        if(!ModelState.IsValid)
        {

            var errores =ModelState.Values.SelectMany(x => x.Errors.Select(y => $"<li>{y.ErrorMessage}</li>").ToList()).ToList();
            
            
            ViewData["Validation"] = errores;
            if (TempData["SucursalId"] != null || sucursalId != null)
            {
                Guid guid = Guid.Empty;
                if (TempData["SucursalId"] != null)
                    Guid.TryParse(TempData["SucursalId"].ToString(), out guid);
                else
                {
                    guid = (Guid)sucursalId;
                    TempData["SucursalId"] = sucursalId;
                }


                 return await Crear(guid);
            }
            return await Crear();
        }
        try
        {
            if(await _inventarioServices.ExistInventarioByExpression(x=>x.SucursalId == inventario.SucursalId && x.ProductoId ==inventario.ProductoId))
            {
                List<string> errores = new();

                if(ViewData["Validation"]!=null)
                {
                    errores = ViewData["Validation"] as List<string>;
                }
                var sucursal =await _sucursalService.GetSucursal(inventario.SucursalId);
                var producto = await _productoServices.GetProducto(inventario.ProductoId);
                errores.Add($" <li> Ya existe un registro para la sucursal {sucursal?.Nombre} ( {inventario.SucursalId} ) con el producto {producto?.Nombre} ({inventario?.ProductoId}) <li>");

                ViewData["Validation"] = errores;
                if (TempData["SucursalId"] != null || sucursalId != null)
                {
                    Guid guid = Guid.Empty;
                    if (TempData["SucursalId"] != null)
                        Guid.TryParse(TempData["SucursalId"].ToString(), out guid);
                    else
                    {
                        guid = (Guid)sucursalId;
                        TempData["SucursalId"] = sucursalId;
                    }


                    return await Crear(guid);
                }
                return await Crear();
            }
            await _inventarioServices.CreateInventario(inventario);
           return RedirectToAction("Lista");
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit([FromQuery]Guid inventarioid)
    {
        var inventario =await _inventarioServices.GetInventarioById(inventarioid);
        var productos = await _productoServices.GetAll();
        var sucursales = await _sucursalService.GetAll();
        return View(new EditInventarioViewModel
        {  
            Inventario = inventario, 
            Productos = productos,
            Sucursales = sucursales
        });
    }
    [HttpPost]
    public async Task<IActionResult> Edit([FromForm]Inventario inventario ,[FromQuery] Guid inventarioid)
    {
            if (!ModelState.IsValid)
            {
                inventario = await _inventarioServices.GetInventarioById(inventarioid);
                var productos = await _productoServices.GetAll();
                var sucursales = await _sucursalService.GetAll();
                return View(new EditInventarioViewModel
                {
                    Inventario = inventario,
                    Productos = productos,
                    Sucursales = sucursales
                });
            }
           

            try
            {
                if (await _inventarioServices.ExistInventarioByExpression(x=>x.SucursalId == inventario.SucursalId && x.ProductoId ==inventario.ProductoId))
                {
                    ModelState.AddModelError("ProductoId", "Ya existe un producto en esta sucursal");
                    inventario = await _inventarioServices.GetInventarioById(inventarioid);
                    var productos = await _productoServices.GetAll();
                    var sucursales = await _sucursalService.GetAll();
                    return View(new EditInventarioViewModel
                    {
                        Inventario = inventario,
                        Productos = productos,
                        Sucursales = sucursales
                    });
                }
                await  _inventarioServices.UpdateInventorio(inventario);
                return RedirectToAction("Lista");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete([FromQuery][Required] Guid? inventarioId)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("id", "Debe ser valido");
                return RedirectToAction("Lista");
            }
            try
            {
                await _inventarioServices.DeleteInventarioById((Guid)inventarioId!);
                return RedirectToAction("Lista");
            }
            catch (Exception ex)
            {

                throw;
            }
        }


}
