using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EfinanceCadastro.Models
{
    public class EstadoModel : IDisposable
    {
        private SqlConnection connection;

        public EstadoModel()
        {
            string strConn = "Data Source=LEONOTE\\SQLEXPRESS;Initial Catalog=EFINANCE;Integrated Security=true";
            connection = new SqlConnection(strConn);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public List<Estado> Listar()
        {
            List<Estado> lista = new List<Estado>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT idestado,codufestado,nomeestado,ufestado FROM estado AND statusestado = TRUE";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Estado estado = new Estado();
                estado.idEstado = (int)reader["idestado"];
                estado.codUfEstado = (int)reader["codufestado"];
                estado.nomeEstado = (String)reader["nomeestado"];
                estado.ufEstado = (String)reader["ufestado"];

                lista.Add(estado);
            }

            return lista;
        }

        public int CadastroEstado(Estado estado)
        {
            int idEstado = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @" INSERT INTO estado(codufestado,nomeestado,ufestado) OUTPUT Inserted.idestado "
                             + " VALUES (@coduf,@nome,@uf)                                                    ";

            cmd.Parameters.AddWithValue("@coduf", estado.codUfEstado);
            cmd.Parameters.AddWithValue("@nome", estado.nomeEstado);
            cmd.Parameters.AddWithValue("@uf", estado.ufEstado);

            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {
                dataReader.Read();
                idEstado = Convert.ToInt32(dataReader["idestado"]);
            }
            dataReader.Close();
            return (idEstado);
        }

        public Estado GetEstado(int? id)
        {
            Estado estado = new Estado();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM estado WHERE idestado= @id AND statusestado = true ";
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                estado.idEstado = (int)reader["idestado"];
                estado.codUfEstado = (int)reader["codufestado"];
                estado.nomeEstado = (String)reader["nomeestado"];
                estado.ufEstado = (String)reader["ufestado"];
            }
            return estado;
        }

        public void Excluir(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE estado SET statusestado = FALSE WHERE idcliente = @id";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public void Alterar(Estado estado)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @" UPDATE estado SET codufestado = @coduf , nomeestado = @nome , "
                              +" ufestado = @uf WHERE idestado = @idestado                     ";

            cmd.Parameters.AddWithValue("@idestado", estado.codUfEstado);
            cmd.Parameters.AddWithValue("@coduf", estado.codUfEstado);
            cmd.Parameters.AddWithValue("@nome", estado.nomeEstado);
            cmd.Parameters.AddWithValue("@uf", estado.ufEstado);

            cmd.ExecuteNonQuery();
        }
    }
}