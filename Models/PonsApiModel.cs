using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Newtonsoft.Json;
using CommonTypes;

namespace Models
{
    public class PonsApiModel
    {
        /// <summary>
        /// Make request and obtain request results
        /// </summary> 
        public string apiRequest(string termToLookUp, string languageCode)
        {
            //ToDo: pons user, password, secret angeben
            string user = "_________";
            string password = "___________";

            // Create a request for the URL.   

            //ToDo: Hier wird eine Exception ausgelöst. Weshalb? Lösung? Weshalb hat das 2018 noch funktioniert???        
            //siehe https://andydunkel.net/2020/08/03/c-es-konnte-kein-geschuetzter-ssl-tls-kanal-erstellt-werden/
            //siehe https://www.bsi.bund.de/SharedDocs/Downloads/DE/BSI/Mindeststandards/Archivdokumente/Migrationsleitfaden_Mindeststandard_BSI_TLS_Version_1_2.pdf?__blob=publicationFile&v=1                        

            string endPoint = "https://api.pons.com/v1/dictionary";
            string query = endPoint + "?q=" + termToLookUp + "&l=" + languageCode;

            // Do request             
            WebRequest request = WebRequest.Create(query);            

            //Den Secret erhält man beim Freischalten seines Pons-Accounts
            request.Headers.Add("X-Secret: _____________________________");


            // Set the credentials  
            request.Credentials = new NetworkCredential(user, password);

            try
            {
                // Get the response
                WebResponse response = request.GetResponse();
                
                // Get the stream containing content returned by the server  
                Stream dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access 
                StreamReader reader = new StreamReader(dataStream);

                // Read the content  
                string responseFromServer = reader.ReadToEnd();

                // Clean up the streams and the response.  
                reader.Close();
                response.Close();

                CreateJsonFile(responseFromServer);

                //if successfull return 0
                return responseFromServer;
            }
            catch (Exception e)
            {               
                return null;
            }
        }        

        /// <summary>
        /// Nur zum Erzeugen eindeutiger Dateinamen, damit man die Json-Dateien die vom Pons geliefert werden
        /// auch anschauen kann. Eigentich überflüssig.
        /// </summary>
        /// <param name="json"></param>
        private void CreateJsonFile(string json)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(DateTime.Now.ToString()));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            if (!Directory.Exists(@"json\{0}.json")) Directory.CreateDirectory(@"json\{0}.json");

            File.WriteAllText(string.Format(@"json\{0}.json", Sb.ToString()), json);
        }


        /// <summary>
        /// Erzeugt aus der Json-Datei Objekte
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public List<PonsTranslation> Parse(string json)
        {
            List<PonsTranslation> ptlst = JsonConvert.DeserializeObject<List<PonsTranslation>>(json);

            foreach(PonsTranslation pt in ptlst)
            {
                foreach(Hits h in pt.hits)
                {
                    foreach(Roms r in h.roms)
                    {
                        foreach(Arabs a in r.arabs)
                        {
                            //Wie man in den json-Datein sieht, sind diese Attribute im HTML-Format.
                            //Das muss weg, wir brauchen plain text
                            //ToDo: HTML raus, Methode Strip verwenden
                            

                        }
                    }
                }
            }

            return ptlst;
        }

        public string Strip(string HTML)
        {
            string html = StripHTML(HTML);

            html = StripHTMLSpecialChars(html);

            return html;
        }

        public string StripHTML(string HTML)
        {
            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            return reg.Replace(HTML, "");
        }

        public string StripHTMLSpecialChars(string HTML)
        {
            string html = "";

            html = HTML.Replace("&lt", "");
            html = html.Replace("&gt", "");
            html = html.Replace("\n", "");
            html = html.Replace("\r", "");

            return html;
        }
    }
}
