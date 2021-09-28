using Confluent.Kafka;
using ConsumerBLL.Interfaces;
using ConsumerBLL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageConsumer.Controllers
{
    [Route("api/Message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            IEnumerable<string> result;

            try
            {
                result = await _messageService.GetMessages();

                if (result!=null && !result.Any())
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }
    }
}
