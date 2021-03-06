﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using StructureMap;

namespace Customer.Project.Mvc.Instrumentation
{
    public class StructureMapWebApiDependencyResolver : IDependencyResolver 
    {
        public StructureMapWebApiDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsAbstract || serviceType.IsInterface)
            {
                return _container.TryGetInstance(serviceType);
            }
            else
            {
                return _container.GetInstance(serviceType);
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances<object>()
                .Where(s => s.GetType() == serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        private readonly IContainer _container;
        public void Dispose()
        {
            // When BeginScope returns 'this', the Dispose method must be a no-op.
        }
    }
}