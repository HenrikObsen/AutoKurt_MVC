using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace AKrepo
{
    public class BrugerFac : AutoFac<Bruger>
    {

        public Bruger Login(string email, string adgangskode)
        {

            using (var cmd = new SqlCommand("SELECT * FROM Bruger WHERE Email=@Email AND Adgangskode=@Adgangskode", Conn.CreateConnection()))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Adgangskode", adgangskode);

                var mapper = new Mapper<Bruger>();
                var r = cmd.ExecuteReader();
                Bruger per = new Bruger();

                if (r.Read())
                {
                    per = mapper.Map(r);
                }

                r.Close();
                cmd.Connection.Close();
                return per;

            }
        }
    }
}
