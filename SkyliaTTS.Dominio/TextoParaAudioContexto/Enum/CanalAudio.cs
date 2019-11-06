using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyliaTTS.Dominio.TextoParaAudioContexto.Enum
{
    public enum CanalAudio
    {
        mono = 0,
        estereo = 1
    }

    public enum TaxaAmostragem
    {
        oito_bits = 0,
        dezesseis_bits = 1
    }

    public enum Codificacao
    {
        pcm = 0,
        aLaw = 1,
        uLaw = 2
    }
}
