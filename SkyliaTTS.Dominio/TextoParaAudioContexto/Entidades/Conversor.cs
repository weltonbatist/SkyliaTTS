using Flunt.Notifications;
using SkyliaTTS.Dominio.TextoParaAudioContexto.Enum;
using SkyliaTTS.Dominio.TextoParaAudioContexto.ObjetoValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;


namespace SkyliaTTS.Dominio.TextoParaAudioContexto.Entidades
{
    public class Conversor : Notifiable, IDisposable
    {
        public readonly string IdSolicitacao;
        public SpeechSynthesizer SpeechSynthesizer { get; private set; }
        public Estado Estado { get; set; }
        public Configuracao Configuracao { get; private set; }
        public bool HabilitaProcessamento { get; private set; }

        private static object objLock = new object();

        public Conversor(Configuracao configuracao, string id_solicitacao)
        {

            if (configuracao.Notifications.Count > 0)
            {
                AddNotification(new Notification("Conversor.Configuracao", "A Configuração atrelada ao conversor é inválida"));
                HabilitaProcessamento = false;
                Estado = Estado.configuracao_invalida;
            }
            else
            {
                HabilitaProcessamento = true;
                Estado = Estado.pronto;
                Configuracao = configuracao;
                SpeechSynthesizer = new SpeechSynthesizer();
                SpeechSynthesizer.StateChanged += SpeechSynthesizer_StateChanged;

            }

            IdSolicitacao = id_solicitacao;
        }

        private void SpeechSynthesizer_StateChanged(object sender, StateChangedEventArgs e)
        {
            switch (e.State)
            {
                case SynthesizerState.Ready:
                    Estado = Estado.pronto;
                    break;
                case SynthesizerState.Speaking:
                    Estado = Estado.processando;
                    break;
                case SynthesizerState.Paused:
                    Estado = Estado.parado;
                    break;
            }
        }

        public void TextoFala(string texto, string diretorio)
        {
            try
            {
                if (HabilitaProcessamento && Estado == Estado.pronto)
                {
                    SpeechAudioFormatInfo sfi = CarregaConfiguracoes();
                    SpeechSynthesizer.Rate = Configuracao.Velocidade;
                    SpeechSynthesizer.Volume = Configuracao.Volume;
                    SpeechSynthesizer.SetOutputToWaveFile(diretorio, sfi);
                    SpeechSynthesizer.SpeakAsync(texto);
                }
                else
                {
                    AddNotification(new Notification("erro_logico", "Instancia não pode realizar processamento devido a falhas"));
                }
            }
            catch (Exception ex)
            {
                AddNotification(new Notification("erro_global", ex.ToString()));
            }
        }

        public void TextoFalaSSML(string texto, string diretorio)
        {
            try
            {
                if (HabilitaProcessamento && Estado == Estado.pronto)
                {
                    SpeechAudioFormatInfo sfi = CarregaConfiguracoes();
                    SpeechSynthesizer.Rate = Configuracao.Velocidade;
                    SpeechSynthesizer.Volume = Configuracao.Volume;
                    SpeechSynthesizer.SetOutputToWaveFile(diretorio, sfi);
                    SpeechSynthesizer.SpeakSsmlAsync(texto);
                }
                else
                {
                    AddNotification(new Notification("erro_logico", "Instancia não pode realizar processamento devido a falhas"));
                }
            }
            catch (Exception ex)
            {
                AddNotification(new Notification("erro_global", ex.ToString()));
            }
        }

        private SpeechAudioFormatInfo CarregaConfiguracoes()
        {
            if (Configuracao.ConfiguracaoAvancada)
            {
                EncodingFormat encodingFormat = Configuracao.Codificacao == Codificacao.aLaw ? EncodingFormat.ALaw : (Configuracao.Codificacao == Codificacao.pcm ? EncodingFormat.Pcm : EncodingFormat.ULaw);
                return new SpeechAudioFormatInfo(encodingFormat, Configuracao.AmostraPorSegundo, Configuracao.BistsPorAmostra, Configuracao.QuantidadeCanais, Configuracao.BytesMedioPorSegundo, Configuracao.AlinhamentoBloco, Configuracao.FormatoEspecificoData);
            }
            else
            {
                AudioBitsPerSample aups = Configuracao.Taxa == TaxaAmostragem.dezesseis_bits ? AudioBitsPerSample.Sixteen : AudioBitsPerSample.Eight;
                AudioChannel audioChannel = Configuracao.Canal == CanalAudio.estereo ? AudioChannel.Stereo : AudioChannel.Mono;
                return new SpeechAudioFormatInfo(Configuracao.AmostraPorSegundo, aups, audioChannel);
            }
        }

        public void Dispose()
        {
            SpeechSynthesizer.Dispose();
        }
    }
}
