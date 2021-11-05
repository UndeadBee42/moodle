using System;
using System.Collections.Generic;
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
    }
    public class cvs_opener : file_opener
    {
        /* awd
         пример открывателя файлов
        здесь реализация
         */
        public cvs_opener() {
            
        }
        public void parse(string content) {
            /*
             реализация parse()
             */
            Console.WriteLine("cvs parser:");
            Console.Write(content);
        }
        string _type = "csv";
        string file_opener.type { get { return _type; } set { _type = value; } }
    }
}
