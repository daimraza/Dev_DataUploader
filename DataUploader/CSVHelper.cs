using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
namespace DataUploader
{
   public class CSVHelper
    {

        public List<Entity> GetEntitiesfromCSV(string path)
        {
            List<Entity> returnEntities = new List<Entity>();
            try
            {
               

                string[] CsvSpilited = File.ReadAllText(path).Split('\n');


                string[] headers = CsvSpilited[0].Split(',');

                for (int i = 1; i < CsvSpilited.Length; i++)
                {
                    Entity entity = new Entity();

                    string[] cellArray = CsvSpilited[i].Split(','); 




                    for (int j = 0; j < headers.Length; j++)
                    {
                        string headerVal = headers[j];
                        string cellVal =cellArray[j];

                        switch (headerVal.Trim())
                        {
                            case "Name":
                                entity.Name = cellVal;
                                break;
                            case "Date":
                                entity.Date = cellVal;
                                break;
                            case "Event":
                                entity.Event = cellVal;
                                break;
                            case "Publication":
                                entity.Publication = cellVal;
                                break;
                            case "Year":
                                entity.Year = cellVal;
                                break;
                            case "Description":
                                entity.Description = cellVal;
                                break;
                            case "Location":
                                entity.Location = cellVal;
                                break;
                            case "Theme":
                                entity.Theme = cellVal;
                                break;
                            case "Path":
                                entity.Path = cellVal.Trim();
                                break;
                            default:
                                break;
                        }

                    }
                    

                    returnEntities.Add(entity);

                }


            }
            catch(Exception e)
            {

            }
            return returnEntities;
        }
    }
}
