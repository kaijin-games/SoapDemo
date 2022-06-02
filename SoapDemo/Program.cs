using System;
using System.Text;
using System.Net;

namespace SoapDemo
{
    class Program
    {
        [STAThread]
        public static void Main()
        {
            var task = RequestWithSoap();
            task.Wait();
        }

        public static async Task RequestWithSoap()
        {
            // https://www.soapui.org/getting-started/soap-test/
            var url = "https://www.dataaccess.com/webservicesserver/numberconversion.wso";
            var soapRequest = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:web=""http://www.dataaccess.com/webservicesserver/"">
   <soapenv:Header/>
   <soapenv:Body>
      <web:NumberToWords>
         <web:ubiNum>34</web:ubiNum>
      </web:NumberToWords>
   </soapenv:Body>
</soapenv:Envelope>";


            var data = new StringContent(soapRequest, Encoding.UTF8, "application/soap+xml");

            var username = "abc";
            var password = "123";
            string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                           .GetBytes(username + ":" + password));

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

            var response = await client.PostAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }
    }
}
