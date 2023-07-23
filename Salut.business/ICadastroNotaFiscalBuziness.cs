using Salut.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salut.business
{
    public interface ICadastroNotaFiscalBuziness
    {
        Task<bool> Salvar(NotaFiscal notaFiscal);
    }
}
