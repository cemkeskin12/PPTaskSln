using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PPTask.Entity.DTOs;
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
        [Authorize]
        [HttpPost]
        public async Task AddSubscriber(SubscriberAddDto subscriberAddDto)
        {
            await subscriberService.AddSubscriber(subscriberAddDto);
        }
        [Authorize]
        [HttpPost]
        public async Task DeleteSubscribe(SubscriberDeleteDto subscriberDeleteDto)
        {
            await subscriberService.DeleteSubscriber(subscriberDeleteDto);
        }
    }
}
