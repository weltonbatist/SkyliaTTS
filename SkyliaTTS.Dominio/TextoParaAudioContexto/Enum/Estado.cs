using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyliaTTS.Dominio.TextoParaAudioContexto.Enum
{
    public enum Estado
    {
        pronto = 0,
        parado = 1,
        processando = 2,
        processado = 3,
        configuracao_invalida = 4
    }
}
