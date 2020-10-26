using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApriorit.Core.Models;
using TestApriorit.Core.ViewModels;
using TestApriorit.Core.Services;

namespace TestApriorit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeePositionsController : ControllerBase
    {
        private readonly IEmployeePositionService _employeePositionService;

        public EmployeePositionsController(IEmployeePositionService employeePositionService)
        {
            _employeePositionService = employeePositionService;
        }

        // GET: api/EmployeePositions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeePositionViewModel>>> GetEmployeePositions([FromQuery] string sortOrder, int pageNumber, int pageSize)
        {
            var employeePositions = await _employeePositionService.GetAllEmployeePositions(sortOrder, pageNumber, pageSize);

            return employeePositions.ToList();
        }

        // GET: api/
        [HttpGet]
        [Route("getCount")]
        public async Task<ActionResult<int>> GetEmployeePositionsCount()
        {
            return await _employeePositionService.GetEmployeePositionsCount();
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> CreateEmployeePosition(EmployeePositionViewModel employeePositionViewModel)
        {
            EmployeePositionViewModel newEmployeePosition = await _employeePositionService.CreateEmployeePosition(employeePositionViewModel);

            return CreatedAtAction("GetEmployeePosition", new { id = newEmployeePosition.EmployeePosition.EmployeePositionId }, newEmployeePosition);
        }
    }
}
