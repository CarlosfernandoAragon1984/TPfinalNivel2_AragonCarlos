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
    public partial class fmrArticulo : Form
    {

        // declaro objeto lista de Articulos
        private List<Articulo> listaArticulo;
        public fmrArticulo()
        {
            InitializeComponent();
        }

        private void fmrArticulo_Load(object sender, EventArgs e)
        {
            Cargar();
            cboCampo.Items.Add("Id");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Marca");
            cboCampo.Items.Add("Categoría");
            cboCampo.Items.Add("Precio");
          

        }
        private void Cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                //inicio objeto listaArticulo
                listaArticulo = negocio.Listar();
                dgvArticulo.DataSource = listaArticulo;
                this.OcultarColumna();
                CargarImagen(listaArticulo[0].ImagenUrl);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void OcultarColumna()
        {
            dgvArticulo.Columns["ImagenUrl"].Visible = false;
            dgvArticulo.Columns["Codigo"].Visible = false;
        }

        private void dgvArticulo_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulo.CurrentRow !=null)
            {
                Articulo seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                CargarImagen(seleccionado.ImagenUrl);
            }
            
        }
        private void CargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception )
            {

                pbxArticulo.Load("https://winguweb.org/wp-content/uploads/2022/09/placeholder.png");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo nuevaVentana = new frmAltaArticulo();
            nuevaVentana.ShowDialog();
            Cargar();
            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            frmAltaArticulo modificar = new frmAltaArticulo(seleccionado);
            modificar.ShowDialog();
            Cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo selecionado;

            try
            {
               DialogResult resultado= MessageBox.Show("¿Quieres eliminar el registro? ","Eliminando",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) ;

                if (resultado==DialogResult.Yes)
                {
                    selecionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                    negocio.Eliminar(selecionado.Id);

                    Cargar();
                } 
               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            //Dentro de FindAll se usa una expreción landa
            listaFiltrada = listaArticulo.FindAll(x => x.Nombre == txtFiltro.Text);
            dgvArticulo.DataSource = null;
            dgvArticulo.DataSource = listaArticulo;
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
          
            /* if(opcion=="Código"|| opcion == "Precio")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
                cboCriterio.Items.Add("Mayor a");
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Contiene ...");
                cboCriterio.Items.Add("Termina con");
            }*/
            switch (opcion)
            {
                case "Id":
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Menor a");
                    cboCriterio.Items.Add("Igual a");
                    cboCriterio.Items.Add("Mayor a");
                    break;
                case "Precio":
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Menor a $$");
                    cboCriterio.Items.Add("Igual a $$");
                    cboCriterio.Items.Add("Mayor a $$");
                    break;
                   

                default:
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Comienza con");
                    cboCriterio.Items.Add("Contiene ...");
                    cboCriterio.Items.Add("Termina con");
                    break;
            }
        }

      /* private bool ValidarFiltro()
        {
            if (cboCampo.SelectedIndex <0)
            {
                MessageBox.Show("Seleccione Campo");
                return true;
            }
            if (cboCriterio.SelectedIndex<0)
            {
                MessageBox.Show("Seleccione Criterio");
                return true;
            }
            if (cboCampo.SelectedItem.ToString()== "Id" || cboCampo.SelectedItem.ToString()=="Precio")
            {
                if (string.IsNullOrEmpty(txtFiltro2.Text))
                {
                    MessageBox.Show("Filtro no puede ser vacio");
                    return true;
                }
                if (!(SoloNumero(txtFiltro2.Text)))
                {
                    MessageBox.Show("Sólo Campo Numérico");
                    return true;
                }
            }
            return false;
        }
        private bool SoloNumero(string cadena)
        {
            foreach  (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                {
                    return false;
                }
            }
            return true;
        }*/

        private void btnBuscar1_Click_1(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Validacion validar = new Validacion();
            try
            {
                validar.selector = "Campo";
                if (validar.ValidarFiltro(cboCampo.SelectedIndex))
                {
                    return;
                }
                validar.selector = "Criterio";
                if (validar.ValidarFiltro(cboCriterio.SelectedIndex))
                {
                    return;
                }
                validar.cajaFiltro = txtFiltro2.Text;
                if (validar.ValidarFiltro(cboCampo.SelectedItem.ToString()))
                {
                    return;
                }
                

                /* if (ValidarFiltro())
                 {
                     return;
                 }*/
                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltro2.Text;
                dgvArticulo.DataSource = negocio.Filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnRestablecer_Click(object sender, EventArgs e)
        {
            Cargar();
        }
    }
}
