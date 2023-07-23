using Microsoft.AspNetCore.Mvc;
using Salut.business;
using Salut.models;

namespace Salut.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroNotaFiscalController : ControllerBase
    {
        private readonly ICadastroNotaFiscalBuziness _ICadastroNotaFiscalBuziness;

        public CadastroNotaFiscalController(ICadastroNotaFiscalBuziness iCadastroNotaFiscalBuziness)
        {
            _ICadastroNotaFiscalBuziness = iCadastroNotaFiscalBuziness;
        }


        [HttpPost("CadastroNotaFiscal")]
        public async Task<ActionResult> CadastroNotaFiscal([FromForm] NotaFiscal NotaFiscal)
        {
            try
            {
                var isSuccess = await _ICadastroNotaFiscalBuziness.Salvar(NotaFiscal);

                //Aqui eu poderia devolver uma mensagem de erro mais detalha 
                if (!isSuccess)
                    return BadRequest("Ocorreu um erro");

                
                return Ok("Salvado com sucesso");
            }
            catch (Exception ex)
            { 
                //aqui eu poderia salvar o erro em uma tabela de log no banco para mapear o erro com facilidade.
                //poderia devolver para a mensagem para o front indicando o erro ocorrido com mais detalhes.

                return BadRequest(ex);
            }
        }


    }
}
