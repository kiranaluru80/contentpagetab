using System;
using System.Collections.Generic;
using System.Linq;
using ConEd.PAP.Models;
using SQLite.Net;
using SQLite.Net.Interop;
using SQLite.Net.Async;
using System.Threading.Tasks;
using System.Text;
using System.Globalization;
using ConEd.PAP.WebAPI;

namespace ConEd.PAP.Data
{
    public class PoliciesRepository
    {
        public static List<Policies> staticItemsList;
        private SQLiteAsyncConnection dbConn;
        private SQLiteConnection dbConnSync;

        //public string StatusMessage { get; set; }

        public PoliciesRepository(ISQLitePlatform sqlitePlatform, string dbPath)
        {
            //initialize a new SQLiteConnection 
            if (dbConn == null)
            {
                var connectionFunc = new Func<SQLiteConnectionWithLock>(() =>
                    new SQLiteConnectionWithLock
                    (
                        sqlitePlatform,
                        new SQLiteConnectionString(dbPath, storeDateTimeAsTicks: false)
                    ));

                dbConn = new SQLiteAsyncConnection(connectionFunc);
                
                dbConn.CreateTableAsync<Policies>();
            }
            if (dbConnSync == null)
            {
                dbConnSync= new SQLiteConnection(sqlitePlatform, dbPath, false);
            }
        }

        public async Task AddNewPersonAsync(string name)
        {
            int result = 0;
            try
            {
                //basic validation to ensure a name was entered
                

                //insert a new person into the Person table
                //result = await dbConn.InsertAsync(new Person { Name = name });
                result = await dbConn.InsertAsync(new Policies { DocType = name });
                
            }
            catch (Exception ex)
            {
                //StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }

        public void UpdateLastModified(List<Policies> OfflineList)
        {
            try
            {
                foreach (var item in OfflineList)
                {
                //check here for existing record in sqlite
                string docName = item.DocName;
                //docName = docName.Remove(docName.Length - 4);   
               
                    List<Policies> plist = dbConnSync.Table<Policies>().Where(c => c.DocName == docName).ToList();
                    int docCount = plist.Count();
                    if (docCount > 0)
                    {
                        string dateval = item.ModifiedDate;
                        //dbConn.UpdateAsync("update policies set ModifiedDate=" + item.ModifiedDate + " where DocName=" + docName);

                        //String[] args = new String[] { dateval, docName };
                        //string qry = "update Policies set ModifiedDate=? where DocName=?";
                        //////dbConn.update(TABLE_LATLONG, values, "Loc_lati=? AND Loc_longi=?", args);
                        //////dbConn.ExecuteAsync("Policies", values,"", args);
                        //dbConn.ExecuteScalarAsync<Policies>(qry,args);
                        //string sss = "Delete from policies where DocName='" + docName + "'";
                        dbConn.ExecuteAsync("Delete from policies where DocName='" + docName + "'");
                        //dbConnSync.Execute("Delete from policies where DocName='" + docName + "'");
                        Policies ps = new Policies();
                        ps.DocId = plist[0].DocId;
                        ps.DocName = docName;
                        ps.DocSubType = plist[0].DocSubType;
                        ps.DocSubTypeID = plist[0].DocSubTypeID;
                        ps.DocType = plist[0].DocType;
                        ps.IsFavourite = plist[0].IsFavourite;
                        ps.ModifiedBy = item.ModifiedBy;
                        ps.ModifiedDate = dateval;

                        //dbConn.InsertAsync(new Policies {  DocId= plist[0].DocId, DocSubType= plist[0].DocSubType, DocSubTypeID= plist[0].DocSubTypeID, DocType= plist[0].DocType, IsFavourite= plist[0].IsFavourite, DocName = item.DocName, ModifiedDate= item.ModifiedDate, ModifiedBy= item.ModifiedBy });
                        dbConn.InsertAsync(ps);
                        //dbConn.UpdateAsync(ps);

                        if(plist[0].IsFavourite)
                        {
							DocumentDownloadModel.DocumentRequest dr = new DocumentDownloadModel.DocumentRequest();
                            dr.Path = "";
                            dr.Name = docName;
                            /* network check*/

							ServiceBusRelay.DownloadFile("api/policies/DownloadFile", dr);

						}
                        ps = null;


                    }
                    else
                    {
                        dbConn.InsertAsync(item);
                    }
                }
                SetLastModifiedDate();


            }           
            catch (Exception ex)
            {
                //docName = ex.Message;
                throw;
            }

        }

        private void SetLastModifiedDate()
        {
            string dateTime = DateTime.Now.ToString();
            string createddate = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd h:mm tt");
            string ss = "update PolicySync set LastModified='" + createddate + "' where ID=1";

            //dbConn.ExecuteAsync("UPDATE PolicySync set LastModified = ?, WHERE ID = ?", createddate, 1);
            string msg = "";
            try
            {
                dbConn.ExecuteAsync("Delete from PolicySync");
                dbConn.InsertAsync(new PolicySync { LastModified = createddate, ID = 1 });
                //dbConn.UpdateAsync(ss);                
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                throw;
            }
        }

        public void UpdateSync()
        {
            string ss = string.Empty;
            try
            {
                dbConnSync.Update(new PoliciesSync { LastModified = "2017-12-21", ID = 1 });
            }
            catch (Exception ex)
            {
                ss = ex.Message;
                throw;
            }
            //string ss = "update PolicySync set LastModified='" + createddate + "' where ID=1";
           
        }

        public void DeleteAllPolicies()
        {
            dbConn.ExecuteAsync("Delete from Policies");
        }

        public string GetSQLiteLastSyncDate()
        {
            string returnVal = string.Empty;
            //var obj = await dbConn.Table<PolicySync>().Where(c => c.ID == 1).ToListAsync().RunSynchronously();
            List<PolicySync> plist= dbConnSync.Table<PolicySync>().Where(c => c.ID == 1).ToList();

            foreach (var item in plist)
            {
                returnVal = item.LastModified;
                break;
            }
            return returnVal;
        }

        public async Task<string> GetLastSyncDate()
        {
            string returnVal = string.Empty;

            List<PolicySync> plist = await GetSyncDate();
            foreach (var item in plist)
            {
                returnVal = item.ToString();
                break;
            }


            return returnVal;
        }
        public async Task<List<PolicySync>> GetSyncDate()
        {
            //var lastModifiedVal = dbConn.ExecuteAsync("select LastMofified from PolicySync").Result.ToString();
           
            //var obj = await dbConn.ExecuteScalarAsync<string>("select LastMofified from PolicySync where ID=1");
            List<PolicySync> syncList = await dbConn.Table<PolicySync>().Where(c => c.ID == 1).ToListAsync();
            return syncList;
        }

        public void AddAllPolicies(List<Policies> offlinePList)
        {
            try
            {
                dbConn.InsertAllAsync(offlinePList);
                SetLastModifiedDate();
            }
            catch (Exception ex)
            {

                throw ex;
            }
                
            
            //foreach (Policies p in offlinePList)
            //{
                
            //    dbConn.InsertAsync(new Policies { DocType = p.DocName });
            //    //dbConn.UpdateAsync(new Policies { DocType = p.DocName });
                
            //}
        }

       public int UpdateFavorites(string docName, string isFavorite)
        {
            int returnVal = 1;

			try
			{
				List<Policies> favList = dbConnSync.Table<Policies>().Where(c => c.DocName == docName).ToList();
                Policies ps = new Policies();
				foreach (var item in favList)
				{
					
                    ps.DocId = item.DocId;
					ps.DocName = item.DocName;
					ps.DocSubType = item.DocSubType;
					ps.DocSubTypeID = item.DocSubTypeID;
					ps.DocType =item.DocType;
                    if(isFavorite.ToLower()=="1")
                    {
                        ps.IsFavourite = true;
                    }
                    else
                    {
                        ps.IsFavourite = false;
                    }
					
					ps.ModifiedBy = item.ModifiedBy;
					ps.ModifiedDate = item.ModifiedDate;
                    break;
				}
				

				//dbConn.InsertAsync(new Policies {  DocId= plist[0].DocId, DocSubType= plist[0].DocSubType, DocSubTypeID= plist[0].DocSubTypeID, DocType= plist[0].DocType, IsFavourite= plist[0].IsFavourite, DocName = item.DocName, ModifiedDate= item.ModifiedDate, ModifiedBy= item.ModifiedBy });
				
				dbConnSync.Execute("Delete from Policies where DocName='" + docName + "'");
                dbConn.InsertAsync(ps);
				
				//dbConn.UpdateAsync(ss);                
			}
			catch (Exception ex)
			{
                string msg;
				msg = ex.Message;
				throw;
			}

            try
            {
                String[] qryArgs = new String[] { isFavorite, docName };
                string qry = "update Policies set IsFavourite=? where DocName=?";
                dbConnSync.Execute(qry, qryArgs);
                //dbConn.ExecuteAsync("UPDATE Policies set IsFavourite = ?, WHERE DocName = ?", 1, docName);
            }
            catch (Exception)
            {
                returnVal = 0;
                throw;
            }
            
            return returnVal;
        }

        public bool IsFavorite(string DocumentName)
        {
            bool isFavorite = false;
            List<Policies> favList = dbConnSync.Table<Policies>().Where(c => c.DocName == DocumentName).ToList();
            foreach (var item in favList)
            {
                isFavorite = item.IsFavourite;
            }
            return isFavorite;
        }

        public List<Policies> GetFavorites()
        {
            List<Policies> favList = dbConnSync.Table<Policies>().Where(c => c.IsFavourite == true).ToList();
            return favList;
        }

        public List<Policies> GetAllPeopleAsync(string categoryName)
        {
            List<Policies> policiesList = dbConnSync.Table<Policies>().Where(c => c.DocType == categoryName).ToList();
            
            return policiesList;
        }

        public List<Policies> GetAllPolicies()
        {

            List<Policies> policiesList = dbConnSync.Table<Policies>().ToList();
           
            return policiesList;
        }


    }
}
