using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Coins.Business.Interfaces;
using Coins.Business.Models;
using AutoMapper;
using Coins.App.ViewModels;

namespace Coins.App.Controllers
{
    [Route("api/moedas")]
    [ApiController]
    public class MoedasController : Controller
    {
        private readonly ICoinRepository _moedaRepository;
        private readonly IMapper _mapper;

        public MoedasController(ICoinRepository moedaRepository,
                                IMapper mapper)
        {
            _moedaRepository = moedaRepository;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<CoinViewModel>> GetItemFila()
        {
            var moeda = await _moedaRepository.GetLastItemAdicionado();

            if (moeda == null)
                return Ok(new { mensagem = "Não existe moedas para retornar" });

            await _moedaRepository.DeleteItem(moeda.Id);

            return Ok(_mapper.Map<CoinViewModel>(moeda));
        }

        [HttpPost()]
        public async Task<IActionResult> AddItensFila([FromBody] IList<CoinViewModel> moedas)
        {
            foreach (var moeda in moedas)
            {
                await _moedaRepository.AddItem(_mapper.Map<Coin>(moeda));
            }

            return Ok("Inserido com Sucesso!");
        }
    }
}
