///////////////////////////////////////////////////////////
//  IUserService.cs
//  Implementation of the Interface IUserService
//  Generated by Enterprise Architect
//  Created on:      30-Jul-2020 3:49:02 PM
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
    public interface IUserService
    {

        [OperationContract]
        /// 
        /// <param name="user"></param>
        bool AddUser(User user);

        [OperationContract]
        List<User> GetAllUsers();

        [OperationContract]
        /// 
        /// <param name="name"></param>
        User FindUserByUserName(string name);

        [OperationContract]
        /// 
        /// <param name="id"></param>
        User GetUserData(int id);

        [OperationContract]
        /// 
        /// <param name="id"></param>
        /// <param name="newName"></param>
        /// <param name="newLastName"></param>
        bool UpdateUser(int id, string newName, string newLastName);

    }//end IUserService
}