using System.Security.Cryptography.X509Certificates;

namespace aspnet_core_demodotcomsite.Helpers;

public static class CertificateHelper
{

    public static X509Certificate2 GetServiceCertificate(string certificateName)
    {
        try
        {
            var certificateStore = new X509Store(StoreLocation.LocalMachine);
            certificateStore.Open(OpenFlags.OpenExistingOnly);
            var certificateCollection = certificateStore.Certificates.Find(X509FindType.FindBySubjectName, certificateName, true);
            return certificateCollection[0];
        }
        catch (Exception ex)
        {
            throw new Exception($"Service certificate {certificateName} not found!", ex);
        }
    }
}