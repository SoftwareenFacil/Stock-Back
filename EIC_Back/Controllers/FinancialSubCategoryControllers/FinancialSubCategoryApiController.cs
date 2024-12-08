using AutoMapper; // Maps objects between different types
using Microsoft.AspNetCore.Mvc; // Provides base classes for controllers
using EIC_Back.BLL.Models.FinancialSubCategoryModelDTO; // Contains Category DTOs
using EIC_Back.DAL.Context; // Provides access to the database context

namespace EIC_Back.Controllers.FinancialSubCategoryApiControllers
{
    /// <summary>
    /// API controller for managing FinancialSubCategory.
    /// </summary>
    [Route("api/[controller]/[action]")] // Route pattern for this controller
    [ApiController]
    public class FinancialSubCategoryApiController : ControllerBase
    {
        private readonly FinancialSubcategoryResponseController _responseController;

        public FinancialSubCategoryApiController(AppDbContext dbContext, IMapper mapper)
        {
            _responseController = new FinancialSubcategoryResponseController(dbContext, mapper);
        }

        /// <summary>
        /// Gets a list of FinancialSubCategory or a single Category by ID.
        /// </summary>
        /// <param name="id">Optional ID of the Category to retrieve.</param>
        /// <param name="pageNumber">Optional page number for pagination (default 1).</param>
        /// <param name="pageSize">Optional page size for pagination (default 10).</param>
        /// <returns>IActionResult containing the list of FinancialSubCategory or a specific Category.</returns>
        [HttpGet]
        public async Task<IActionResult> GetFinancialSubCategory(int id, int? categoryid)
        {
            return await _responseController.GetResponseFinancialSubCategory(id, categoryid);
        }

        /// <summary>
        /// Inserts a new Category.
        /// </summary>
        /// <param name="FinancialSubCategory">The FinancialSubCategoryInsertDTO object containing the Category data.</param>
        /// <returns>IActionResult containing the created Category.</returns>
        [HttpPost]
        [SuperAdminRequired]
        public async Task<IActionResult> InsertFinancialSubCategory([FromBody] FinancialSubCategoryInsertDTO FinancialSubCategory)
        {
            return await _responseController.Insert(FinancialSubCategory);
        }

        /// <summary>
        /// Updates an existing Category.
        /// </summary>
        /// <param name="FinancialSubCategoryEdited">The FinancialSubCategoryEditDTO object containing the updated Category data.</param>
        /// <returns>IActionResult containing the updated Category.</returns>
        [HttpPut]
        [SuperAdminRequired]
        public async Task<IActionResult> UpdateFinancialSubCategory([FromBody] FinancialSubCategoryEditDTO FinancialSubCategoryEdited)
        {
            return await _responseController.Update(FinancialSubCategoryEdited);
        }

        /// <summary>
        /// Deletes a Category by ID.
        /// </summary>
        /// <param name="id">The ID of the Category to delete.</param>
        /// <returns>IActionResult indicating success or failure.</returns>
        [HttpDelete]
        [SuperAdminRequired]
        public async Task<IActionResult> DeleteFinancialSubCategory(int id)
        {
            return await _responseController.Delete(id);
        }
    }
}
