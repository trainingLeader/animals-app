using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAnimals.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiAnimals.Controllers;
public class DepartamentoController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DepartamentoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DepartamentoDto>>> get()
    {
        var departs = await _unitOfWork.Departamentos.GetAllAsync();
        return _mapper.Map<List<DepartamentoDto>>(departs);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DepartamentoDto>> Get(int id)
    {
        var depart = await _unitOfWork.Departamentos.GetByIdAsync(id);
        if (depart == null)
        {
            return NotFound();
        }
        return _mapper.Map<DepartamentoDto>(depart);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Departamento>> Post(DepartamentoDto departamentoDto)
    {
        var depar = _mapper.Map<Departamento>(departamentoDto);
        this._unitOfWork.Departamentos.Add(depar);
        await _unitOfWork.SaveAsync();
        if (depar == null)
        {
            return BadRequest();
        }
        departamentoDto.Id = depar.Id;
        return CreatedAtAction(nameof(Post), new {id = departamentoDto.Id}, departamentoDto);
    } 

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DepartamentoDto>> Put(int id, [FromBody] DepartamentoDto departamentoDto)
    {

        var depart = _mapper.Map<Departamento>(departamentoDto);
        if (depart == null)
        {
            return NotFound();
        }    
        /* DepartamentoDto.Id = departamentoDto.Id == 0 ? id : BadRequest();  */
        if (depart.Id == 0)
        {
            depart.Id = id;
        }
        
        if (depart.Id != id)
        {
            return BadRequest();
        }
        departamentoDto.Id = depart.Id;
        _unitOfWork.Departamentos.Update(depart);
        await _unitOfWork.SaveAsync();
        return departamentoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var depart = await _unitOfWork.Departamentos.GetByIdAsync(id);
        if (depart == null)
        {
            return NotFound();
        }

        _unitOfWork.Departamentos.Remove(depart);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

}

