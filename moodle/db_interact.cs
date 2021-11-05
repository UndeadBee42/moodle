using System;
using System.Collections.Generic;
using System.Linq;
namespace db_interact
{
    public interface db_interact
    {
        /*
         интерфейс
         */
        void connect();
        Dictionary<string, string> get_settings();
        void set_settings(Dictionary<string, string> s);
        // добавить пользователей
    }
    public class sql_server : db_interact
    {
        public sql_server() { _setting = new Dictionary<string, string>(); }
        public void connect() { }
        Dictionary<string, string> _setting;
        public Dictionary<string, string> get_settings()
        {
            return _setting;
        }
        public void set_settings(Dictionary<string, string> s)
        {
            _setting = s;
        }
    }
}
