using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Http;

namespace SoapDemo2
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _address = "https://www.dataaccess.com/webservicesserver/numberconversion.wso";
        public string Address
        {
            get => _address; set
            {
                if (_address != value)
                {
                    _address = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _user = "user";
        public string User
        {
            get => _user; set
            {
                if (_user != value)
                {
                    _user = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _password = "password";
        public string Password
        {
            get => _password; set
            {
                if (_password != value)
                {
                    _password = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _mimeType = "application/soap+xml";
        public string MimeType
        {
            get => _mimeType; set
            {
                if (_mimeType != value)
                {
                    _mimeType = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _request = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:web=""http://www.dataaccess.com/webservicesserver/"">
   <soapenv:Header/>
   <soapenv:Body>
      <web:NumberToWords>
         <web:ubiNum>34</web:ubiNum>
      </web:NumberToWords>
   </soapenv:Body>
</soapenv:Envelope>";
        public string Request
        {
            get => _request; set
            {
                if (_request != value)
                {
                    _request = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _response = "Response SOAP XML";
        public string Response
        {
            get => _response; set
            {
                if (_response != value)
                {
                    _response = value;
                    RaisePropertyChanged();
                }
            }
        }
        public async Task SendRequest()
        {
            var data = new StringContent(Request, Encoding.UTF8, "application/soap+xml");
            string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                           .GetBytes(User + ":" + Password));

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

            var response = await client.PostAsync(Address, data);
            Response = await response.Content.ReadAsStringAsync();
        }
    }
}
