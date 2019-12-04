using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace DataUploader
{
   public class FileEntity
    {
        public string FileName { get; set; }

        public MemoryStream ContentStream { get; set; }
    }
}
