using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CosultaDeliveryCliente.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanceladoController : ControllerBase
    {
        private SqlConnection _connectionKlk;
        private SqlConnection _connectionDamasco;
        public IConfiguration _configuration { get; }
        public CanceladoController(IConfiguration configuration)
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
        // GET: api/<CanceladoController>
        [HttpGet]
       
        public IEnumerable<ListadoFletyCliente> Get()
        {
            connection();
            var fletyClientes = new List<ListadoFletyCliente>();
            //agregar --and li.CodArticulo ='S0000007
            SqlCommand command = new SqlCommand("SELECT fa.NumFactura as  NumFactura  ,fa.CodCliente as CodCliente,fa.NomCliente as NomCliente, fe.Estatus as Estatus," +
                "fa.Sucursal as Sucursal,fa.Direccion as Direccion,fa.FechaFactura as FechaFactura, fe.Fecha_Actualizacion as Fecha_Actualizacion,fa.IDSucursal as IdSucursal, COUNT(*) AS TotalProductos FROM KLK_FACTURALINE li INNER JOIN flety fe ON li.NumFactura=fe.Numfactura AND LEFT(fe.Tienda, 2)=li.IDSucursal" +
                " INNER JOIN KLK_FACTURAHDR fa ON fa.NumFactura=li.NumFactura AND fa.IDSucursal=li.IDSucursal and li.CodArticulo ='S0000007' and fe.Estatus='Cancelado' " +
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

        [HttpGet("canceladoportienda")]
        public IEnumerable<ListadoFletyCliente> GetFromTiendas(string store)
        {
            connection();
            var fletyClientes = new List<ListadoFletyCliente>();
            //agregar --and li.CodArticulo ='S0000007
            SqlCommand command = new SqlCommand("SELECT fa.NumFactura as  NumFactura  ,fa.CodCliente as CodCliente,fa.NomCliente as NomCliente, fe.Estatus as Estatus," +
                "fa.Sucursal as Sucursal,fa.Direccion as Direccion,fa.FechaFactura as FechaFactura, fe.Fecha_Actualizacion as Fecha_Actualizacion,fa.IDSucursal as IdSucursal, COUNT(*) AS TotalProductos FROM KLK_FACTURALINE li INNER JOIN flety fe ON li.NumFactura=fe.Numfactura AND LEFT(fe.Tienda, 2)=li.IDSucursal" +
                " INNER JOIN KLK_FACTURAHDR fa ON fa.NumFactura=li.NumFactura AND fa.IDSucursal=li.IDSucursal and li.CodArticulo ='S0000007' and fa.IDSucursal = @IdSucursal  and  fe.Estatus = 'Cancelado'  GROUP BY fa.CodCliente, fa.NumFactura,fa.NomCliente,fa.Sucursal,fe.Estatus,fa.Direccion,fa.FechaFactura,fe.Fecha_Actualizacion,fa.IDSucursal order by fe.Fecha_Actualizacion desc", _connectionKlk);
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

        [HttpGet("flotastatustiendafecha")]
        public IEnumerable<ListadoFletyCliente> GetFechaStatusTiendas(string store, DateTime startDate, DateTime endDate)
        {
            connection();
            var fletyClientes = new List<ListadoFletyCliente>();
            //agregar --and li.CodArticulo ='S0000007
            SqlCommand command = new SqlCommand("GetEntregasFletyPorRangoDeFechaTiendaEstatus", _connectionKlk);
            command.CommandType = System.Data.CommandType.StoredProcedure;



            command.Parameters.AddWithValue("@Estatus", "Cancelado");
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

        [HttpGet("flotadamascofecha")]
        public IEnumerable<ListadoFletyCliente> GetFechaClientes(DateTime startDate, DateTime endDate)
        {
            connection();
            var fletyClientes = new List<ListadoFletyCliente>();
            //agregar --and li.CodArticulo ='S0000007
            SqlCommand command = new SqlCommand("SELECT fa.NumFactura as  NumFactura  ,fa.CodCliente as CodCliente,fa.NomCliente as NomCliente, fe.Estatus as Estatus," +
                "fa.Sucursal as Sucursal,fa.Direccion as Direccion,fa.FechaFactura as FechaFactura, fe.Fecha_Actualizacion as Fecha_Actualizacion,fa.IDSucursal as IdSucursal, COUNT(*) AS TotalProductos FROM KLK_FACTURALINE li INNER JOIN flety fe ON li.NumFactura=fe.Numfactura AND LEFT(fe.Tienda, 2)=li.IDSucursal" +
                " INNER JOIN KLK_FACTURAHDR fa ON fa.NumFactura=li.NumFactura AND fa.IDSucursal=li.IDSucursal and li.CodArticulo ='S0000007'  and fe.Estatus = 'Cancelado' and fa.FechaFactura  BETWEEN @FechaDesde AND @FechaHasta " +
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

    }
}
