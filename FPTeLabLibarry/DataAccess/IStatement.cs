using System;
using System.Collections.Generic;
using System.Text;

namespace FPTeLabLibarry.DataAccess
{
    public interface IStatement
    {
        string Name { get; }
        string GetCommandText(object parameter);
    }
    public class Statement : IStatement
    {
        public string _text;
        public string Name { get; }
        public Statement(string id, string text)
        {
            _text = text;
            Name = id;
        }
        public string GetCommandText(object parameter)
        {
            return _text;
        }
        public static Statement CreateStatement(string id, string text)
        {
            return new Statement(id, text);
        }
    }
}
