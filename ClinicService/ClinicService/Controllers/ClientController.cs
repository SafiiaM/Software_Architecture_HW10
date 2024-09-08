using System.Reflection.Metadata;
using ClinicService.Models;
using ClinicService.Models.Requests;
using ClinicService.Services;
using ClinicService.Services.impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientrepository;
        public ClientController(IClientRepository clientRepository)
        {
            _clientrepository = clientRepository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateClientRequest createRequest)
        {
            int res = _clientrepository.Create(new Client
            {
            Document = createRequest.Document,
            SurName = createRequest.SurName,
            FirstName = createRequest.FirstName,
            Patronymic = createRequest.Patronymic,
            Birthday = createRequest.Birthday,
            });
        return Ok(res);
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] UpdateClientRequest updateRequest)
        {
            int res = _clientrepository.Update(new Client
            {
            ClientId = updateRequest.ClientId,
            SurName = updateRequest.SurName,
            FirstName = updateRequest.FirstName,
            Patronymic = updateRequest.Patronymic,
            Birthday = updateRequest.Birthday,
            });
        return Ok(res);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] int clientId)
        {
            int res = _clientrepository.Delete(clientId);
            return Ok(res);
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(_clientrepository.GetAll());
        }

        [HttpGet("get/{clientId}")]
        public IActionResult GetById([FromRoute] int clientId)
        {
            return Ok(_clientrepository.GetById(clientId));
        }
        

    }
}


