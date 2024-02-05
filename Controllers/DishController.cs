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

        [HttpGet("GetDishById/{dishId}")]
        public async Task<ActionResult<DishDto>> GetDishById(int dishId,
            CancellationToken cancellationToken)
        {
            var dish = await dishService.GetDishById(dishId, cancellationToken);
            var dishDto = this._mapper.Map<DishDto>(dish);
            if (dishDto == null)
            {
                return NotFound();
            }

            return Ok(dishDto);
        }

        [HttpGet("GetDishByName/{dishName}")]
        public async Task<ActionResult<List<DishDto>>> GetDishByName(string dishName, CancellationToken cancellationToken)
        {
            var dish = await dishService.GetDishByName(dishName, cancellationToken);
            var dishDto = this._mapper.Map<List<DishDto>>(dish);

            if (dishDto == null || dishDto.Count == 0)
            {
                return NotFound();
            }

            return Ok(dishDto);
        }

        [HttpGet("GetDishesFromCuisine/{cuisine}")]
        public async Task<ActionResult<List<DishDto>>> GetDishesFromCuisine(string cuisine, CancellationToken cancellationToken)
        {
            var dish = await dishService.GetDishesFromCuisine(cuisine, cancellationToken);
            var dishDto = this._mapper.Map<List<DishDto>>(dish);

            if (dishDto == null || dishDto.Count == 0)
            {
                return NotFound();
            }

            return Ok(dishDto);
        }

        [HttpGet("GetVeganDishes")]
        public async Task<ActionResult<List<DishDto>>> GetVeganDishes(CancellationToken cancellationToken)
        {
            var dish = await dishService.GetVeganDishes(cancellationToken);
            var dishDto = this._mapper.Map<List<DishDto>>(dish);

            if (dishDto == null || dishDto.Count == 0)
            {
                return NotFound();
            }

            return Ok(dishDto);
        }

        [HttpGet("GetVegetarianDishes")]
        public async Task<ActionResult<List<DishDto>>> GetVegetarianDishes(CancellationToken cancellationToken)
        {
            var dish = await dishService.GetVegetarianDishes(cancellationToken);
            var dishDto = this._mapper.Map<List<DishDto>>(dish);

            if (dishDto == null || dishDto.Count == 0)
            {
                return NotFound();
            }

            return Ok(dishDto);
        }

        [HttpGet("GetSpicyDishes")]
        public async Task<ActionResult<List<DishDto>>> GetSpicyDishes(CancellationToken cancellationToken)
        {
            var dish = await dishService.GetSpicyDishes(cancellationToken);
            var dishDto = this._mapper.Map<List<DishDto>>(dish);

            if (dishDto == null || dishDto.Count == 0)
            {
                return NotFound();
            }

            return Ok(dishDto);
        }

        [HttpGet("GetDishesByParameters")]
        public async Task<ActionResult<List<DishDto>>> GetDishesByParameters(
            [FromQuery] string? dishName = null,
            [FromQuery] string? allergens = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] string? cuisine = null,
            [FromQuery] bool? vegetarian = null,
            [FromQuery] bool? vegan = null,
            [FromQuery] bool? spicy = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var dishes = await dishService.GetDishesByParameters(
                    dishName, allergens, maxPrice, cuisine, vegetarian, vegan, spicy, cancellationToken);

                var dishDto = this._mapper.Map<List<DishDto>>(dishes);

                if (dishes == null || dishes.Count == 0)
                {
                    return NotFound();
                }

                return Ok(dishDto);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AddDish")]
        public async Task<ActionResult<DishDto>> AddDish(string dishName, string? allergens, decimal price, string cuisine, bool vegetarian, 
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
