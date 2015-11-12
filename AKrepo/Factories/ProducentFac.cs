using System.Collections.Generic;
using System.Data.SqlClient;

namespace AKrepo
{
    public class ProducentFac: AutoFac<Producent>
    {

        public string GetName(int ID)
        {
            using (var cmd = new SqlCommand("SELECT Navn FROM Producent WHERE ID=@ID", Conn.CreateConnection()))
            {
                cmd.Parameters.AddWithValue("@ID", ID);

                var r = cmd.ExecuteReader();
                string navn = "";

                if (r.Read())
                {
                    navn = r["Navn"].ToString();
                }

                r.Close();
                cmd.Connection.Close();
                return navn;

            }

        }

        public List<Producent> GetAllProducent()
        {
            string Sql = "SELECT * FROM Producent";

            SqlCommand cmd = new SqlCommand(Sql, Conn.CreateConnection());

            var r = cmd.ExecuteReader();
            var list = new List<Producent>();

            while (r.Read())
            {
                Producent producent = new Producent();
                producent.ID = int.Parse(r["ID"].ToString());
                producent.Navn = r["Navn"].ToString();
                producent.Logo = r["Logo"].ToString();
                list.Add(producent);
            }

            return list;
        }

        public void UpdateLogo(int ID, string Logo)
        {
            using (var cmd = new SqlCommand("UPDATE Producent SET Logo=@Logo WHERE ID=@ID", Conn.CreateConnection()))
            {
                cmd.Parameters.AddWithValue("@Logo", Logo);
                cmd.Parameters.AddWithValue("@ID", ID);

                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public string InsertAndReturnID(string Navn, string Logo)
        {
            string ID = "0";
            using (var cmd = new SqlCommand("INSERT INTO Producent(Navn, Logo) VALUES(@Navn, @Logo);SELECT SCOPE_IDENTITY() AS curID;", Conn.CreateConnection()))
            {
                cmd.Parameters.AddWithValue("@Navn", Navn);
                cmd.Parameters.AddWithValue("@Logo", Logo);

                var r = cmd.ExecuteReader();

                if (r.Read())
                {
                    ID = r["curID"].ToString();
                }
                r.Close();
                cmd.Connection.Close();
            }

            return ID;
        }

        public void DeleteWithCars(int ID)
        {
            Delete(ID);

            BilFac bf = new BilFac();
            bf.DeleteBy("ProducentID", ID);


        }

        BilFac bf = new BilFac();

        public ProducentMedBil HentMedBil(int producent)
        {
            ProducentMedBil pmb = new ProducentMedBil();
            pmb.Producent = Get(producent);
            pmb.Biler = bf.GetBy("ProducentID", producent);

            return pmb;
        }

    }
}

