using BE_Homnayangi.Modules.SubCateModule.Interface;
using BE_Homnayangi.Modules.SubCateModule.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ISubCateService _subCateService;

        public SubCategoriesController(ISubCateService subCateService)
        {
            _subCateService = subCateService;
        }

        // GET: api/SubCategories/5
        [HttpGet("{cateId}")]
        public async Task<ActionResult<SubCateResponse>> GetSubCategoryByCateId(Guid cateId)
        {
            var subCategories = await _subCateService.GetSubCatesByCategoryId(cateId);

            return new JsonResult(new
            {
                total = subCategories.Count,
                result = subCategories
            });
        }
    }
}
