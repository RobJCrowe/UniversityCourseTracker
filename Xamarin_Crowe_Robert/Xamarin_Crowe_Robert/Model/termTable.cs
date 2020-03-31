using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Crowe_Robert.Model
{
    public class termTable 
    {
        [PrimaryKey, AutoIncrement]
        public int termID { get; set; }

        [MaxLength(50)]
        public string termName { get; set; }
        public DateTime termStart { get; set; }
        public DateTime termEnd { get; set; }

        public static List<termTable> GetTerms()
        {
            try
            {
                using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
                {
                    context.CreateTable<termTable>();
                    List<termTable> terms = context.Table<termTable>().ToList();
                    return terms;
                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public static termTable GetTermID(int index)
        {
            try
            {
                using (SQLiteConnection context = new SQLiteConnection(App.DatabaseLocation))
                {
                    termTable tt = context.Get<termTable>(index);
                    return tt;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
