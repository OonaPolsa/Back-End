using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Middleware;
using ReservationSystem.Models;
using ReservationSystem.Services;

namespace ReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        //private readonly ReservationContext _context;
        private readonly IItemService _service;
        private readonly IUserAuthenticationService _authenticationService;
        private readonly ReservationContext _context;

        public ItemsController(IItemService service, IUserAuthenticationService authenticationService, ReservationContext context)
        {
            _service = service;
            _authenticationService = authenticationService;
            _context = context;
        }
        
        
        /* // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            // return await _context.Items.ToListAsync();
            return null;
        }
       */

        
        // GET: api/Items
        /// <summary>
        /// Gets a list of all items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems()
        {
            return Ok(await _service.GetAllItems());
        }
        
        
        // GET: api/Items/5
        /// <summary>
        /// Gets one item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems(long id)
        {
            
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            //return item;
            
            return Ok(await _service.GetAllItems());
        }

        
        // GET: api/Items/Username
        /// <summary>
        /// Gets items by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("user/{username}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems(String username)
        {

            return Ok(await _service.GetItems(username));

        }
        
        
        // GET: api/Items/query
        /// <summary>
        /// Returns all items
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("{query}")]
        [Authorize]

        public async Task<ActionResult<IEnumerable<ItemDTO>>> QueryItems(String query)
        {
            return Ok(await _service.QueryItems(query));
        }
           

        // PUT: api/Items/5
        /// <summary>
        /// Edit item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(long id, Item item)
        {
            // if (id != item.Id)
            // {
            //     return BadRequest();
            //  }

            //  _context.Entry(item).State = EntityState.Modified;

            // try
            //{
            //     await _context.SaveChangesAsync();
            //}
            // catch (DbUpdateConcurrencyException)
            // {
            //    if (!ItemExists(id))
            //    {
            //    return NotFound();
            // }
            //  else
            //  {
            //      throw;
            //  }
            // }

            // return NoContent();
            return null;
        }

        // POST: api/Items
        /// <summary>
        /// Creates a new item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(ItemDTO item)
        {
           //Oikeus muokata?
            bool isAllowed = await _authenticationService.IsAllowed(this.User.FindFirst(ClaimTypes.Name).Value, item);
            if (!isAllowed)
            {
                return Unauthorized();
            }

            ItemDTO newItem = await _service.CreateItemAsync(item);

            if (newItem != null)
            {
                return CreatedAtAction("GetItem", new { id = newItem.Id }, newItem);
            }
            return StatusCode(500);
        }

        // DELETE: api/Items/5
        /// <summary>
        /// Deletes selected item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> DeleteItem(long id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
             {
                return NotFound();
            }

             _context.Items.Remove(item);
             await _context.SaveChangesAsync();

             return item;
            //return null;
        }
    }
}
