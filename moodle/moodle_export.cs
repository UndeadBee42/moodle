using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace moodle_export
{
    public class moodle_exporter
    {
        /*
         реализуем экспортер
         */
        public List<moodle.student> _students;
        public moodle_exporter() { }
        string translit(string input) {
            string output = "";
            Dictionary<string, string> leters = new Dictionary<string, string>();
            leters["а"] = "a";
            leters["б"] = "b";
            leters["в"] = "v";
            leters["г"] = "g";
            leters["д"] = "d";
            leters["е"] = "e";
            leters["ё"] = "e";
            leters["ж"] = "zh";
            leters["з"] = "z";
            leters["и"] = "i";
            leters["й"] = "y";
            leters["к"] = "k";
            leters["л"] = "l";
            leters["м"] = "m";
            leters["н"] = "n";
            leters["о"] = "o";
            leters["п"] = "p";
            leters["р"] = "r";
            leters["с"] = "s";
            leters["т"] = "t";
            leters["у"] = "u";
            leters["ф"] = "f";
            leters["х"] = "kh";
            leters["ц"] = "ts";
            leters["ч"] = "ch";
            leters["ш"] = "sh";
            leters["щ"] = "shch";
            leters["ъ"] = "";
            leters["ы"] = "y";
            leters["ь"] = "";
            leters["э"] = "e";
            leters["ю"] = "yu";
            leters["я"] = "ya";
            foreach (var item in input)
            {
                if (leters.ContainsKey(item.ToString()))
                {
                    output += leters[item.ToString()];
                }
                else
                {
                    output += item;
                }
            }
            return output;
        }
        public void export(string php, string php_args, string file_out) {
            //username,firstname,lastname,email,password,profile_field_group
            //student1,Student,One,s1@example.com,student1,a
            string file = "username,firstname,lastname,email,password,profile_field_group\n";
            foreach (var item in _students)
            { 
                file += translit(item.firstname.ToLower()) + ",";
                file += translit(item.firstname.ToLower()) + ",";
                file += translit(item.lastname.ToLower()) + ",";
                file += translit(item.firstname.ToLower()) + "@example.com,";
                string password = translit(item.firstname.ToLower());
                password += password.ToUpper()[0] + "1*.!2";
                file += translit(password) + ",";
                file += translit(item.group.ToLower());
                file += "\n";
            }
            File.WriteAllText(file_out, file, Encoding.UTF8);
            System.Diagnostics.Process.Start(php, php_args);
        }
    }
}
