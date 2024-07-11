/* SPDX-License-Identifier: BSD-3-Clause */

package it.mds.sdk.aldpm;

import com.fasterxml.jackson.core.JsonProcessingException;
import it.mds.sdk.aljavalib.exception.AccessLayerException;
import it.mds.sdk.aljavalib.module.DpmVerificaStatoResponse;
import it.mds.sdk.aljavalib.module.FlussoRequestDpm;
import it.mds.sdk.aljavalib.module.FlussoRequestDpmInvioFileMds;
import it.mds.sdk.aljavalib.module.InvioMdsResponse;
import it.mds.sdk.aljavalib.module.deserializer.DpmRisultatoValidazione;
import it.mds.sdk.aljavalib.service.FlussoService;
import it.mds.sdk.gestoreesiti.modelli.InfoRun;

public class FlussoDpm {


    private FlussoService flussoService;

    public FlussoDpm(FlussoService flussoService) {
        this.flussoService = flussoService;
    }

    public DpmRisultatoValidazione lanciaFlusso(FlussoRequestDpm flussoRequestDpm) throws AccessLayerException, JsonProcessingException {
        DpmRisultatoValidazione risultatoInizioValidazione = flussoService.eseguiFlussoDPM(flussoRequestDpm);
        return risultatoInizioValidazione;
    }

    public InfoRun getInfoRun(String idRun, String idClient) throws AccessLayerException {
        InfoRun infoRun = flussoService.getInfoRun(idRun, idClient);
        return infoRun;
    }

    public InvioMdsResponse api2(FlussoRequestDpmInvioFileMds flussoRequestDpmInvioFileMds) throws AccessLayerException {
        InvioMdsResponse invioMdsResponse = flussoService.api2(flussoRequestDpmInvioFileMds);
        return invioMdsResponse;
    }

    public DpmVerificaStatoResponse getApi3(String identificativoSoggettoAlimentante, String regioneSoggettoAlimentante, String cap, String codiceSoggettoAlimentante, String indirizzo) throws AccessLayerException {
        DpmVerificaStatoResponse verificaStato = flussoService.getApi3(identificativoSoggettoAlimentante, regioneSoggettoAlimentante, cap, codiceSoggettoAlimentante, indirizzo);
        return verificaStato;
    }
}
