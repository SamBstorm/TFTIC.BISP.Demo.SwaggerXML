using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFTIC.BISP.Math.API.Controllers
{
    [ApiController]
    public class MathController : ControllerBase
    {
        private static int answer;
        private static string calcul = default;

        private readonly ILogger<MathController> _logger;

        public MathController(ILogger<MathController> logger)
        {
            _logger = logger;
        }

        [HttpGet("addition/{nombre_1:int}/{nombre_2:int}")]
        public ActionResult<int> Get(int nombre_1, int nombre_2)
        {
            return Ok(nombre_1 + nombre_2);
        }


        [HttpGet("table/[action]/")]
        [Produces("application/xml")]
        public ActionResult<List<string>> Multiplication()
        {
            List<string> result = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    result.Add($"{i}X{j} = {i * j}");
                }
            }
            return Ok(result);
        }
        [HttpGet("table/[action]/{nombre:int}")]
        public ActionResult<Dictionary<string,int>> Multiplication(int nombre)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            for (int i = 1; i <= 10; i++)
            {
                result.Add($"{i}X{nombre}", i * nombre);
            }
            return Ok(result);
        }

        [HttpGet("[action]")]
        public ActionResult<string> Interrogation()
        {
            Random RNG = new Random();
            int nombre_1 = RNG.Next(1, 10), nombre_2 = RNG.Next(1, 10);
            switch (RNG.Next(0, 3))
            {
                case 0:
                    calcul = $"{nombre_1}+{nombre_2}";
                    answer = nombre_1 + nombre_2;
                    break;
                case 1:
                    calcul = $"{nombre_1}-{nombre_2}";
                    answer = nombre_1 - nombre_2;
                    break;
                case 2:
                    calcul = $"{nombre_1}*{nombre_2}";
                    answer = nombre_1 * nombre_2;
                    break;
                case 3:
                    calcul = $"{nombre_1}/{nombre_2}";
                    answer = nombre_1 / nombre_2;
                    break;
            }
            return Ok(calcul + " = ? ");
        }

        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400,Type=typeof(string))]
        public ActionResult<string> Interrogation([FromBody]int result)
        {
            if (result == answer) return Ok($"Félicitation! La réponse de {calcul} est bien : {answer}!");
            return BadRequest($"Dommage! La réponse de {calcul} est : {answer}!");
        }
    }
}
