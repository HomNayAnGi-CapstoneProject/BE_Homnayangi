using BE_Homnayangi.Modules.SubCateModule.Interface;
using BE_Homnayangi.Modules.SubCateModule.Request;
using BE_Homnayangi.Modules.SubCateModule.Response;
using FluentValidation.Results;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ISubCateService _subCateService;

        public SubCategoriesController(ISubCateService subCateService)
        {
            _subCateService = subCateService;
        }

        // GET: api/Categories/5/SubCategories
        [HttpGet("/categories/{cateId}/sub-categories")]
        public async Task<ActionResult<SubCateResponse>> GetSubCategoriesByCateId(Guid cateId)
        {
            var subCategories = await _subCateService.GetSubCatesByCategoryId(cateId);

            return new JsonResult(new
            {
                total = subCategories.Count,
                result = subCategories
            });
        }
        // GET: api/Categories/5/SubCategories
        [HttpGet("/categories/{cateId}/sub-categories/staff")]
        public async Task<ActionResult<SubCateResponse>> GetSubCategoriesByCateIdForStaff(Guid cateId)
        {
            var subCategories = await _subCateService.GetSubCatesByCategoryIdForStaff(cateId);

            return new JsonResult(new
            {
                total = subCategories.Count,
                result = subCategories
            });
        }
        // GET: api/SubCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategory>>> GetSubCategories()
        {
            return Ok(await _subCateService.GetAll());
        }

        // GET: api/SubCategories
        [HttpGet("staff")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<ActionResult<IEnumerable<SubCategory>>> GetSubCategoriesForStaff()
        {
            return Ok(await _subCateService.GetAllForStaff());
        }

        // GET: api/SubCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategory>> GetSubCategory(Guid id)
        {
            var subCategory = _subCateService.GetSubCateByID(id);

            if (subCategory == null)
            {
                return NotFound();
            }

            return subCategory;
        }

        // GET: api/SubCategories/5
        [HttpGet("{id}/staff")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<ActionResult<SubCategory>> GetSubCategoryForStaff(Guid id)
        {
            var subCategory = _subCateService.GetSubCateByIDForStaff(id);

            if (subCategory == null)
            {
                return NotFound();
            }

            return subCategory;
        }

        // PUT: api/SubCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> PutSubCategory(UpdateSubCategoryRequest subCategoryUpdate)
        {
            ValidationResult result = new UpdateSubCategoryRequestValidator().Validate(subCategoryUpdate);
            if (!result.IsValid)
            {
                return BadRequest();
            }

            if (await _subCateService.UpdateSubCate(subCategoryUpdate) == false)
            {
                return NotFound();
            }

            return Ok();
        }

        // POST: api/SubCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<ActionResult<SubCategory>> PostSubCategory(CreateSubCategoryRequest reqSubCategory)
        {
            ValidationResult result = new CreateSubCategoryRequestValidator().Validate(reqSubCategory);
            if (!result.IsValid)
            {
                return BadRequest();
            }

            if (await _subCateService.AddNewSubCate(reqSubCategory) == false)
            {
                return NotFound();
            }

            return Ok();
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _subCateService.DeleteSubCate(id);
            return Ok();
        }
    }
}
