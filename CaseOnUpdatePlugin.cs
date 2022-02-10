using ark2020.crm.Models;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;

namespace ark2020.crm.Plugins.Common
{
    public class CaseOnUpdatePlugin : IPlugin
    {
        IOrganizationService service;
        IPluginExecutionContext context;
        ITracingService trace;

        private void GetOrganizationService(IServiceProvider serviceProvider)
        {
            try
            {
                context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
                service = ((IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(context.UserId);
                trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }
        public void Execute(IServiceProvider serviceProvider)
        {
            try
            {
                GetOrganizationService(serviceProvider);
                UpdateCase();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }

        private void UpdateCase()
        {
            Entity incident = (Entity)context.InputParameters["Target"];

            IncidentResolution resolution = new IncidentResolution()
            {
                Subject = "Case Closed",
                IncidentId = new EntityReference(incident.LogicalName, incident.Id)
            };

            incident["statuscode"] = new OptionSetValue(5);
            var status = incident["statuscode"];

            CloseIncidentRequest close = new CloseIncidentRequest();
            close.IncidentResolution = resolution;

            close.Status = (OptionSetValue)status;

            service.Execute(close);
        }
    }
}
