using Microsoft.Xrm.Sdk;
using System; 

namespace ark2020.crm.Plugins.Common
{
    public static class EmailSender
    {
        public static void SendToCreatedContact(Entity reciever, IOrganizationService service, Guid initiatingUserId)
        {
            Entity email = new Entity("email");
            email["subject"] = "Testing Email Sender!";
            email["description"] = "Contact Successfully Created!";

            Entity to = new Entity("activityparty");
            to["partyid"] = new EntityReference("contact", reciever.Id);
            Entity[] sendTo = { to };
            email["to"] = sendTo;

            Entity from = new Entity("activityparty");
            from["partyid"] = new EntityReference("systemuser", initiatingUserId);
            Entity[] recieveFrom = { from };
            email["from"] = recieveFrom;

            service.Create(email);
        }

        public static void SendToUpdatedContact(Entity reciever, IOrganizationService service, Guid initiatingUserId)
        {
            Entity email = new Entity("email");
            email["subject"] = "Update Contact!";
            email["description"] = "Contact Successfully Updated!";

            Entity to = new Entity("activityparty");
            to["partyid"] = new EntityReference("contact", reciever.Id);
            Entity[] sendTo = { to };
            email["to"] = sendTo;

            Entity from = new Entity("activityparty");
            from["partyid"] = new EntityReference("systemuser", initiatingUserId);
            Entity[] recieveFrom = { from };
            email["from"] = recieveFrom;

            service.Create(email);
        }

        public static void ReplyAboutCreatedCase(Entity reciever, IOrganizationService service, Guid initiatingUserId)
        {
            var values = reciever.Attributes;
            var id = values["customerid"];

            EntityReference entity = (EntityReference)id;

            Entity email = new Entity("email");
            email["subject"] = "New Case Created!";
            email["description"] = "New Case Successfully Created!";

            Entity to = new Entity("activityparty");
            to["partyid"] = new EntityReference("contact", entity.Id);
            Entity[] sendTo = { to };
            email["to"] = sendTo;

            Entity from = new Entity("activityparty");
            from["partyid"] = new EntityReference("systemuser", initiatingUserId);
            Entity[] recieveFrom = { from };
            email["from"] = recieveFrom;

            service.Create(email);
        }
    }
}
