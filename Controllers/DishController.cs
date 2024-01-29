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

        
    }


}
