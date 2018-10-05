﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;
using System.Data.SqlClient;

namespace Lab4_BD
{
    public partial class ListaEstudiante : MetroForm
    {
        public Estudiante estudiante;
        public ListaEstudiante()
        {
            InitializeComponent();
            estudiante = new Estudiante();
        }

        private void ListaEstudiante_Load(object sender, EventArgs e)
        {
            //Llena el combobox de nombres de estudiante
            LlenarCombobox(nombres);
            //Llena el datagridview de estudiantes con todas las tuplas de
            //estudiante de la interfaz
            LlenarTabla(listEstu);
        }

        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }

        private void nombres_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LlenarCombobox(ComboBox combobox)
        {
            // Se obtiene un dataReader con todos los nombres de los estudiantes
            //de la base de datos
            SqlDataReader datos = estudiante.ObtenerListaNombresEstudiantes();
            /* Si existen datos en la base de datos se carga como primer
            elemento del combobox un dato "Seleccione" y luego se cargan todos los
            datos de la base de datos*/
            if (datos != null)
            {
                combobox.Items.Add("Seleccione");
                while (datos.Read())
                {
                    combobox.Items.Add(datos.GetValue(0));
                }
            }
            /* Si no hay tuplas en la base de datos se limpia el combobox y se
            carga unicamente el valor "Seleccione"*/
            else
            {
                combobox.Items.Clear();
                combobox.Items.Add("Seleccione");
            }
            // Se pone por defecto la primera entrada del combobox seleccionada
            combobox.SelectedIndex = 0;
        }

        private void LlenarTabla(DataGridView dataGridView)
        {
            /* Obtiene un dataTable con todos los estudiantes que se encuentran
            en la base de datos (null, null) es para vengan todas las tuplas sin
            ningún filtro*/
            DataTable tabla = estudiante.ObtenerEstudiantes(null, null);
            // Se inicializa el source para cargar el datagridview y se le
            //asigna el dataTable obtenido
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = tabla;
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            dataGridView.DataSource = bindingSource;
            // Ciclo para darle un ancho a cada columna del datagridview proporcionado
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                dataGridView.Columns[i].Width = 100;
            }
        }

        private void listEstu_SelectionChanged(object sender, EventArgs e)
        {
            LlenarTabla(listEstu);
        }

        private void buscar_Click(object sender, EventArgs e)
        {
            // Crea la interfaz EliminarEstudiante y la muestra, desaparece la
            //interfaz actual
            EliminarEstudiante eliminar = new EliminarEstudiante();
            eliminar.Show();
            this.Hide();
           
        }
    }
}
