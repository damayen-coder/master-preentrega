using System;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using PreEntregaProyectoFinal.Modulos;

namespace PreEntregaProyectoFinal
{
    public class StartUp
    {
       
        public static void Main(string[] args)
        {
            string OpcionElegida = string.Empty;

            while (OpcionElegida != "Z")
            {
                Console.WriteLine("Buenvenido al Sistema de Test de Proyecto");
                Console.WriteLine("Opcion A: Traer Usuario:  Recibe como parámetro un nombre del usuario, buscarlo en la base de datos y devolver el objeto con todos sus datos");
                Console.WriteLine("Opcion B: Traer Producto: Recibe un número de IdUsuario como parámetro, debe traer todos los productos cargados en la base de este usuario en particular.");
                Console.WriteLine("Opcion C: Traer Productos Vendidos: Traer Todos los productos vendidos de un Usuario, cuya información está en su producto");
                Console.WriteLine("Opcion D: Traer Ventas: Recibe como parámetro un IdUsuario, debe traer todas las ventas de la base asignados al usuario particular.");
                Console.WriteLine("Opcion E: Inicio de sesión: Se le pase como parámetro el nombre del usuario y la contraseña, buscar en la base de datos si el usuario existe");
                
                Console.WriteLine("\nElige la opcion a Testear: A - B - C - D - E \n");

                OpcionElegida = Console.ReadLine().ToUpper();
                Console.WriteLine("\n");

                switch (OpcionElegida)
                {
                    case "A":
                        TraerUsuario();
                        break;
                    case "B":
                        TraerProductos();
                        break;
                    case "C":
                        TraerProductosVendidos();
                        break;
                    case "D":
                        TraerVentas();
                        break;
                    case "E":
                        IniciarSesion();
                        break;
                    default:
                        Console.WriteLine("La Opcion no es Correcta");
                        break;
                }
                Console.WriteLine("Preciona ENTER para intentar otra opcion");
                Console.ReadLine();
                Console.Clear();
            }
            Console.WriteLine("Buenvenido al Sistema de Test de Proyecto");
            Console.WriteLine("Opcion A: Traer Usuario:  Recibe como parámetro un nombre del usuario, buscarlo en la base de datos y devolver el objeto con todos sus datos");
            Console.WriteLine("Opcion B: Traer Producto: Recibe un número de IdUsuario como parámetro, debe traer todos los productos cargados en la base de este usuario en particular.");
            Console.WriteLine("Opcion C: Traer Productos Vendidos: Traer Todos los productos vendidos de un Usuario, cuya información está en su producto");
            Console.WriteLine("Opcion D: Traer Ventas: Recibe como parámetro un IdUsuario, debe traer todas las ventas de la base asignados al usuario particular.");
            Console.WriteLine("Opcion E: Inicio de sesión: Se le pase como parámetro el nombre del usuario y la contraseña, buscar en la base de datos si el usuario existe");
            Console.WriteLine("\nElige la opcion a Testear: A - B - C - D - E \n");

            OpcionElegida = Console.ReadLine().ToUpper();
            Console.WriteLine("\n");
        }



        static  void TraerUsuario()
        {

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-VRSGEG4";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;
            var conn = conecctionbuilder.ConnectionString;
            var ListaUser = new List<Usuarios>();

            Console.WriteLine("Ingrese el Nombre del Usuario");
            string user = Console.ReadLine();

            Console.WriteLine();

            using (SqlConnection connection = new SqlConnection(conn))
            {                
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Usuario WHERE NombreUsuario = @UserNom";

                
                var param = new SqlParameter();
                param.ParameterName = "UserNom";
                param.SqlDbType = System.Data.SqlDbType.VarChar;
                param.Value = user;
                cmd.Parameters.Add(param);

                connection.Open();
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user2 = new Usuarios();

                        user2.id = Convert.ToInt32(reader.GetValue(0));
                        user2.Nombre = reader.GetValue(1).ToString();
                        user2.Apellido = reader.GetValue(2).ToString();
                        user2.NombreUsuario = reader.GetValue(3).ToString();
                        user2.Contraseña = reader.GetValue(4).ToString();
                        user2.Mail = reader.GetValue(5).ToString();

                        ListaUser.Add(user2);

                    }
                    Console.WriteLine("------Usuario---------");
                    Console.WriteLine("\n");
                    foreach (var user2 in ListaUser)
                    {
                        Console.WriteLine("id = " + user2.id);
                        Console.WriteLine("Nombre = " + user2.Nombre);
                        Console.WriteLine("Apellido = " + user2.Apellido);
                        Console.WriteLine("NombreUsuario = " + user2.NombreUsuario);
                        Console.WriteLine("Contraseña = " + user2.Contraseña);
                        Console.WriteLine("Mail = " + user2.Mail);
                        Console.WriteLine("\n");
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine("\n");


                    }
                }
            }
                                    
        } //Salio despues de 8hs :D

        static void TraerProductos()
        {
            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-VRSGEG4";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;
            var conn = conecctionbuilder.ConnectionString;
            var ListaProducto = new List<Producto>();

            Console.WriteLine("Ingrese el Id del Usuario");
            string Iduser = Console.ReadLine();

            Console.WriteLine();

            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Producto WHERE IdUsuario = @UserId";


                var param = new SqlParameter();
                param.ParameterName = "UserId";
                param.SqlDbType = System.Data.SqlDbType.VarChar;
                param.Value = Iduser;
                cmd.Parameters.Add(param);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var producto = new Producto();

                        producto.id = Convert.ToInt32(reader.GetValue(0));
                        producto.Descripcion = reader.GetValue(1).ToString();
                        producto.Costo = Convert.ToInt32(reader.GetValue(2));
                        producto.PrecioVenta = Convert.ToInt32(reader.GetValue(3));
                        producto.Stock = Convert.ToInt32(reader.GetValue(4));
                        producto.IdUsuario = Convert.ToInt32(reader.GetValue(5));

                        ListaProducto.Add(producto);

                    }
                    Console.WriteLine("------Productos---------");
                    Console.WriteLine("\n");
                    foreach (var producto in ListaProducto)
                    {
                        Console.WriteLine("id = " + producto.id);
                        Console.WriteLine("Descripcion = " + producto.Descripcion);
                        Console.WriteLine("Costo = " + producto.Costo);
                        Console.WriteLine("PrecioVenta = " + producto.PrecioVenta);
                        Console.WriteLine("Stock = " + producto.Stock);
                        Console.WriteLine("IdUsuario = " + producto.IdUsuario);
                        Console.WriteLine("\n");
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine("\n");


                    }
                }
            }

        }

        static void TraerProductosVendidos()
        {

            
            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-VRSGEG4";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;
            var conn = conecctionbuilder.ConnectionString;

            var ListaProducto = new List<Producto>();
            var productoVendi = new List<ProductoVendido>();

            Console.WriteLine("Ingrese el Id del Usuario");
            string Iduser = Console.ReadLine();

            Console.WriteLine();

            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT P.Id, P.Descripciones, PV.Stock from Producto p inner join ProductoVendido pv on P.Id = PV.IdProducto where IdUsuario = @userId";


                var param = new SqlParameter();
                param.ParameterName = "userId";
                param.SqlDbType = System.Data.SqlDbType.VarChar;
                param.Value = Iduser;
                cmd.Parameters.Add(param);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var producto = new Producto();
                        var pv = new ProductoVendido();

                        producto.id = Convert.ToInt32(reader.GetValue(0));
                        producto.Descripcion = reader.GetValue(1).ToString();
                        pv.Stock = Convert.ToInt32(reader.GetValue(2));

                        ListaProducto.Add(producto);
                        productoVendi.Add(pv);


                    }
                    Console.WriteLine("------Productos---------");
                    Console.WriteLine("\n");
                    foreach (var producto in ListaProducto)
                    {
                        Console.WriteLine("id = " + producto.id);
                        Console.WriteLine("Descripcion = " + producto.Descripcion);
                        Console.WriteLine("\n");
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine("\n");
                    }
                    foreach (var pv in productoVendi)
                    {
                        Console.WriteLine("Vendidos = " + pv.Stock);
                    }
                }
            }
        }
        static void TraerVentas()
        {

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-VRSGEG4";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;
            var conn = conecctionbuilder.ConnectionString;
            var ventas = new List<Venta>();

            Console.WriteLine("Ingrese el ID del Usuario");
            string user = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Venta WHERE IdUsuario = @idUser";


                var param = new SqlParameter();
                param.ParameterName = "idUser";
                param.SqlDbType = System.Data.SqlDbType.VarChar;
                param.Value = user;
                cmd.Parameters.Add(param);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ven = new Venta();

                        ven.id = Convert.ToInt32(reader.GetValue(0));
                        ven.Comentarios = reader.GetValue(1).ToString();
                        ven.IdUsuario = Convert.ToInt32(reader.GetValue(2));


                        ventas.Add(ven);

                    }
                    Console.WriteLine("------Usuario---------");
                    Console.WriteLine("\n");
                    foreach (var venta in ventas)
                    {
                        Console.WriteLine("id = " + venta.id);
                        Console.WriteLine("Comentarios = " + venta.Comentarios);
                        Console.WriteLine("IdUsuario = " + venta.IdUsuario);
                        Console.WriteLine("\n");
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine("\n");


                    }
                }
            }

        }
        static void IniciarSesion()
        {
            int getID = 0;
            string userName = string.Empty;
            string pass = string.Empty;
            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "DESKTOP-VRSGEG4";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;
            var conn = conecctionbuilder.ConnectionString;


            var Usuario = new List<Usuarios>();

            Console.WriteLine("Ingrese Nombre de Usuario");
            userName = Console.ReadLine();
            Console.WriteLine("Ingrese Contraseña");
            pass = Console.ReadLine();


            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Usuario WHERE NombreUsuario = @userName and Contraseña = @userPass;";


                var param = new SqlParameter();
                param.ParameterName = "userName";
                param.SqlDbType = System.Data.SqlDbType.VarChar;
                param.Value = userName;
                cmd.Parameters.Add(param);

                var param2 = new SqlParameter();
                param2.ParameterName = "userPass";
                param2.SqlDbType = System.Data.SqlDbType.VarChar;
                param2.Value = pass;
                cmd.Parameters.Add(param2);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var User = new Usuarios();

                            User.id = Convert.ToInt32(reader.GetValue(0));
                            User.Nombre = reader.GetValue(1).ToString();
                            User.Apellido = reader.GetValue(2).ToString();
                            User.NombreUsuario = reader.GetValue(3).ToString();
                            User.Contraseña = reader.GetValue(4).ToString();
                            User.Mail = reader.GetValue(5).ToString();

                            Usuario.Add(User);

                        }
                        Console.WriteLine("------Datos Del Usuario---------");
                        Console.WriteLine("\n");
                        foreach (var User in Usuario)
                        {
                            Console.WriteLine("id = " + User.id);
                            Console.WriteLine("Nombre = " + User.Nombre);
                            Console.WriteLine("Apellido = " + User.Apellido);
                            Console.WriteLine("NombreUsuario = " + User.NombreUsuario);
                            Console.WriteLine("Contraseña = " + User.Contraseña);
                            Console.WriteLine("Mail = " + User.Mail);
                            Console.WriteLine("\n");
                            Console.WriteLine("-----------------------------");
                            Console.WriteLine("\n");

                        }
                    }
                    else
                    {
                        getID = 0;
                        Console.WriteLine("El Usuario No Existe en la base de Datos!");
                    }
                    
                }
            }

        }
    }

}


