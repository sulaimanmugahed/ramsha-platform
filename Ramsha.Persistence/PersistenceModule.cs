using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Domain;
using Ramsha.EntityFrameworkCore.PostgreSql;

namespace Ramsha.Persistence
{
    public class PersistenceModule : RamshaModule
    {
        public override void Register(RegisterContext context)
        {
            base.Register(context);

            context
            .DependsOn<DomainModule>()
            .DependsOn<EntityFrameworkCorePostgreSqlModule>();
        }

        public override void BuildServices(BuildServicesContext context)
        {
            base.BuildServices(context);

            context.Services.AddPersistenceInfrastructure(context.Configuration);
        }
    }
}