using FPTeLabLibarry.Settings;

namespace FPTeLabService
{
    public class Settings : ISettings
    {
        public int Repeat { get; set; }
        public int RepeatResult { get; set; }
        public string HISConnection { get; set; }
        public string eLabConnection { get; set; }
        public string[] SQLQueryHis { get; set; }
        public string[] SQLQueryeLab { get; set; }
        public bool ConnectHis_v2 { get; set; }
        public string Token { get; set; }
        public string UrlApi { get; set; }
        public string UserName { get; set; }
        public string BenhVien_Id { get; set; }
    }
}
