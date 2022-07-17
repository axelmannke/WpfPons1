using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mk.DBConnector;
using CommonTypes;

namespace Models
{
    public static class DbModel
    {
        internal static DBAdapter db;
        static DbModel()
        {
            //ToDo: dbuser, dbpasswd ersetzen
            db = new DBAdapter(DatabaseType.MySql, Instance.Singleton, "localhost", 3306, "pons", "dbuser", "dbpasswd", "pons.log");
            db.Adapter.LogFile = true;
        }

        /// <summary>
        /// Eigentlich sollte im Model der Typ PonsTranslation verwendet werden.
        /// Dieser Typ wird zwischen Model und ViewModel ausgetauscht
        /// </summary>
        /// <param name="ponslst"></param>
        /// <returns></returns>
        public static long InsertTranslation(List<PonsTranslation> ponslst)
        {
            //ToDo: Implementierung ponslst in Datenbank
            return 0;
        }


        /// <summary>
        /// Nur als Beispiel zur Verwendung mk.dbconnector
        /// </summary>
        /// <param name="word"></param>
        /// <param name="translation"></param>
        /// <returns></returns>
        public static long InsertWord(string word, string translation)
        {
            //ToDo: Wort nur in Datenbank, falls noch nicht vorhanden 
            string sql = string.Format("INSERT INTO words (word, translation) VALUES ({0}WORD, {0}TRANSLATION)", db.Adapter.ParameterTag);
            
            List<Parameter> plst = new List<Parameter>();
            plst.Add(new Parameter("WORD", word));
            plst.Add(new Parameter("TRANSLATION", translation));

            long id = db.Adapter.InsertParameters(sql, plst);

            return id;
        }
    }
}
