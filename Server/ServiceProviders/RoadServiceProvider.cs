///////////////////////////////////////////////////////////
//  RoadServiceProvider.cs
//  Implementation of the Class RoadServiceProvider
//  Generated by Enterprise Architect
//  Created on:      30-Jul-2020 3:54:53 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.ServiceModel;
using Server.Contracts;
using System.Data.Entity;
using Common.Contracts;
using Common.DomainModels;

namespace Server.ServiceProviders
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RoadServiceProvider : IRoadService
    {

        private ILogging logger;
        private IDatabaseContextFactory factory;

        /// 
        /// <param name="logger"></param>
        /// <param name="roadRepository"></param>
        public RoadServiceProvider(ILogging logger, IDatabaseContextFactory factory)
        {

            this.logger = logger;
            this.factory = factory;
        }

        /// 
        /// <param name="road"></param>
        public Road AddRoad(Road road)
        {

            try
            {
                logger.LogNewMessage($"Adding new road to database", LogType.INFO);
                using (var dbContext = factory.GetContext())
                {
                    foreach (Station s in road.Stations)
                    {
                        dbContext.Stations.Attach(s);
                    }
                    dbContext.Roads.Add(road);
                    dbContext.SaveChanges();
                    logger.LogNewMessage($"Road added.", LogType.INFO);
                    return road;
                }
                
            }
            catch (Exception ex)
            {
                logger.LogNewMessage($"Error occured, couldn't add entity. Error message {ex.Message}", LogType.ERROR);
                throw new FaultException(ex.Message);
            }

        }

        /// 
        /// <param name="road"></param>
        public Road CloneRoad(Road road)
        {
            try
            {
                logger.LogNewMessage($"Cloning road with name {road.Name} from database..", LogType.INFO);
                using (var dbContext = factory.GetContext())
                {
                    foreach (Station s in road.Stations)
                    {
                        dbContext.Stations.Attach(s);
                    }
                    road.Id = 0;
                    dbContext.Roads.Add(road);
                    dbContext.SaveChanges();
                    logger.LogNewMessage($"Road cloned.", LogType.INFO);

                    return road;
                }

            }
            catch (Exception ex)
            {
                logger.LogNewMessage($"Error occured, road couldn't be cloned. Error message {ex.Message}", LogType.ERROR);
                throw new FaultException(ex.Message);
            }
        }

        /// 
        /// <param name="id"></param>
        public bool DeleteRoad(int roadID)
        {

            try
            {
                logger.LogNewMessage($"Deleting road with id {roadID} from database..", LogType.INFO);
                using (var dbContext = factory.GetContext())
                {
                    Road roadToDelete = new Road() { Id = roadID };
                    dbContext.Roads.Attach(roadToDelete);
                    dbContext.Roads.Remove(roadToDelete);
                    dbContext.SaveChanges();
                }
                logger.LogNewMessage($"Road deleted.", LogType.INFO);
                return true;

            }
            catch (Exception ex)
            {
                logger.LogNewMessage($"Error occured, road couldn't be deleted. Error message {ex.Message}", LogType.ERROR);
                throw new FaultException(ex.Message);
            }
        }

        public List<Road> GetAllRoads()
        {
            try
            {
                logger.LogNewMessage($"Getting all roads from database..", LogType.INFO);
                using (var dbContext = factory.GetContext())
                {
                    return dbContext.Roads.Include("Stations").ToList();
                }

            }
            catch (Exception ex)
            {
                logger.LogNewMessage($"Error occured, message {ex.Message}", LogType.ERROR);
                throw new FaultException(ex.Message);
            }

        }

        /// 
        /// <param name="newData"></param>
        public bool UpdateRoad(Road newData)
        {

            try
            {
                logger.LogNewMessage($"Updating data for road with id {newData.Id}.", LogType.INFO);
                using (var dbContext = factory.GetContext())
                {
                    foreach (Station s in newData.Stations) s.Tracks = null; //Has to be done to avoid database integrity exceptions because of detached state
                    var road = dbContext.Roads.Where(p => p.Id == newData.Id)
                                                .Include(p => p.Stations)
                                                .SingleOrDefault();
                    dbContext.Entry(road).CurrentValues.SetValues(newData);

                    // Delete children
                    foreach (var existingStation in road.Stations.ToList())
                    {
                        if (!newData.Stations.Any(c => c.Id == existingStation.Id))
                            road.Stations.Remove(existingStation);
                    }
                    //Add children
                    foreach (var station in newData.Stations)
                    {
                        if (!road.Stations.Any(x => x.Id == station.Id))
                        {
                            dbContext.Stations.Attach(station);
                            road.Stations.Add(station);
                        }
                    }

                    dbContext.SaveChanges();
                }
                logger.LogNewMessage($"Update successful", LogType.INFO);
                return true;
            }
            catch (Exception ex)
            {

                logger.LogNewMessage($"Error occured updating road  message:\n {ex.Message}", LogType.ERROR);
                throw new FaultException(ex.Message);
            }

        }

        public Road GetRoadById(int id)
        {
            try
            {
                logger.LogNewMessage($"Getting road with id {id} from database..", LogType.INFO);
                using (var dbContext = factory.GetContext())
                {
                    var road = dbContext.Roads.Where(x => x.Id == id).Include("Stations").SingleOrDefault();
                    logger.LogNewMessage($"Road retrieved.", LogType.INFO);
                    return road;
                }

            }
            catch (Exception ex)
            {
                logger.LogNewMessage($"Error occured, message {ex.Message}", LogType.ERROR);
                throw new FaultException(ex.Message);
            }
        }

        public List<Road> SearchRoads(string name, string label)
        {
            try
            {
                logger.LogNewMessage($"Searching for roads..", LogType.INFO);
                bool shouldFilterName = name != "";
                bool shouldFilterLabel = label != "";
                using (var dbContext = factory.GetContext())
                {
                    IQueryable<Road> result = null;
                    if (shouldFilterName) result = dbContext.Roads.Where(x => x.Name == name);
                    if (shouldFilterLabel && result != null)
                    {
                        result = result.Where(x => x.Label == label);
                    }
                    else if (shouldFilterLabel)
                    {
                        result = dbContext.Roads.Where(x => x.Label == label);
                    }

                    return result.Include("Stations").ToList();
                }

            }
            catch (Exception ex)
            {
                logger.LogNewMessage($"Error occured, message {ex.Message}", LogType.ERROR);
                throw new FaultException(ex.Message);
            }
        }
    }//end RoadServiceProvider
}