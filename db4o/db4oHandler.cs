using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;

namespace db4o
{
    public class Db4oHandler
    {
        private IObjectContainer db;
        public void openDatabase(String path)
        {
            IEmbeddedConfiguration conf = Db4oEmbedded.NewConfiguration();
            db = Db4oEmbedded.OpenFile(conf, path);
        }
        public void close()
        {
            try
            {
                db.Commit(); //commit the last in-mem cached (not persisted) changes!
                db.Close();
            }
            catch (Exception e)
            {
                //exception handling here!
            }
        }
        public IObjectContainer getDb()
        {
            return db;
        }
    }
}