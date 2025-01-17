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

public class PedidoController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public PedidoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PedidoDto>>> Get()
    {
        var data = await unitOfWork.Pedidos.GetAllAsync();
        return mapper.Map<List<PedidoDto>>(data);
    }

    [HttpGet("{codigoPedido}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int codigoPedido)
    {
        var data = await unitOfWork.Pedidos.GetByIdAsync(codigoPedido);
        return Ok(data);
    }

    [HttpGet("Pagination")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PedidoDto>>> GetPagination([FromQuery] Params dataParams)
    {
        var datos = await unitOfWork.Pedidos.GetAllAsync(dataParams.PageIndex, dataParams.PageSize, dataParams.Search);
        var listData = mapper.Map<List<PedidoDto>>(datos.registros);
        return new Pager<PedidoDto>(listData, datos.totalRegistros, dataParams.PageIndex, dataParams.PageSize, dataParams.Search);
    }

    [HttpGet("Consulta2")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get2()
    {
        var data = await unitOfWork.Pedidos.Consulta2();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta4")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get4()
    {
        var data = await unitOfWork.Pedidos.Consulta4();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta5")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get5()
    {
        var data = await unitOfWork.Pedidos.Consulta5();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta6")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get6()
    {
        var data = await unitOfWork.Pedidos.Consulta6();
        return mapper.Map<List<object>>(data);
    }


    [HttpGet("Consulta7")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get7()
    {
        var data = await unitOfWork.Pedidos.Consulta7();
        return mapper.Map<List<object>>(data);
    }
    [HttpGet("Consulta32")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get32()
    {
        var data = await unitOfWork.Pedidos.Consulta32();
        return mapper.Map<List<object>>(data);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PedidoDto>> Post(PedidoDto dataDto)
    {
        var data = mapper.Map<Pedido>(dataDto);
        unitOfWork.Pedidos.Add(data);
        await unitOfWork.SaveAsync();
        if (data == null)
        {
            return BadRequest();
        }
        dataDto.CodigoPedido = data.CodigoPedido;
        return CreatedAtAction(nameof(Post), new { codigoPedido = dataDto.CodigoPedido }, dataDto);
    }

    [HttpPut("{codigoPedido}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PedidoDto>> Put(int codigoPedido, [FromBody] PedidoDto dataDto)
    {
        if (dataDto == null)
        {
            return NotFound();
        }
        var data = mapper.Map<Pedido>(dataDto);
        unitOfWork.Pedidos.Update(data);
        await unitOfWork.SaveAsync();
        return dataDto;
    }

    [HttpDelete("{codigoPedido}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int codigoPedido)
    {
        var data = await unitOfWork.Pedidos.GetByIdAsync(codigoPedido);
        if (data == null)
        {
            return NotFound();
        }
        unitOfWork.Pedidos.Remove(data);
        await unitOfWork.SaveAsync();
        return NoContent();
    }


}