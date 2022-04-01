using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Sopra.Labs.ConsoleApp2.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.SopraSteria._1
{

    public class Program
    {
        static void Main(string[] args)
        {
            BusquedasComplejas();
        }

        static void Ejercicios01042022()
        {

            var context = new ModelNorthwind();
            DateTime fecha = DateTime.Now;
            //Listado de empleados que son mayores sus jefes(ReportsTo es id Jefe)

            Console.WriteLine("Empleados mayores que su jefe");




            var empleados = context.Employees
            .Where(r => r.BirthDate < context.Employees
                    .Where(s => s.EmployeeID == r.ReportsTo)
                    .Select(s => s.BirthDate)
                    .FirstOrDefault())
            .ToList();

            Console.WriteLine("");
            foreach (var item in empleados)
            {
                Console.WriteLine($"Nombre:{item.FirstName}");
                Console.WriteLine($"Fecha de nacimiento:{item.BirthDate}");
                Console.WriteLine($"Pais:{item.Country}");
                Console.WriteLine($"Ciudad:{item.City}");
                Console.WriteLine("");
            }

            //Listado de productos: Nombre del Producto, Stock, Valor del Stock

            Console.WriteLine("Listado Productos");
            var productosStock = context.Products
             .Select(s => new { s.ProductName, s.UnitsInStock, Total = s.UnitsInStock * s.UnitPrice })
             .ToList();

            Console.WriteLine("");
            foreach (var item in productosStock)
            {

                Console.WriteLine($"Nombre del producto:{item.ProductName}");
                Console.WriteLine($"Stock:{item.UnitsInStock}");
                Console.WriteLine($"Valor stock:{item.Total}");
                Console.WriteLine("");
            }

            //Listado de empleados: Nombre, Apellido, Número total de pedidos en 1997
            Console.WriteLine("Listado Empleados");

            var empleadosLista = context.Employees
           .OrderBy(r => r.EmployeeID)
           .Select(r => new
           {
               r.FirstName,
               r.LastName,
               numPedidos = context.Orders
                        .Count(s => s.OrderDate.Value.Year == 1997 && r.EmployeeID == s.EmployeeID)
           })
            .ToList();

            Console.WriteLine("");
            foreach (var item in empleadosLista)
            {

                Console.WriteLine($"Nombre:{item.FirstName}");
                Console.WriteLine($"Apellido:{item.LastName}");
                Console.WriteLine($"Numero pedidos:{item.numPedidos}");
                Console.WriteLine("");
            }

            //El tiempo medio en dias de la preparación pedido

            var tiempoMedio = context.Orders
                .Where(r => r.ShippedDate != null && r.OrderDate != null)
                .AsEnumerable()
                .Average(r => r.ShippedDate.Value.Subtract(r.OrderDate.Value).Days);

            Console.WriteLine("Tiempo medio: " + tiempoMedio);
            Console.WriteLine("");
        }
        static void Ejercicios31032022()
        {
            var context = new ModelNorthwind();
            DateTime fecha = DateTime.Now;
            //Clientes de Usa
            Console.WriteLine("Clientes USA");
            var clientesUSA = context.Customers
               .Where(r => r.Country == "USA")
               .OrderBy(r => r.City)
               .ToList();

            foreach (var item in clientesUSA)
            {
                Console.WriteLine($"ID:{item.CustomerID}");
                Console.WriteLine($"Empresa:{item.CompanyName}");
                Console.WriteLine($"Pais:{item.Country}");
                Console.WriteLine($"Ciudad:{item.City}");
                Console.WriteLine("");
            }

            //Proveedores (Suppliers) de BERLIN
            Console.WriteLine("Proveedores Berlin");
            var proveedoresBerlin = context.Suppliers
              .Where(r => r.City == "Berlin")
              .OrderBy(r => r.CompanyName)
              .ToList();

            foreach (var item in proveedoresBerlin)
            {
                Console.WriteLine($"ID:{item.SupplierID}");
                Console.WriteLine($"Empresa:{item.CompanyName}");
                Console.WriteLine($"Pais:{item.Country}");
                Console.WriteLine($"Ciudad:{item.City}");
                Console.WriteLine("");
            }

            //Los empleados con ID 3 , 5 y 8

            Console.WriteLine("Empleados con ID 3,5 y 8");

            /*   int[] employeesIds= new int[] {  3, 5, 8 };
               var empleados1 = context.Employees
               .Where(r => employeesIds.Contains(r.EmployeeID))
               .OrderBy(r => r.City)
               .ToList();

               var empleados2 = context.Employees
               .Where(r => new int[]{ 3, 5, 8 }.Contains(r.EmployeeID))
               .ToList();
          */
            var empleados = context.Employees
             .Where(r => (r.EmployeeID == 3 || r.EmployeeID == 5 || r.EmployeeID == 8))
             .OrderBy(r => r.City)
             .ToList();

            foreach (var item in empleados)
            {
                Console.WriteLine($"ID:{item.EmployeeID}");
                Console.WriteLine($"Nombre:{item.FirstName}");
                Console.WriteLine($"Apellido:{item.LastName}");
                Console.WriteLine($"Pais:{item.Country}");
                Console.WriteLine($"Ciudad:{item.City}");
                Console.WriteLine("");
            }

            //Productos con stock mayor de cero
            Console.WriteLine("Productos con stock mayor que 0");
            var productosStock = context.Products
             .Where(r => r.UnitsInStock > 0)
             .OrderBy(r => r.UnitsInStock)
             .ToList();

            foreach (var item in productosStock)
            {
                Console.WriteLine($"ID:{item.ProductID}");
                Console.WriteLine($"Nombre del producto:{item.ProductName}");
                Console.WriteLine($"Stock:{item.UnitsInStock}");
                Console.WriteLine("");
            }

            //Productos con stock mayor de cero de los proveedores con id 1, 3 y 5
            Console.WriteLine("Productos con stock mayor que 0 de los proveedores con id 1 , 3 y 5");
            var productosStockProveedor = context.Products
             .Where(r => (r.UnitsInStock > 0 && (r.SupplierID == 1 || r.SupplierID == 3 || r.SupplierID == 5)))
             .OrderBy(r => r.UnitsInStock)
             .ToList();

            foreach (var item in productosStockProveedor)
            {
                Console.WriteLine($"ID:{item.ProductID}");
                Console.WriteLine($"ID Proveedor:{item.SupplierID}");
                Console.WriteLine($"Nombre del producto:{item.ProductName}");
                Console.WriteLine($"Stock:{item.UnitsInStock}");
                Console.WriteLine("");
            }

            //Productos precio mayor de 20 y menor de 90
            Console.WriteLine("Productos con precio mayor que 20 y menor que 90");
            var productosPrice = context.Products
            .Where(r => r.UnitPrice > 20 && r.UnitPrice < 90)
            .OrderBy(r => r.UnitsInStock)
            .ToList();

            foreach (var item in productosPrice)
            {
                Console.WriteLine($"ID:{item.ProductID}");
                Console.WriteLine($"Nombre del producto:{item.ProductName}");
                Console.WriteLine($"Stock:{item.UnitsInStock}");
                Console.WriteLine($"Precio por unidad:{item.UnitPrice}");
                Console.WriteLine("");
            }

            //Pedidos entre 01.01.97 y 15.07.97
            Console.WriteLine("Pedidos entre");
            var pedidosentre = context.Orders
             .Where(r => (r.OrderDate >= new DateTime(1997, 01, 01) && r.OrderDate <= new DateTime(1997, 07, 15)))
             .OrderBy(r => r.OrderDate)
             .ToList();

            foreach (var item in pedidosentre)
            {
                Console.WriteLine($"ID:{item.OrderID}");
                Console.WriteLine($"Nombre del producto:{item.OrderDate}");
                Console.WriteLine($"Stock:{item.EmployeeID}");
                Console.WriteLine($"Precio por unidad:{item.CustomerID}");
                Console.WriteLine("");
            }

            //Pedidos del año 97 registrado por los empleados con id 1, 3, 4 y 8
            Console.WriteLine("Pedidos año 97");
            var pedidosano97 = context.Orders
              .Where(r => r.OrderDate.Value.Year == 1997 && (r.EmployeeID == 1 || r.EmployeeID == 3 || r.EmployeeID == 4 || r.EmployeeID == 8))
              .OrderBy(r => r.OrderDate)
              .ToList();

            foreach (var item in pedidosano97)
            {
                Console.WriteLine($"ID:{item.OrderID}");
                Console.WriteLine($"Nombre del producto:{item.OrderDate}");
                Console.WriteLine($"Stock:{item.EmployeeID}");
                Console.WriteLine($"Precio por unidad:{item.CustomerID}");
                Console.WriteLine("");
            }


            //Pedidos de Abril del 96
            Console.WriteLine("Pedidos año 96 en abril");
            var pedidosAbril = context.Orders
              .Where(r => r.OrderDate.Value.Year == 1996 && r.OrderDate.Value.Month == 4)
              .OrderBy(r => r.OrderDate)
              .ToList();

            foreach (var item in pedidosAbril)
            {
                Console.WriteLine($"ID:{item.OrderID}");
                Console.WriteLine($"Nombre del producto:{item.OrderDate}");
                Console.WriteLine($"Stock:{item.EmployeeID}");
                Console.WriteLine($"Precio por unidad:{item.CustomerID}");
                Console.WriteLine("");
            }

            //Pedidos realizados los dias 1 de cada mes del año 98
            Console.WriteLine("Pedidos año 98 dia 1");
            var pedidosdia1 = context.Orders
           .Where(r => r.OrderDate.Value.Year == 1998 && r.OrderDate.Value.Day == 1)
           .OrderBy(r => r.OrderDate)
           .ToList();

            foreach (var item in pedidosdia1)
            {
                Console.WriteLine($"ID:{item.OrderID}");
                Console.WriteLine($"Nombre del producto:{item.OrderDate}");
                Console.WriteLine($"Stock:{item.EmployeeID}");
                Console.WriteLine($"Precio por unidad:{item.CustomerID}");
                Console.WriteLine("");
            }

            //Clientes que no tienen FAX
            Console.WriteLine("Clientes sin fax");
            var clientesFax = context.Customers
             .Where(r => r.Fax == null)
             .OrderBy(r => r.City)
             .ToList();

            foreach (var item in clientesFax)
            {
                Console.WriteLine($"ID:{item.CustomerID}");
                Console.WriteLine($"Empresa:{item.CompanyName}");
                Console.WriteLine($"Pais:{item.Country}");
                Console.WriteLine($"Ciudad:{item.City}");
                Console.WriteLine("");
            }


            //Los 10 productos mas baratos
            Console.WriteLine("10 productos baratos");
            var productosBaratos = context.Products
                .Where(r => r.UnitsInStock > 0)
                .OrderByDescending(r => r.UnitPrice)
                .Take(10)
                .ToList();

            foreach (var item in productosBaratos)
            {
                Console.WriteLine($"ID:{item.ProductID}");
                Console.WriteLine($"ID Proveedor:{item.SupplierID}");
                Console.WriteLine($"Unidades:{item.UnitsInStock}");
                Console.WriteLine($"Precio unidad:{item.UnitPrice}");
                Console.WriteLine("");
            }

            //Los 10 productos mas caros con stock
            Console.WriteLine("10 productos caros");
            var productosCaros = context.Products
           .OrderBy(r => r.UnitPrice)
           .Take(10)
           .ToList();

            foreach (var item in productosBaratos)
            {
                Console.WriteLine($"ID:{item.ProductID}");
                Console.WriteLine($"ID Proveedor:{item.SupplierID}");
                Console.WriteLine($"Unidades:{item.UnitsInStock}");
                Console.WriteLine($"Precio unidad:{item.UnitPrice}");
                Console.WriteLine("");
            }

            //Empresas que comienzan por la letra B de UK
            Console.WriteLine("Empresas B");
            var empresasB = context.Customers
            .Where(r => r.CompanyName.StartsWith("B") && r.Country == "UK")
            .OrderBy(r => r.City)
            .ToList();

            foreach (var item in empresasB)
            {
                Console.WriteLine($"ID:{item.CustomerID}");
                Console.WriteLine($"Empresa:{item.CompanyName}");
                Console.WriteLine($"Pais:{item.Country}");
                Console.WriteLine($"Ciudad:{item.City}");
                Console.WriteLine("");
            }


            //Productos de la categoria 3 y 5
            Console.WriteLine("Productos baratos categoria 3 y 5");
            var productosCategoria = context.Products
            .Where(r => r.CategoryID == 3 || r.CategoryID == 50)
            .OrderBy(r => r.ProductName)
            .ToList();

            foreach (var item in productosCategoria)
            {
                Console.WriteLine($"ID:{item.ProductID}");
                Console.WriteLine($"Nombre del producto:{item.ProductName}");
                Console.WriteLine($"Stock:{item.UnitsInStock}");
                Console.WriteLine($"Precio por unidad:{item.UnitPrice}");
                Console.WriteLine("");
            }

            //Valor total del stock
            Console.WriteLine("Valor del stock");

            var valorStock = context.Products
                .Sum(r => r.UnitsInStock * r.UnitPrice);


            Console.WriteLine("Valor del stock " + valorStock);

            //Todos los pedidos de los clientes de argentina

            Console.WriteLine("Clientes Argentina");

            var clientesArg = context.Customers
           .Where(s => s.Country == "Argentina")
           .Select(s => s.CustomerID)
           .ToList();

            var pedidos = context.Orders
             .Where(r => clientesArg.Contains(r.CustomerID))
            .ToList();


            var pedidos2 = context.Orders
            .Where(r => context.Customers
           .Where(s => s.Country == "Argentina")
           .Select(s => s.CustomerID)
           .ToList().Contains(r.CustomerID))
           .ToList();


        }
        static void TrabajandoConEF()
        {
            //EF o EntityFrameworkCore (Manejamos la base de datos como colecciones)

            var context = new ModelNorthwind();

            //Consultas:

            var resultado = context.Products
                .Where(r => r.ProductName.Contains("Queso"))
                .ToList();


            foreach (var item in resultado)
            {
                Console.WriteLine(item.ProductName);
            }

            //Eliminar Datos:

            var cliente2 = context.Customers
              .Where(r => r.CustomerID == "DEMO2")
              .FirstOrDefault();

            if (cliente2 == null)
            {

            }
            else
            {
                context.Customers.Remove(cliente2);

            }
            context.SaveChanges();

            //Actualizar datos:

            var cliente = context.Customers
                .Where(r => r.Country == "Spain")
                .OrderBy(r => r.City)
                .ToList();


            foreach (var item in cliente)
            {
                item.Country = "España";
            }

            context.SaveChanges();
            //Insertar Datos:

            var nuevocliente = new Customer()
            {
                CustomerID = "DEMO",
                CompanyName = "Empresa Demo SL",
                ContactName = "Borja Cabeza",
                ContactTitle = "Gerente",
                Address = "Calles Unos, SN",
                PostalCode = "28033",
                City = "Madrid",
                Country = "España",
                Region = "Madrid",
                Phone = "900 100 100",
                Fax = "900 101 101"
            };
            var nuevocliente2 = new Customer()
            {
                CustomerID = "DEMO2",
                CompanyName = "Empresa Demo2 SL",
                ContactName = "Borja Cabeza",
                ContactTitle = "Gerente",
                Address = "Calles Unos, SN",
                PostalCode = "28033",
                City = "Madrid",
                Country = "España",
                Phone = "900 100 100",

            };


            //context.Customers.Add(nuevocliente);
            //context.Customers.Add(nuevocliente2);
            context.SaveChanges();

            Console.WriteLine("Registro insertado correctamente");

            //Consulta de Datos: SELECT * FROM dbo.Customers WHERE Country = 'Spain' ORDER BY City 


            var clientes = context.Customers
                .Where(r => r.Country == "Spain")
                .OrderBy(r => r.City)
                .ToList();

            foreach (var item in clientes)
            {
                Console.WriteLine($"ID:{item.CustomerID}");
                Console.WriteLine($"Empresa:{item.CompanyName}");
                //Console.WriteLine($"Empresa:{reader["CostumerID"]}");
                Console.WriteLine($"Pais:{item.Country}");
                Console.WriteLine($"Ciudad:{item.City}");
                Console.WriteLine("");
            }
        }

        static void TrabajandoConADONET()
        {
            //ADO.NET Access Data Objet (manejamos la base de datos con comandos de Transat-Sql

            //Consulta de Datos: SELECT * FROM dbo.Customers WHERE Country = 'Spain' ORDER BY City

            //Crear cadena de conexion contra la base de datos

            var connectionString = new SqlConnectionStringBuilder()
            {
                DataSource = "LOCALHOST",       //Servido de Base de Datos, nombre o ip
                InitialCatalog = "NORTHWIND",   //Nombre de la base de datos
                UserID = "",                    //Usuario
                Password = "",                  //Contraseña
                IntegratedSecurity = true,      //true es Seguridad basada en Windows
                ConnectTimeout = 60
            };

            //Mostrar la cadena de conexion
            Console.WriteLine($"Conection String{connectionString.ToString()}");

            //Creamos un objeto conexion, que representa la conexion con la base de datos
            var connect = new SqlConnection()
            {
                ConnectionString = connectionString.ToString()
            };

            Console.WriteLine($"Estado:{connect.State}");
            connect.Open();
            Console.WriteLine($"Estado:{connect.State}");

            //Creamos el comando que lanzaremos a la base de datos
            var command = new SqlCommand()
            {
                Connection = connect,
                CommandText = "SELECT * FROM dbo.Customers WHERE Country = 'Spain' ORDER BY City"
            };

            //Ejecutamos el comando y recibir la respuesta

            SqlDataReader reader = command.ExecuteReader(); //Comando de tipo consulta SELECT   

            //int registros = command.ExecuteNonQuery();      //Comando no consultas INSERT, UPDATE, DELETE

            if (reader.HasRows == false)
            {
                Console.WriteLine("Registros no encontrados");
            }
            else
            {
                while (reader.Read() == true)
                {
                    Console.WriteLine($"ID:{reader["CustomerID"]}");
                    Console.WriteLine($"Empresa:{reader.GetValue(1)}");
                    //Console.WriteLine($"Empresa:{reader["CostumerID"]}");
                    Console.WriteLine($"Pais:{reader["Country"]}");
                    Console.WriteLine("");
                }
            }
            reader.Close();
            command.Dispose();
            connect.Close();
            connect.Dispose();
        }


        /// <summary>
        /// Búsquedeas básicas utilizando LINQ
        /// </summary>
        static void BusquedasBasicas()
        {
            //SELECT campos o columnas o *FROM servidor.basesdedatos.esquema.tabla WHERE filtro ORDER BY ordenación


            //SELECT Descripcion, Precio FROM ListaProductos WHERE precio > 2 ORDER BY Precio DESC

            var r5a = DataLists.ListaProductos.Where(r => r.Precio > 2)
                .OrderByDescending(r => r.Precio)
                .Select(r => new { r.Descripcion, r.Precio })
                      .ToList();

            var r5b = from r in DataLists.ListaProductos
                      where r.Precio > 2
                      orderby r.Precio descending
                      select new { r.Descripcion, r.Precio };


            foreach (var item in r5b)
            {
                Console.WriteLine($"{item.Descripcion} {item.Precio.ToString("N2")}");
            }


            //SELECT Descripcion, Precio FROM ListaProductos WHERE precio > 2 ORDER BY Precio DESC

            var r4a = DataLists.ListaProductos.Where(r => r.Precio > 2)
                .OrderByDescending(r => r.Precio)
                .ToList();

            var r4b = from r in DataLists.ListaProductos
                      where r.Precio > 2
                      orderby r.Precio descending
                      select r;


            foreach (var item in r4b)
            {
                Console.WriteLine($"{item.Id} {item.Descripcion} {item.Precio.ToString("N2")}");
            }

            //SELECT Descripcion, Precio FROM ListaProductos WHERE precio > 2

            var r3a = DataLists.ListaProductos.Where(r => r.Precio > 2)
                      .ToList();

            var r3b = from r in DataLists.ListaProductos
                      where r.Precio > 2
                      select r;


            foreach (var item in r3b)
            {
                Console.WriteLine($"{item.Id} {item.Descripcion} {item.Precio.ToString("N2")}");
            }


            //SELECT Id,Descripcion,Precio FROM ListaProductos
            //SELECT * FROM ListaProductos
            var r1a = DataLists.ListaProductos.AsEnumerable();
            var r1b = DataLists.ListaProductos.ToList();

            var r2a = from r in DataLists.ListaProductos
                      select r;

            var r2b = (from r in DataLists.ListaProductos
                       select r).ToList();


            foreach (var item in r2b)
            {
                Console.WriteLine($"{item.Id} {item.Descripcion}");
            }
        }

        static void BusquedasEjercicio()
        {
            //Clientes nacidos entre 1980 y 1990
            Console.WriteLine("Clientes nacidos entre 1980 y 1990:");
            Console.WriteLine("");
            DateTime fecha = DateTime.Now;
            var clientes1 = DataLists.ListaClientes
                .Where(r => (r.FechaNac.Year >= 1980 && r.FechaNac.Year <= 1990))
                 .Select(r => r.Nombre)
                 .ToList();

            foreach (string item in clientes1)
            {
                Console.WriteLine($"{item}");
            }

            Console.WriteLine("");

            //Clientes mayores de 25 años
            Console.WriteLine("Clientes Mayores de 25 años");
            Console.WriteLine("");
            var clientes2 = DataLists.ListaClientes
             .Where(r => ((fecha.Subtract(r.FechaNac).Days / 365) > 25))
              .Select(r => r.Nombre)
              .ToList();

            foreach (string item in clientes2)
            {
                Console.WriteLine($"{item}");
            }

            Console.WriteLine("");

            //Producto con el precio mas alto
            Console.WriteLine("Producto con el precio mas alto");
            Console.WriteLine("");

            var productos = DataLists.ListaProductos
                .Max(r => r.Precio);

            Console.WriteLine($"{ productos}");

            Console.WriteLine("");


            //Precio medio de todos los productos

            Console.WriteLine("Media de todos los productos");
            Console.WriteLine("");
            float media = 0;

            var productos2 = DataLists.ListaProductos
                .Average(r => r.Precio);

            media = productos2;
            Console.WriteLine($"{media}");

            Console.WriteLine("");


            //Productos con un precio inferior a la media

            Console.WriteLine("Producto con el precio mas alto");
            Console.WriteLine("");

            var productos3 = DataLists.ListaProductos
                .Where(r => r.Precio < media)
                .Select(r => r.Descripcion)
                .ToList();

            foreach (string item in productos3)
            {
                Console.WriteLine($"{item}");
            }

            Console.WriteLine("");

        }

        static void BusquedasComplejas()
        {
            var context = new ModelNorthwind();

            //Intersect

            //Clientes pedido producto 57

            var i1 = context.Order_Details
                .Include(s=>s.Order)
                .Where(s => s.ProductID == 57)
                .Select(s => s.Order.CustomerID)
                .ToList();




            //Clientes pedido producto 72 en 1997

            var i2 = context.Order_Details
              .Include(s => s.Order)
              .Where(s => s.ProductID == 72 && s.Order.OrderDate.Value.Year==1997)
              .Select(s => s.Order.CustomerID)
              .ToList();



            //Clientes coincidentes 57+72 en 1997

            var cliente = i1.Intersect(i2).ToList();


            foreach(var item in cliente) Console.WriteLine(item);

            Console.ReadKey();
            // 

            /////////////////////////////////////////////////////////////
            int[]id1 = { 44, 26, 125,32,71,128 };
            int[] id2 = { 44, 126, -125,72, 781, 14, 98, 32 };

            var resultado = id1.Intersect(id2).ToList();    

            foreach(var item in resultado)Console.WriteLine(item);


            //Include

            //Listado de pedidos de los clientes de USa

            var clientesUsa = context.Orders
               .Include(s => s.Customer)
               .Where(s=>s.Customer.Country =="USA")
               .ToList();


            foreach (var item in clientesUsa)
            {
                Console.WriteLine($"{item.Customer.CompanyName} {item.OrderID} {item.OrderDate}");
                Console.WriteLine("");
            }

            //Listado de empleados: Nombre, Apellido, Número total de pedidos en 1997

            var empleados97 = context.Employees         
               .Include(s => s.Orders)
               .Select(s => new {s.FirstName, s.LastName, pedidos = s.Orders.Count(r=>r.OrderDate.Value.Year==1997)})
               .ToList();

            foreach (var item in empleados97)
            {
                Console.WriteLine($"{item.FirstName} {item.LastName} {item.pedidos}");
            }

            //Productos de la categoria condiments y seafood

            var productos = context.Products
                .Include(s => s.Category)
                .Where(s=> s.Category.CategoryName == "Condiments" || s.Category.CategoryName == "Seafood")
                .ToList();

            foreach (var item in productos)
            {
                Console.WriteLine($"{item.ProductName} {item.UnitPrice}");
            }


            Console.ReadKey();
            ////////////////////////////////////////////////////////////////
            var clientes3 = context.Customers
                .Include(r=>r.Orders)
                .ToList();


            var clientes3b = (from r in context.Customers
                              select r).Include(r => r.Orders);

            foreach (var item in clientes3)
            {
                Console.WriteLine($"{item.CustomerID} {item.CompanyName}");

                var pedidos = context.Orders
                    .Where(r => r.CustomerID == item.CustomerID)
                    .ToList();

                foreach (var pedido in item.Orders)
                {
                    Console.Write($"{pedido.OrderID} ");
                    Console.WriteLine(Environment.NewLine);
                }
            }

            var clientes = context.Customers.ToList();

            foreach (var item in clientes)
            {
                Console.WriteLine($"{item.CustomerID} {item.CompanyName}");

                var pedidos = context.Orders
                    .Where(r => r.CustomerID == item.CustomerID)
                    .ToList();

                foreach (var pedido in pedidos)
                {
                    Console.Write($"{pedido.OrderID} ");
                    Console.WriteLine(Environment.NewLine);
                }
            }

            var clientes2 = context.Customers
                .Select(r => new
                {
                    r.CustomerID,
                    r.CompanyName,
                    Pedidos = context.Orders.Where(s => s.CustomerID == r.CustomerID)
                })
                .ToList();


            foreach (var item in clientes2)
            {
                Console.WriteLine($"{item.CustomerID} {item.CompanyName}");

                var pedidos = context.Orders
                    .Where(r => r.CustomerID == item.CustomerID)
                    .ToList();

                foreach (var pedido in item.Pedidos)
                {
                    Console.Write($"{pedido.OrderID} ");
                    Console.WriteLine(Environment.NewLine);

                }




            }



        }
    }


    /// <summary>
    /// Representa el Objeto Cliente
    /// </summary>
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNac { get; set; }
    }

    /// <summary>
    /// Representa el Objeto Producto
    /// </summary>
    public class Producto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public float Precio { get; set; }

        public int ProveedorID { get; set; }
    }

    /// <summary>
    /// Representa el Objeto Pedido
    /// </summary>
    public class Pedido
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaPedido { get; set; }
    }

    /// <summary>
    /// Representa el Objeto Linea de Pedido
    /// </summary>
    public class LineaPedido
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }

    /// <summary>
    /// Representa una Base de datos en memoria utilizando LIST
    /// </summary>
    public static class DataLists
    {
        private static List<Cliente> _listaClientes = new List<Cliente>() {
            new Cliente { Id = 1,   Nombre = "Carlos Gonzalez Rodriguez",   FechaNac = new DateTime(1980, 10, 10) },
            new Cliente { Id = 2,   Nombre = "Luis Gomez Fernandez",        FechaNac = new DateTime(1961, 4, 20) },
            new Cliente { Id = 3,   Nombre = "Ana Lopez Diaz ",             FechaNac = new DateTime(1947, 1, 15) },
            new Cliente { Id = 4,   Nombre = "Fernando Martinez Perez",     FechaNac = new DateTime(1981, 8, 5) },
            new Cliente { Id = 5,   Nombre = "Lucia Garcia Sanchez",        FechaNac = new DateTime(1973, 11, 3) }
        };

        private static List<Producto> _listaProductos = new List<Producto>()
        {
            new Producto { Id = 1,      Descripcion = "Boligrafo",          Precio = 0.35f },
            new Producto { Id = 2,      Descripcion = "Cuaderno grande",    Precio = 1.5f },
            new Producto { Id = 3,      Descripcion = "Cuaderno pequeño",   Precio = 1 },
            new Producto { Id = 4,      Descripcion = "Folios 500 uds.",    Precio = 3.55f },
            new Producto { Id = 5,      Descripcion = "Grapadora",          Precio = 5.25f },
            new Producto { Id = 6,      Descripcion = "Tijeras",            Precio = 2 },
            new Producto { Id = 7,      Descripcion = "Cinta adhesiva",     Precio = 1.10f },
            new Producto { Id = 8,      Descripcion = "Rotulador",          Precio = 0.75f },
            new Producto { Id = 9,      Descripcion = "Mochila escolar",    Precio = 12.90f },
            new Producto { Id = 10,     Descripcion = "Pegamento barra",    Precio = 1.15f },
            new Producto { Id = 11,     Descripcion = "Lapicero",           Precio = 0.55f },
            new Producto { Id = 12,     Descripcion = "Grapas",             Precio = 0.90f }
        };

        private static List<Pedido> _listaPedidos = new List<Pedido>()
        {
            new Pedido { Id = 1,     IdCliente = 1,  FechaPedido = new DateTime(2013, 10, 1) },
            new Pedido { Id = 2,     IdCliente = 1,  FechaPedido = new DateTime(2013, 10, 8) },
            new Pedido { Id = 3,     IdCliente = 1,  FechaPedido = new DateTime(2013, 10, 12) },
            new Pedido { Id = 4,     IdCliente = 1,  FechaPedido = new DateTime(2013, 11, 5) },
            new Pedido { Id = 5,     IdCliente = 2,  FechaPedido = new DateTime(2013, 10, 4) },
            new Pedido { Id = 6,     IdCliente = 3,  FechaPedido = new DateTime(2013, 7, 9) },
            new Pedido { Id = 7,     IdCliente = 3,  FechaPedido = new DateTime(2013, 10, 1) },
            new Pedido { Id = 8,     IdCliente = 3,  FechaPedido = new DateTime(2013, 11, 8) },
            new Pedido { Id = 9,     IdCliente = 3,  FechaPedido = new DateTime(2013, 11, 22) },
            new Pedido { Id = 10,    IdCliente = 3,  FechaPedido = new DateTime(2013, 11, 29) },
            new Pedido { Id = 11,    IdCliente = 4,  FechaPedido = new DateTime(2013, 10, 19) },
            new Pedido { Id = 12,    IdCliente = 4,  FechaPedido = new DateTime(2013, 11, 11) }
        };

        private static List<LineaPedido> _listaLineasPedido = new List<LineaPedido>()
        {
            new LineaPedido() { Id = 1,  IdPedido = 1,  IdProducto = 1,     Cantidad = 9 },
            new LineaPedido() { Id = 2,  IdPedido = 1,  IdProducto = 3,     Cantidad = 7 },
            new LineaPedido() { Id = 3,  IdPedido = 1,  IdProducto = 5,     Cantidad = 2 },
            new LineaPedido() { Id = 4,  IdPedido = 1,  IdProducto = 7,     Cantidad = 2 },
            new LineaPedido() { Id = 5,  IdPedido = 2,  IdProducto = 9,     Cantidad = 1 },
            new LineaPedido() { Id = 6,  IdPedido = 2,  IdProducto = 11,    Cantidad = 15 },
            new LineaPedido() { Id = 7,  IdPedido = 3,  IdProducto = 12,    Cantidad = 2 },
            new LineaPedido() { Id = 8,  IdPedido = 3,  IdProducto = 1,     Cantidad = 4 },
            new LineaPedido() { Id = 9,  IdPedido = 4,  IdProducto = 2,     Cantidad = 3 },
            new LineaPedido() { Id = 10, IdPedido = 5,  IdProducto = 4,     Cantidad = 2 },
            new LineaPedido() { Id = 11, IdPedido = 6,  IdProducto = 1,     Cantidad = 20 },
            new LineaPedido() { Id = 12, IdPedido = 6,  IdProducto = 2,     Cantidad = 7 },
            new LineaPedido() { Id = 13, IdPedido = 7,  IdProducto = 1,     Cantidad = 4 },
            new LineaPedido() { Id = 14, IdPedido = 7,  IdProducto = 2,     Cantidad = 10 },
            new LineaPedido() { Id = 15, IdPedido = 7,  IdProducto = 11,    Cantidad = 2 },
            new LineaPedido() { Id = 16, IdPedido = 8,  IdProducto = 12,    Cantidad = 2 },
            new LineaPedido() { Id = 17, IdPedido = 8,  IdProducto = 3,     Cantidad = 9 },
            new LineaPedido() { Id = 18, IdPedido = 9,  IdProducto = 5,     Cantidad = 11 },
            new LineaPedido() { Id = 19, IdPedido = 9,  IdProducto = 6,     Cantidad = 9 },
            new LineaPedido() { Id = 20, IdPedido = 9,  IdProducto = 1,     Cantidad = 4 },
            new LineaPedido() { Id = 21, IdPedido = 10, IdProducto = 2,     Cantidad = 7 },
            new LineaPedido() { Id = 22, IdPedido = 10, IdProducto = 3,     Cantidad = 2 },
            new LineaPedido() { Id = 23, IdPedido = 10, IdProducto = 11,    Cantidad = 10 },
            new LineaPedido() { Id = 24, IdPedido = 11, IdProducto = 12,    Cantidad = 2 },
            new LineaPedido() { Id = 25, IdPedido = 12, IdProducto = 1,     Cantidad = 20 }
        };

        // Propiedades de los elementos privados
        public static List<Cliente> ListaClientes { get { return _listaClientes; } }
        public static List<Producto> ListaProductos { get { return _listaProductos; } }
        public static List<Pedido> ListaPedidos { get { return _listaPedidos; } }
        public static List<LineaPedido> ListaLineasPedido { get { return _listaLineasPedido; } }
    }

}
