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

public class ClienteController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public ClienteController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
    {
        var data = await unitOfWork.Clientes.GetAllAsync();
        return mapper.Map<List<ClienteDto>>(data);
    }

    [HttpGet("{CodigoCliente}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClienteDto>> Get(int codigoCliente)
    {
        var data = await unitOfWork.Clientes.GetByIdAsync(codigoCliente);
        if (data == null)
        {
            return NotFound();
        }
        return mapper.Map<ClienteDto>(data);
    }

    [HttpGet("Pagination")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ClienteDto>>> GetPagination([FromQuery] Params dataParams)
    {
        var datos = await unitOfWork.Clientes.GetAllAsync(dataParams.PageIndex, dataParams.PageSize, dataParams.Search);
        var listData = mapper.Map<List<ClienteDto>>(datos.registros);
        return new Pager<ClienteDto>(listData, datos.totalRegistros, dataParams.PageIndex, dataParams.PageSize, dataParams.Search);
    }



    [HttpGet("Consulta1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get1()
    {
        var data = await unitOfWork.Clientes.Consulta1();
        return mapper.Map<List<object>>(data);
    }
    [HttpGet("Consulta11")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get11()
    {
        var data = await unitOfWork.Clientes.Consulta11();
        return mapper.Map<List<object>>(data);
    }


    [HttpGet("Consulta12Sql2I")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get12I()
    {
        var data = await unitOfWork.Clientes.Consulta12Sql2I();
        return mapper.Map<List<object>>(data);
    }
    [HttpGet("Consulta12Sql2N")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get12N()
    {
        var data = await unitOfWork.Clientes.Consulta12Sql2N();
        return mapper.Map<List<object>>(data);
    }






    [HttpGet("Consulta13Sql2I")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get13I()
    {
        var data = await unitOfWork.Clientes.Consulta13Sql2I();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta13Sql2N")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get13N()
    {
        var data = await unitOfWork.Clientes.Consulta13Sql2N();
        return mapper.Map<List<object>>(data);
    }



    [HttpGet("Consulta14Sql2I")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get14I()
    {
        var data = await unitOfWork.Clientes.Consulta14Sql2I();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta14Sql2N")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get14N()
    {
        var data = await unitOfWork.Clientes.Consulta14Sql2N();
        return mapper.Map<List<object>>(data);
    }





    [HttpGet("Consulta15Sql2I")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get15I()
    {
        var data = await unitOfWork.Clientes.Consulta15Sql2I();
        return mapper.Map<List<object>>(data);
    }



    [HttpGet("Consulta15Sql2N")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get15N()
    {
        var data = await unitOfWork.Clientes.Consulta15Sql2N();
        return mapper.Map<List<object>>(data);
    }



    [HttpGet("Consulta16Sql2I")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get16I()
    {
        var data = await unitOfWork.Clientes.Consulta16Sql2I();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta16Sql2N")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get16N()
    {
        var data = await unitOfWork.Clientes.Consulta16Sql2N();
        return mapper.Map<List<object>>(data);
    }



    [HttpGet("Consulta18Sql2I")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get18I()
    {
        var data = await unitOfWork.Clientes.Consulta18Sql2I();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta18Sql2N")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get18N()
    {
        var data = await unitOfWork.Clientes.Consulta18Sql2N();
        return mapper.Map<List<object>>(data);
    }



    [HttpGet("Consulta19Sql2I")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get19I()
    {
        var data = await unitOfWork.Clientes.Consulta19Sql2I();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta19Sql2N")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get19N()
    {
        var data = await unitOfWork.Clientes.Consulta19Sql2N();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta20Left")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get20Left()
    {
        var data = await unitOfWork.Clientes.Consulta20Left();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta20Right")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get20Rigth()
    {
        var data = await unitOfWork.Clientes.Consulta20Right();
        return mapper.Map<List<object>>(data);
    }





    [HttpGet("Consulta21Left")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get21Left()
    {
        var data = await unitOfWork.Clientes.Consulta21Left();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta21Right")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get21Rigth()
    {
        var data = await unitOfWork.Clientes.Consulta21Right();
        return mapper.Map<List<object>>(data);
    }





    [HttpGet("Consulta22Left")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get22Left()
    {
        var data = await unitOfWork.Clientes.Consulta22Left();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta22Right")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get22Rigth()
    {
        var data = await unitOfWork.Clientes.Consulta22Right();
        return mapper.Map<List<object>>(data);
    }





    [HttpGet("Consulta27Left")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get27Left()
    {
        var data = await unitOfWork.Clientes.Consulta27Left();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta27Right")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get27Rigth()
    {
        var data = await unitOfWork.Clientes.Consulta27Right();
        return mapper.Map<List<object>>(data);
    }


    [HttpGet("Consulta33")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<int> GetConsulta33()
    {

        var cantidad = await unitOfWork.Clientes.Consulta33();
        return cantidad;
    }

    [HttpGet("Consulta34")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get34()
    {
        var data = await unitOfWork.Clientes.Consulta34();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta36")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<int> GetConsulta36()
    {

        var cantidad = await unitOfWork.Clientes.Consulta36();
        return cantidad;
    }

    [HttpGet("Consulta37")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get37()
    {
        var data = await unitOfWork.Clientes.Consulta37();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta45")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<string> GetConsulta45()
    {

        var data = await unitOfWork.Clientes.Consulta45();
        return data;
    }

    [HttpGet("Consulta48")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetConsulta48()
    {
        var data = await unitOfWork.Clientes.Consulta48();
        return mapper.Map<List<object>>(data);
    }
    [HttpGet("Consulta49")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<string> GetConsulta49()
    {

        var data = await unitOfWork.Clientes.Consulta49();
        return data;
    }

    [HttpGet("Consulta51")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetConsulta51()
    {
        var data = await unitOfWork.Clientes.Consulta51();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta52")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetConsulta52()
    {
        var data = await unitOfWork.Clientes.Consulta52();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta55")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetConsulta55()
    {
        var data = await unitOfWork.Clientes.Consulta55();
        return mapper.Map<List<object>>(data);
    }
    [HttpGet("Consulta56")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetConsulta56()
    {
        var data = await unitOfWork.Clientes.Consulta56();
        return mapper.Map<List<object>>(data);
    }
    [HttpGet("Consulta57")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetConsulta57()
    {
        var data = await unitOfWork.Clientes.Consulta57();
        return mapper.Map<List<object>>(data);
    }
    [HttpGet("Consulta58")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetConsulta58()
    {
        var data = await unitOfWork.Clientes.Consulta58();
        return mapper.Map<List<object>>(data);
    }
    [HttpGet("Consulta59")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetConsulta59()
    {
        var data = await unitOfWork.Clientes.Consulta59();
        return mapper.Map<List<object>>(data);
    }
    [HttpGet("Consulta60")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetConsulta60()
    {
        var data = await unitOfWork.Clientes.Consulta60();
        return mapper.Map<List<object>>(data);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClienteDto>> Post(ClienteDto dataDto)
    {
        var data = mapper.Map<Cliente>(dataDto);
        unitOfWork.Clientes.Add(data);
        await unitOfWork.SaveAsync();
        if (data == null)
        {
            return BadRequest();
        }
        dataDto.CodigoCliente = data.CodigoCliente;
        return CreatedAtAction(nameof(Post), new { codigoCliente = dataDto.CodigoCliente }, dataDto);
    }

    [HttpPut("{codigoCliente}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteDto>> Put(int codigoCliente, [FromBody] ClienteDto dataDto)
    {
        if (dataDto == null)
        {
            return NotFound();
        }
        var data = mapper.Map<Cliente>(dataDto);
        unitOfWork.Clientes.Update(data);
        await unitOfWork.SaveAsync();
        return dataDto;
    }

    [HttpDelete("{codigoCliente}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int codigoCliente)
    {
        var data = await unitOfWork.Clientes.GetByIdAsync(codigoCliente);
        if (data == null)
        {
            return NotFound();
        }
        unitOfWork.Clientes.Remove(data);
        await unitOfWork.SaveAsync();
        return NoContent();
    }




}