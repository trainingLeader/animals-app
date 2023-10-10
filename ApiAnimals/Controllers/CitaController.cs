using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAnimals.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiAnimals.Controllers
{
    public class CitaController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CitaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CitaDto>>>Get()
        {
            var citas = await _unitOfWork.Citas.GetAllAsync();
            return _mapper.Map<List<CitaDto>>(citas);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CitaDto>>Get(int id)
        {
            var citas = await _unitOfWork.Citas.GetByIdAsync(id);
            if(citas == null)
            {
                return NotFound();
            }
            return _mapper.Map<CitaDto>(citas);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CitaDto>>Post(CitaDto citaDto)
        {
            var cita = _mapper.Map<Cita>(citaDto);

            // if (citaDto.Fecha == DateTime.MinValue)
            // {
            //     citaDto.Fecha = DateTime.Now; 
            // }
            this._unitOfWork.Citas.Add(cita);
            await _unitOfWork.SaveAsync();
            
            if(cita == null)
            {
                return BadRequest();
            }
            citaDto.Id = cita.Id;
            return CreatedAtAction(nameof(Post), new {id = citaDto.Id}, citaDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CitaDto>> Put(int id, [FromBody] CitaDto citaDto)
        {
            if(citaDto == null)
            {
                return NotFound();
            }
            var citas = _mapper.Map<Cita>(citaDto);
            _unitOfWork.Citas.Update(citas);
            await _unitOfWork.SaveAsync();
            return citaDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>Delete(int id)
        {
            var cita = await _unitOfWork.Citas.GetByIdAsync(id);
            if(cita == null)
            {
                return NotFound();
            }
            _unitOfWork.Citas.Remove(cita);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

    }
}