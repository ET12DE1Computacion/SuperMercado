using Super.Core;
using Super.Dapper;
using MySqlConnector;

namespace Super.Winf;

public partial class frmAltaProducto : Form
{
    //Propiedad que representa el objeto mediante el cual voy a leer/escribir en la BD
    IAdo _ado;
    public frmAltaProducto()
    {
        var cadena = "Server=localhost;User ID=5to_agbd;Password=Trigg3rs!;Database=5to_Supermercado;";
        var conector = new MySqlConnection(cadena);
        
        //Instancio el objeto como un acceso a nuestro Repo en Dapper
        _ado = new AdoDapper(conector);
        InitializeComponent();
    }

    private void CargarListBox()
    {
        //Esta propiedad es la que recibe la colección para el control
        lstCategorias.DataSource = _ado.ObtenerCategorias();

        //Esta propiedad, recibe el nombre de la propiedad a mostrar por cada elemento
        lstCategorias.DisplayMember = nameof(Categoria.Nombre);

        //Esta propiedad, recibe el nombre de la propiedad que vamos a usar como valor
        lstCategorias.ValueMember = nameof(Categoria.IdCategoria);
    }

    private void Carga(object sender, EventArgs e) => CargarListBox();

    private void CrearProducto()
    {
        try
        {
            if (lstCategorias.SelectedItem is null)
                throw new InvalidOperationException("Seleccione primero una categoria");

            var categoria = lstCategorias.SelectedItem as Categoria;
            var nombre = txtNombre.Text;
            var precio = numPrecio.Value;
            var stock = ((ushort)numStock.Value);
            var producto = new Producto(categoria, nombre, precio, stock);

            //Fijense como con el objeto ya instanciado, se lo paso al ado/repo para que lo inserte en la BD
            _ado.AltaProducto(producto);

            MessageBox.Show($"Se creó correctamente a: {producto.Nombre}");
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void btnCrear_Click(object sender, EventArgs e) => CrearProducto();
}
