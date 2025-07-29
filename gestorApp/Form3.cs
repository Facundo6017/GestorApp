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

namespace gestorApp
{
    public partial class Form3 : Form
    {
        private Articulo aux;
        private List<Categoria> listaCategoria = new List<Categoria>();
        private List<Marca> listaMarca = new List<Marca>();
        public Form3(Articulo sel)
        {
            InitializeComponent();
            aux = sel;
            Text = "Ver";
            verLoad(aux);
        }
        private void verLoad(Articulo aux)
        {
            ArticuloService service = new ArticuloService();
            try
            {
                cboCatLoad(service);
                cboMarLoad(service);
                textBox1.Text = this.aux.codigo;
                textBox2.Text = this.aux.nombre;
                textBox3.Text = this.aux.descripcion;
                textBox7.Text = this.aux.precio.ToString();
                cboMarca.SelectedValue = this.aux.marca.id;
                cboCategoria.SelectedValue = this.aux.categoria.id;
                cargarImg(aux.urlImg);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        private void cargarImg(string urlimg)
        {
            try
            {
                pb1.LoadAsync(urlimg);
            }
            catch (Exception)
            {
                pb1.Load("https://carte.com.ar/img/nd.png");
            }
        }
        public void cboMarLoad(ArticuloService service)
        {
            try
            {
                this.listaMarca = service.listarMarca();
                cboMarca.DataSource = listaMarca;
                cboMarca.DisplayMember = "Descripcion";
                cboMarca.ValueMember = "Id";

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void cboCatLoad(ArticuloService service)
        {
            try
            {
                this.listaCategoria = service.listarCategoria();
                cboCategoria.DataSource = listaCategoria;
                cboCategoria.DisplayMember = "Descripcion";
                cboCategoria.ValueMember = "Id";

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
