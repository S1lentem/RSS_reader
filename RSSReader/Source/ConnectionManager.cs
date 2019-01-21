using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace RSSReader.Source
{
    enum TypeConnection
    {
        NotСonnection,
        WiFi,
        Mobile
    }

    static class ConnectionManager
    {
        public static TypeConnection GetCurrentConnection()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                var internetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
                if (internetConnectionProfile != null)
                {
                    if (internetConnectionProfile.IsWlanConnectionProfile)
                    {
                        return TypeConnection.WiFi;
                    }
                    else if (internetConnectionProfile.IsWwanConnectionProfile)
                    {
                        return TypeConnection.Mobile;
                    }
                }
            }
            return TypeConnection.NotСonnection;
        }
    }
}
