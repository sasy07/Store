using Application.Dtos.Products;
using Application.Features.Products.Queries.Get;
using Application.Features.Products.Queries.GetAll;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

namespace Web.Controllers;

public class ProductController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]GetAllProductsQuery request , CancellationToken cancellationToken)
        => Ok(await Mediator.Send(request , cancellationToken));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> Get([FromRoute] int id, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(new GetProductQuery(id), cancellationToken));
}