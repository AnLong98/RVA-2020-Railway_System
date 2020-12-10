///////////////////////////////////////////////////////////
//  IRoadService.cs
//  Implementation of the Interface IRoadService
//  Generated by Enterprise Architect
//  Created on:      30-Jul-2020 3:49:01 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ServiceModel;
using Common.DomainModels;

namespace Common.Contracts
{

    [ServiceContract]
    public interface IRoadService
    {

        [OperationContract]
        /// 
        /// <param name="road"></param>
        Road AddRoad(Road road);

        [OperationContract]
        /// 
        /// <param name="id"></param>
        Road GetRoadById(int id);

        [OperationContract]
        /// 
        /// <param name="road"></param>
        Road CloneRoad(Road road);

        [OperationContract]
        /// 
        /// <param name="road"></param>
        bool DeleteRoad(int roadID);


        [OperationContract]
        /// 
        /// <param name="name"></param>
        /// <param name="label"></param>
        List<Road> SearchRoads(string name, string label);

        [OperationContract]
        List<Road> GetAllRoads();

        [OperationContract]
        /// 
        /// <param name="newData"></param>
        bool UpdateRoad(Road newData);
    }//end IRoadService
}