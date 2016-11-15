using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA.TrabalhoNB.TextDocs
{
    public class ClassificationResult
    {

        private Dictionary<TextDoc, ClassDoc> testSet;
        public Dictionary<TextDoc, ClassDoc> TestSet
        {
            get { return testSet; }
            set { testSet = value; }
        }

        private List<ClassDoc> classes;
        public List<ClassDoc> Classes
        {
            get { return classes; }
            set { classes = value; }
        }

        private int[][] confusionMatrix;

        public int[][] ConfusionMatrix
        {
            get { return confusionMatrix; }
            set { confusionMatrix = value; }
        }
        private double[] standardDeviation;
        public double[] StandardDeviation
        {
            get { return standardDeviation; }
            set { standardDeviation = value; }
        }


        public ClassificationResult() 
        { 
            TestSet         = new Dictionary<TextDoc,ClassDoc>();
        }

        public void Refresh() { 
            


        }
    }
}
