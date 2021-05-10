using FPTeLabLibarry.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FPTeLabLibarry.DataAccess
{
    public abstract class Connection : IConnection
    {
        private readonly ILogger _logger;
        private static System.Xml.Serialization.XmlSerializer _serializer = new System.Xml.Serialization.XmlSerializer(typeof(StatementCollectionNode));
        private IDictionary<string, IStatement> _xmlDictionary = new Dictionary<string, IStatement>();
        protected ISettings settings;
        private string prefixdb;
        private string prefixConnectString;
        protected virtual string[] SQLQuery { get { return settings.SQLQueryHis; } }
        protected virtual string ConnectionString { get { return settings.HISConnection; } }
        public Connection(ISettings _settings, ILogger logger)
            : this(string.Empty, _settings, logger)
        {
            _logger = logger;
            settings = _settings;
            LoadStatements(SQLQuery);
        }
        public Connection(string name, ISettings _settings, ILogger logger)
        {
        }
        public IExecuteConnection CreateConnection()
        {
            try
            {
                IExecuteConnection executeConnection;
                string[] prefix = this.ConnectionString.Split(':');
                this.prefixdb = prefix[0].ToLower();
                this.prefixConnectString = prefix[1];

                switch (prefixdb)
                {
                    case prefixDbConnection.ORACLE:
                        executeConnection = new OracleHospital(this, prefixConnectString);
                        break;
                    default:
                        executeConnection = new SqleHospital(this, prefixConnectString);
                        break;
                }
                return executeConnection;
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException("Prefix wrong configuration.");
            }
        }
        protected void LoadStatements(string[] files)
        {
            foreach (string file in files)
            {
                string f = file?.Trim();
                AddStatements(f);
            }
        }
        public void AddStatements(string path)
        {
            string f = path?.Trim();

            if (string.IsNullOrEmpty(f))
                throw new ArgumentException("Invalid xml path!", nameof(path));

            if (!File.Exists(f))
                throw new FileNotFoundException("File not found!", path);

            using (FileStream filestream = new FileStream(f, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                AddStatements(filestream);
            }
        }
        public void AddStatements(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            StatementCollectionNode statements = null;
            using (StreamReader reader = new StreamReader(stream))
            {
                statements = (StatementCollectionNode)_serializer.Deserialize(reader);
            }

            foreach (var item in statements.Statements)
            {
                string id = item.Id?.Trim();
                _xmlDictionary.Add(id, Statement.CreateStatement(id, item.Text?.Trim()));
            }
        }
        public IStatement GetStatement(string id)
        {
            Debug.Assert(!string.IsNullOrEmpty(id));

            try
            {
                return _xmlDictionary[id];
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException($"Statement '{id}' not found!");
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
        }
    }
    public static class prefixDbConnection
    {
        public const string MSSQL = "MSSQL";
        public const string ORACLE = "ORACLE";
        public const string SQLITE = "SQLITE";
    }
    [System.Diagnostics.DebuggerDisplay("{Id}")]
    public class SqlStatementNode
    {
        [System.Xml.Serialization.XmlAttribute("id")]
        public string Id { get; set; }

        [System.Xml.Serialization.XmlText]
        public string Text { get; set; }
    }


    [System.Xml.Serialization.XmlRoot("Statements")]
    public class StatementCollectionNode
    {
        [System.Xml.Serialization.XmlElement("Statement", typeof(SqlStatementNode))]
        public SqlStatementNode[] Statements { get; set; }
    }
}
