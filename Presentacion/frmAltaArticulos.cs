using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class frmAltaArticulo : Form
   

    {
        private Articulo articulo = null;
        public frmAltaArticulo()
        {
            InitializeComponent();
            Text = "Agregar Artículo";
        }
        public frmAltaArticulo( Articulo artModicar)
        {
            InitializeComponent();
            this.articulo = artModicar;
            Text = "Modificar Artículo";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Articulo cargarArticulo = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();
            Validacion validar =new Validacion();

            try
            {
                validar.cajaFiltro = "Codigo";
                if (validar.noVacio(txtCodigo.Text))
                {
                    return;
                }
                validar.cajaFiltro = "Nombre";
                if (validar.noVacio(txtNombre.Text))
                {
                    return;
                }
                validar.cajaFiltro = "Descripcion";
                if (validar.noVacio(txtDescripcion.Text))
                {
                    return;
                }

                if (validar.NoVacio(txtPrecio.Text))
                {
                    return;
                }
                validar.cajaFiltro = "UrlImagen";
                if (validar.noVacio(txtImagenUrl.Text))
                {
                    return;
                }

                if (articulo == null)
                {
                    articulo = new Articulo();
                }
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Precio = decimal.Parse( txtPrecio.Text);
                articulo.marca = (Marca)cboMarca.SelectedItem;
                articulo.categoria = (Categoria)cboCategoria.SelectedItem;
                articulo.ImagenUrl = txtImagenUrl.Text;
                if (articulo.Id!=0)
                {
                    negocio.Modificar(articulo);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.Agregar(articulo);
                    MessageBox.Show("Agreado exitosamente");
                }
                
               
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio negocioMarca = new MarcaNegocio();
            CategoriaNegocio negocioCategoria = new CategoriaNegocio();

            try
            {
               
                
                cboMarca.DataSource = negocioMarca.Listar();
                cboMarca.ValueMember = "id";
                cboMarca.DisplayMember = "Descripcion";
                cboCategoria.DataSource = negocioCategoria.Listar();
                cboCategoria.ValueMember = "id";
                cboCategoria.DisplayMember = "Descripcion";
                // carga informacion si se utiliza la opción modificar de frmArticulos
                if (articulo!=null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtPrecio.Text = articulo.Precio.ToString();
                    txtImagenUrl.Text = articulo.ImagenUrl;
                    CargarImagen(articulo.ImagenUrl);
                    cboMarca.SelectedValue = articulo.marca.id;
                    cboCategoria.SelectedValue = articulo.categoria.id;
                    
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtImagenUrl_Leave(object sender, EventArgs e)
        {
            CargarImagen(txtImagenUrl.Text);
        }
        private void CargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception)
            {

                pbxArticulo.Load("https://winguweb.org/wp-content/uploads/2022/09/placeholder.png");
            }
        }
    }
}
