using System;
using System.Collections.Generic;
using System.Text;

namespace FPTeLabLibarry.Settings
{
    public interface ISettings
    {
        int Repeat { get; }
        int RepeatResult { get; }
        string HISConnection { get; }
        string eLabConnection { get; }
        string[] SQLQueryHis { get; }
        string[] SQLQueryeLab { get; }
        bool ConnectHis_v2 { get; }
        string Token { get; }
        string UrlApi { get; }
        string UserName { get; }
        string BenhVien_Id { get; }
    }
}
