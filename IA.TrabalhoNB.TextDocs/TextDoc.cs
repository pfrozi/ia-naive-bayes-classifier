using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IA.TrabalhoNB.TextDocs
{
    public class TextDoc
    {
        
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        private ClassDoc myClass;

        public ClassDoc MyClass
        {
            get { return myClass; }
            set { myClass = value; }
        }

        private List<string> myWords;

        public List<string> MyWords
        {
            get { return myWords; }
            set { myWords = value; }
        }

        public TextDoc() {

            MyWords = new List<string>();

        }

        public void Read() {


            StreamReader file = new StreamReader(Path);
            
            string allText = file.ReadToEnd();

            MyWords = StopWords.tokenWithoutSwAndLinks(allText);

            file.Close();
            file.Dispose();
        }
    }
}
