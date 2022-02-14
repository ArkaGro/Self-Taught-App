function actionCall(executionContext) {

    let incidentId = Xrm.Page.data.entity.getId().replace('{', '').replace('}', '');
    let contactId = Xrm.Page.getAttribute("customerid").getValue();
    let id = "";

    function executeAction(successCallback, errorCallBack) {

        let parameterTypes = {
            "entity": {
                typeName: "mscrm.incident",
                structuralProperty: 5
            },
            "requestAction": {
                "typeName": "Edm.String",
                "structuralProperty": 1
            }
        };


        let target = {};
        target.entityType = "incident";
        target.id = incidentId;

        let req = {};
        req.requestAction = id;
        req.entity = target;

        req.getMetadata = function () {
            return {
                boundParameter: "entity",
                parameterTypes: parameterTypes,
                operationType: 0,
                operationName: "new_CaseCallForAction"
            };
        };

        Xrm.WebApi.online.execute(req).then((result) => {
            if (result.ok) {
                result.json().then((data) => {
                    var response = data.responseAction;
                    successCallback(response);
                });
            }
            else
                errorCallBack(response);
        }, (error) => {
            errorCallBack(error);
        })
    }

    if (contactId != null) {
        id = contactId[0].id.replace('{', '').replace('}', '');

        executeAction((result) => {

            console.log(result);
        }, (error) => {
            console.log(error);
        });
    }
}