using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Client.Contracts;
using Common.Contracts;

namespace Client.Connectors
{
    public class RailwayServiceConnector : IRailwayServiceProxyCreationFacade
    {
        private string userEndpoint = "http://localhost:18000/IUserService";
        private string roadEndpoint = "http://localhost:18000/IRoadService";
        private string stationEndpoint = "http://localhost:18000/IStationService";
        private string trackEndpoint = "http://localhost:18000/ITrackService";
        private string placeEndpoint = "http://localhost:18000/IPlaceService";
        private string countryEndpoint = "http://localhost:18000/ICountryService";
        private Binding binding;

        public RailwayServiceConnector()
        {
            binding = GetRailwayServiceBinding();
        }


        public string UserEndpoint { get => userEndpoint; set => userEndpoint = value; }
        public string RoadEndpoint { get => roadEndpoint; set => roadEndpoint = value; }
        public string StationEndpoint { get => stationEndpoint; set => stationEndpoint = value; }
        public string TrackEndpoint { get => trackEndpoint; set => trackEndpoint = value; }
        public string PlaceEndpoint { get => placeEndpoint; set => placeEndpoint = value; }
        public string CountryEndpoint { get => countryEndpoint; set => countryEndpoint = value; }

        public IUserService GetUserServiceProxy(string username, string password)
        {
            ChannelFactory<IUserService> channelFactory = new ChannelFactory<IUserService>(binding, userEndpoint);
            channelFactory.Credentials.UserName.UserName = username;
            channelFactory.Credentials.UserName.Password = password;
            return channelFactory.CreateChannel();
        }

        public IRoadService GetRoadServiceProxy(string username, string password)
        {
            ChannelFactory<IRoadService> channelFactory = new ChannelFactory<IRoadService>(binding, roadEndpoint);
            channelFactory.Credentials.UserName.UserName = username;
            channelFactory.Credentials.UserName.Password = password;
            return channelFactory.CreateChannel();
        }

        public IStationService GetStationServiceProxy(string username, string password)
        {
            ChannelFactory<IStationService> channelFactory = new ChannelFactory<IStationService>(binding, stationEndpoint);
            channelFactory.Credentials.UserName.UserName = username;
            channelFactory.Credentials.UserName.Password = password;
            return channelFactory.CreateChannel();
        }

        public ITrackService GetTrackServiceProxy(string username, string password)
        {
            ChannelFactory<ITrackService> channelFactory = new ChannelFactory<ITrackService>(binding, trackEndpoint);
            channelFactory.Credentials.UserName.UserName = username;
            channelFactory.Credentials.UserName.Password = password;
            return channelFactory.CreateChannel();
        }

        public IPlaceService GetPlaceServiceProxy(string username, string password)
        {
            ChannelFactory<IPlaceService> channelFactory = new ChannelFactory<IPlaceService>(binding, placeEndpoint);
            channelFactory.Credentials.UserName.UserName = username;
            channelFactory.Credentials.UserName.Password = password;
            return channelFactory.CreateChannel();
        }

        public ICountryService GetCountryServiceProxy(string username, string password)
        {
            ChannelFactory<ICountryService> channelFactory = new ChannelFactory<ICountryService>(binding, countryEndpoint);
            channelFactory.Credentials.UserName.UserName = username;
            channelFactory.Credentials.UserName.Password = password;
            return channelFactory.CreateChannel();
        }


        private Binding GetRailwayServiceBinding()
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            return basicHttpBinding;
        }

    }
}
