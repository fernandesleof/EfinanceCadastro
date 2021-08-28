using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EfinanceCadastro.Models
{
    public class CidadeModel : IDisposable
    {
        private SqlConnection connection;

        public CidadeModel()
        {
            string strConn = "Data Source=LEONOTE\\SQLEXPRESS;Initial Catalog=EFINANCE;Integrated Security=true";
            connection = new SqlConnection(strConn);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();

        }

        public List<Cidade> Listar()
        {
            List<Cidade> lista = new List<Cidade>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @" SELECT * FROM listarcidade ORDER BY nomecidade ";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Cidade cidade = new Cidade();
                cidade.idCidade = (int)reader["idcidade"];                
                cidade.nomeCidade = (String)reader["nomecidade"];
                cidade.codigoIbgeCidade = (int)reader["codigoibgecidade"];
                cidade.idEstado = (int)reader["idestado"];

                lista.Add(cidade);
            }

            return lista;
        }

        public void CadastroCidade(Cidade cidade)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            /*cmd.CommandText = @" INSERT INTO cidade( idestado , nomecidade , codigoibgecidade) "
                             + " VALUES ( @idestado , @nome , @codigoibge)                     ";*/
            cmd.CommandText = @"EXECUTE crudCidade             "
                           + "@pidcidade = 0,                  "
                           + "@pidestado = @idestado,          "
                           + "@pnome = @nome,                  "
                           + "@pcodigoibge = @codigoibge,      "
                           + "@ptipo = 'I'                     ";

            cmd.Parameters.AddWithValue("@idestado", cidade.idEstado);
            cmd.Parameters.AddWithValue("@nome", cidade.nomeCidade);
            cmd.Parameters.AddWithValue("@codigoibge", cidade.codigoIbgeCidade);

            SqlDataReader dataReader = cmd.ExecuteReader();
        }

        public Cidade GetCidade(int? id)
        {
            Cidade cidade = new Cidade();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @" SELECT * FROM listarcidade WHERE idcidade= @id ";
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cidade.idCidade = (int)reader["idcidade"];
                cidade.nomeCidade = (String)reader["nomecidade"];
                cidade.codigoIbgeCidade = (int)reader["codigoibgecidade"];
                cidade.idEstado = (int)reader["idestado"];
            }
            return cidade;
        }

        public void Excluir(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            /*cmd.CommandText = @"UPDATE cidade SET statusCidade = 'FALSE' WHERE idcidade = @id";*/
            cmd.CommandText = @"EXECUTE crudCidade             "
                           + "@pidcidade = @id,                "
                           + "@pidestado = 0,                  "
                           + "@pnome = '',                     "
                           + "@pcodigoibge = '',               "
                           + "@ptipo = 'D'                     ";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public void Alterar(Cidade cidade)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            /*cmd.CommandText = @" UPDATE cidade SET idestado =@idestado , nomecidade =@nome , "
                              + "  codigoibgecidade =@codigoibge  WHERE idcidade = @idcidade   ";*/
            cmd.CommandText = @"EXECUTE crudCidade              "
                            + "@pidcidade = @idcidade,          "
                            + "@pidestado = @idestado,          "
                            + "@pnome = @nome,                  "
                            + "@pcodigoibge = @codigoibge,      "
                            + "@ptipo = 'U'                     ";

            cmd.Parameters.AddWithValue("@idcidade", cidade.idCidade);
            cmd.Parameters.AddWithValue("@idestado", cidade.idEstado);
            cmd.Parameters.AddWithValue("@nome", cidade.nomeCidade);
            cmd.Parameters.AddWithValue("@codigoibge", cidade.codigoIbgeCidade);

            cmd.ExecuteNonQuery();
        }
    }
}