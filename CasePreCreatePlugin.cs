using Microsoft.Xrm.Sdk;
using System;

namespace ark2020.crm.Plugins.Common
{
    public class CasePreCreatePlugin : IPlugin
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
                SendEmailAboutCreatedCase();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }
        private void SendEmailAboutCreatedCase()
        {
            currentEntity = (Entity)context.InputParameters["Target"];

            try
            {
                EmailSender.ReplyAboutCreatedCase(currentEntity, service, context.InitiatingUserId);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }
    }
}
