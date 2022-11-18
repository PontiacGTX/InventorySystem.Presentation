using InventorySystem.Data.Entities;
using InventorySystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Presentation.Controllers;

[Route("[controller]/[action]")]
public class ProductoController : Controller
{
    private ProductoServices _productoServices;

    public ProductoController(ProductoServices productoServices)
    {
        _productoServices = productoServices;
    }

    [HttpGet]
    public async Task<IActionResult> Lista()
    {
        var productos = await _productoServices.GetAll();
        return View(productos);
    }

    [HttpGet]
    public async Task<IActionResult> Edit([FromQuery][Required]Guid? id)
    {
        
        return View(await _productoServices.GetProducto((Guid)id));
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Producto producto)
    {
        if (!ModelState.IsValid)
        {
            return View(producto);
        }

        try
        {
            
            await _productoServices.UpdateProducto(producto);
            return RedirectToAction("Lista");
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpGet]
    public async Task<IActionResult> Crear()
    {
        TempData["ExpectedRedirectAction"] = TempData["ExpectedRedirectAction"];
        TempData["ExpectedRedirectController"] = TempData["ExpectedRedirectController"];
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Crear(Producto  producto)
    {
        if(!ModelState.IsValid)
        {
            return View(producto);
        }

        try
        {
            if (await _productoServices.ExistProducto(x =>
             x.Nombre.ToLower() == producto.Nombre.ToLower() || x.CodigoDeBarra.ToLower() == producto.CodigoDeBarra.ToLower()))
            {
                ModelState.AddModelError("Producto.Nombre", "Ya existe una producto con este nombre");
                ModelState.AddModelError("Producto.CodigoDeBarra", "Ya existe una producto con este codigo de barra");
                return View(producto);
            }
            await _productoServices.CreateProducto(producto);
            if (TempData["ExpectedRedirectAction"] != null && TempData["ExpectedRedirectController"] != null)
            {
                return RedirectToAction(actionName: TempData["ExpectedRedirectAction"].ToString(), controllerName: TempData["ExpectedRedirectController"].ToString());
            }

            return RedirectToAction("Lista");
        }
        catch (Exception ex)
        {

            throw;
        }
    
    }

    [HttpGet]
    public async Task<IActionResult> Delete([FromQuery][Required] Guid? id)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("id", "Debe ser valido");
            return RedirectToAction("Lista");
        }
        try
        {
            await _productoServices.DeleteProducto((Guid)id!);
            return RedirectToAction("Lista");
        }
        catch (Exception ex)
        {

            throw;
        }
    }

}
