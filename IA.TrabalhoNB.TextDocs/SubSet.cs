using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA.TrabalhoNB.TextDocs
{
    public class ClassficationNBSetResults
    {
        
        private List<ClassificationResult> results;

        public List<ClassificationResult> Results
        {
          get { return results; }
          set { results = value; }
        }

        public ClassificationResult ResultsAverage
        {
            get { 
                //TODO: You need make this with the average 
                return new ClassificationResult(); 
            }
        
        }


    }
}
