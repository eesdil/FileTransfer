using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace FileTransfer
{



    class MySqlDb
    {

        MySqlConnection dbConnection;

        public MySqlDb(String myConnection)
        {
            dbConnection = new MySqlConnection(myConnection);
        }

        public class IterationEventArgs : EventArgs
        {
            public string iterationNumber { get; set; }
            public string name { get; set; }
        }

        public event EventHandler<IterationEventArgs> IterationComplete;


        public String MoveMedia()
        {

            CRMDataContext da = new CRMDataContext();
            int iteration = 0;

            da.ObjectTrackingEnabled = false;
            dbConnection.Open();

            da.CommandTimeout = 120;

            try
            {

                foreach (Media item in da.Medias)
                {
                    iteration += 1;
                    MySqlCommand mySqlCommand = new MySqlCommand("INSERT into media(Id, Item) VALUES (@Id, @Item)", dbConnection);

                    mySqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    mySqlCommand.Parameters.AddWithValue("@Item", item.Item.ToArray());
                    mySqlCommand.ExecuteNonQuery();


                    IterationEventArgs args = new IterationEventArgs();
                    args.iterationNumber = iteration.ToString();
                    args.name = item.Name.ToString();
                    IterationComplete(this, args);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                dbConnection.Close();
            }

            return "";
        }
    }

}
