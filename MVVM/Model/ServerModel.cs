using System.Collections.Generic;

namespace MinkVPN.MVVM.Model
{
    internal class ServerModel
    {
        public string ID { get; }
        public string UserName { get; }
        public string Password { get; }
        public string ServerOp { get; }
        public string CountryOp { get; }

        public ServerModel(string iD, string userName, string password, string serverOp, string countryOp)
        {
            ID = iD;
            UserName = userName;
            Password = password;
            ServerOp = serverOp;
            CountryOp = countryOp;
        }
    }
}