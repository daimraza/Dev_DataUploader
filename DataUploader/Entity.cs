using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; 
namespace DataUploader
{
   public class Entity
    {


        public string Name { get; set; }
        public string Date { get; set; }

        public string Event { get; set; }
        public string Publication { get; set; }

        public string Year { get; set; }


        public string Description { get; set; }
        public string Location { get; set; }
        public string Theme { get; set; }

        public string Path { get; set; }

        public string GetFileName()
        {
            FileInfo info = new FileInfo(Path);

            return info.Name;
        }


        public MemoryStream GetStream()
        {
            FileInfo info = new FileInfo(Path);

            return new MemoryStream(System.IO.File.ReadAllBytes(Path));
        }
    }
}
