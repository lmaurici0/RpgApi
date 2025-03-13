using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using rpgApi.Models;
using rpg_api.Models.Enums;

namespace rpg_api.Controllers
{
    [ApiController] 
    [Route ("[Controller]")]
    public class PersonagensExemploController: ControllerBase
    {
        private static List<Personagem> personagens = new List<Personagem>()
        {
            new Personagem() { Id = 1, Nome = "Frodo",      PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 2, Nome = "Sam",        PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 3, Nome = "Galadriel",  PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo},
            new Personagem() { Id = 4, Nome = "Gandalf",    PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago},
            new Personagem() { Id = 5, Nome = "Hobbit",     PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 6, Nome = "Celebron",   PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo},
            new Personagem() { Id = 7, Nome = "Radgast",    PontosVida=100, Forca=25, Defesa=11, Inteligencia=34, Classe=ClasseEnum.Mago}
        };

        [HttpGet("Get")]
        public IActionResult GetFirst()
        {
            Personagem p = personagens[0];
            return Ok(p);
        }


        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(personagens);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            return Ok(personagens.FirstOrDefault(pe => pe.Id == id));
        }

        [HttpPost]
        public IActionResult AddPersonagem(Personagem novoPersonagem)
        {
            if(novoPersonagem.Inteligencia == 0)
                return BadRequest("Inteligência não pode ter o valor igual a zero");

            personagens.Add(novoPersonagem);
            return Ok(personagens);

            
        }

        [HttpGet("GetOrdenado")]
        public IActionResult GetOrdem()
        {
            List<Personagem> listaFinal = personagens.OrderBy(p  => p.Forca ).ToList();
            return Ok(listaFinal);
        }

        [HttpGet("GetContagem")]
        public IActionResult GetQuantidada()
        {
            return Ok("Quantidade de personagens: " + personagens.Count);
        }

        [HttpGet("GetSomaForca")]
        public IActionResult GetSomaForca()
        {
            return Ok(personagens.Sum(p => p.Forca));
        }

        [HttpGet("GetSemCavaleiro")]
        public IActionResult GetSemCavaleiro()
        {
            List<Personagem> listaBusca = personagens.FindAll(p => p.Classe != ClasseEnum.Cavaleiro);
            return Ok(listaBusca);
        }

        [HttpGet("GetByNomeAproximado/{nome}")]
        public IActionResult GetByNomeAproximado(string nome)
        {
            List<Personagem> listaBusca = personagens.FindAll(p => p.Nome.Contains(nome));
            return Ok(listaBusca);
        }

        [HttpGet("GetRemovendoMago")]
        public IActionResult GetRemovendoMagos()
        {
            Personagem pRemove = personagens.Find(p => p.Classe == ClasseEnum.Mago);
            personagens.Remove(pRemove);
            return Ok("Personagem removido: " + pRemove.Nome);
        }

        [HttpGet("GetByForca/{forca}")]
        public IActionResult Get(int forca)
        {
            List<Personagem> listaBusca = personagens.FindAll(p => p.Forca == forca);
            return Ok(listaBusca);
        }
    }
}