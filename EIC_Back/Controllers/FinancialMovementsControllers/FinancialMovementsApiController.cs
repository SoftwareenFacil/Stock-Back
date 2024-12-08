using AutoMapper; // Maps objects between different types
using Microsoft.AspNetCore.Mvc; // Provides base classes for controllers
using EIC_Back.BLL.Models.FinancialMovementsModelDTO; // Contains Category DTOs
using EIC_Back.DAL.Context; // Provides access to the database context

namespace EIC_Back.Controllers.FinancialMovementsApiControllers
{
    /// <summary>
    /// API controller for managing FinancialMovements.
    /// </summary>
    [Route("api/[controller]/[action]")] // Route pattern for this controller
    [ApiController]
    public class FinancialMovementsApiController : ControllerBase
    {
        private readonly FinancialMovementsResponseController _controller;
        public FinancialMovementsApiController(AppDbContext dbContext, IMapper mapper)
        {
            _controller = new FinancialMovementsResponseController(dbContext, mapper);
        }

        /// <summary>
        /// Gets a list of FinancialMovements or a single Category by ID.
        /// </summary>
        /// <param name="id">Optional ID of the Category to retrieve.</param>
        /// <param name="pageNumber">Optional page number for pagination (default 1).</param>
        /// <param name="pageSize">Optional page size for pagination (default 10).</param>
        /// <returns>IActionResult containing the list of FinancialMovements or a specific Category.</returns>
        [HttpGet]
        public async Task<IActionResult> GetFinancialMovements(int id, int? pageNumber = 1, int? pageSize = 10)
        {
            return await _controller.GetResponseFinancialMovements(id, pageNumber, pageSize);
        }

        /// <summary>
        /// Inserts a new Category.
        /// </summary>
        /// <param name="FinancialMovements">The FinancialMovementsInsertDTO object containing the Category data.</param>
        /// <returns>IActionResult containing the created Category.</returns>
        [HttpPost]
        public async Task<IActionResult> InsertFinancialMovements([FromBody] FinancialMovementsInsertDTO FinancialMovements)
        {
            return await _controller.Insert(FinancialMovements);
        }

        /// <summary>
        /// Updates an existing Category.
        /// </summary>
        /// <param name="FinancialMovementsEdited">The FinancialMovementsEditDTO object containing the updated Category data.</param>
        /// <returns>IActionResult containing the updated Category.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateFinancialMovements([FromBody] FinancialMovementsEditDTO FinancialMovementsEdited)
        {
            return await _controller.Update(FinancialMovementsEdited);
        }

        /// <summary>
        /// Deletes a Category by ID.
        /// </summary>
        /// <param name="id">The ID of the Category to delete.</param>
        /// <returns>IActionResult indicating success or failure.</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteFinancialMovements(int id)
        {
            return await _controller.Delete(id);
        }
    }
}
