using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using ADOWinForms.Entidades;
using ADOWinForms.ADO;

namespace ADOWinForms
{
    public partial class frmEstatusAlumnos : Form
    {
        //List<EstatusAlumno> _listEs;

        public frmEstatusAlumnos()
        {
            InitializeComponent();


        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            pnlDatos.Visible = true;
            btnAgregar.Enabled = false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Text = "Agregar";
           // refresh();
        }

        private void cbxEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public void frmEstatusAlumnos_Load(object sender, EventArgs e)
        {
            refresh();

        }

        public void refresh() {

            ADOEstatusAlumno ado = new ADOEstatusAlumno();
            dgvEstatus.DataSource = ado.Consultar();
            cbxEstados.DataSource = ado.Consultar();
            cbxEstados.DisplayMember = "nombre";
            cbxEstados.ValueMember = "id";
            //this.Refresh();
        }


        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ADOEstatusAlumno ado = new ADOEstatusAlumno();
            EstatusAlumno ac = (EstatusAlumno)cbxEstados.SelectedItem;
          //  cbxEstados.DataSource = ado.Consultar();

            txtNombre.Text = ac.nombre;
            txtClave.Text = ac.clave;
            lblPruebas.Text = Convert.ToString(ac.id);


            pnlDatos.Visible = true;
            btnAgregar.Enabled = false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Text = "Actualizar";
           // refresh();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ADOEstatusAlumno ado = new ADOEstatusAlumno();
            EstatusAlumno ac = (EstatusAlumno)cbxEstados.SelectedItem;
            cbxEstados.DataSource = ado.Consultar();

            txtNombre.Text = ac.nombre;
            txtClave.Text = ac.clave;

            pnlDatos.Visible = true;
            btnAgregar.Enabled = false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Text = "Eliminar";

            txtClave.Enabled = false;
            txtNombre.Enabled = false;
          //  refresh();
        }

        private void btnCamcelar_Click(object sender, EventArgs e)
        {
            LimpiarCancelar();
            refresh();
        }

        public void LimpiarCancelar() {
            txtClave.Clear();
            txtNombre.Clear();

            btnGuardar.Text = "";
            txtClave.Enabled = true;
            txtNombre.Enabled = true;
            pnlDatos.Visible = false;
            btnAgregar.Enabled = true;
            btnActualizar.Enabled = true;
            btnEliminar.Enabled = true;
            refresh();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (btnGuardar.Text == "Eliminar") {
                EstatusAlumno es = new EstatusAlumno();
                ADOEstatusAlumno ado = new ADOEstatusAlumno();
                string Id = cbxEstados.SelectedValue.ToString();
                lblPruebas.Text = Id;
                es.id = Convert.ToInt32(Id);
                ado.Eliminar(es);
                LimpiarCancelar();
                refresh();
                string message = "Eliminado";
                MessageBox.Show(message);

            }
            else if (btnGuardar.Text == "Agregar") {
                EstatusAlumno estas = new EstatusAlumno();
                ADOEstatusAlumno ado = new ADOEstatusAlumno();
                Console.WriteLine("Clave");

                string clave = txtClave.Text;
                string nombre = txtNombre.Text;


                estas.clave = clave;
                estas.nombre = nombre;
                ado.Agregar(estas);
                LimpiarCancelar();
                refresh();
                string message = "Agregado";
                MessageBox.Show(message);

            } else if (btnGuardar.Text == "Actualizar") {


                ADOEstatusAlumno ado = new ADOEstatusAlumno();
                cbxEstados.DataSource = ado.Consultar();
                cbxEstados.DisplayMember = "nombre";
                cbxEstados.ValueMember = "id";
                EstatusAlumno ac = new EstatusAlumno ();
                Console.WriteLine("Id a actualizar");
                int id;
                id= Convert.ToInt32(lblPruebas.Text);
                string clave = txtClave.Text;
                string nombre = txtNombre.Text;
                ac.id = id;
                ac.clave = clave;
                ac.nombre = nombre;
                ado.Actualizar(ac);
                LimpiarCancelar();
                refresh();


                string message = "Actualizado";
                MessageBox.Show(message);
            }
        }
    }
}
