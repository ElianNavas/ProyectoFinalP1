using p1ACrud13.Clases.entidades;
using p1ACrud13.Clases.Servicio;

namespace WFP1AC14
{
    public partial class Form1 : Form
    {

        ServicioAlumno srvAlumno = new();
        MdAlumnos oAlumnos = new();


        public Form1()
        {
            InitializeComponent();
        }


        private void DesplegarGrid()
        {
            var respuesta = srvAlumno.ConsultaSQL("select * from tb_alumnos");
            dataGridViewAlumnos.DataSource = respuesta;
        }


        private void buttonObtenerDatos_Click(object sender, EventArgs e)
        {
            DesplegarGrid();
        }


        private void MapaoDatosFormulario(MdAlumnos _alumnos)
        {
            textBoxCarnet.Text = _alumnos.carnet;
            textBoxNombre.Text = _alumnos.nombre;
            textBoxCorreo.Text = _alumnos.correo;
            comboBoxClase.Text = _alumnos.clase;
            comboBoxSeccion.Text = _alumnos.seccion;
            textBoxParcial1.Text = _alumnos.parcial1.ToString();
            textBoxParcial2.Text = _alumnos.parcial2.ToString();
            textBoxParcial3.Text = _alumnos.parcial3.ToString();
        }


        private void LimpiarDatos()
        {
            oAlumnos = new();
            MapaoDatosFormulario(oAlumnos);
        }

        private void buscaAlumno(string carnet)
        {
            oAlumnos = null;
            oAlumnos = srvAlumno.ObtenerAlumno(carnet);
            if (oAlumnos == null)
            {
                MessageBox.Show("no existe este cuate");
                LimpiarDatos();
            } else
            {
                MapaoDatosFormulario(oAlumnos);
            }
        }

       

        private void buttonConsulta_Click(object sender, EventArgs e)
        {
            string carnet = textBoxCarnet.Text;
            buscaAlumno(carnet);
        }

        private void validaciones(int x) //Valida que notas de los tres parciales y Nombre que no sea null

        {
            if (x == 0) // Validacion CREAR
            {
                if (Convert.ToInt32(textBoxParcial1.Text) <= 20 && Convert.ToInt32(textBoxParcial2.Text) <= 20 && Convert.ToInt32(textBoxParcial3.Text) <= 35 && textBoxNombre.Text != "")
                {
                    crearAlumno();

                }
                else
                {
                    MessageBox.Show("Lo siento, No puedo guardar el registro");
                }
            }
            if (x == 1) //VALIDACION ACTUALIZAR
            {
                if (Convert.ToInt32(textBoxParcial1.Text) <= 20 && Convert.ToInt32(textBoxParcial2.Text) <= 20 && Convert.ToInt32(textBoxParcial3.Text) <= 35 && textBoxNombre.Text != "")
                {
                   actualizarAlumno();

                }
                else
                {
                    MessageBox.Show("Lo siento, No puedo Actualizarr el registro");
                }

            }
        }
        private bool EvaluaCarnet(string carnet)// VALIDA QUE EL CARNET NO SEA DUPLICADO
        {

            bool existe = false;

            if (srvAlumno.ObtenerAlumno(carnet) != null)
            {

                existe = true;

            }
            return existe;

        }



        private MdAlumnos DatosFormulario()
        {
            MdAlumnos _alumnos = new();
            _alumnos.carnet = textBoxCarnet.Text.Trim();
            _alumnos.nombre = textBoxNombre.Text.Trim();
            _alumnos.correo =  textBoxCorreo.Text.Trim();
            _alumnos.clase = comboBoxClase.Text;
            _alumnos.seccion = comboBoxSeccion.Text;
            _alumnos.parcial1 = Convert.ToInt32(textBoxParcial1.Text);
            _alumnos.parcial2 = Convert.ToInt32(textBoxParcial2.Text);
            _alumnos.parcial3 = Convert.ToInt32(textBoxParcial3.Text);

            return _alumnos;

        }





        private void buttonCrearAlumno_Click(object sender, EventArgs e)
        {
            if (EvaluaCarnet(textBoxCarnet.Text) == true) 
            {
                MessageBox.Show("El Carnet, ya existe");
               
            }
            else
            {
                validaciones(0);
            }
            
            
        }

        private void crearAlumno() //Funcion Crear Alumno
        {
            oAlumnos = DatosFormulario();
            int respuesta = srvAlumno.CrearAlumno(oAlumnos);

            if (respuesta > 0)
            {
                MessageBox.Show("Se creo con existo el Alumno");
                LimpiarDatos();
                DesplegarGrid();
            }
            else
            {
                MessageBox.Show("Perdon hay un problema con la Grabacion");
            }



        }

        private void buttonActualizar_Click(object sender, EventArgs e)
        {
            validaciones(1);
           
        }

        private void actualizarAlumno()
        {
            oAlumnos = DatosFormulario();
            int respuesta = srvAlumno.actualizarAlumno(oAlumnos);

            if (respuesta > 0)
            {
                MessageBox.Show("Se compuso el Alumno");
                LimpiarDatos();
                DesplegarGrid();
            }
            else
            {
                MessageBox.Show("Perdon hay un problema con la Grabacion");
            }
        }








        private void buttonImportar_Click(object sender, EventArgs e)
        {
            string archivo = @"c:\tmp2\alumnos.txt";
            ClsImportExport im = new();
            MessageBox.Show(im.importar(archivo));
        }

        private void buttonExportar_Click(object sender, EventArgs e)
        {
            string archivo = @"c:\tmp2\salida.csv";
            ClsImportExport im = new();
            MessageBox.Show(im.exportar("select * from tb_alumnos where seccion='A'", archivo));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonEliminar_Click(object sender, EventArgs e)

        { //ELIMINAR ALUMNO
            DialogResult result = MessageBox.Show("Seguro que desea eliminar el registro?", "Eliminar", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                srvAlumno.EliminarAlumno(textBoxCarnet.Text);
                MessageBox.Show("Registo Eliminado");
                LimpiarDatos();
            }
            else if (result == DialogResult.No)
            {
            }



           
        }
    }
}