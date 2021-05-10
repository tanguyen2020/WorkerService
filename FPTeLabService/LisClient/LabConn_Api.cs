using FPTeLabLibarry.Settings;
using FPT.eHospital.LISClient;

namespace FPTeLabService
{
    public static class LabConn_Api
    {
        public static LabConnection Execute(ISettings settings)
        {
            return new LabConnection(settings.Token, "token", settings.UrlApi, settings.BenhVien_Id, settings.UserName);
        }
    }
}
