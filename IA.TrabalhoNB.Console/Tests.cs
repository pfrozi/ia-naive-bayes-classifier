using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IA.TrabalhoNB.TextDocs;
using System.Threading;

namespace IA.TrabalhoNB.Console
{
    
    public class Tests
    {
        private const string PATH_POS = "C:\\Users\\PFROZI\\Documents\\UFRGS\\Inteligência Artificial(INF01048)\\ENGEL\\Tarefa NB\\positivo\\";
        private const string PATH_NEG = "C:\\Users\\PFROZI\\Documents\\UFRGS\\Inteligência Artificial(INF01048)\\ENGEL\\Tarefa NB\\negativo\\";

        private List<TextDoc>              textDocsPositives = new List<TextDoc>();
        private List<TextDoc>              textDocsNegatives = new List<TextDoc>();
        private List<ClassificationResult> results           = new List<ClassificationResult>();

        private List<string> filesPositives = new List<string>();
        private List<string> filesNegatives = new List<string>();

        private List<ClassficationNB> classifications = new List<ClassficationNB>();

        public void Test10cv() {

            List<Thread> trds = new List<Thread>();

            ThreadStart readP = new ThreadStart(readPositives);
            Thread tReadP     = new Thread(readP);

            ThreadStart readN = new ThreadStart(readNegatives);
            Thread tReadN     = new Thread(readN);

            int[,] files = new int[10, 2]{ 
                 { 35 , 54 }
                ,{ 55 , 74 }
                ,{ 75 , 94 }
                ,{ 95 , 114 }
                ,{ 115 , 134 }
                ,{ 135 , 154 }
                ,{ 155 , 174 }
                ,{ 175 , 194 }
                ,{ 195 , 214 }
                ,{ 215 , 234 }
            };
            
            for (int i = 0; i < files.Length / 2; i++)
            {

                int j;
                // Positives
                j = 0;
                do
                {
                    string fileName = String.Format("{0}{1}.txt", PATH_POS, (files[i, 0] + j));
                    filesPositives.Add(fileName);

                    j++;
                } while (files[i, 0] + j <= files[i, 1]);
                // Negatives
                j = 0;
                do
                {
                    string fileName = String.Format("{0}{1}.txt", PATH_NEG, (files[i, 0] + j));
                    filesNegatives.Add(fileName);
                    j++;

                } while (files[i, 0] + j <= files[i, 1]);
            }

            tReadP.Start();
            tReadN.Start();

            while (tReadP.ThreadState == ThreadState.Running || tReadN.ThreadState == ThreadState.Running) Thread.Sleep(500);

            for (int i = 0; i < 10; i++)
            {
                ClassficationNB classification = new ClassficationNB();
                classification.Testing = false;

                ClassDoc classPos = new ClassDoc("Pos");
                ClassDoc classNeg = new ClassDoc("Neg");

                classification.TestsDocs = new List<TextDoc>();

                int j;
                for (j = 0; j < 10; j++)
                {
                    textDocsPositives[i * 10 + j].MyClass = classPos;
                    textDocsNegatives[i * 10 + j].MyClass = classNeg;

                    classification.TestsDocs.Add(textDocsPositives[i * 10 + j]);
                    classification.TestsDocs.Add(textDocsNegatives[i * 10 + j]);
                }
                for (int k = 0; k < textDocsPositives.Count; k++)
                {
                    if ((k < i * 10) || (k >= i * 10 + j))
                    {
                        classPos.AddDoc(textDocsPositives[k]);
                        classNeg.AddDoc(textDocsNegatives[k]);
                    }
                }

                classification.AddClass(classPos);
                classification.AddClass(classNeg);

                classifications.Add(classification);

                ThreadStart ts = new ThreadStart(testClassification);
                Thread t = new Thread(ts);

                t.Start();
                trds.Add(t);
            }

            

            while (trds.Count(a=>a.ThreadState==ThreadState.Running)>0) Thread.Sleep(500);

            string msgOK = results.ToString();

        }

        private void testClassification() {

            ClassficationNB c = classifications.First(a => !a.Testing);

            if (c != null)
            {
                c.Testing = true;
                c.Train();
                ClassificationResult result = c.TestSet();
                results.Add(result);
            }

        }
        private void readPositives()
        {
            
            foreach (string path in filesPositives)
            {
                TextDoc tDocPositive = new TextDoc { Path = path, MyClass = null, Description = "" };
                tDocPositive.Read();
                textDocsPositives.Add(tDocPositive);
            }
           
        }
        private void readNegatives()
        {
            foreach (string path in filesNegatives)
            {
                TextDoc tDocNegative = new TextDoc { Path = path, MyClass = null, Description = "" };
                tDocNegative.Read();
                textDocsNegatives.Add(tDocNegative);
            }
        }

        private void ResutAnalysis()
        {
            int totalResultados = results.Count;
            int totalPositivosClassif = 0;
            int totalNegativosClassif = 0;

            int totalPositivos = 0;
            int totalNegativos = 0;

            int mediaPositivosClassif = 0;
            int mediaNegativosClassif = 0;

            int totalPositivosClassifIncorrMedia = 0;
            int totalNegativosClassifIncorrMedia = 0;
            int totalPositivosClassifCorretMedia = 0;
            int totalNegativosClassifCorretMedia = 0;

            for (int i = 0; i < totalResultados; i++)
            {

                totalPositivos = 0;
                totalNegativos = 0;
                totalPositivosClassif = 0;
                totalNegativosClassif = 0;

                int totalPositivosClassifIncorr = 0;
                int totalNegativosClassifIncorr = 0;
                int totalPositivosClassifCorret = 0;
                int totalNegativosClassifCorret = 0;

                foreach (KeyValuePair<TextDoc, ClassDoc> c in results[i].TestSet)
                {

                    if (c.Key.MyClass.ClassName == "Pos")
                        totalPositivos++;
                    else if (c.Key.MyClass.ClassName == "Neg")
                        totalNegativos++;

                    if (c.Value.ClassName == "Pos")
                        totalPositivosClassif++;
                    else if (c.Value.ClassName == "Neg")
                        totalNegativosClassif++;

                    if (c.Value.ClassName == "Pos" && c.Key.MyClass.ClassName == "Pos")
                        totalPositivosClassifCorret++;
                    if (c.Value.ClassName == "Neg" && c.Key.MyClass.ClassName == "Pos")
                        totalPositivosClassifIncorr++;
                    if (c.Value.ClassName == "Pos" && c.Key.MyClass.ClassName == "Neg")
                        totalNegativosClassifIncorr++;
                    if (c.Value.ClassName == "Neg" && c.Key.MyClass.ClassName == "Neg")
                        totalNegativosClassifCorret++;
                }
                mediaPositivosClassif = (mediaPositivosClassif + totalPositivosClassif) / (i + 1);
                mediaNegativosClassif = (mediaNegativosClassif + totalNegativosClassif) / (i + 1);

                totalPositivosClassifIncorrMedia += totalPositivosClassifIncorr;
                totalNegativosClassifIncorrMedia += totalNegativosClassifIncorr;
                totalPositivosClassifCorretMedia += totalPositivosClassifCorret;
                totalNegativosClassifCorretMedia += totalNegativosClassifCorret;
            }

            double dblTotalPositivosClassifIncorrMedia = (double)totalPositivosClassifIncorrMedia / (double)totalResultados;
            double dblTotalNegativosClassifIncorrMedia = (double)totalNegativosClassifIncorrMedia / (double)totalResultados;
            double dblTotalPositivosClassifCorretMedia = (double)totalPositivosClassifCorretMedia / (double)totalResultados;
            double dblTotalNegativosClassifCorretMedia = (double)totalNegativosClassifCorretMedia / (double)totalResultados;

            totalPositivosClassifIncorrMedia /= totalResultados;
            totalNegativosClassifIncorrMedia /= totalResultados;
            totalPositivosClassifCorretMedia /= totalResultados;
            totalNegativosClassifCorretMedia /= totalResultados;
        }
    }
}
