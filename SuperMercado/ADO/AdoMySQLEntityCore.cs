using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace SuperMercado.ADO
{
    public class AdoMySQLEntityCore : DbContext, IADO
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<HistorialPrecio> HistorialPrecios { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Cajero> Cajeros { get; set; }

        public AdoMySQLEntityCore() : base() { }

        internal AdoMySQLEntityCore(DbContextOptions dbo) : base(dbo) { }

        public void agregarCategoria(Categoria categoria)
        {
            Categorias.Add(categoria);
            SaveChanges();
        }

        public void agregarProducto(Producto producto)
        {
            Productos.Add(producto);
            SaveChanges();
        }

        public void actualizarProducto(Producto producto)
        {
            this.Attach<Producto>(producto);
            SaveChanges();
        }

        public void agregarTicket(Ticket ticket)
        {
            Tickets.Add(ticket);
            SaveChanges();
        }

        public List<Categoria> obtenerCategorias() => Categorias.ToList();

        public List<Producto> obtenerProductos()
        {
            return   Productos
                    .Include(producto => producto.Categoria)
                    .ToList();
        }

        public List<HistorialPrecio> historialDe(Producto producto)
        {
            return   HistorialPrecios
                    .Where(historial => historial.Producto == producto)
                    .ToList();
        }

        public List<Ticket> obtenerTickets()
            => Tickets.
               Include(t => t.Cajero).
               ToList();

        public void actualizarTicket(Ticket ticket)
        {
            this.Attach<Ticket>(ticket);
            SaveChanges();
        }
        public List<Item> itemsDe(Ticket ticket)
        {
            return   Items
                    .Where(item => item.Ticket == ticket)
                    .Include(item => item.Producto)
                        .ThenInclude(produ => produ.Categoria)
                    .ToList();
        }

        public Cajero cajeroPorDniPass(int dni, string passEncriptada)
            => Cajeros.FirstOrDefault(c => c.Dni == dni && c.Password == passEncriptada);

        public void altaCajero(Cajero cajero)
        {
            Cajeros.Add(cajero);
            SaveChanges();
        }

        public List<Cajero> obtenerCajeros() => Cajeros.ToList();
    }
}