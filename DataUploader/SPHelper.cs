using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
namespace DataUploader
{
    public class SPHelper
    {

        public void CopytoSharePoint(string URL, string UserName, string Password, string ListName,List<Entity> entities)
        {
            try
            {
                Logger.log("Process Start: ",0,ListName);
                var securePassword = new SecureString();
                foreach (char c in Password)
                {
                    securePassword.AppendChar(c);
                }
                var onlineCredentials = new SharePointOnlineCredentials(UserName, securePassword);

                using (ClientContext clientContext = new ClientContext(URL))
                {
                    clientContext.Credentials = onlineCredentials;

                 
                    clientContext.Load(clientContext.Web);
                    
                    clientContext.ExecuteQuery();
                    List lst = clientContext.Web.Lists.GetByTitle(ListName);
                    clientContext.Load(lst);
                    clientContext.Load(lst.RootFolder);
                    clientContext.Load(lst.RootFolder, s=> s.ServerRelativeUrl);

                    clientContext.Load(lst.RootFolder);
                    clientContext.Load(lst.RoleAssignments);
                    clientContext.ExecuteQuery();
                

                    foreach (Entity dataRow in entities)
                    {

                     

                        IOHelper helper = new IOHelper();
                        FileEntity entityFile = helper.GetFile(dataRow.Path);
                        //ListItem DocumentNameItem = null;
                        //DocumentNameItem = GetLibraryTitle(clientContext, ListName, entityFile.FileName);
                        //if (DocumentNameItem != null)
                        //{
                        //    string DocumentName = Convert.ToString(DocumentNameItem["FileLeafRef"]);
                        //    if (entityFile.FileName == DocumentName)
                        //    {
                        //        entityFile.FileName = dataRow.Name;
                        //    }
                        //}
                        if (!string.IsNullOrWhiteSpace(dataRow.Path))
                        {
                            //FileCreationInformation info = new FileCreationInformation();
                            //info.ContentStream = entityFile.ContentStream;
                            //info.Url = clientContext.Web.ServerRelativeUrl + "/" + ListName + "/" + entityFile.FileName;
                            //File file = lst.RootFolder.Files.Add(info);

                            //clientContext.Load(file);
                            //clientContext.Load(file.ListItemAllFields);
                            //clientContext.ExecuteQuery();

                            
                            string Title = Convert.ToString(dataRow.Name);
                            ListItem LibraryItem = null;
                            LibraryItem = GetSingleItem(clientContext, ListName, Title);
                            if (LibraryItem == null)
                            {

                                ListItem DocumentNameItem = null;
                                DocumentNameItem = GetLibraryTitle(clientContext, ListName, entityFile.FileName);

                                if (DocumentNameItem != null)
                                {
                                    string DocumentName = Convert.ToString(DocumentNameItem["FileLeafRef"]);
                                    if (entityFile.FileName == DocumentName)
                                    {
                                        entityFile.FileName = dataRow.Name;
                                    }
                                }
                                FileCreationInformation info = new FileCreationInformation();
                                info.ContentStream = entityFile.ContentStream;
                                info.Url = clientContext.Web.ServerRelativeUrl + "/" + ListName + "/" + entityFile.FileName;
                                File file = lst.RootFolder.Files.Add(info);

                                clientContext.Load(file);
                                clientContext.Load(file.ListItemAllFields);
                                clientContext.ExecuteQuery();
                                checkOrAddChoiceValue(clientContext, ListName, "Year", dataRow.Year);
                                checkOrAddChoiceValue(clientContext, ListName, "Event_x002f_Activity", dataRow.Description);
                                checkOrAddChoiceValue(clientContext, ListName, "Publications", dataRow.Publication);
                                checkOrAddChoiceValue(clientContext, ListName, "Location", dataRow.Location);
                                checkOrAddChoiceValue(clientContext, ListName, "Theme", dataRow.Theme);
                                file.ListItemAllFields["Title"] = dataRow.Name;
                                //file.ListItemAllFields["Name"] = entityFile.FileName;

                                file.ListItemAllFields["Image_x002f_Publication_x0020_Date"] = dataRow.Date;
                                file.ListItemAllFields["Year"] = dataRow.Year;

                                file.ListItemAllFields["Event_x002f_Activity"] = dataRow.Description;
                                
                                file.ListItemAllFields["Publications"] = dataRow.Publication;

                            file.ListItemAllFields["Description0"] = dataRow.Description;

                            file.ListItemAllFields["Location"] = dataRow.Location;

                            file.ListItemAllFields["Theme"] = dataRow.Theme;





                            file.ListItemAllFields.Update();
                            clientContext.Load(file.ListItemAllFields);

                            clientContext.ExecuteQuery();
                        }
                        else {
                                string LibraryTitle = Convert.ToString(LibraryItem["Title"]);
                                int LibraryItemID = Convert.ToInt32(LibraryItem.Id);


                                Logger.log("Item already exists in SharePoint. Item ID: "+LibraryItemID+" Title: "+LibraryTitle,0,"Reprocess Exists Items");
                            }
                        }


                    }




                }
                Logger.log("Process End: ", 0, ListName);
            }
            catch(Exception e)
            {
                Logger.errorlog("Error in SPHelperClass. Message: "+e.Message,0,ListName);
            }

        }
        public ListItem GetSingleItem(ClientContext clientContext, string ListName,string Title)
        {
            ListItemCollection collListItems = null;
            ListItem Item = null;
            try
            {
                List PublishImageLib = clientContext.Web.Lists.GetByTitle(ListName);
                CamlQuery camlQueryLPublish = new CamlQuery();
                camlQueryLPublish.ViewXml = @"<View><Query><Where><Eq><FieldRef Name='Title' /><Value Type='Text'>"+Title+@"</Value></Eq></Where></Query><RowLimit>1</RowLimit></View>";

                collListItems = PublishImageLib.GetItems(camlQueryLPublish);
                clientContext.Load(collListItems);
                clientContext.ExecuteQuery();

                Logger.log(collListItems.Count + " Items found for the library:" + ListName, 0, ListName);
                if (collListItems.Count > 0)
                {
                    Item = collListItems.First();
                    clientContext.Load(Item);
                    clientContext.ExecuteQuery();
                    Logger.log(" Items Name:" + Item["Title"], 0, ListName);
                }

               
            }
            catch (Exception ex)
            {
                Logger.errorlog("ERROR in Helper function  GetSingleItem: For List:" + ListName + " - Message:" + ex.Message, 0, ListName);
            }
            return Item;
        }
        public ListItem GetLibraryTitle(ClientContext clientContext, string ListName, string Name)
        {
            ListItemCollection collListItems = null;
            ListItem Item = null;
            try
            {
                List PublishImageLib = clientContext.Web.Lists.GetByTitle(ListName);
                CamlQuery camlQueryLPublish = new CamlQuery();
                camlQueryLPublish.ViewXml = @"<View><Query><Where><Eq><FieldRef Name='FileLeafRef' /><Value Type='File'>"+Name+@"</Value></Eq></Where></Query><RowLimit>1</RowLimit></View>";

                collListItems = PublishImageLib.GetItems(camlQueryLPublish);
                clientContext.Load(collListItems);
                clientContext.ExecuteQuery();

              //  Logger.log(collListItems.Count + " Items found for the library:" + ListName, 0, ListName);
                if (collListItems.Count > 0)
                {
                    Item = collListItems.First();
                    clientContext.Load(Item);
                    clientContext.ExecuteQuery();
                    Logger.log(" Items Name:" + Item["Title"], 0, ListName);
                }


            }
            catch (Exception ex)
            {
                Logger.errorlog("ERROR in Helper function  GetLibraryName: For List:" + ListName + " - Message:" + ex.Message, 0, ListName);
            }
            return Item;
        }


        public void checkOrAddChoiceValue(ClientContext clientContext, string ListName, string fieldName, string value)
        {
            try
            {
                List lst = clientContext.Web.Lists.GetByTitle(ListName);

                clientContext.Load(lst);
                clientContext.Load(lst.Fields);
                clientContext.ExecuteQuery();

                Field field = lst.Fields.GetByTitle(fieldName);
                FieldChoice fieldChoice = null;
                fieldChoice = clientContext.CastTo<FieldChoice>(field);
                clientContext.Load(fieldChoice);
                clientContext.ExecuteQuery(); 

                if(fieldChoice!=null)
                {
                    if(!fieldChoice.Choices.Contains(value))
                    {
                        List<string> options = new List<string>(fieldChoice.Choices);
                        options.Add(value);
                        fieldChoice.Choices = options.ToArray();

                        // Update the choice field  
                        fieldChoice.Update();

                        // Execute the query to the server  
                        clientContext.ExecuteQuery();
                    }
                }


            }
            catch(Exception e)
            {
                Logger.errorlog("ERROR in Helper function  checkOrAddChoiceValue: For List:" + ListName + " - Message:" + e.Message, 0, ListName);
            }
        }
    }
}
