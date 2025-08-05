using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Dominio;

namespace gestorApp
{
    public partial class Form1 : Form
    {
        private List<Articulo> lista = new List<Articulo>();
        public Form1()
        {
            InitializeComponent();
            dgvLoad();
        }
        //funciones
        public void dgvLoad()
        {
            ArticuloService service = new ArticuloService();
            try
            {   
                this.lista= service.listarArt();
                dgv1.DataSource = this.lista;
                ocultarColumnas();
            }
            catch (Exception ex)
            {

                MessageBox.Show("No se pueden cargar los articulos:\n"+ex.Message);
            }
        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void nuevoBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Form2 nuevo = new Form2(lista);
                nuevo.ShowDialog();
                dgvLoad();
            }
            catch (Exception ex)
            {

                MessageBox.Show("No se pudo agregar:\n" + ex.ToString());
            }       
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            if (dgv1.CurrentRow == null)
            {
                MessageBox.Show("No se seleccionó ningún artículo.");
                return;// no se que tan bien deberia estar esto si es un void no deberia retornar nada 
            }
            try
            {
                seleccionado = (Articulo)dgv1.CurrentRow.DataBoundItem;
                Form2 modificar = new Form2(seleccionado);
                modificar.ShowDialog();
                dgvLoad();
            }
            catch (Exception ex)
            {

                 MessageBox.Show("No se pudo modificar:\n"+ex.ToString());
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            ArticuloService service = new ArticuloService();
            if (dgv1.CurrentRow == null)
            {
                MessageBox.Show("No se seleccionó ningún artículo.");
                return;
            }
            try
            {
                seleccionado = (Articulo)dgv1.CurrentRow.DataBoundItem;
                DialogResult respuesta = MessageBox.Show("¿Estas seguro de eliminar este articulo?", "Confirmar eleccion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    service.eliminar(seleccionado);
                    MessageBox.Show("Eliminado correctamente");
                }
                dgvLoad();
            }
            catch (Exception ex)
            {

                MessageBox.Show("No se pudo eliminar:\n" + ex.ToString());
            }          
        }
        private void cargarImg(string urlimg)
        {
            try
            {
                 ptc1.LoadAsync(urlimg);         //lo puse asi para que cargue rapido en realidad va con el load
            }
            catch (Exception)
            {
              
               ptc1.Load("https://carte.com.ar/img/nd.png");//No se por que hace que ande lento  debe ser por mi pc
            }
        }

        private void dgv1_SelectionChanged(object sender, EventArgs e)
        {
            if(dgv1.CurrentRow != null){
                Articulo seleccionado = (Articulo)dgv1.CurrentRow.DataBoundItem;
                cargarImg(seleccionado.urlImg);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            if (dgv1.CurrentRow == null)
            {
                MessageBox.Show("No se seleccionó ningún artículo.");
                return;
            }
            try
            {
                seleccionado = (Articulo)dgv1.CurrentRow.DataBoundItem;
                Form3 ver = new Form3(seleccionado);
                ver.ShowDialog();
            }
            catch (Exception ex)
            {

                throw ex;
            }
      

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> buscados;
            string filtro = textBox1.Text;
            if (filtro.Length > 1)
            {
                buscados = lista.FindAll(x => x.codigo.ToUpper().Contains(filtro.ToUpper()) || x.nombre.ToUpper().Contains(filtro.ToUpper()) || x.marca.descripcion.ToUpper().Contains(filtro.ToUpper()) || x.categoria.descripcion.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                buscados = lista;
            }
            dgv1.DataSource = null;
            dgv1.DataSource = buscados;
            ocultarColumnas();
        }
        private void ocultarColumnas()
        {
            dgv1.Columns[0].Visible = false;
            dgv1.Columns[6].Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
