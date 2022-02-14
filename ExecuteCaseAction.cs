using ark2020.crm.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace ark2020.crm.Plugins.Common
{
    public class ExecuteCaseAction : IPlugin
    {
        IOrganizationService service;
        IPluginExecutionContext context;
        Entity currentEntity;
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
                ExecuteAction();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }

        private void ExecuteAction()
        {
            var contactId = (string)context.InputParameters["requestAction"];
            Guid id = new Guid(contactId);

            var contact = service.Retrieve("contact", id, new ColumnSet("firstname", "mobilephone", "lastname", "emailaddress1", "new_d_balance"));
            var balance = contact["new_d_balance"];

            context.OutputParameters["responseAction"] = balance;
        }
    }
}
