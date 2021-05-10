using FPTeLabLibarry.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FPTeLabLibarry.DataAccess
{
    public abstract class ConnectioneLab : Connection
    {
        private IDictionary<string, IStatement> _xmlDictionary = new Dictionary<string, IStatement>();
        protected override string[] SQLQuery { get { return settings.SQLQueryeLab; } }
        protected override string ConnectionString { get { return settings.eLabConnection; } }
        public ConnectioneLab(ISettings _settings, ILogger logger)
            : this(string.Empty, _settings, logger)
        {
            
        }
        public ConnectioneLab(string name, ISettings _settings, ILogger logger): base(_settings, logger)
        {
        }
    }
}
