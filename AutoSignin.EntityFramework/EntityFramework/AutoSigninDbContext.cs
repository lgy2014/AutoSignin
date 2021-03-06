﻿using System.Data.Entity;
using Abp.EntityFramework;
using AutoSignin.People;

namespace AutoSignin.EntityFramework
{
    public class AutoSigninDbContext : AbpDbContext
    {
        public virtual IDbSet<Person> People { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public AutoSigninDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in AutoSigninDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of AutoSigninDbContext since ABP automatically handles it.
         */
        public AutoSigninDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
    }
}
