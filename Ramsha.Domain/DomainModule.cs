using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Common.Domain;

namespace Ramsha.Domain
{
    public class DomainModule : RamshaModule
    {
        public override void Register(RegisterContext context)
        {
            base.Register(context);
            context.DependsOn<CommonDomainModule>();
        }
    }
}