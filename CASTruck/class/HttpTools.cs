using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.IO;


namespace CASTruck
{
    class HttpTools
    {
        private string addressServer;
        private string service;
        private string result;

        private Dictionary<string, object> data = new Dictionary<string, object>();

        public string AddressServer
        {
            get
            {
                return addressServer;
            }

            set
            {
                addressServer = value;
            }
        }

        public string Service
        {
            get
            {
                return service;
            }

            set
            {
                service = value;
            }
        }

        public Dictionary<string, object> Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        public string Result
        {
            get
            {
                return result;
            }

            set
            {
                result = value;
            }
        }

        public void sendServerWeb()
        {
            try
            {
                
                var request = HttpWebRequest.Create("http://cas-bo.com/vias/insert_data.php") as HttpWebRequest;

                //var request = HttpWebRequest.Create("http://192.168.0.100:8888/vias/insert_data.php") as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/json";
                request.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), request);
            }
            catch(WebException we)
            {
                Console.WriteLine(we.Message);
            }
           

        }

        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

                Stream postStream = request.EndGetRequestStream(asynchronousResult);
                string postData = JsonConvert.SerializeObject(data, Formatting.None);
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                postStream.Write(byteArray, 0, byteArray.Length);
                postStream.Close();

                request.BeginGetResponse(new AsyncCallback(GetResponceStreamCallback), request);
            }
            catch(WebException we)
            {
                Console.WriteLine(we.Message);
            }
            
           
        }

        void GetResponceStreamCallback(IAsyncResult callbackResult)
        {
            HttpWebRequest request = (HttpWebRequest)callbackResult.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(callbackResult);
            using (StreamReader httpWebStreamReader = new StreamReader(response.GetResponseStream()))
            {
                result = httpWebStreamReader.ReadToEnd();
                Console.WriteLine(result);
            }

        }
    }
}
