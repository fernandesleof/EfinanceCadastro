using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EfinanceCadastro.Models
{
    public class ClienteModel : IDisposable
    {
        private SqlConnection connection;

        public ClienteModel()
        {
            string strConn = "Data Source=LEONOTE\\SQLEXPRESS;Initial Catalog=EFINANCE;Integrated Security=true";
            connection = new SqlConnection(strConn);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT idcliente , nomecliente , telefonecliente , idcidade FROM cliente WHERE statuscliente = 'TRUE'";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Cliente cliente = new Cliente();
                cliente.idCliente = (int)reader["idcliente"];
                cliente.idCidade = (int)reader["idcidade"];
                cliente.nomeCliente = (String)reader["nomecliente"];
                cliente.telefoneCliente = (String)reader["telefonecliente"];

                lista.Add(cliente);
            }

            return lista;
        }

        public int CadastroCliente(Cliente cliente)
        {
            int idCliente = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @" INSERT INTO cliente(nomecliente,telefonecliente,cpfcnpjcliente,idcidade) "
                             + " VALUES (@nome,@telefone,@cpfcnpj,@idcidade)                     ";

            cmd.Parameters.AddWithValue("@nome", cliente.nomeCliente);
            cmd.Parameters.AddWithValue("@telefone", cliente.telefoneCliente);
            cmd.Parameters.AddWithValue("@cpfcnpj", cliente.cpfCnpjCliente);
            cmd.Parameters.AddWithValue("@idcidade", cliente.idCidade);

            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {
                dataReader.Read();
                idCliente = Convert.ToInt32(dataReader["idcliente"]);
            }
            dataReader.Close();
            return (idCliente);
        }

        public Cliente GetCliente(int? id)
        {
            Cliente cliente = new Cliente();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM cliente WHERE idcliente= @id AND statuscliente = 'TRUE' ";
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cliente.idCliente = (int)reader["idcliente"];
                cliente.nomeCliente = (String)reader["nomecliente"];
                cliente.telefoneCliente = (String)reader["telefonecliente"];
                cliente.cpfCnpjCliente = (String)reader["cpfcnpjcliente"];
                cliente.idCidade = (int)reader["idcidade"];
            }
            return cliente;
        }

        public void Excluir(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE cliente SET statuscliente = 'FALSE' WHERE idcliente = @id";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public void Alterar(Cliente cliente)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @" UPDATE cliente SET  nomecliente = @nome , telefonecliente = @telefone "
                              +" , cpfcnpjcliente = @cpfcnpj , idcidade = @idcidade WHERE idcliente = @idcliente                ";

            cmd.Parameters.AddWithValue("@idcliente", cliente.idCliente);
            cmd.Parameters.AddWithValue("@nome", cliente.nomeCliente);
            cmd.Parameters.AddWithValue("@telefone", cliente.telefoneCliente);
            cmd.Parameters.AddWithValue("@cpfcnpj", cliente.cpfCnpjCliente);
            cmd.Parameters.AddWithValue("@idcidade", cliente.idCidade);

            cmd.ExecuteNonQuery();
        }
    }
}