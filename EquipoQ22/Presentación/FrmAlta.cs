
using EquipoQ22.Dominio;
using EquipoQ22.Servicios;
using EquipoQ22.Servicios.interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace EquipoQ22
{
    public partial class FrmAlta : Form
    {
        private Equipos equipoN;
        private IServicio service;
        private Jugadores jugadorN;
        public FrmAlta()
        {
            InitializeComponent();
            equipoN = new Equipos();
            jugadorN = new Jugadores();
            service = new serviceFactoryImpl().crearServicio();

        }
        private void FrmAlta_Load(object sender, EventArgs e)
        {
            cargarCombo();
        }

        private void cargarCombo()
        {
            cboPersona.DataSource = service.obtenerDato();
                                    //COMO LO TENGO PUESTO EN LA CLASE
            cboPersona.ValueMember = "IDPersona";
            cboPersona.DisplayMember = "nombreCompleto";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (validar() && existe(cboPersona.Text))
            {
                jugadorN.persona = (Personas)cboPersona.SelectedItem;
                jugadorN.posicion = (string)cboPosicion.SelectedItem;
                jugadorN.nroCamiseta = (int)nudCamiseta.Value;
                equipoN.agregar(jugadorN);
                dgvDetalles.Rows.Add(new object[] {jugadorN.persona.IDPersona,jugadorN.persona.nombreCompleto,jugadorN.nroCamiseta,jugadorN.posicion });

            }
        }

        private bool validar()
        {
            if (txtPais.Text == string.Empty)
            {
                MessageBox.Show("Pais vacio","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;

            }
            if (txtDT.Text == String.Empty)
            {
                MessageBox.Show("DT vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cboPersona.SelectedIndex == -1)
            {
                MessageBox.Show("Persona vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (nudCamiseta.Value < 1 || nudCamiseta.Value > 23)
            {
                MessageBox.Show("Camiseta tiene que ser entre 1 y 23", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cboPosicion.SelectedIndex == -1)
            {
                MessageBox.Show("Posicion vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            foreach (DataGridViewRow dr in dgvDetalles.Rows)
            {
                if (dr.Cells["camiseta"].Value == nudCamiseta)
                {
                    MessageBox.Show("Ese numero de camiseta ya fue agregado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }
        private bool existe(string selectedItem)
        {
            bool existe = false;
            foreach (DataGridViewRow item in dgvDetalles.Rows)
            {
                if (item.Cells["jugador"].Value.ToString().Equals(selectedItem))
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            equipoN.pais = txtPais.Text;
            equipoN.directorTecnico = txtDT.Text;
            jugadorN.nroCamiseta = (int)nudCamiseta.Value;

            if (service.guardarAlta(equipoN))
            {
                MessageBox.Show("Datos guardados correctamente!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }
        }


        private void LimpiarCampos()
        {
            txtDT.Text = string.Empty;
            txtPais.Text = string.Empty;
            cboPosicion.SelectedIndex = -1;
            cboPersona.SelectedIndex = -1;
            dgvDetalles.Rows.Clear();
            lblTotal.Text = "Total de Jugadores: ";
            nudCamiseta.Value = 1;
        }
        private void calcularTotal()
        {
            lblTotal.Text = "Total: " + dgvDetalles.Rows.Count.ToString();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == 4)
            {
                equipoN.quitar(dgvDetalles.CurrentRow.Index);

                dgvDetalles.Rows.Remove(dgvDetalles.CurrentRow);
                calcularTotal();
         
            }
        }

        private void txtPais_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
