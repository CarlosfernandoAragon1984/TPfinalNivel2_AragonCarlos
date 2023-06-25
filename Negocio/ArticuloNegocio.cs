using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Conexion;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetConsulta("select Codigo,Nombre,A.id,A.Descripcion,M.Descripcion as Marca,C.Descripcion as Categoria,ImagenUrl,Precio,A.IdMarca,A.IdCategoria from ARTICULOS A,CATEGORIAS C, MARCAS M where IdMarca=M.Id and C.Id=IdCategoria");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["id"];
                    if (!(datos.Lector["Codigo"] is DBNull))
                    {
                        aux.Codigo = (string)datos.Lector["Codigo"];
                    }
                    if (!(datos.Lector["Nombre"] is DBNull))
                    {
                       aux.Nombre = (string)datos.Lector["Nombre"];

                    }
                    if (!(datos.Lector["Descripcion"] is DBNull))
                    {
                        aux.Descripcion = (string)datos.Lector["Descripcion"];
                    }
                   
                    
                    aux.marca = new Marca();
                    
                    aux.marca.id = (int)datos.Lector["IdMarca"];
                    if (!(datos.Lector["Marca"] is DBNull))
                    {
                        aux.marca.descripcion = (string)datos.Lector["Marca"];
                    }
                    
                    aux.categoria = new Categoria();
                    
                    aux.categoria.id=(int)datos.Lector["IdCategoria"];
                    if (!(datos.Lector["Categoria"] is DBNull))
                    {
                        aux.categoria.descripcion = (string)datos.Lector["Categoria"];
                    }
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    }
                    if (!(datos.Lector["Precio"] is DBNull))
                    {
                        aux.Precio = (decimal)datos.Lector["Precio"];
                    }
                   
                    
                    lista.Add(aux);
                   
                }
                    
                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }

        }
        public void Agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetConsulta("insert into ARTICULOS (Codigo,Nombre,Descripcion,IdMarca,IdCategoria,ImagenUrl,Precio)values(@Codigo,@Nombre,@Descripcion,@IdMarca,@IdCategoria,@ImagenUrl,@Precio)");
                datos.SetParametros("@Codigo", nuevo.Codigo);
                datos.SetParametros("@Nombre", nuevo.Nombre);
                datos.SetParametros("@Descripcion", nuevo.Descripcion);
                datos.SetParametros("@IdMarca", nuevo.marca.id);
                datos.SetParametros("@IdCategoria", nuevo.categoria.id);
                datos.SetParametros("@ImagenUrl", nuevo.ImagenUrl);
                datos.SetParametros("@Precio", nuevo.Precio);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void Modificar(Articulo modificado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetConsulta("update ARTICULOS set Codigo=@Codigo,Nombre=@Nombre,Descripcion=@Desc,IdMarca=@IdMarca,IdCategoria=@IdCategoria,ImagenUrl=@Url,Precio=@Precio where id=@id");
                datos.SetParametros("@Codigo", modificado.Codigo);
                datos.SetParametros("@Nombre",modificado.Nombre );
                datos.SetParametros("@Desc", modificado.Descripcion);
                datos.SetParametros("@IdMarca", modificado.marca.id);
                datos.SetParametros("@IdCategoria", modificado.categoria.id);
                datos.SetParametros("@Url", modificado.ImagenUrl);
                datos.SetParametros("@Precio", modificado.Precio);
                datos.SetParametros("@id", modificado.Id);

                datos.EjecutarAccion();
            }

            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.SetConsulta("delete from ARTICULOS where id =@id ");
                datos.SetParametros("@id", id);
                datos.EjecutarAccion();
                datos.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public List<Articulo> Filtrar(string campo, string criterio,string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consultas = "select Codigo,Nombre,A.id,A.Descripcion,M.Descripcion as Marca,C.Descripcion as Categoria,ImagenUrl,Precio,A.IdMarca,A.IdCategoria from ARTICULOS A,CATEGORIAS C, MARCAS M where IdMarca=M.Id and C.Id=IdCategoria and";

                switch (campo)
                {
                    case "Id":
                        switch (criterio)
                        {
                            case "Menor a":
                                consultas = consultas + " A.id <" + filtro;
                                break;
                            case "Igual a":
                                consultas = consultas + " A.id =" + filtro;
                                break;
                            case "Mayor a":
                                consultas = consultas + " A.id >"  +filtro;
                                break;
                            default:
                                break;
                        }
                        break;
                    case"Precio":
                        switch (criterio)
                        {
                            case "Menor a $$":
                                consultas = consultas + " Precio <" + filtro;
                                break;
                            case "Igual a $$":
                                consultas = consultas + " Precio =" + filtro;
                                break;
                            case "Mayor a $$":
                                consultas = consultas + " Precio > " + filtro;
                                break;
                            default:
                                break;
                        }
                        break;
                    case "Nombre":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consultas = consultas + " Nombre like'" + filtro+"%'";
                                break;
                            case "Contiene ...":
                                consultas = consultas + " Nombre like'%"+filtro+"%'" ;
                                break;
                            case "Termina con":
                                consultas = consultas + " Nombre like'%" + filtro + "'";
                                break;
                            default:
                                break;
                        }
                        break;
                    case "Marca":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consultas = consultas + " Marca like'" + filtro + "%'";
                                break;
                            case "Contiene ...":
                                consultas = consultas + " Marca like'%" + filtro + "%'";
                                break;
                            case "Termina con":
                                consultas = consultas + " Marca like'%" + filtro + "'";
                                break;
                            default:
                                break;
                        }
                        break;
                    case "Categoría":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consultas = consultas + " Categoria like'" + filtro + "%'";
                                break;
                            case "Contiene ...":
                                consultas = consultas + " Categoria like'%" + filtro + "%'";
                                break;
                            case "Termina con":
                                consultas = consultas + " Categoria like'%" + filtro + "'";
                                break;
                            default:
                                break;
                        }
                        break;
                        


                        
                    default:
                       
                        break;
                }

                datos.SetConsulta(consultas);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["id"];
                    if (!(datos.Lector["Codigo"] is DBNull))
                    {
                        aux.Codigo = (string)datos.Lector["Codigo"];
                    }
                    if (!(datos.Lector["Nombre"] is DBNull))
                    {
                        aux.Nombre = (string)datos.Lector["Nombre"];

                    }
                    if (!(datos.Lector["Descripcion"] is DBNull))
                    {
                        aux.Descripcion = (string)datos.Lector["Descripcion"];
                    }


                    aux.marca = new Marca();

                    aux.marca.id = (int)datos.Lector["IdMarca"];
                    if (!(datos.Lector["Marca"] is DBNull))
                    {
                        aux.marca.descripcion = (string)datos.Lector["Marca"];
                    }

                    aux.categoria = new Categoria();

                    aux.categoria.id = (int)datos.Lector["IdCategoria"];
                    if (!(datos.Lector["Categoria"] is DBNull))
                    {
                        aux.categoria.descripcion = (string)datos.Lector["Categoria"];
                    }
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    }
                    if (!(datos.Lector["Precio"] is DBNull))
                    {
                        aux.Precio = (decimal)datos.Lector["Precio"];
                    }


                    lista.Add(aux);

                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
