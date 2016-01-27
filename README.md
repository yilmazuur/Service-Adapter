# Service-Adapter
External Service Adapter that provides to access outer (non-restful) services with proxy classes. It aims to keep clean main application from services references or proxy classes. 

In the main application you can consume services as shown below:


integrationDefinition object comes from DB or wherever you want it to come.

var ws = ServiceIntegrator<ServiceClient>.Integrate(integrationDefinition);

