using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTaurantAPI.DTOs;
using RESTaurantAPI.Services;

namespace RESTaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class DishController : Controller
    {
        private readonly IMapper _mapper;
        private readonly DishService dishService;

        public DishController(DishService dishService, IMapper _mapper)
        {
            this._mapper = _mapper;
            this.dishService = dishService;
        }

        [HttpGet("GetAllDishes")]
        public async Task<ActionResult<List<DishDto>>> GetAllDishes(CancellationToken cancellationToken,
            int? skip = null, int? limit = null)
        {
            var dishes = await this.dishService.GetAllDishes(cancellationToken);
            var dishesDto = this._mapper.Map<List<DishDto>>(dishes);

            if (skip.HasValue)
            {
                dishesDto = dishesDto.Skip(skip.Value).ToList();
            }

            if (limit.HasValue)
            {
                dishesDto = dishesDto.Take(limit.Value).ToList();
            }

            return Ok(dishesDto);
        }

        [HttpPost("AddDish")]
        public async Task<ActionResult<DishDto>> AddDish(string dishName, string allergens, decimal price, string cuisine, bool vegetarian, 
            bool vegan, bool spicy, CancellationToken cancellationToken)
        {
            var dish = await this.dishService.AddDish(dishName, allergens, price, cuisine, vegetarian, vegan, spicy, cancellationToken);
            var dishDto = this._mapper.Map<DishDto>(dish);

            return Ok(dishDto);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateDish(int id, string dishName, string allergens, decimal price, string cuisine, bool vegetarian, bool vegan, bool spicy, CancellationToken cancellationToken)
        {
            await dishService.UpdateDish(id, dishName, allergens, price, cuisine, vegetarian, vegan, spicy, cancellationToken);

            return Ok("Dish updated successfully.");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteDish(int id, CancellationToken cancellationToken)
        {
            await this.dishService.DeleteDish(id, cancellationToken);

            return Ok("Dish has been deleted successfully");
        }
    }


}
