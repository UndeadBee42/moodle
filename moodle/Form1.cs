using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
namespace moodle
{
    public struct student {
        public student(string _firstname, string _lastname,string _middlename,string _group) {
            firstname = _firstname;
            lastname = _lastname;
            middlename = _middlename;
            group = _group;
        }
        public string firstname, lastname, middlename,group;
    }
    public partial class Form1 : Form
    {
        //контейнеры для открывателей файлов и БД взаимодействователей
        List<file_opener.file_opener> file_openers = new List<file_opener.file_opener>();
        List<db_interact.db_interact> db_interactors = new List<db_interact.db_interact>();
        moodle_export.moodle_exporter exporter = new moodle_export.moodle_exporter();
        List<student> students = new List<student>();
        bool keep_server_alive = true;
        public Dictionary<string, string> setting;
        server.server server;
        public void load_settings(Dictionary<string, string> setting, string path) {
            string[] setti = File.ReadAllLines(path);
            foreach (string item in setti)
            {
                string[] tmp = item.Split('~');
                setting[tmp[0]] = tmp[1];
            }
        }

        public void do_server() {
            //настройка сервера

            //основной цикл
            while (keep_server_alive) {
                server.HandleIncomingConnections();
                Thread.Sleep(1000);
            }
        }
        public Form1()
        {
            InitializeComponent();
            // добавляем экспортеры и классы для работы с базами данных
            file_openers.Add(new file_opener.cvs_opener());
            db_interactors.Add(new db_interact.sql_server());
            setting = new Dictionary<string, string>();
            server = new server.server(setting);
            exporter._students = students;
            server.exporter = exporter;
            //загружаем настройки и применяем их
            load_settings(setting,"..\\..\\conf.txt");
            server.set_settings(setting);
            server.listener.Prefixes.Add("http://"+server.setting["adress"]+":"+server.setting["port"]+"/");
            server.global_settings = setting;
            foreach (var parser in file_openers)
            {
                server.parsers[parser.type] = parser;
                parser.push_students(students);
            }
            foreach (db_interact.db_interact item in db_interactors)
            {
                item.set_settings(setting);
            }
            //стартуем серверный поток
            ThreadStart server_ref = new ThreadStart(do_server);
           Thread server_thread = new Thread(server_ref);
           server_thread.Start();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("button click");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            keep_server_alive = false;
            server.stop();
            File.WriteAllLines("..\\..\\conf.txt",
                setting.Select(x => x.Key + "~" + x.Value).ToArray());
        }
    }
}
