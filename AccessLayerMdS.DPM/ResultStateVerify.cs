using AccessLayerMdS.Models;
using AccessLayerMdS.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace AccessLayerMdS.DPM
{
    public class ResultStateVerify
    {
        public string esitoServizio { get; set; }
        public string descrizioneEsitoServizio { get; set; }
        public string numeroAtto { get; set; }
        public string identificativoSoggettoAlimentante { get; set; }
        public string tipoEsito { get; set; }
        public DettaglioType dettagli { get; set; }

        public ResultStateVerify()
        {

        }
        public string ToLog()
        {
            string log = "";

            log += $"esitoServizio: {esitoServizio} \n";
            log += $"descrizioneEsitoServizio: {descrizioneEsitoServizio} \n";
            log += $"numeroAtto: {numeroAtto} \n";
            log += $"identificativoSoggettoAlimentante: {identificativoSoggettoAlimentante} \n";
            log += $"tipoEsito: {tipoEsito} \n";

            if (dettagli != null)
                log += $"dettagli: {dettagli.ToLog()} \n";

            return log;
        }
    }
}
