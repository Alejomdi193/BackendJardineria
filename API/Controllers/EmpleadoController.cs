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

public class EmpleadoController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public EmpleadoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmpleadoDto>>> Get()
    {
        var data = await unitOfWork.Empleados.GetAllAsync();
        return mapper.Map<List<EmpleadoDto>>(data);
    }

    [HttpGet("{codigoEmpleado}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmpleadoDto>> Get(int codigoEmpleado)
    {
        var data = await unitOfWork.Empleados.GetByIdAsync(codigoEmpleado);
        if (data == null)
        {
            return NotFound();
        }
        return mapper.Map<EmpleadoDto>(data);
    }
    [HttpGet("Consulta17")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get17()
    {
        var data = await unitOfWork.Empleados.Consulta17();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta17Sql2I")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get17I()
    {
        var data = await unitOfWork.Empleados.Consulta17Sql2I();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta17Sql2N")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get17N()
    {
        var data = await unitOfWork.Empleados.Consulta17Sql2N();
        return mapper.Map<List<object>>(data);
    }


    [HttpGet("Consulta23Left")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get23Left()
    {
        var data = await unitOfWork.Empleados.Consulta23Left();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta23Right")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get23Rigth()
    {
        var data = await unitOfWork.Empleados.Consulta23Right();
        return mapper.Map<List<object>>(data);
    }


    [HttpGet("Consulta23NaturalLeft")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get23NaturalLeft()
    {
        var data = await unitOfWork.Empleados.Consulta23NaturalLeft();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta23NaturalRight")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get23NaturaRight()
    {
        var data = await unitOfWork.Empleados.Consulta23NaturalRight();
        return mapper.Map<List<object>>(data);
    }


    [HttpGet("Consulta26Right")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get26Rigth()
    {
        var data = await unitOfWork.Empleados.Consulta26Right();
        return mapper.Map<List<object>>(data);
    }


    [HttpGet("Consulta26NaturalRight")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get26NaturaRight()
    {
        var data = await unitOfWork.Empleados.Consulta26NaturalRight();
        return mapper.Map<List<object>>(data);
    }



    [HttpGet("Consulta28Left")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get28Left()
    {
        var data = await unitOfWork.Empleados.Consulta28Left();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta28Right")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get28Rigth()
    {
        var data = await unitOfWork.Empleados.Consulta28Right();
        return mapper.Map<List<object>>(data);
    }


    [HttpGet("Consulta28NaturalLeft")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get28NaturalLeft()
    {
        var data = await unitOfWork.Empleados.Consulta28NaturalLeft();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta28NaturalRight")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get28NaturaRight()
    {
        var data = await unitOfWork.Empleados.Consulta28NaturalRight();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta29")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<int> GetConsulta29()
    {

        var cantidadEmpleados = await unitOfWork.Empleados.Consulta29();
        return cantidadEmpleados;
    }

    [HttpGet("Consulta30")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get30()
    {
        var data = await unitOfWork.Empleados.Consulta30();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta35")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get35()
    {
        var data = await unitOfWork.Empleados.Consulta35();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta54")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get54()
    {
        var data = await unitOfWork.Empleados.Consulta54();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Consulta61")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> Get61()
    {
        var data = await unitOfWork.Empleados.Consulta61();
        return mapper.Map<List<object>>(data);
    }

    [HttpGet("Pagination")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<EmpleadoDto>>> GetPagination([FromQuery] Params dataParams)
    {
        var datos = await unitOfWork.Empleados.GetAllAsync(dataParams.PageIndex, dataParams.PageSize, dataParams.Search);
        var listData = mapper.Map<List<EmpleadoDto>>(datos.registros);
        return new Pager<EmpleadoDto>(listData, datos.totalRegistros, dataParams.PageIndex, dataParams.PageSize, dataParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmpleadoDto>> Post(EmpleadoDto dataDto)
    {
        var data = mapper.Map<Empleado>(dataDto);
        unitOfWork.Empleados.Add(data);
        await unitOfWork.SaveAsync();
        if (data == null)
        {
            return BadRequest();
        }
        dataDto.CodigoEmpleado = data.CodigoEmpleado;
        return CreatedAtAction(nameof(Post), new { codigoEmpleado = dataDto.CodigoEmpleado }, dataDto);
    }

    [HttpPut("{codigoEmpleado}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmpleadoDto>> Put(int codigoEmpleado, [FromBody] EmpleadoDto dataDto)
    {
        if (dataDto == null)
        {
            return NotFound();
        }
        var data = mapper.Map<Empleado>(dataDto);
        unitOfWork.Empleados.Update(data);
        await unitOfWork.SaveAsync();
        return dataDto;
    }

    [HttpDelete("{codigoEmpleado}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int codigoEmpleado)
    {
        var data = await unitOfWork.Empleados.GetByIdAsync(codigoEmpleado);
        if (data == null)
        {
            return NotFound();
        }
        unitOfWork.Empleados.Remove(data);
        await unitOfWork.SaveAsync();
        return NoContent();
    }
}