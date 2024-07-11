using AccessLayerMdS.Core;
using AccessLayerMdS.Models;
using AccessLayerMdS.Models.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Text.Json;

namespace AccessLayerMdS.DPM
{
    /// <summary>
    /// A Core logic for interfacing with API Gateway for flux DPM.
    /// </summary>
    public class AccessLayer : AccessLayerCore
    {
        private string ENDPOINT_API2 = "/v1/flusso/{nomeFlusso}/record/invio";
        private string ENDPOINT_API3 = "/v1/flusso/{nomeFlusso}/stato/{identificativoSoggettoAlimentante}";

        public AccessLayer() : base("FLUSSO_DPM")
        {
        }

        /// <summary>
        /// Sending processing of the single record.
        /// </summary>
        /// <param name="IdClient">Identification of the operation client side.</param>
        /// <param name="record">The <see cref="Record"/> single record to be processed.</param>
        /// <param name="mod">Operative mode.</param>
        /// <param name="annoRiferimento">Anno Riferimento.</param>
        /// <param name="periodoRiferimento">Periodo Riferimento.</param>
        /// <param name="codiceRegione">Codice Regione.</param>
        /// <returns>Containing the <see cref="ResultSingleRecord"/> result of operation.</returns>
        public ResultSingleRecord SendSingleRecord(
            int IdClient, 
            Record record, 
            ModOp mod,
            string annoRiferimento,
            string periodoRiferimento,
            string codiceRegione
            )
        {
            return base.SendSingleRecord(IdClient, record, mod, annoRiferimento, periodoRiferimento, codiceRegione);
        }

        /// <summary>
        /// SendFile.
        /// </summary>
        /// <param name="IdClient">Identification of the operation client side.</param>
        /// <param name="NomeFile">Xml file that represents the stream to be processed.</param>
        /// <returns>Containing the <see cref="ResultSendFile"/> result of operation.</returns>
        public ResultSendFile SendFile(string IdClient, string NomeFile)
        {

            if (string.IsNullOrEmpty(RemotePath))
                throw new Exception("Access Layer not initialized!");

            ResultSendFile result = new ResultSendFile();

            try
            {
                RequestSendFile request = new RequestSendFile()
                {
                    idClient = IdClient,
                    nomeFile = NomeFile
                };

                string json = JsonSerializer.Serialize(request);

                ENDPOINT_API2 = ENDPOINT_API2.Replace("{nomeFlusso}", FluxName);

                InsLog("SendFile", $"host: {RemotePath + ENDPOINT_API2}");
                InsLog("SendFile", $"json: {json}");

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(RemotePath + ENDPOINT_API2);
                httpWebRequest.ContentType = @"application/json; charset=utf-8";
                //httpWebRequest.Headers.Add("Authorization", "Bearer " + AccessToken);

                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                using (var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    if (httpResponse != null && httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        using (var reader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var objText = reader.ReadToEnd();

                            InsLog("SendFile", $"Response: {objText}");

                            result = JsonSerializer.Deserialize<ResultSendFile>(objText);
                        }
                    }
                }
            }
            catch (Exception excep)
            {
                throw excep;
            }

            return result;
        }

        /// <summary>
        /// SendFile.
        /// </summary>
        /// <param name="identificativoSoggettoAlimentante">identificativoSoggettoAlimentante.</param>
        /// <param name="regioneSoggettoAlimentante">regioneSoggettoAlimentante.</param>
        /// <param name="cap">cap.</param>
        /// <param name="codiceSoggettoAlimentante">codiceSoggettoAlimentante.</param>
        /// <param name="indirizzo">indirizzo.</param>
        /// <returns>Containing the <see cref="ResultStateVerify"/> result of operation.</returns>
        public ResultStateVerify StateVerify(string identificativoSoggettoAlimentante,
                                             string regioneSoggettoAlimentante,
                                             string cap,
                                             string codiceSoggettoAlimentante,
                                             string indirizzo)
        {

            if (string.IsNullOrEmpty(RemotePath))
                throw new Exception("Access Layer not initialized!");

            ResultStateVerify result = new ResultStateVerify();

            try
            {
                string url = ENDPOINT_API3.Replace("{nomeFlusso}", FluxName).Replace("{identificativoSoggettoAlimentante}", identificativoSoggettoAlimentante);

                url += $"?regioneSoggettoAlimentante={regioneSoggettoAlimentante}";
                url += $"&cap={cap}";
                url += $"&codiceSoggettoAlimentante={codiceSoggettoAlimentante}";
                url += $"&indirizzo={indirizzo}";

                InsLog("SendFile", $"host: {RemotePath + url}");

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(RemotePath + url);
                httpWebRequest.ContentType = @"application/json; charset=utf-8";
                //httpWebRequest.Headers.Add("Authorization", "Bearer " + AccessToken);

                httpWebRequest.Method = "GET";

                using (var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    if (httpResponse != null && httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        using (var stream = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            using (var reader = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                var objText = reader.ReadToEnd();

                                InsLog("StateVerify", $"Response: {objText}");

                                result = JsonSerializer.Deserialize<ResultStateVerify>(objText);
                            }
                        }
                    }
                }
            }
            catch (Exception excep)
            {
                throw excep;
            }

            return result;
        }
    }
}
