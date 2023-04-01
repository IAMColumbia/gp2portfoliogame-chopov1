using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;


namespace RhythmGameLibrary
{
    
    public class FileReader
    {
        
        public string dataPath = "GameData/";
        List<string> data = new List<string>();
        public FileReader()
        {
            
        }

        public List<string> ReadFile(string path)
        {
            data = File.ReadAllLines(path).ToList();
            return data;
        }

        public void writeFile(string path, List<string> content)
        {
            
            File.WriteAllLines(path, content);
            
        }
    }
}
