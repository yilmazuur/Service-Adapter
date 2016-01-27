using ExternalServiceAdapter.Interfaces;
using Newtonsoft.Json;

namespace ExternalServiceAdapter
{
    /// <summary>
    /// Verilen identifier bilgisine göre db'den servis bilgisini bulup, servis objesini geri döner.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceIntegrator<T> where T : class
    {
        public static T Integrate(IIntegrationDefinition integrationDefinition)
        {
            try
            {
                //generic servis tipi T farklı servislere bağlanmak için gerekli, integrationIdentifier o servisin farklı ortamlardaki urline bağlanırken kod değişikliği yapmamak için gerekli
                ServiceParameter parameter = parseParameters(integrationDefinition.IntegrationParams);
                if (parameter != null)
                {
                    return AdapterFactory<T>.CreateAdapter(parameter.Url, parameter.Certificate, parameter.MessageCredentialType);
                }
                return AdapterFactory<T>.CreateAdapter(integrationDefinition.IntegrationParams);
            }
            catch
            {
                return default(T);
            }
        }

        private static ServiceParameter parseParameters(string integrationParams)
        {
            try
            {
                return JsonConvert.DeserializeObject<ServiceParameter>(integrationParams);
            }
            catch 
            {
                return null;
            }
        }
    }
}
