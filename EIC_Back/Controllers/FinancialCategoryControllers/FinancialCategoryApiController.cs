using AutoMapper; // Maps objects between different types
using Microsoft.AspNetCore.Mvc; // Provides base classes for controllers
using EIC_Back.BLL.Models.FinancialCategoryModelDTO; // Contains Category DTOs
using EIC_Back.DAL.Context;
using EIC_Back.Models; // Provides access to the database context

namespace EIC_Back.Controllers.FinancialCategoryApiControllers
{
    /// <summary>
    /// API controller for managing FinancialCategory.
    /// </summary>
    [Route("api/[controller]/[action]")] // Route pattern for this controller
    [ApiController]
    public class FinancialCategoryApiController : ControllerBase
    {
        private readonly FinancialCategoryResponseController _financialCategoryResponseController;
        public FinancialCategoryApiController(AppDbContext dbContext, IMapper mapper)
        {
            _financialCategoryResponseController = new FinancialCategoryResponseController(dbContext, mapper);
        }

        /// <summary>
        /// Gets a list of FinancialCategory or a single Category by ID.
        /// </summary>
        /// <param name="id">Optional ID of the Category to retrieve.</param>
        /// <param name="pageNumber">Optional page number for pagination (default 1).</param>
        /// <param name="pageSize">Optional page size for pagination (default 10).</param>
        /// <returns>IActionResult containing the list of FinancialCategory or a specific Category.</returns>
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<FinancialCategoryDTO>>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetFinancialCategory(int id, int? pageNumber = 1, int? pageSize = 10)
        {
            return await _financialCategoryResponseController.GetResponseFinancialCategory(id, pageNumber, pageSize);
        }
        /// <summary>
        /// Gets a list of FinancialCategoryFullDTO or a single CategoryFull by ID.
        /// </summary>
        /// <param name="id">Optional ID of the Category to retrieve.</param>
        /// <param name="pageNumber">Optional page number for pagination (default 1).</param>
        /// <param name="pageSize">Optional page size for pagination (default 10).</param>
        /// <returns>IActionResult containing the list of FinancialCategoryFullDTO or a specific Category.</returns>
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<FinancialCategoryFullDTO>>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetFullFinancialCategory(int id, int? pageNumber = 1, int? pageSize = 10)
        {
            return await _financialCategoryResponseController.GetFullResponseFinancialCategory(id, pageNumber, pageSize);
        }
        /// <summary>
        /// Inserts a new Category.
        /// </summary>
        /// <param name="FinancialCategory">The FinancialCategoryInsertDTO object containing the Category data.</param>
        /// <returns>IActionResult containing the created Category.</returns>
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<FinancialCategoryDTO>>), 200)]
        [HttpPost]
        [SuperAdminRequired]
        public async Task<IActionResult> InsertFinancialCategory([FromBody] FinancialCategoryInsertDTO FinancialCategory)
        {
            return await _financialCategoryResponseController.Insert(FinancialCategory);
        }

        /// <summary>
        /// Updates an existing Category.
        /// </summary>
        /// <param name="FinancialCategoryEdited">The FinancialCategoryEditDTO object containing the updated Category data.</param>
        /// <returns>IActionResult containing the updated Category.</returns>
        [HttpPut]
        [SuperAdminRequired]
        public async Task<IActionResult> UpdateFinancialCategory([FromBody] FinancialCategoryEditDTO FinancialCategoryEdited)
        {
            return await _financialCategoryResponseController.Update(FinancialCategoryEdited);
        }

        /// <summary>
        /// Deletes a Category by ID.
        /// </summary>
        /// <param name="id">The ID of the Category to delete.</param>
        /// <returns>IActionResult indicating success or failure.</returns>
        [HttpDelete]
        [SuperAdminRequired]
        public async Task<IActionResult> DeleteFinancialCategory(int id)
        {
            return await _financialCategoryResponseController.Delete(id);
        }

    }
}
