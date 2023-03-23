using AutoMapper;
using BE_Homnayangi.Modules.CategoryModule.Interface;
using BE_Homnayangi.Modules.CategoryModule.Request;
using BE_Homnayangi.Modules.CategoryModule.Response;
using FluentValidation;
using FluentValidation.Results;
using Library.DataAccess;
using Library.Models;
using Library.Models.DTO.CategoryDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreateCategoryRequest = BE_Homnayangi.Modules.CategoryModule.Request.CreateCategoryRequest;
using CreateCategoryRequestValidator = BE_Homnayangi.Modules.CategoryModule.Request.CreateCategoryRequestValidator;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ICategoryService _categoryService;

        public CategoriesController(HomnayangiContext context, IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }


        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return Ok(await _categoryService.GetAll());
        }

        // GET: api/Categories/available
        [HttpGet("dropdown-category")]
        public async Task<ActionResult<IEnumerable<DropdownCategory>>> GetCategoriesAvailable()
        {
            var result = await _categoryService.GetDropdownCategory();
            return new JsonResult(new
            {
                total = result.Count,
                result = result,
            });
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            var category = _categoryService.GetCategoryByID(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> PutCategory(UpdateCategoryRequest category)
        {
            ValidationResult result = new UpdateCategoryRequestValidator().Validate(category);
            if (!result.IsValid)
            {
                return new JsonResult(new
                {
                    status = "failed"
                });
            }

            if (await _categoryService.UpdateCategory(category) == false) return NotFound();

            return Ok();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<ActionResult<Category>> PostCategory(CreateCategoryRequest reqCategory)
        {
            ValidationResult result = new CreateCategoryRequestValidator().Validate(reqCategory);
            if (!result.IsValid)
            {
                return new JsonResult(new
                {
                    status = "failed",
                });
            }

            return Ok(await _categoryService.AddNewCategory(reqCategory));
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _categoryService.DeleteCategory(id);
            return Ok();
        }
    }
}
