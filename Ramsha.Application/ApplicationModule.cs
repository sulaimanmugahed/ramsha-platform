using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Common.Application;
using Ramsha.Domain;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application
{
    public class ApplicationModule : RamshaModule
    {
        public override void Register(RegisterContext context)
        {
            base.Register(context);

            context
            .DependsOn<DomainModule>()
            .DependsOn<CommonApplicationModule>();
        }

        public override void Prepare(PrepareContext context)
        {
            base.Prepare(context);
            context.PrepareOptions<LocalMessagingOptions>(options =>
            {
                options.AddMessagesFromAssembly<ApplicationModule>();
            });
        }

        public override void BuildServices(BuildServicesContext context)
        {
            base.BuildServices(context);
            context.Services.AddApplicationLayer(context.Configuration);
        }
    }
}