using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA.TrabalhoNB.TextDocs
{
    public class ClassficationNB
    {
        private List<ClassDoc> classes;

        public List<ClassDoc> Classes
        {
            get { return classes; }
            set { classes = value; }
        }

        private List<TextDoc> testsDocs;

        public List<TextDoc> TestsDocs
        {
            get { return testsDocs; }
            set { testsDocs = value; }
        }

        private Vocabulary myVocabulary;

        public Vocabulary MyVocabulary
        {
            get { return myVocabulary; }
            set { myVocabulary = value; }
        }

        public int NDocs
        {
            get {
                int nDocs = 0;

                foreach (ClassDoc c in Classes) {
                    nDocs += c.Docs.Count;
                }

                return nDocs; 
            }
        }


        private bool testing;

        public bool Testing
        {
            get { return testing; }
            set { testing = value; }
        }

        public ClassficationNB()
        {
            Classes      = new List<ClassDoc>();
            MyVocabulary = new Vocabulary();
        }


        public void AddClass(ClassDoc class1)
        {

            MyVocabulary.AddRange(class1.ClassVocabulary);
            //MyVocabulary.RemoveDup();

            Classes.Add(class1);
        }
        public void AddClass(string className, string[] pathOfFiles)
        {

            ClassDoc classDoc = new ClassDoc(className);

            foreach (string file in pathOfFiles) {

                classDoc.AddDoc(file);
                MyVocabulary.AddRange(classDoc.AllWordsConcat);
                //MyVocabulary.RemoveDup();
            }

           

            Classes.Add(classDoc);
        }


        public void Train(){

            foreach (ClassDoc c in Classes)
            {
                c.SetPrior(NDocs);

                int n = c.AllWordsConcat.Count;

                foreach (string w in MyVocabulary) {
                    int nw = c.AllWordsConcat.Count(a => a == w);
                    double prob = (double)(nw + 1) / (double)(n + MyVocabulary.Count);
  
                    c.Probs.Add(w,prob);
                }
            }

        }


        public ClassificationResult TestSet()
        {
            ClassificationResult result   = new ClassificationResult();
            
            result.Classes = Classes;

            foreach(TextDoc doc in TestsDocs){
                result.TestSet.Add(doc,Test(doc)); 
            }

            return result;
        }

        public ClassDoc Test(TextDoc textDoc)
        {
            Dictionary<ClassDoc, double> score = new Dictionary<ClassDoc,double>();
            ClassDoc classMax = null;

            foreach (ClassDoc c in Classes) {

                score.Add(c, c.Prior);
                foreach (string w in textDoc.MyWords)
                {
                    if (c.Probs.ContainsKey(w))
                    {
                        score[c] += Math.Log10(c.Probs[w]);
                    }
                }

                if (classMax == null || (score[c] > score[classMax]))
                {
                    classMax = c;
                }
            }

            return classMax;
        }
    }
}
