using AccessLayerMdS.Models;
using AccessLayerMdS.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace AccessLayerMdS.DPM
{
    public class ResultSendFile
    {
        public string esito { get; set; }

        public string esitoServizio { get; set; }

        public string descrizioneEsitoServizio { get; set; }

        public string tipoEsito { get; set; }

        public DettaglioType dettagli { get; set; }

        public List<Esito> esitiValidazione { get; set; }

        public ResultSendFile()
        {

        }
        public string ToLog()
        {
            string log = "";

            log += $"esito: {esito} \n";
            log += $"esitoServizio: {esitoServizio} \n";
            log += $"descrizioneEsitoServizio: {descrizioneEsitoServizio} \n";
            log += $"tipoEsito: {tipoEsito} \n";

            if(dettagli != null)
                log += $"dettagli: {dettagli.ToLog()} \n";

            if (esitiValidazione != null)
            {
                log += $"esitiValidazione: \n";
                foreach (Esito e in esitiValidazione)
                    log += e.ToLog();
            }

            return log;
        }
    }
}
