using FPTeLabLibarry.Settings;

namespace FPTeLabService
{
    public class SettingsConfig
    {
        public ISettings ReSource { get; }
        public SettingsConfig()
        {
            ReSource = new Settings();
        }
    }
}
