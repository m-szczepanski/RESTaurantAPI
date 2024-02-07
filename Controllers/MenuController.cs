using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTaurantAPI.DTOs;
using RESTaurantAPI.Services;


namespace RESTaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MenuController : Controller
    {
        private readonly IMapper _mapper;
        private readonly MenuService _menuService;

        public MenuController(MenuService menuService, IMapper _mapper)
        {
            this._mapper = _mapper;
            this._menuService = menuService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<MenuDto>>> GetAll(
            CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var menus = await this._menuService.GetAll(cancellationToken);
            var menusDto = this._mapper.Map<List<MenuDto>>(menus);

            if (skip.HasValue)
            {
                menusDto = menusDto.Skip(skip.Value).ToList();
            }

            if (limit.HasValue)
            {
                menusDto = menusDto.Take(limit.Value).ToList();
            }

            return Ok(menusDto);
        }

        [HttpPost("Add")]
        public async Task<ActionResult<MenuDto>> AddMenu(List<int> dishIds, string startDateString, string endDateString)
        {
            var menu = await _menuService.AddMenu(dishIds, startDateString, endDateString);

            var menuDto = _mapper.Map<MenuDto>(menu);

            return Ok(menuDto);
        }

        [HttpPut("UpdateStartDate/{id}")]
        public async Task<ActionResult> UpdateStartDate(int id, DateTime startDate, CancellationToken cancellationToken)
        {
            await _menuService.UpdateStartDate(id, startDate, cancellationToken);

            return Ok("Start date has been modified.");
        }

        [HttpPut("UpdateEndDate/{id}")]
        public async Task<ActionResult> UpdateEndDate(int id, DateTime endDate, CancellationToken cancellationToken)
        {
            await _menuService.UpdateStartDate(id, endDate, cancellationToken);

            return Ok("End date has been modified.");
        }

        [HttpDelete("Delete{id}")]
        public async Task<ActionResult> DeleteMenu(int id, CancellationToken cancellationToken)
        {
            await this._menuService.DeleteMenu(id, cancellationToken);

            return Ok("Menu has been deleted.");
        }
    }
}
