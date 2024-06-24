using CallForBids.Data;
using CallForBids.Models;
using CallForBids.Session;
using Microsoft.AspNetCore.Mvc;

namespace CallForBids.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly BidsRepository _repository;

        public BidsController(BidsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber = 1, string searchTerm = null, int? officeId = null)
        {
            var bidses = await _repository.GetBidsesAsync(pageNumber, 10, searchTerm, officeId);
            var totalItems = await _repository.GetTotalBidsesCountAsync(searchTerm, officeId);
            return Ok(bidses);
        }

        // Additional action methods for CRUD operations can be added here
    
    }
}
