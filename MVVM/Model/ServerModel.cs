using System.Collections.Generic;

namespace MinkVPN.MVVM.Model
{
    internal class ServerModel
    {
        public string ID { get; }
        public string UserName { get; }
        public string Password { get; }
        public string Address { get; }
        public string CountryOp { get; }

        public ServerModel(string iD, string userName, string password, string address, string countryOp)
        {
            ID = iD;
            UserName = userName;
            Password = password;
            Address = address;
            CountryOp = countryOp;
        }
    }
}