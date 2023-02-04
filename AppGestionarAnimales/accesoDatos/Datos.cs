﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace AppTiendaMascotas.accesoDatos
{
    class Datos
    {
		public static string cadenaConexion = "";

		//metodo que crea una intruccion dml;
		public int ejecutarDML(string consulta) 
        {
			int filasAfectadas = 0;
			OracleConnection miConexion = new OracleConnection(cadenaConexion);
			OracleCommand miComando = new OracleCommand(consulta, miConexion);
			try
			{
				miConexion.Open();
				filasAfectadas = miComando.ExecuteNonQuery();
				miConexion.Close();
				return filasAfectadas;
			}
			catch (Exception ex)
			{
				miConexion.Close();
				MessageBox.Show("Ocurrió un error con la base de datos: " + ex.Message);
			}
			return filasAfectadas;
		}

		public void setCadenaConexion(string userId, string hostName, string portNumber, string password)
		{
			cadenaConexion = "Data Source = " + hostName + ":" + portNumber + "/ xe; User Id = " + userId + "; Password = " + password;
		}

		public string getCadenaConexion()
		{
			return cadenaConexion;
		}

		public DataSet ejecutarSELECT(string consulta) 
        {
            //paso1: crear un dataset vacio
            DataSet ds = new DataSet();
            //paso2: creo un adaptador
            OracleDataAdapter miAdaptador = new OracleDataAdapter(consulta,cadenaConexion);
            //paso3: a traves del adaptador llenar el dataset
            miAdaptador.Fill(ds, "ResultadoDatos");
            return ds;
        }

    }
}
