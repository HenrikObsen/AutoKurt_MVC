
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AKrepo
{
    public class BilFac : AutoFac<Bil>
    {
        public List<Bil> Search(string keyWord)
        {
            using (var cmd = new SqlCommand("SELECT * FROM Bil WHERE Beskrivelse Like @keyword", Conn.CreateConnection()))
            {
                cmd.Parameters.AddWithValue("@keyword", "%" + keyWord + "%");
                var mapper = new Mapper<Bil>();
                List<Bil> list = mapper.MapList(cmd.ExecuteReader());
                cmd.Connection.Close();
                return list;
            }
        }

        public List<Bil> GetRnd(int antal)
        {
            using (var cmd = new SqlCommand("SELECT TOP " + antal + " * FROM Bil ORDER BY NEWID()", Conn.CreateConnection()))
            {

                var mapper = new Mapper<Bil>();
                List<Bil> list = mapper.MapList(cmd.ExecuteReader());
                cmd.Connection.Close();
                return list;
            }

        }


        public string GetAntal(int PID)
        {
            using (var cmd = new SqlCommand("SELECT Count(ID) as antal FROM Bil WHERE ProducentID=@PID", Conn.CreateConnection()))
            {
                var r = cmd.ExecuteReader();
                cmd.Parameters.AddWithValue("@PID", PID);
                string Antal = "0";

                if (r.Read())
                {
                    Antal = r["antal"].ToString();
                }

                r.Close();
                cmd.Connection.Close();

                return Antal;

            }
        }


        public BilMedBilleder GetWithImages(int ID)
        {
            BilledeFac bf = new BilledeFac();
            BilMedBilleder bilMb = new BilMedBilleder();
            bilMb.Bil = Get(ID);
            bilMb.Billeder = bf.GetBy("BilID", ID);

            return bilMb;
        }


        public List<BilMedBilleder> GetAllWithImages()
        {
            BilledeFac bf = new BilledeFac();
            List<BilMedBilleder> listBilMb = new List<BilMedBilleder>();

            foreach (var bil in GetAll())
            {
              listBilMb.Add(GetWithImages(bil.ID));
            }
            return listBilMb;
        }


        public List<BilMedProducent> GetAllBilMedProducent()
        {
            using (var cmd = new SqlCommand("SELECT Bil.Model, Bil.ID, Bil.ProducentID, Bil.Pris, Bil.Beskrivelse, Producent.Navn AS ProducentNavn FROM Bil INNER JOIN Producent ON Bil.ProducentID = Producent.ID", Conn.CreateConnection()))
            {
                var mapper = new Mapper<BilMedProducent>();
                List<BilMedProducent> list = mapper.MapList(cmd.ExecuteReader());
                cmd.Connection.Close();
                return list;
            }
        }


        public List<Bil> AdvSearch(string producent, string maxpris, string keyword)
        {
            string SQL = "SELECT * FROM Bil WHERE Beskrivelse LIKE @Keyword";
            decimal max = 0;

            if (maxpris != "")
            {
                max = decimal.Parse(maxpris);
                SQL += " AND Pris <= @pris";
            }

            if (producent != "0")
            {
                SQL += " AND ProducentID=@producent";
            }

            using (var cmd = new SqlCommand(SQL, Conn.CreateConnection()))
            {
                cmd.Parameters.AddWithValue("@pris", max);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                cmd.Parameters.AddWithValue("@producent", int.Parse(producent));

                var mapper = new Mapper<Bil>();
                List<Bil> list = mapper.MapList(cmd.ExecuteReader());
                cmd.Connection.Close();
                return list;
            }

        }

    }
}

