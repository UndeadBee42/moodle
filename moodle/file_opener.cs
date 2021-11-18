using System;
using System.Collections.Generic;
using System.Text;
namespace file_opener
{
    
    public interface file_opener
    {
        /*
         Интерфей.
        сюда пишим функции которые будут у всех открывателей файлов
         */
       void parse(string content);
       string type { get; set; }
        void push_students(List<moodle.student> _students);
    }
    public class cvs_opener : file_opener
    {
        /* awd
         пример открывателя файлов
        здесь реализация
         */
        List<moodle.student> students;
        public void push_students(List<moodle.student> _students) {
            students = _students;
        }
        public cvs_opener() {
            
        }
        public void parse(string content) {
            string[] lines = content.Split(
                    new string[] { Environment.NewLine },
                                StringSplitOptions.None
                                );
            foreach (var line in lines)
            {
                if (line.Length != 0)
                {
                    string[] param = line.Split(',');
                    students.Add(new moodle.student(param[1], param[0], param[2], param[3]));
                }
                
            }
        }
        string _type = "csv";
        string file_opener.type { get { return _type; } set { _type = value; } }
    }
}
