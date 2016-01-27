using System;
using System.ServiceModel;
using System.Text;

namespace ExternalServiceAdapter
{
    /// <summary>
    /// Farklı Integratorlar(örn: ServiceIntegrator) kullanarak farklı sistemlere entegre olurken kodun çağırıldığı 
    /// yerde farklı constructorlar kullanmak yerine generic tek tip kullanım olması için oluşturulan factory nesnesidir.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AdapterFactory<T> where T : class
    {
        /// <summary>
        /// WSDL.EXE PROXY CLASSI İLE UYUMLU ÇALIŞIR
        /// AdapterFactory<T> şeklinde verilen T'nin tipinde bir obje oluşturur.
        /// Örn: Service'e bağlanılacaksa servis, başka bir bağlantı tipi kullanılacaksa onun objesi oluşturulur
        /// wsdl.exe ile üretilen proxy classları ile uyumludur
        /// </summary>
        /// <returns></returns>
        public static T CreateAdapter(string url)
        {
            try
            {
                Type objType = typeof(T);
                if (objType == null)
                {
                    return default(T);
                }

                // Böylelikle bağlanılacak external ortamın dev-test-prod environmentlarına erişmek için sadece DB'deki IP bilgisi değiştirilmesi yeterli olacak.
                // Kodda değişiklik gerekmeyecek. Örn: DB'de IntegrationDefinition tablosunda servis urlsi değiştirilir.
                var adapter = Activator.CreateInstance(objType, url);

                return (T)adapter;
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// WSDL.EXE PROXY CLASSI İLE UYUMLU ÇALIŞIR
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T CreateAdapter(string url, string encodedIdentity, string messageCredentialType)
        {
            try
            { 
                Type objType = typeof(T);
                if (objType == null)
                {
                    return default(T);
                }
                WSHttpBinding binding = new WSHttpBinding();
                binding.Security.Message.ClientCredentialType = setMessageCredentialType(messageCredentialType);
                EndpointAddress endpoint = null;

                if(!string.IsNullOrEmpty(url))
                    endpoint = new EndpointAddress(new Uri(url));

                if(!string.IsNullOrEmpty(encodedIdentity))
                    endpoint = setEndpointAddress(url, encodedIdentity);

                if (!string.IsNullOrEmpty(messageCredentialType) && endpoint != null)
                {
                    var adapter = Activator.CreateInstance(objType, binding, endpoint);
                    return (T)adapter;
                }
                else if (endpoint != null)
                {
                    var adapter = Activator.CreateInstance(objType, endpoint);
                    return (T)adapter;
                }
                else
                {
                    var adapter = Activator.CreateInstance(objType);
                    return (T)adapter;
                }
            }
            catch
            {
                return default(T);
            }
        }

        private static EndpointAddress setEndpointAddress(string url, string encodedIdentity)
        {
            EndpointIdentity identity = EndpointIdentity.CreateX509CertificateIdentity(new System.Security.Cryptography.X509Certificates.X509Certificate2(Encoding.UTF8.GetBytes(encodedIdentity)));
            return new EndpointAddress(new Uri(url), identity);
        }

        private static MessageCredentialType setMessageCredentialType(string messageCredentialType)
        {
            if (messageCredentialType.Equals("UserName")) 
            {
                return MessageCredentialType.UserName;
            }
            else if (messageCredentialType.Equals("Certificate")) 
            {
                return MessageCredentialType.Certificate;
            }
            else if (messageCredentialType.Equals("IssuedToken"))
            {
                return MessageCredentialType.IssuedToken;
            }
            else if (messageCredentialType.Equals("Windows"))
            {
                return MessageCredentialType.Windows;
            }
            return MessageCredentialType.None;
        }
    }
}
