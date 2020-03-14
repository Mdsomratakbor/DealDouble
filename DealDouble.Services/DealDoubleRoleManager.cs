﻿using DealDouble.Database;
using DealDouble.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Services
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class DealDoubleRoleManager : RoleManager<IdentityRole>
    {
        public DealDoubleRoleManager(IRoleStore<IdentityRole, string> roleStore): base(roleStore)
        {
        }

        public static DealDoubleRoleManager Create(IdentityFactoryOptions<DealDoubleRoleManager> options, IOwinContext context)
        {
            var manager = new DealDoubleRoleManager(new RoleStore<IdentityRole>(context.Get<Context>()));
          
            return manager;
        }
    }

}
