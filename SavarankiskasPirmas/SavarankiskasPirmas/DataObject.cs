using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SavarankiskasPirmas
{
    public class DataObject
    {
        public string Name { get; set; }
        public int Age { get; set; }
        private List<string> programmingLanguages;
        public DataObject(string name, int age, List<string> pL) // pL -> ProgrammingLanguages
        {
            Name = name;
            Age = age;
            programmingLanguages = pL;
        }

        public string GetLanguagesString()
        {
            return String.Join(" ", programmingLanguages);
        }
    }
}