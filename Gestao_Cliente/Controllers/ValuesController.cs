using Gestao_Cliente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;

namespace Gestao_Cliente.Controllers
{
    public class ValuesController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString);
        Clientes c = new Clientes();
        // GET api/values
        public List<Clientes> Get()
        {
            SqlDataAdapter da = new SqlDataAdapter("usp_GetCliente", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Clientes> lstCliente = new List<Clientes>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Clientes cli = new Clientes();
                    cli.cpf = dt.Rows[i]["cpf"].ToString();
                    cli.nome = dt.Rows[i]["nome"].ToString();
                    cli.tipo_Cliente = dt.Rows[i]["tipo_Cliente"].ToString();
                    cli.sexo = dt.Rows[i]["sexo"].ToString();
                    cli.situacao_Cliente = dt.Rows[i]["situacao_Cliente"].ToString();
                    lstCliente.Add(cli);
                }
            }  
                if(lstCliente.Count > 0)
                {
                    return lstCliente;
                }
                else
                {
                    return null;
                }
            
        }

        // GET api/values/5
        public Clientes Get(string id)
        {
            SqlDataAdapter da = new SqlDataAdapter("usp_GetClienteCPF", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@cpf", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Clientes cli = new Clientes();

            if (dt.Rows.Count > 0)
            {


                cli.cpf = dt.Rows[0]["cpf"].ToString();
                cli.nome = dt.Rows[0]["nome"].ToString();
                cli.tipo_Cliente = dt.Rows[0]["tipo_Cliente"].ToString();
                cli.sexo = dt.Rows[0]["sexo"].ToString();
                cli.situacao_Cliente = dt.Rows[0]["situacao_Cliente"].ToString();


            }
                if (cli != null)
                {
                    return cli;
                }
                else
                {
                    return null;
                }
            
        }

        // POST api/values
        public string Post(Clientes clientes)
        {
            string msg = "";
            if(clientes != null)
            {
                SqlCommand cmd = new SqlCommand("usp_AddCliente", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cpf", clientes.cpf);
                cmd.Parameters.AddWithValue("@nome", clientes.nome);
                cmd.Parameters.AddWithValue("@tipo_Cliente", clientes.tipo_Cliente);
                cmd.Parameters.AddWithValue("@sexo", clientes.sexo);
                cmd.Parameters.AddWithValue("@situacao_Cliente", clientes.situacao_Cliente);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if(i > 0)
                {
                    msg = "Foi inserido";
                }
                else
                {
                    msg = "Erro";
                }
            }
            return msg;
        }

        // PUT api/values/5
        public string Put(string id, Clientes clientes)
        {
            string msg = "";
            if (clientes != null)
            {
                SqlCommand cmd = new SqlCommand("usp_UpdateCliente", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cpf", id);
                cmd.Parameters.AddWithValue("@nome", clientes.nome);
                cmd.Parameters.AddWithValue("@tipo_Cliente", clientes.tipo_Cliente);
                cmd.Parameters.AddWithValue("@sexo", clientes.sexo);
                cmd.Parameters.AddWithValue("@situacao_Cliente", clientes.situacao_Cliente);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
                {
                    msg = "Foi atualizado";
                }
                else
                {
                    msg = "Erro";
                }
            }
            return msg;
        }

        // DELETE api/values/5
        public string Delete(string id)
        {
            string msg = "";
           
                SqlCommand cmd = new SqlCommand("usp_DeleteCliente", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cpf", id);
               
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
                {
                    msg = "Foi deletado";
                }
                else
                {
                    msg = "Erro";
                }
            
            return msg;
        }
    }
}
