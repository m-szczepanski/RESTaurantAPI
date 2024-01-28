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
        public async Task<ActionResult<List<TableDto>>> GetAllTables(
            CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var tables = await this.tableService.GetAllTables(cancellationToken);
            var tablesDto = this._mapper.Map<List<TableDto>>(tables);

            if (skip.HasValue)
            {
                tablesDto = tablesDto.Skip(skip.Value).ToList();
            }

            if (limit.HasValue)
            {
                tablesDto = tablesDto.Take(limit.Value).ToList();
            }

            return Ok(tablesDto);
        }

        [HttpGet("GetTableById/{tableId}")]
        public async Task<ActionResult<TableDto>> GetTableById(int tableId, CancellationToken cancellationToken)
        {
            var table = await tableService.GetTableById(tableId, cancellationToken);
            var tableDto = this._mapper.Map<TableDto>(table);
            if (tableDto == null)
            {
                return NotFound();
            }

            return Ok(tableDto);
        }

        [HttpPost("AddTable")]
        public async Task<ActionResult<TableDto>> AddTable(int seats, bool availability, CancellationToken cancellationToken)
        {
            var table = await this.tableService.AddTable(seats, availability, cancellationToken);
            var tableDto = this._mapper.Map<TableDto>(table);

            return Ok(tableDto);
        }
        
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateTable(int id, int seats, bool availability, CancellationToken cancellationToken)
        {
            await tableService.UpdateTable(id, seats, availability, cancellationToken);

            return Ok("Table updated successfully.");
        }

        [HttpPut("MarkAsTaken/{id}")]
        public async Task<ActionResult> MarkAsTaken(int id, CancellationToken cancellationToken)
        {
            await tableService.MarkAsTaken(id, cancellationToken);

            return Ok("Table's status has been changed to taken.");
        }

        [HttpPut("MarkAsEmpty/{id}")]
        public async Task<ActionResult> MarkAsEmpty(int id, CancellationToken cancellationToken)
        {
            await tableService.MarkAsEmpty(id, cancellationToken);

            return Ok("Table's status has been changed to available.");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteTable(int id, CancellationToken cancellationToken)
        {
            await this.tableService.DeleteTable(id, cancellationToken);

            return Ok("Table deleted successfully");
        }
    }
}
