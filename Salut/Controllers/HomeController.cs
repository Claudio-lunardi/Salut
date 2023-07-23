using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using Microsoft.Extensions.Options;
using Salut.models;
using Salut.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Salut.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<DadosBase> _dadosbase;

        public HomeController(IHttpClientFactory httpClient, IOptions<DadosBase> dadosbase)
        {
            _httpClient = httpClient.CreateClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            _dadosbase = dadosbase;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] NotaFiscal NotaFiscal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var form = new MultipartFormDataContent())
                    {
                        form.Add(new StringContent(NotaFiscal.CNPJ), "CNPJ");
                        form.Add(new StringContent(NotaFiscal.CanalcompraProduto), "CanalcompraProduto");
                        form.Add(new StringContent(NotaFiscal.DataCompra.ToString("yyyy-MM-dd")), "DataCompra");
                        form.Add(new StringContent(NotaFiscal.NumeroCupomFiscal), "NumeroCupomFiscal");
                        form.Add(new StreamContent(NotaFiscal.ArquivoFiscal.OpenReadStream()), "ArquivoFiscal", NotaFiscal.ArquivoFiscal.FileName);

                        if (NotaFiscal.Produto != null && NotaFiscal.Produto.Count > 0)
                        {
                            int itemIndex = 0;
                            foreach (var item in NotaFiscal.Produto)
                            {
                                form.Add(new StringContent(item.Nome), $"Produto[{itemIndex}].Nome");
                                form.Add(new StringContent(item.Quantidade.ToString()), $"Produto[{itemIndex}].Quantidade");
                                itemIndex++;
                            }

                        }

                        HttpResponseMessage response = await _httpClient.PostAsync($"{_dadosbase.Value.API_URL_BASE}CadastroNotaFiscal/CadastroNotaFiscal", form);
                        var result = await response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return BadRequest(result);
                        }

                    }
                }
                else
                {
                    return View();
                }


            }
            catch (Exception)
            {
                throw;
            }


        }



    }
}