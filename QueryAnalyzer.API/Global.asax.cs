using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using QueryAnalyzer.API.Domain;
using QueryAnalyzer.Common;
using QueryAnalyzer.Interfaces;
using QueryAnalyzer.Modules.OData;

namespace QueryAnalyzer.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            // Web API configuration and services
            var container = new UnityContainer();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(container);

            container.RegisterType<CountriesRepository>(new ContainerControlledLifetimeManager());

            // Register the strategies for OData Parsing //
            Strategy.AddStrategy<IRegex>(new ODataRegexStrategy());
            Strategy.AddStrategy<IAnalyzerStrategy>(new AnalyzerStrategy());
            
            // Set the Default Module to the OData Module //
            var module = new ODataModule();
            Strategy.For<IAnalyzerStrategy>().Default = module;
        }
    }
}
