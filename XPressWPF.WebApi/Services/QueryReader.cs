using System;
using System.IO;

namespace XPressWPF.WebApi.Services
{
    public interface IQueryReader
    {
        string GetQueryFromSqlFolderWithSqlExtension(string query);
    }

    internal class QueryReader : IQueryReader
    {
        //TODO implement exception handling
        public string GetQueryFromSqlFolderWithSqlExtension(string query)
        {
            string pathToSqlFolder = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            return File.ReadAllText(pathToSqlFolder + "\\SQL\\" + query + ".sql");
        }
    }
}
