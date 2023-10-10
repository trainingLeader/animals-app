using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAnimals.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiAnimals.Controllers;

public class CiudadController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CiudadController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<CiudadDto>>> get()
    {
        var cities = await _unitOfWork.Ciudades.GetAllAsync();
        return _mapper.Map<List<CiudadDto>>(cities);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<CiudadDto>> Get(int id)
    {
        var city = await _unitOfWork.Ciudades.GetByIdAsync(id);
        if (city == null)
        {
            return NotFound();
        }
        return _mapper.Map<CiudadDto>(city);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<CiudadDto>> Post(CiudadDto ciudadDto)
    {
        var ciudad = _mapper.Map<Ciudad>(ciudadDto);
        this._unitOfWork.Ciudades.Add(ciudad);
        await _unitOfWork.SaveAsync();

        if (ciudad == null)
        {
            return BadRequest();
        }

        ciudadDto.Id = ciudad.Id;
        return CreatedAtAction(nameof(Post), new { id = ciudadDto.Id}, ciudadDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<CiudadDto>> Put(int id, [FromBody] CiudadDto ciudadDto)
    {
        var ciud = _mapper.Map<Ciudad>(ciudadDto);
        if (ciud == null)
        {
            return NotFound();
        }

        if(ciud.Id == 0)
        {
            ciud.Id = id;
        }

        if(ciud.Id != id)
        {
            return BadRequest();
        }

        ciudadDto.Id = ciud.Id;
        _unitOfWork.Ciudades.Update(ciud);
        await _unitOfWork.SaveAsync();
        return ciudadDto;
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult> Delete(int id)
    {
        var ciuda = await _unitOfWork.Ciudades.GetByIdAsync(id);
        
        if (ciuda == null)
        {
            return NotFound();
        }

        _unitOfWork.Ciudades.Remove(ciuda);
        await _unitOfWork.SaveAsync();
        return NoContent();

    }
}
