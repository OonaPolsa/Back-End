﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Models;
using ReservationSystem.Services;

namespace ReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        //private readonly ReservationContext _context;
        private readonly IReservationService _service;

        public ReservationsController(IReservationService service)
        {
            _service = service;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            //return await _context.Reservations.ToListAsync();
            return null;
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(long id)
        {
            //var reservation = await _context.Reservations.FindAsync(id);

            //if (reservation == null)
            //{
            //   return NotFound();
            //}

            // return reservation;
            return null;
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(long id, Reservation reservation)
        {
            //if (id != reservation.Id)
            //{
             //   return BadRequest();
            //}

           // _context.Entry(reservation).State = EntityState.Modified;

            //try
           // {
            //    await _context.SaveChangesAsync();
           // }
           // catch (DbUpdateConcurrencyException)
           // {
            //    if (!ReservationExists(id))
            //    {
                 //   return NotFound();
               // }
               // else
              //  {
              //      throw;
             //   }
            //}

            return NoContent();
        }

        // POST: api/Reservations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            
            await _service.CreateReservationAsync(reservation);

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
            
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reservation>> DeleteReservation(long id)
        {
            // var reservation = await _context.Reservations.FindAsync(id);
            // if (reservation == null)
            // {
            //     return NotFound();
            // }

            //  _context.Reservations.Remove(reservation);
            // await _context.SaveChangesAsync();

            // return reservation;
            return null;
        }
    }
}
