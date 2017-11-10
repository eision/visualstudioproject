using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Morphemename;
using System.Diagnostics;
using System.IO;

namespace MeCabMorphologicalAna
{
    public class MeCabMorphologicalAnalyzer
    {

        public MeCabMorphologicalAnalyzer()
        {
            
        }

        public List<Morpheme> Analyse(string str)
        {
            StringReader outputlines;
            string line;
            List<Morpheme> res = new List<Morpheme>();
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.FileName = @"C:\Program Files (x86)\MeCab\bin\Mecab.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            StreamWriter myStreamWriter = p.StandardInput;
            myStreamWriter.WriteLine(str);
            myStreamWriter.Close();
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();
            outputlines = new StringReader(output);
            while ((line = outputlines.ReadLine()) != null)
            {
                if(line != "EOS"){
                    String[] substrings = line.Split('\t',',');
                    Morpheme wordanalytic = new Morpheme(substrings[0], substrings[1]);
                    res.Add(wordanalytic);
                }
            }
            
            return res;
        }
    }
}
