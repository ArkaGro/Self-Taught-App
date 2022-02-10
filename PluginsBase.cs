using Microsoft.Xrm.Sdk;
using System;

namespace ark2020.crm.Plugins.Common
{
    public class PluginsBase: IPlugin
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
                SendEmailToCreatedContact();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }

        private void SendEmailToCreatedContact()
        {
            currentEntity = (Entity)context.InputParameters["Target"];

            try
            {
                EmailSender.SendToCreatedContact(currentEntity, service, context.InitiatingUserId);
            }
            catch(Exception ex)
            {
                Console.Write(ex);
            }
        }
    }
}
