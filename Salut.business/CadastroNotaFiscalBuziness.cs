using Salut.infra;
using Salut.models;

namespace Salut.business
{
    public class CadastroNotaFiscalBuziness : ICadastroNotaFiscalBuziness
    {
        private readonly EntityContext _entityContext;

        public CadastroNotaFiscalBuziness(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public async Task<bool> Salvar(NotaFiscal notaFiscal)
        {
            notaFiscal.GuidArquivoName = Guid.NewGuid();
            try
            {
                if (ValidarDados(notaFiscal))
                {
                    if (SalvarNotaFiscal(notaFiscal) && SalvarProduto(notaFiscal, NotaFiscalRetornoID(notaFiscal)))
                    {
                        SalvarArquivo(notaFiscal);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }




        }

        private bool ValidarDados(NotaFiscal notaFiscal)
        {
            try
            {
                bool ArquivoType = false;
                bool DadaCompra = false;
                bool QuantidadeProduto = false;

                if (notaFiscal != null)
                {
                    string extensao = System.IO.Path.GetExtension(notaFiscal.ArquivoFiscal.FileName).ToLower();
                    // Lista das extensões permitidas
                    string[] extensoesPermitidas = { ".jpg", ".png", ".pdf" };

                    // Verifica se a extensão está na lista de extensões permitidas
                    foreach (string ext in extensoesPermitidas)
                    {
                        if (extensao == ext)
                        {
                            ArquivoType = true;
                        }
                    }

                    var dataInicio = new DateTime(2023, 05, 01);
                    var dataFinal = new DateTime(2023, 06, 15);
                    //validar se o range de data é valido
                    if (notaFiscal.DataCompra >= dataInicio && notaFiscal.DataCompra <= dataFinal)
                    {
                        DadaCompra = true;
                    }

                    //validar se a quantidade do produto é maior que 6
                    int QtdProduto = 0;
                    foreach (var item in notaFiscal.Produto)
                    {
                        QtdProduto += item.Quantidade;
                    };

                    if (QtdProduto <= 6)
                        QuantidadeProduto = true;

                }

                if (ArquivoType && DadaCompra && QuantidadeProduto)
                    return true;

                return false;
            }
            catch (Exception)
            {
                throw;
            }



        }
        private bool SalvarProduto(NotaFiscal notaFiscal, int NotaFiscalID)
        {
            try
            {
                foreach (var item in notaFiscal.Produto)
                {
                    item.NotaFiscalID = NotaFiscalID;
                    _entityContext.Produto.Add(item);
                    _entityContext.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        private bool SalvarNotaFiscal(NotaFiscal notaFiscal)
        {
            try
            {
                _entityContext.NotaFiscal.Add(notaFiscal);
                _entityContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        private void SalvarArquivo(NotaFiscal notaFiscal)
        {
            string caminho = @"C:\\Salut";

            if (!Directory.Exists(caminho))
            {
                Directory.CreateDirectory(caminho);
            }

            string nomeArquivo = notaFiscal.GuidArquivoName + Path.GetExtension(notaFiscal.ArquivoFiscal.FileName);

            string caminhoCompleto = Path.Combine(caminho, nomeArquivo);

            using (var fileStream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                notaFiscal.ArquivoFiscal.CopyTo(fileStream);
            }

        }
        private int NotaFiscalRetornoID(NotaFiscal notaFiscal)
        {
            return _entityContext.NotaFiscal.Where(x => x.GuidArquivoName.Equals(notaFiscal.GuidArquivoName)).Select(c => c.ID).FirstOrDefault();
        }

    }
}
