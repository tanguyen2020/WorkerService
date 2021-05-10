using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using FPTeLabLibarry.Settings;

namespace FPTeLabLibarry.DataAccess
{
    public class XMLStatement : Connection
    {
        private ISettings settings;
        private readonly ILogger _logger;
        public XMLStatement(ISettings _settings, ILogger logger)
            : base(_settings, logger)
        {
            _logger = logger;
            settings = _settings;

        }

        public XMLStatement(string name, ISettings _settings, ILogger logger)
            : base(name, _settings, logger)
        {
        }
    }
    public class XMLStatementeLab : ConnectioneLab
    {
        private ISettings settings;
        private readonly ILogger _logger;
        public XMLStatementeLab(ISettings _settings, ILogger logger)
            : base(_settings, logger)
        {
            _logger = logger;
            settings = _settings;

        }
        public XMLStatementeLab(string name, ISettings _settings, ILogger logger)
            : base(name, _settings, logger)
        {
        }
    }
}
