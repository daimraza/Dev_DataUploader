using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace DataUploader
{
    public class IOHelper
    {

        public FileEntity GetFile(string path)
        {
            
            FileEntity entityReturn = new FileEntity();

            try
            {
                FileInfo info = new FileInfo(path);

                entityReturn.FileName = info.Name;
                entityReturn.ContentStream = new MemoryStream(File.ReadAllBytes(path));
            }
            catch (Exception e)
            { }
            return entityReturn;
        }

    }
}
