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

    public class TableController : Controller
    {
        private readonly IMapper _mapper;
        private readonly TableService tableService;

        public TableController(TableService tableService, IMapper _mapper)
        {
            this._mapper = _mapper;
            this.tableService = tableService;
        }

        [HttpGet("GetAllTables")]
        public async Task<ActionResult<List<TableDTO>>> GetAllTables(
            CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var tables = await this.tableService.GetAllTables(cancellationToken);
            var tableDto = this._mapper.Map<List<TableDTO>>(tables);

            if (skip.HasValue)
            {
                tableDto = tableDto.Skip(skip.Value).ToList();
            }

            if (limit.HasValue)
            {
                tableDto = tableDto.Take(limit.Value).ToList();
            }

            return Ok(tableDto);
        }
    }
}
