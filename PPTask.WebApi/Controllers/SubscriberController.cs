using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PPTask.Service.Services.Subscribers;

namespace PPTask.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly ISubscriberService subscriberService;

        public SubscriberController(ISubscriberService subscriberService)
        {
            this.subscriberService = subscriberService;
        }
        [HttpPost]
        public async Task<IActionResult> GetInvoiceBySubscribeId(int id)
        {
            var subs = await subscriberService.ListSubscriberByIdAsync(id);
            if(subs?.Any() == true)
                return Ok(subs);
            return BadRequest("Böyle bir veri bulunamadı");
        }
    }
}
