using Super.Core;
using Super.Dapper;
using MySqlConnector;
using Super.Core.Repos;

namespace Super.Winf;
public partial class FrmAltaProducto : Form
{
    //Propiedad que representa el objeto mediante el cual voy a leer/escribir en la BD
    readonly IRepoProducto repoProducto;
    public FrmAltaProducto()
    {
        var cadena = "Server=localhost;User ID=5to_agbd;Password=Trigg3rs!;Database=5to_Supermercado;";
        var conector = new MySqlConnection(cadena);

        //Instancio el objeto como un acceso a nuestro Repo en Dapper
        repoProducto = new RepoProducto(conector);
        
        //Es importante que este metodo este al final
        InitializeComponent();
    }

    private void CargarListBox()
    {
        //Esta propiedad es la que recibe la colección para el control
        lstCategorias.DataSource = repoProducto.ObtenerCategorias();

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
            var producto = ObtenerProductoDesdeForm();

            //Fijense como con el objeto ya instanciado, se lo paso al ado/repo para que lo inserte en la BD
            repoProducto.AltaProducto(producto);

            MessageBox.Show($"Se creó correctamente a: {producto.Nombre}");
        }
        catch (Exception)
        {

            throw;
        }
    }

    private Producto ObtenerProductoDesdeForm()
    {
        if (lstCategorias.SelectedItem is null)
            throw new InvalidOperationException("Seleccione primero una categoria");

        //Como el elemento seleccionado siempre es de tipo object, lo "transformo" (cast) a Categoria
        var categoria = lstCategorias.SelectedItem as Categoria;
        if (categoria is null)
            throw new InvalidOperationException("Seleccione una categoria válida");
        var nombre = txtNombre.Text;
        var precio = numPrecio.Value;
        var stock = (ushort)numStock.Value;
        return new Producto(categoria, nombre, precio, stock);
    }

    private void btnCrear_Click(object sender, EventArgs e) => CrearProducto();
}
