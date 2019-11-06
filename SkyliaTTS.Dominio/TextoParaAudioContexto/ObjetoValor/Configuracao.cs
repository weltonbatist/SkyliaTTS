using Flunt.Notifications;
using SkyliaTTS.Dominio.TextoParaAudioContexto.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyliaTTS.Dominio.TextoParaAudioContexto.ObjetoValor
{
    public class Configuracao : Notifiable
    {
        public int Volume { get; private set; }
        public int Velocidade { get; private set; }
        public int AmostraPorSegundo { get; private set; }
        public int BistsPorAmostra { get; }
        public int QuantidadeCanais { get; }
        public int BytesMedioPorSegundo { get; }
        public int AlinhamentoBloco { get; }
        public byte[] FormatoEspecificoData { get; }
        public TaxaAmostragem Taxa { get; private set; }
        public CanalAudio Canal { get; private set; }
        public Codificacao Codificacao { get; private set; }

        public bool ConfiguracaoAvancada { get; set; }

        //EncodingFormat encodingFormat, int samplesPerSecond, int bitsPerSample, int channelCount, int averageBytesPerSecond, int blockAlign, byte[] formatSpecificData
        //info = new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null);
        public Configuracao(Codificacao codificacao, int amostra_por_segundo, int bits_por_amostra, int quantidade_canais, int bytes_medio_por_segundo, int bloco_alinhamento, byte[] formato_especifico_data, int volume, int velocidade)
        {
            if (EhValido(amostra_por_segundo, bits_por_amostra, quantidade_canais, bytes_medio_por_segundo, bloco_alinhamento, volume, velocidade))
            {
                Codificacao = codificacao;
                AmostraPorSegundo = amostra_por_segundo;
                BistsPorAmostra = bits_por_amostra;
                QuantidadeCanais = quantidade_canais;
                BytesMedioPorSegundo = bytes_medio_por_segundo;
                AlinhamentoBloco = bloco_alinhamento;
                FormatoEspecificoData = formato_especifico_data;
                Volume = volume;
                Velocidade = velocidade;
                ConfiguracaoAvancada = true;
            }
        }

        //int samplesPerSecond, AudioBitsPerSample bitsPerSample, AudioChannel channel
        public Configuracao(int amostra_por_segundo, TaxaAmostragem taxaAmostragem, CanalAudio canalAudio, int volume, int velocidade)
        {
            if (EhValido(amostra_por_segundo, volume, velocidade))
            {
                AmostraPorSegundo = amostra_por_segundo;
                Taxa = taxaAmostragem;
                Canal = canalAudio;
                Volume = volume;
                Velocidade = velocidade;
                ConfiguracaoAvancada = false;
            }
        }

        private bool EhValido(int amostra_por_segundo, int bits_por_amostra, int quantidade_canais, int bytes_medio_por_segundo, int bloco_alinhamento, int volume, int velocidade)
        {
            bool result = true;

            if (amostra_por_segundo < 0)
            {
                AddNotification(new Notification("amostra_por_segundo", "amostra_por_segundo deve ser maior que zero"));
                result = false;
            }
            if (bits_por_amostra < 0)
            {
                AddNotification(new Notification("bits_por_amostra", "bits_por_amostra deve ser maior que zero"));
                result = false;
            }
            if (quantidade_canais < 0)
            {
                AddNotification(new Notification("quantidade_canais", "quantidade_canais deve ser maior que zero"));
                result = false;
            }
            if (bytes_medio_por_segundo < 0)
            {
                AddNotification(new Notification("bytes_medio_por_segundo", "bytes_medio_por_segundo deve ser maior que zero"));
                result = false;
            }
            if (bloco_alinhamento < 0)
            {
                AddNotification(new Notification("bloco_alinhamento", "bloco_alinhamento deve ser maior que zero"));
                result = false;
            }
            if (volume < 0)
            {
                AddNotification(new Notification("volume", "volume deve ser maior que zero"));
                result = false;
            }
            if (velocidade < -10 || velocidade > 10)
            {
                AddNotification(new Notification("velocidade", "velocidade deve ser entre -10 e 10"));
                result = false;
            }

            return result;
        }

        private bool EhValido(int amostra_por_segundo, int volume, int velocidade)
        {
            bool result = true;

            if (volume < 0)
            {
                AddNotification(new Notification("volume", "volume deve ser maior que zero"));
                result = false;
            }
            if (velocidade < -10 || velocidade > 10)
            {
                AddNotification(new Notification("velocidade", "velocidade deve ser entre -10 e 10"));
                result = false;
            }
            if (amostra_por_segundo <= 0)
            {
                AddNotification(new Notification("amostra_por_segundo", "amostra_por_segundo deve ser maior que zero"));
            }

            return result;
        }


    }
}
