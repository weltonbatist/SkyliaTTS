using System;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkyliaTTS.Dominio.TextoParaAudioContexto.Entidades;
using SkyliaTTS.Dominio.TextoParaAudioContexto.Enum;
using SkyliaTTS.Dominio.TextoParaAudioContexto.ObjetoValor;

namespace SkyliaTTS.Teste
{
    [TestClass]
    public class TextoParaAudioTest
    {
        [TestMethod]
        public void GerarArquivoTextoConfiguracaoSimples()
        {
            string texto = "Olá, eu estou reproduzino um arquivo que foi gerado dinâmicamente atravez do sistema VerisysTTS";
            Configuracao Configuracao = new Configuracao(16000, TaxaAmostragem.dezesseis_bits, CanalAudio.estereo, 100, 0);
            Conversor Conversor = new Conversor(Configuracao, "30052019");

            var dir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString()) + @"\simples_arquivo.wav";
            Conversor.TextoFala(texto, dir);

            Thread.Sleep(5000);

            Assert.AreEqual(0, Conversor.Notifications.Count);

        }

        [TestMethod]
        public void GerarArquivoTextoConfiguracaoAvancada()
        {
            string texto = "GUSTAVO É GUÊI HOMOS";
            Configuracao Configuracao = new Configuracao(Codificacao.aLaw, 16000, 8, 1, 3200, 2, null, 100, 0);
            Conversor Conversor = new Conversor(Configuracao, "30052019");

            var dir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString()) + @"\avancado_arquivo.wav";
            Conversor.TextoFala(texto, dir);

            Thread.Sleep(5000);

            Assert.AreEqual(0, Conversor.Notifications.Count);

        }

        [TestMethod]
        public void GerarArquivoTextoSSML()
        {
            string texto = "<p><s>Consciente do seu patrimônio espiritual e moral<break time=\"300ms\"/>, a União é fundamentada nos valores indivisíveis e universais da dignidade humana, <prosody rate=\" - 15 % \">liberdade, igualdade e solidariedade; </prosody> é baseada nos princípios da democracia e estado de direito<break time=\"500ms\"/>. </s> <s> <prosody rate=\" + 15 % \">Ela coloca o indivíduo no centro de suas ações, </prosody> ao instituir a cidadania da União e ao criar um espaço de liberdade, segurança e justiça.</s></p>";
            Configuracao Configuracao = new Configuracao(Codificacao.aLaw, 16000, 8, 1, 3200, 2, null, 100, 0);
            Conversor Conversor = new Conversor(Configuracao, "30052019");

            var dir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString()) + @"\avancado_arquivoSSML.wav";
            Conversor.TextoFalaSSML(texto, dir);

            Thread.Sleep(150000);

            Assert.AreEqual(0, Conversor.Notifications.Count);

        }
    }
}
