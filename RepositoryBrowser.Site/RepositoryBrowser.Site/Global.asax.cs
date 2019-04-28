using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RepositoryBrowser.IoC;
using RepositoryBrowser.Models;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace RepositoryBrowser.Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var settings = new Settings
            {
                GitHubBaseAddress = System.Configuration.ConfigurationManager.AppSettings["GitHubBaseAddress"]
            };

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            var dependencies = new Dependencies();
            dependencies.Register(container, settings);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
