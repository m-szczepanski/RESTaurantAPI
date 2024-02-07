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

        [HttpPost("Add")]
        public async Task<ActionResult<MenuDto>> AddMenu(List<int> dishIds, string dateString)
        {
            var menu = await _menuService.AddMenu(dishIds, dateString);

            var menuDto = _mapper.Map<MenuDto>(menu);

            return Ok(menuDto);
        }
    }
}
