﻿using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CosultaDeliveryCliente.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PendienteController : ControllerBase
    {
        private SqlConnection _connectionKlk;
        private SqlConnection _connectionDamasco;
        public IConfiguration _configuration { get; }
        public PendienteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void connection()
        {
            string connectionKlk = _configuration["ConnectionStrings:SQLConnection"];
            _connectionKlk = new SqlConnection(connectionKlk);
        }

        public void connectionDamsco()
        {
            string connectionDamasco = _configuration["ConnectionStrings:SQLConnection2"];
            _connectionDamasco = new SqlConnection(connectionDamasco);
        }
        // GET: api/<PendienteController>
        [HttpGet]
        public IEnumerable<ListadoFletyCliente> Get()
        {
            connection();
            var fletyClientes = new List<ListadoFletyCliente>();
            //agregar --and li.CodArticulo ='S0000007
            SqlCommand command = new SqlCommand("SELECT fa.NumFactura as  NumFactura  ,fa.CodCliente as CodCliente,fa.NomCliente as NomCliente, fe.Estatus as Estatus," +
                "fa.Sucursal as Sucursal,fa.Direccion as Direccion,fa.FechaFactura as FechaFactura, fe.Fecha_Actualizacion as Fecha_Actualizacion,fa.IDSucursal as IdSucursal, COUNT(*) AS TotalProductos FROM KLK_FACTURALINE li INNER JOIN flety fe ON li.NumFactura=fe.Numfactura AND LEFT(fe.Tienda, 2)=li.IDSucursal" +
                " INNER JOIN KLK_FACTURAHDR fa ON fa.NumFactura=li.NumFactura AND fa.IDSucursal=li.IDSucursal and li.CodArticulo ='S0000007' and fe.Estatus = 'Pendiente' " +
                " GROUP BY fa.CodCliente, fa.NumFactura,fa.NomCliente,fa.Sucursal,fe.Estatus,fa.Direccion,fa.FechaFactura,fe.Fecha_Actualizacion,fa.IDSucursal order by fe.Fecha_Actualizacion desc;", _connectionKlk);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            System.Data.DataTable dataTable = new System.Data.DataTable();

            _connectionKlk.Open();
            adapter.Fill(dataTable);

            foreach (DataRow item in dataTable.Rows)

            {

                fletyClientes.Add(new ListadoFletyCliente
                {

                    NumFactura = Convert.ToString(item["NumFactura"]),
                    CodCliente = Convert.ToString(item["CodCliente"]).Trim(),
                    NomCliente = Convert.ToString(item["NomCliente"]).Trim(),
                    Status = Convert.ToString(item["Estatus"]).Trim(),
                    Sucursal = Convert.ToString(item["Sucursal"]).Trim(),
                    IdSucursal = Convert.ToString(item["IdSucursal"]).Trim(),
                    Direccion = Convert.ToString(item["Direccion"]).Trim(),
                    FechaActualizacion = Convert.ToDateTime(item["Fecha_Actualizacion"]),
                    FechaFactura = Convert.ToDateTime(item["FechaFactura"]),
                });
            }
            _connectionKlk.Close();
            return fletyClientes;
        }

        // GET api/<PendienteController>/5
        [HttpGet("getfactura/{numfactura}/sucursal/{sucursal}")]
        public FletyCliente GetClientes(string numfactura, string sucursal)
        {
            FletyCliente fletyCliente = new FletyCliente();

            connection();
            //cabcera del cliente

            SqlCommand facturaHeaderCommand = new SqlCommand("select fa.NumFactura as NumFactura,fa.CodCliente as CodCliente," +
                "fa.NomCliente as NomCliente," +
                "fa.Telefono as Telefono,fe.Estatus as Estatus,fa.Sucursal as Sucursal,fa.Direccion as Direccion,fa.FechaFactura as FechaFac,fe.Fecha_Actualizacion as FechaActFlety from KLK_FACTURAHDR fa " +
                "join flety fe on fa.NumFactura=fe.Numfactura and LEFT(fe.Tienda, 2)=fa.IDSucursal where fa.NumFactura=@numfactura and fa.IDSucursal = @IdSucursal", _connectionKlk);

            SqlDataAdapter adapterHeader = new SqlDataAdapter(facturaHeaderCommand);

            facturaHeaderCommand.Parameters.AddWithValue("@numfactura", numfactura);
            facturaHeaderCommand.Parameters.AddWithValue("@IdSucursal", sucursal);
            _connectionKlk.Open();
            using (var reader = facturaHeaderCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    try
                    {
                        fletyCliente.NumFactura = Convert.ToString(reader["NumFactura"]).Trim();
                        fletyCliente.CodCliente = Convert.ToString(reader["CodCliente"]).Trim();
                        fletyCliente.NomCliente = Convert.ToString(reader["NomCliente"]).Trim();
                        fletyCliente.Status = Convert.ToString(reader["Estatus"]).Trim();
                        fletyCliente.Sucursal = Convert.ToString(reader["Sucursal"]).Trim();
                        fletyCliente.NumeroTelefono = Convert.ToString(reader["Telefono"]).Trim();
                        fletyCliente.FechaFactura = Convert.ToDateTime(reader["FechaFac"]);
                        fletyCliente.FechaActualizacion = Convert.ToDateTime(reader["FechaActFlety"]);
                        fletyCliente.Direccion = Convert.ToString(reader["Direccion"]).Trim();

                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            _connectionKlk.Close();
            //cabaecera del product
            List<Articulos> listaArticulos = new List<Articulos>();
            SqlCommand commandLineaFactura = new SqlCommand("select fa.NumFactura as  NumFactura,li.CodArticulo as CodArticulo,li.Descripcion as Descripcion,li.Cantidad as Cantidad from KLK_FACTURALINE li" +
                " inner join flety fe on li.NumFactura=fe.Numfactura and LEFT(fe.Tienda, 2)=li.IDSucursal" +
                " inner join KLK_FACTURAHDR fa on fa.NumFactura=li.NumFactura and fa.IDSucursal=li.IDSucursal " +
                "where fa.NumFactura=@numFactura and li.IDSucursal=@idSucursal and li.CodArticulo not like '%S0000007'  ", _connectionKlk);

            commandLineaFactura.Parameters.AddWithValue("@numfactura", numfactura);
            commandLineaFactura.Parameters.AddWithValue("@IdSucursal", sucursal);
            SqlDataAdapter adapter = new SqlDataAdapter(commandLineaFactura);

            System.Data.DataTable dataTable = new System.Data.DataTable();
            _connectionKlk.Open();
            adapter.Fill(dataTable);
            _connectionKlk.Close();
            foreach (DataRow item in dataTable.Rows)
            {
                listaArticulos.Add(new Articulos
                {
                    CantidadArticulo = Convert.ToInt32(item["Cantidad"]),
                    CodArticulo = Convert.ToString(item["CodArticulo"]).Trim(),
                    Descripcion = Convert.ToString(item["Descripcion"]).Trim()



                });

            }

            fletyCliente.Articulos = listaArticulos;
            return fletyCliente;
        }

        // POST api/<PendienteController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // Patch api/<PendienteController>/5
        [HttpPatch("factura/{factura}")]
        public IActionResult PatchStatus(string factura, StatusFlety bodyStatus)
        {
            connection();

            _connectionKlk.Open();
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            DateTime localTime = TimeZoneInfo.ConvertTime(bodyStatus.FechaActualizacion, TimeZoneInfo.Utc, localTimeZone);
            SqlCommand actualziacionComando = new SqlCommand("UPDATE [dbo].[Flety] SET [Estatus] = @estatus,[Fecha_Actualizacion] = @fecha WHERE [Numfactura] = @numFactura", _connectionKlk);
            actualziacionComando.Parameters.AddWithValue("@estatus", bodyStatus.Status);
            actualziacionComando.Parameters.AddWithValue("@fecha", localTime);
            actualziacionComando.Parameters.AddWithValue("@numFactura", factura);
            actualziacionComando.ExecuteNonQuery();
            _connectionKlk.Close(); ;
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ingreso Exitoso" });
        }

        [HttpGet("tiendas")]
        public IEnumerable<TiendasDamasco> GetTiendas()
        {
            connectionDamsco();
            List<TiendasDamasco> tiendasDamasco = new List<TiendasDamasco>();
            SqlCommand sqlCommand = new SqlCommand("select nombre,id_sucursal from aplicativofront", _connectionDamasco);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

            System.Data.DataTable dataTable = new System.Data.DataTable();
            _connectionDamasco.Open();
            adapter.Fill(dataTable);
            _connectionDamasco.Close();
            foreach (DataRow item in dataTable.Rows)
            {
                tiendasDamasco.Add(new TiendasDamasco
                {
                    Sucursal = Convert.ToString(item["nombre"]).Trim(),
                    IdSucursal = Convert.ToString(item["id_sucursal"]).Trim(),

                });

            }
            return tiendasDamasco;


        }
      
        [HttpGet("portienda")]
        public IEnumerable<ListadoFletyCliente> GetFromTiendas(string store)
        {
            connection();
            var fletyClientes = new List<ListadoFletyCliente>();
            //agregar --and li.CodArticulo ='S0000007
            SqlCommand command = new SqlCommand("SELECT fa.NumFactura as  NumFactura  ,fa.CodCliente as CodCliente,fa.NomCliente as NomCliente, fe.Estatus as Estatus," +
                "fa.Sucursal as Sucursal,fa.Direccion as Direccion,fa.FechaFactura as FechaFactura, fe.Fecha_Actualizacion as Fecha_Actualizacion,fa.IDSucursal as IdSucursal, COUNT(*) AS TotalProductos FROM KLK_FACTURALINE li INNER JOIN flety fe ON li.NumFactura=fe.Numfactura AND LEFT(fe.Tienda, 2)=li.IDSucursal" +
                " INNER JOIN KLK_FACTURAHDR fa ON fa.NumFactura=li.NumFactura AND fa.IDSucursal=li.IDSucursal and li.CodArticulo ='S0000007' and fa.IDSucursal = @IdSucursal  and  fe.Estatus = 'Pendiente'  GROUP BY fa.CodCliente, fa.NumFactura,fa.NomCliente,fa.Sucursal,fe.Estatus,fa.Direccion,fa.FechaFactura,fe.Fecha_Actualizacion,fa.IDSucursal order by fe.Fecha_Actualizacion desc", _connectionKlk);
            //command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            command.Parameters.AddWithValue("@IdSucursal", store);

           
            _connectionKlk.Open();
            adapter.Fill(dataTable);

            foreach (DataRow item in dataTable.Rows)

            {

                fletyClientes.Add(new ListadoFletyCliente
                {

                    NumFactura = Convert.ToString(item["NumFactura"]),
                    CodCliente = Convert.ToString(item["CodCliente"]).Trim(),
                    NomCliente = Convert.ToString(item["NomCliente"]).Trim(),
                    Status = Convert.ToString(item["Estatus"]).Trim(),
                    Sucursal = Convert.ToString(item["Sucursal"]).Trim(),
                    IdSucursal = Convert.ToString(item["IdSucursal"]).Trim(),
                    Direccion = Convert.ToString(item["Direccion"]).Trim(),
                    FechaActualizacion = Convert.ToDateTime(item["Fecha_Actualizacion"]),
                    FechaFactura = Convert.ToDateTime(item["FechaFactura"]),
                });
            }
            _connectionKlk.Close();
            return fletyClientes;
        }
        [HttpGet("statusfecha")]
        public IEnumerable<ListadoFletyCliente> GetEntregasFletyPorFechaYStatus(string status, DateTime startDate, DateTime endDate)
        {
            connection();
            var fletyClientes = new List<ListadoFletyCliente>();
            //agregar --and li.CodArticulo ='S0000007
            SqlCommand command = new SqlCommand("SELECT fa.NumFactura as  NumFactura  ,fa.CodCliente as CodCliente,fa.NomCliente as NomCliente, fe.Estatus as Estatus," +
                "fa.Sucursal as Sucursal,fa.Direccion as Direccion,fa.FechaFactura as FechaFactura, fe.Fecha_Actualizacion as Fecha_Actualizacion,fa.IDSucursal as IdSucursal, " +
                "COUNT(*) AS TotalProductos FROM KLK_FACTURALINE li INNER JOIN flety fe ON li.NumFactura=fe.Numfactura AND LEFT(fe.Tienda, 2)=li.IDSucursal" +
                " INNER JOIN KLK_FACTURAHDR fa ON fa.NumFactura=li.NumFactura AND fa.IDSucursal=li.IDSucursal and li.CodArticulo ='S0000007' and fe.Estatus = @Estatus    and fa.FechaFactura  BETWEEN @StartDate AND @EndDate GROUP BY fa.CodCliente, fa.NumFactura,fa.NomCliente,fa.Sucursal,fe.Estatus,fa.Direccion,fa.FechaFactura,fe.Fecha_Actualizacion,fa.IDSucursal order by fe.Fecha_Actualizacion desc", _connectionKlk);
            // command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            command.Parameters.AddWithValue("@Estatus", "Pendiente");

            command.Parameters.AddWithValue("@StartDate", startDate.Date);
            command.Parameters.AddWithValue("@EndDate", endDate.Date);
            _connectionKlk.Open();
            adapter.Fill(dataTable);

            foreach (DataRow item in dataTable.Rows)

            {

                fletyClientes.Add(new ListadoFletyCliente
                {

                    NumFactura = Convert.ToString(item["NumFactura"]),
                    CodCliente = Convert.ToString(item["CodCliente"]).Trim(),
                    NomCliente = Convert.ToString(item["NomCliente"]).Trim(),
                    Status = Convert.ToString(item["Estatus"]).Trim(),
                    Sucursal = Convert.ToString(item["Sucursal"]).Trim(),
                    IdSucursal = Convert.ToString(item["IdSucursal"]).Trim(),
                    Direccion = Convert.ToString(item["Direccion"]).Trim(),
                    FechaActualizacion = Convert.ToDateTime(item["Fecha_Actualizacion"]),
                    FechaFactura = Convert.ToDateTime(item["FechaFactura"]),
                });
            }
            _connectionKlk.Close();
            return fletyClientes;
        }

        [HttpGet("statustiendafecha")]
        public IEnumerable<ListadoFletyCliente> GetFechaStatusTiendas( string store, DateTime startDate, DateTime endDate)
        {
            connection();
            var fletyClientes = new List<ListadoFletyCliente>();
            //agregar --and li.CodArticulo ='S0000007
            SqlCommand command = new SqlCommand("GetEntregasFletyPorRangoDeFechaTiendaEstatus", _connectionKlk);
            command.CommandType = System.Data.CommandType.StoredProcedure;



            command.Parameters.AddWithValue("@Estatus", "Pendiente");
            command.Parameters.AddWithValue("@IdSucursal", store);
            command.Parameters.AddWithValue("@FechaDesde", startDate.Date);
            command.Parameters.AddWithValue("@FechaHasta", endDate.Date);


            SqlDataAdapter adapter = new SqlDataAdapter(command);
            System.Data.DataTable dataTable = new System.Data.DataTable();

            _connectionKlk.Open();
            adapter.Fill(dataTable);

            foreach (DataRow item in dataTable.Rows)

            {

                fletyClientes.Add(new ListadoFletyCliente
                {

                    NumFactura = Convert.ToString(item["NumFactura"]),
                    CodCliente = Convert.ToString(item["CodCliente"]).Trim(),
                    NomCliente = Convert.ToString(item["NomCliente"]).Trim(),
                    Status = Convert.ToString(item["Estatus"]).Trim(),
                    Sucursal = Convert.ToString(item["Sucursal"]).Trim(),
                    IdSucursal = Convert.ToString(item["IdSucursal"]).Trim(),
                    Direccion = Convert.ToString(item["Direccion"]).Trim(),
                    FechaActualizacion = Convert.ToDateTime(item["Fecha_Actualizacion"]),
                    FechaFactura = Convert.ToDateTime(item["FechaFactura"]),
                });
            }
            _connectionKlk.Close();
            return fletyClientes;
        }

        [HttpGet("clientespendientesfecha")]
        public IEnumerable<ListadoFletyCliente> GetFechaClientes(DateTime startDate, DateTime endDate)
        {
            connection();
            var fletyClientes = new List<ListadoFletyCliente>();
            //agregar --and li.CodArticulo ='S0000007
            SqlCommand command = new SqlCommand("SELECT fa.NumFactura as  NumFactura  ,fa.CodCliente as CodCliente,fa.NomCliente as NomCliente, fe.Estatus as Estatus," +
                "fa.Sucursal as Sucursal,fa.Direccion as Direccion,fa.FechaFactura as FechaFactura, fe.Fecha_Actualizacion as Fecha_Actualizacion,fa.IDSucursal as IdSucursal, COUNT(*) AS TotalProductos FROM KLK_FACTURALINE li INNER JOIN flety fe ON li.NumFactura=fe.Numfactura AND LEFT(fe.Tienda, 2)=li.IDSucursal" +
                " INNER JOIN KLK_FACTURAHDR fa ON fa.NumFactura=li.NumFactura AND fa.IDSucursal=li.IDSucursal and li.CodArticulo ='S0000007'  and fe.Estatus = 'Pendiente' and fa.FechaFactura  BETWEEN @FechaDesde AND @FechaHasta " +
                " GROUP BY fa.CodCliente, fa.NumFactura,fa.NomCliente,fa.Sucursal,fe.Estatus,fa.Direccion,fa.FechaFactura,fe.Fecha_Actualizacion,fa.IDSucursal order by fe.Fecha_Actualizacion desc;", _connectionKlk);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            command.Parameters.AddWithValue("@FechaDesde", startDate.Date);
            command.Parameters.AddWithValue("@FechaHasta", endDate.Date);
            _connectionKlk.Open();
            adapter.Fill(dataTable);

            foreach (DataRow item in dataTable.Rows)

            {

                fletyClientes.Add(new ListadoFletyCliente
                {

                    NumFactura = Convert.ToString(item["NumFactura"]),
                    CodCliente = Convert.ToString(item["CodCliente"]).Trim(),
                    NomCliente = Convert.ToString(item["NomCliente"]).Trim(),
                    Status = Convert.ToString(item["Estatus"]).Trim(),
                    Sucursal = Convert.ToString(item["Sucursal"]).Trim(),
                    IdSucursal = Convert.ToString(item["IdSucursal"]).Trim(),
                    Direccion = Convert.ToString(item["Direccion"]).Trim(),
                    FechaActualizacion = Convert.ToDateTime(item["Fecha_Actualizacion"]),
                    FechaFactura = Convert.ToDateTime(item["FechaFactura"]),
                });
            }
            _connectionKlk.Close();
            return fletyClientes;
        }

        public IEnumerable<ListadoFletyCliente> GetStatusTiendas()
        {
            connection();
            var fletyClientes = new List<ListadoFletyCliente>();
            //agregar --and li.CodArticulo ='S0000007
            SqlCommand command = new SqlCommand("SELECT fa.NumFactura as  NumFactura  ,fa.CodCliente as CodCliente,fa.NomCliente as NomCliente, fe.Estatus as Estatus," +
                "fa.Sucursal as Sucursal,fa.Direccion as Direccion,fa.FechaFactura as FechaFactura, fe.Fecha_Actualizacion as Fecha_Actualizacion,fa.IDSucursal as IdSucursal, COUNT(*) AS TotalProductos FROM KLK_FACTURALINE li INNER JOIN flety fe ON li.NumFactura=fe.Numfactura AND LEFT(fe.Tienda, 2)=li.IDSucursal" +
                " INNER JOIN KLK_FACTURAHDR fa ON fa.NumFactura=li.NumFactura AND fa.IDSucursal=li.IDSucursal and li.CodArticulo ='S0000007' " +
                " GROUP BY fa.CodCliente, fa.NumFactura,fa.NomCliente,fa.Sucursal,fe.Estatus,fa.Direccion,fa.FechaFactura,fe.Fecha_Actualizacion,fa.IDSucursal order by fa.FechaFactura desc;", _connectionKlk);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            System.Data.DataTable dataTable = new System.Data.DataTable();

            _connectionKlk.Open();
            adapter.Fill(dataTable);

            foreach (DataRow item in dataTable.Rows)

            {

                fletyClientes.Add(new ListadoFletyCliente
                {

                    NumFactura = Convert.ToString(item["NumFactura"]),
                    CodCliente = Convert.ToString(item["CodCliente"]).Trim(),
                    NomCliente = Convert.ToString(item["NomCliente"]).Trim(),
                    Status = Convert.ToString(item["Estatus"]).Trim(),
                    Sucursal = Convert.ToString(item["Sucursal"]).Trim(),
                    IdSucursal = Convert.ToString(item["IdSucursal"]).Trim(),
                    Direccion = Convert.ToString(item["Direccion"]).Trim(),
                    FechaActualizacion = Convert.ToDateTime(item["Fecha_Actualizacion"]),
                    FechaFactura = Convert.ToDateTime(item["FechaFactura"]),
                });
            }
            _connectionKlk.Close();
            return fletyClientes;
        }
    }
}
