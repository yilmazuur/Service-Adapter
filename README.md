# Service-Adapter
External Service Adapter that provides to access outer (non-restful) services with proxy classes. It aims to keep clean main application from services references or proxy classes. 


**In the main application you can consume service as shown below**

    var ws = ServiceIntegrator<ServiceClientFromProxyClass>.Integrate(integrationDefinition);

integrationDefinition object comes from DB or wherever you want it to come.
After creating the "ws" instance, you can set credentials, certificates or consume service methods in an ordinary way.

