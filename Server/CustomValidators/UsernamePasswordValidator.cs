using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Selectors;
using System.ServiceModel;
using Server.Contracts;
using System.Data.Entity;
using Common.Contracts;
using Common.DomainModels;

namespace Server.CustomValidators
{
    public class UsernamePasswordValidator : UserNamePasswordValidator
    {
        private ILogging logger;
        private IDatabaseContextFactory factory;

        public UsernamePasswordValidator(IDatabaseContextFactory factory, ILogging logger)
        {
            this.logger = logger;
            this.factory = factory;
        }

        public override void Validate(string userName, string password)
        {
            try
            {
                logger.LogNewMessage($"Validating username and password for user {userName}", LogType.INFO);

                using (var dbContext = factory.GetContext())
                {
                    var user = dbContext.Users.Where(x => x.UserName == userName).SingleOrDefault();

                    if (user == null || user == new User())
                    {
                        var errorMessage = $"User with username {userName} does not exist in database. Access denied.";
                        logger.LogNewMessage(errorMessage, LogType.WARNING);
                        throw new FaultException(errorMessage);
                    }

                    if (!user.CheckPassword(password))
                    {
                        var errorMessage = $"Wrong password for user  {userName}. Access denied.";
                        logger.LogNewMessage(errorMessage, LogType.WARNING);
                        throw new FaultException(errorMessage);
                    }
                }

                logger.LogNewMessage($"User {userName} authenticated.", LogType.INFO);

            }catch(Exception ex)
            {
                logger.LogNewMessage($"Authentication exception. Error {ex.Message}", LogType.ERROR);
                throw new FaultException(ex.Message);
            }
        }
    }
}
