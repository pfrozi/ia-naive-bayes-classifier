using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA.TrabalhoNB.TextDocs
{
    public class Vocabulary : List<string>
    {
        public void RemoveDup() {
            
            this.RemoveAll(a=>this.Count(b=>b==a)>1);

        }
        public void AddRange(List<string> l)
        {

            foreach (string s in l)
            {
                if (this.IndexOf(s) < 0)
                {
                    this.Add(s);
                }
            }

        }
    }
}
