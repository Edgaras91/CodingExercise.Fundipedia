using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fundipedia.TechnicalInterview.Api.Models.Requests;
using Fundipedia.TechnicalInterview.Api.Models.Response;
using Fundipedia.TechnicalInterview.Domain;
using Fundipedia.TechnicalInterview.Mappers;
using Fundipedia.TechnicalInterview.Model.Supplier;
using Microsoft.AspNetCore.Mvc;

namespace Fundipedia.TechnicalInterview.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuppliersController : ControllerBase
{
    private readonly SupplierService _supplierService;

    public SuppliersController(SupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    // GET: api/Suppliers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupplierResponse>>> GetSupplier()
    {
        var suppliers = await _supplierService.GetSuppliers();
        return suppliers.Select(supplier => supplier.ToResponseDto()).ToList();
    }

    // GET: api/Suppliers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierResponse>> GetSupplier(Guid id)
    {
        var supplier = await _supplierService.GetSupplier(id);

        if (supplier == null)
        {
            return NotFound();
        }

        return supplier.ToResponseDto();
    }

    // POST: api/Suppliers
    [HttpPost]
    public async Task<ActionResult<Supplier>> PostSupplier(SupplierRequest supplierRequest)
    {
        //For reviewer: Model validation is in the middleware.

        await _supplierService.InsertSupplier(supplierRequest.ToDomainDto());

        return CreatedAtAction("GetSupplier", new {id = supplierRequest.Id}, supplierRequest);
    }

    // DELETE: api/Suppliers/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Supplier>> DeleteSupplier(Guid id)
    {
        var supplier = await _supplierService.DeleteSupplier(id);
        return supplier;
    }
}