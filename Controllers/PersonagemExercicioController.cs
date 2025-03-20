using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using rpgApi.Models;
using rpg_api.Models.Enums;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PersonagensExercicioController : ControllerBase
    {
        private static List<Personagem> personagens = new List<Personagem>
        {
            new Personagem { Id = 1, Nome = "Frodo", PontosVida = 100, Forca = 17, Defesa = 23, Inteligencia = 33, Classe = ClasseEnum.Cavaleiro },
            new Personagem { Id = 2, Nome = "Sam", PontosVida = 100, Forca = 15, Defesa = 25, Inteligencia = 30, Classe = ClasseEnum.Cavaleiro },
            new Personagem { Id = 3, Nome = "Galadriel", PontosVida = 100, Forca = 18, Defesa = 21, Inteligencia = 35, Classe = ClasseEnum.Clerigo },
            new Personagem { Id = 4, Nome = "Gandalf", PontosVida = 100, Forca = 18, Defesa = 18, Inteligencia = 37, Classe = ClasseEnum.Mago },
            new Personagem { Id = 5, Nome = "Hobbit", PontosVida = 100, Forca = 20, Defesa = 17, Inteligencia = 31, Classe = ClasseEnum.Cavaleiro },
            new Personagem { Id = 6, Nome = "Celeborn", PontosVida = 100, Forca = 21, Defesa = 13, Inteligencia = 34, Classe = ClasseEnum.Clerigo },
            new Personagem { Id = 7, Nome = "Radagast", PontosVida = 100, Forca = 25, Defesa = 11, Inteligencia = 35, Classe = ClasseEnum.Mago }
        };

        [HttpGet("GetByNome/{nome}")]
        public IActionResult GetByNome(string nome)
        {
            var personagem = personagens.FirstOrDefault(p => p.Nome.ToLower() == nome.ToLower());
            if (personagem != null)
            {
                return Ok(personagem);
            }
            return NotFound("Personagem não encontrado");
        }

        [HttpGet("GetClerigoMago")]
        public IActionResult GetClerigoMago()

        {
            var lista = personagens.FindAll(p => p.Classe == ClasseEnum.Clerigo || p.Classe == ClasseEnum.Mago)
            .OrderByDescending(p => p.PontosVida)
            .ToList();
            return Ok(lista);
        }

        [HttpGet("GetEstatisticas")]
        public IActionResult GetEstatisticas()

        {
            return Ok(new
            {
                TotalPersonagens = personagens.Count,

                SomaInteligencia = personagens.Sum(p => p.Inteligencia)
            });

        }

        [HttpPost("PostValidacao")]
        public IActionResult PostValidacao(int id, string nome, int pontosVida, int forca, int defesa, int inteligencia, ClasseEnum classe)
        {
            if (defesa <= 10 || inteligencia > 30)
            {
                return BadRequest("Defesa deve ser maior que 10 e inteligência menor ou igual a 30.");
            }
            personagens.Add(new Personagem { Id = id, Nome = nome, PontosVida = pontosVida, Forca = forca, Defesa = defesa, Inteligencia = inteligencia, Classe = classe });
            return Ok(personagens);
        }

        [HttpGet("GetByClasse/{classe}")]
        public IActionResult GetByClasse(ClasseEnum classe)
        {
            List<Personagem> lista = new List<Personagem>();
            for (int i = 0; i < personagens.Count; i++)
            {
                if (personagens[i].Classe == classe)
                {
                    lista.Add(personagens[i]);
                }
            }
            return Ok(lista);
        }

        [HttpPost("PostValidacaoMago/{inteligencia}")]
        public IActionResult PostValidacaoMago(int inteligencia)

        {
            if (inteligencia < 35)
                return BadRequest("Apenas Magos com Inteligência maior ou igual a 35 podem ser incluídos.");
            return Ok("Personagem válido para inclusão.");
        }
    }
}
