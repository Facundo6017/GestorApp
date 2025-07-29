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
    public partial class Form2 : Form
    {
        private Articulo aux = null;
        private List<Categoria> listaCategoria = new List<Categoria>();
        private List<Marca> listaMarca = new List<Marca>();

        public Form2()
        {
            InitializeComponent();
            Text = "Nuevo Articulo";
            articuloLoad();
        }
        public Form2(Articulo aux)
        {
            InitializeComponent();
            this.aux = aux;
            Text = "Modificar Articulo";
            articuloLoad();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ArticuloService service = new ArticuloService();
            try
            {
                if (this.aux == null) this.aux = new Articulo();

                aux.codigo = textBox1.Text;
                aux.nombre = textBox2.Text;
                aux.descripcion = textBox3.Text;
                aux.urlImg = textBox6.Text;
                aux.precio = decimal.Parse(textBox7.Text);
                aux.marca = (Marca)cboMarca.SelectedItem;
                aux.categoria = (Categoria)cboCategoria.SelectedItem;

                if (aux.id != 0)
                {
                    service.modificar(this.aux);
                }
                else
                {
                    service.agregar(this.aux);
                }
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("No se pudo guardar el articulo:\n" + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void articuloLoad()
        {
            ArticuloService service = new ArticuloService();
            MarcaService marcaService = new MarcaService();
            CategoriaService categoriaService = new CategoriaService();
            try
            {
                cboLoad(marcaService);
                cboLoad(categoriaService);
                if (this.aux != null)
                {
                    textBox1.Text = this.aux.codigo;
                    textBox2.Text = this.aux.nombre;
                    textBox3.Text = this.aux.descripcion;
                    textBox6.Text = this.aux.urlImg;
                    textBox7.Text = this.aux.precio.ToString();
                    cboMarca.SelectedValue = this.aux.marca.id;
                    cboCategoria.SelectedValue = this.aux.categoria.id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el artículo: " + ex.Message);
            }
        }
        public void cboLoad(CategoriaService service)
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
        public void cboLoad(MarcaService service)
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

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
