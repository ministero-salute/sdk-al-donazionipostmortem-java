/* SPDX-License-Identifier: BSD-3-Clause */

package it.mds.sdk.aldpm;

import com.fasterxml.jackson.databind.ObjectMapper;
import it.mds.sdk.aljavalib.exception.AccessLayerException;
import it.mds.sdk.aljavalib.module.FlussoRequestDpm;
import it.mds.sdk.aljavalib.module.FlussoRequestDpmInvioFileMds;
import it.mds.sdk.aljavalib.module.RecordDtoDpm;
import it.mds.sdk.aljavalib.service.FlussoService;
import it.mds.sdk.gestoreesiti.modelli.ModalitaOperativa;
import org.junit.jupiter.api.Disabled;
import org.junit.jupiter.api.Test;

import java.io.File;
import java.io.IOException;

@Disabled
public class FlussoDpmTest {

    private FlussoDpm flusso = new FlussoDpm(FlussoService.getInstanceDefaultHttpClient("http" +
            "://localhost:8080", "donazionipostmortem/record"));

    private FlussoDpm flussoApi2 = new FlussoDpm(FlussoService.getInstanceDefaultHttpClient("http" +
            "://localhost:8080", "donazionipostmortem/record/invio"));

    private FlussoDpm flussoApi3 = new FlussoDpm(FlussoService.getInstanceDefaultHttpClient("http" +
            "://localhost:8080", "donazionipostmortem/stato"));

    @Test
    public void eseguiFlussoIntegration() throws AccessLayerException, IOException {
        ObjectMapper objectMapper = new ObjectMapper();
        RecordDtoDpm recordDtoDpm = objectMapper.readValue(new File("C:\\work\\Development\\SDK\\al-java-dpm\\src\\test\\resources\\recordDtoDpm.json"), RecordDtoDpm.class);
        FlussoRequestDpm flussoRequestDpm = new FlussoRequestDpm();
        flussoRequestDpm.setIdClient("110");
        flussoRequestDpm.setModalitaOperativa(ModalitaOperativa.T);
        flussoRequestDpm.setRecordDto(recordDtoDpm);
        flusso.lanciaFlusso(flussoRequestDpm);

    }

    @Test
    public void infoRunIntegration() throws AccessLayerException {
        flusso.getInfoRun("6", null);
    }

    @Test
    public void eseguiFlussoInvioMdsResponse() throws AccessLayerException {
        FlussoRequestDpmInvioFileMds flussoRequest = new FlussoRequestDpmInvioFileMds();
        flussoRequest.setIdClient("110");
        flussoRequest.setNomeFile("DPM_OUTPUT_744-signed-cades-baseline-b.p7m");
        flussoApi2.api2(flussoRequest);
    }

    @Test
    public void verificaStatoTest() throws AccessLayerException {
        flussoApi3.getApi3("500", "REGIONE", "16149", "COD_S_A", "via_test");
    }
}
