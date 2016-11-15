using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA.TrabalhoNB.TextDocs
{
    public class ClassDoc
    {
        private string className;

        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }
        
        private List<TextDoc> docs;

        public List<TextDoc> Docs
        {
            get { return docs; }
            set { docs = value; }
        }

        private double prior;

        public double Prior
        {
            get { return prior; }
            set { prior = value; }
        }

        private Dictionary<string, double> probs;

        public Dictionary<string, double> Probs
        {
            get { return probs; }
            set { probs = value; }
        }

        private List<string> allWordsConcat;

        public List<string> AllWordsConcat
        {
            get { return allWordsConcat; }
            set { allWordsConcat = value; }
        }
        private Vocabulary classVocabulary;

        public Vocabulary ClassVocabulary
        {
            get { return classVocabulary; }
            set { classVocabulary = value; }
        }

        public ClassDoc() {

            Docs            = new List<TextDoc>();
            Probs           = new Dictionary<string, double>();
            AllWordsConcat  = new List<string>();
            ClassVocabulary = new Vocabulary();
        }
        public ClassDoc(string name)
        {
            ClassName = name;
            Docs = new List<TextDoc>();
            Probs = new Dictionary<string, double>();
            AllWordsConcat = new List<string>();
            ClassVocabulary = new Vocabulary();
        }

        public void AddDoc(string path) {

            TextDoc tDoc = new TextDoc { Path = path, MyClass = this, Description = "" };

            tDoc.Read();
            allWordsConcat.AddRange(tDoc.MyWords);

            Docs.Add(tDoc);
        }
        public void AddDoc(TextDoc tDoc)
        {
            tDoc.MyClass = this;

            allWordsConcat.AddRange(tDoc.MyWords);
            Docs.Add(tDoc);

            ClassVocabulary.AddRange(tDoc.MyWords);
            //ClassVocabulary.RemoveDup();

        }

        public void SetPrior(int NDocs)
        {
            Prior = (double)Docs.Count / (double)NDocs;
        }
    }
}
