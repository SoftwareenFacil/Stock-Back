using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock_Back.Models;
using Stock_Back.ModelsDTO;

namespace Stock_Back.Controllers
{
    [Route("api/People")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly Context _context;

        public PeopleController(Context context)
        {
            _context = context;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetTodoItems()
        {
            return await _context.Personas
                .Select(x => ToDTO(x))
                .ToListAsync();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDTO>> GetPerson(long id)
        {
            if (_context.Personas == null)
            {
                return NotFound();
            }
            var person = await _context.Personas.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return ToDTO(person);
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(long id, PersonDTO personDTO)
        {
            if (id != personDTO.Id)
            {
                return BadRequest();
            }
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            persona.Name = personDTO.Name;
            persona.Age = personDTO.Age;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PersonExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PersonDTO>> PostPerson(PersonDTO personDTO)
        {
            var persona = new Person
            {
                Name = personDTO.Name,
                Age = personDTO.Age
            };

            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
            nameof(GetPerson),
            new { id = persona.Id },
            ToDTO(persona));

        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(long id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(long id)
        {
            return (_context.Personas?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static PersonDTO ToDTO(Person persona) =>
           new PersonDTO
           {
               Id = persona.Id,
               Name = persona.Name,
               Age = persona.Age
           };
    }
}
