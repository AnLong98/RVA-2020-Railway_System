using Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Contracts
{
    public interface IRailwayServiceProxyCreationFacade
    {
        IUserService GetUserServiceProxy(string username, string password);
        ICountryService GetCountryServiceProxy(string username, string password);
        IPlaceService GetPlaceServiceProxy(string username, string password);
        ITrackService GetTrackServiceProxy(string username, string password);
        IStationService GetStationServiceProxy(string username, string password);
        IRoadService GetRoadServiceProxy(string username, string password);
    }
}
