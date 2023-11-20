using API.Dtos;
using API.Helpers.Errors;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]
//[Authorize]

public class ProductoController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public ProductoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
    {
        var data = await unitOfWork.Productos.GetAllAsync();
        return mapper.Map<List<ProductoDto>>(data);
    }

    [HttpGet("{codigoProducto}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductoDto>> Get(string codigoProducto)
    {
        var data = await unitOfWork.Productos.GetByIdAsync(codigoProducto);
        if (data == null)
        {
            return NotFound();
        }
        return mapper.Map<ProductoDto>(data);
    }
    [HttpGet("Consulta10")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get10()
    {
        var data = await unitOfWork.Productos.Consulta10();
        return mapper.Map<List<object>>(data);
    }



    [HttpGet("Consulta24Left")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get24Left()
    {
        var data = await unitOfWork.Productos.Consulta24Left();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta24Right")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get24Rigth()
    {
        var data = await unitOfWork.Productos.Consulta24Right();
        return mapper.Map<List<object>>(data);
    }


    [HttpGet("Consulta24NaturalLeft")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get24NaturalLeft()
    {
        var data = await unitOfWork.Productos.Consulta24NaturalLeft();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta24NaturalRight")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get24NaturaRight()
    {
        var data = await unitOfWork.Productos.Consulta24NaturalRight();
        return mapper.Map<List<object>>(data);
    }





    [HttpGet("Consulta25Left")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get25Left()
    {
        var data = await unitOfWork.Productos.Consulta25Left();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta25Right")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get25Rigth()
    {
        var data = await unitOfWork.Productos.Consulta25Right();
        return mapper.Map<List<object>>(data);
    }


    [HttpGet("Consulta25NaturalLeft")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get25NaturalLeft()
    {
        var data = await unitOfWork.Productos.Consulta25NaturalLeft();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta25NaturalRight")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get25NaturaRight()
    {
        var data = await unitOfWork.Productos.Consulta25NaturalRight();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta46")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<string> GetConsulta46()
    {

        var data = await unitOfWork.Productos.Consulta46();
        return data;
    }

    [HttpGet("Consulta47")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<string> GetConsulta47()
    {

        var data = await unitOfWork.Productos.Consulta47();
        return data;
    }
    [HttpGet("Consulta50")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<string> GetConsulta50()
    {

        var data = await unitOfWork.Productos.Consulta50();
        return data;
    }

    [HttpGet("Consulta53")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<Producto>> Get53()
    {
        var data = await unitOfWork.Productos.Consulta53();
        return mapper.Map<List<Producto>>(data);
    }
    [HttpGet("Pagination")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ProductoDto>>> GetPagination([FromQuery] Params dataParams)
    {
        var datos = await unitOfWork.Productos.GetAllAsync(dataParams.PageIndex, dataParams.PageSize, dataParams.Search);
        var listData = mapper.Map<List<ProductoDto>>(datos.registros);
        return new Pager<ProductoDto>(listData, datos.totalRegistros, dataParams.PageIndex, dataParams.PageSize, dataParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductoDto>> Post(ProductoDto dataDto)
    {
        var data = mapper.Map<Producto>(dataDto);
        unitOfWork.Productos.Add(data);
        await unitOfWork.SaveAsync();
        if (data == null)
        {
            return BadRequest();
        }
        dataDto.CodigoProducto = data.CodigoProducto;
        return CreatedAtAction(nameof(Post), new { codigoProducto = dataDto.CodigoProducto }, dataDto);
    }

    [HttpPut("{codigoProducto}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductoDto>> Put(string codigoProducto, [FromBody] ProductoDto dataDto)
    {
        if (dataDto == null)
        {
            return NotFound();
        }
        var data = mapper.Map<Producto>(dataDto);
        unitOfWork.Productos.Update(data);
        await unitOfWork.SaveAsync();
        return dataDto;
    }

    [HttpDelete("{codigoProducto}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string codigoProducto)
    {
        var data = await unitOfWork.Productos.GetByIdAsync(codigoProducto);
        if (data == null)
        {
            return NotFound();
        }
        unitOfWork.Productos.Remove(data);
        await unitOfWork.SaveAsync();
        return NoContent();
    }
}