using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.EntityFrameworkCore.Extensions;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SuperMercado.ADO
{
    public class AdoMySQLEntityCore : DbContext, IADO
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<HistorialPrecio> HistorialPrecios { get; set; }
        
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
            SaveChanges();
        }

        public List<Categoria> obtenerCategorias() => Categorias.ToList();

        public List<Producto> obtenerProductos()
        {
            var productos = Productos.Include(p => p.Categoria)                            
                                     .ToList();
            productos.ForEach(p => cargarHistorialesDe(p));
            return productos;
        }

        private void cargarHistorialesDe(Producto producto)
        {
            producto.HistorialPrecios = HistorialPrecios
                                       .Where(h => h.Producto == producto)
                                       .ToList();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=supermercado;user=root;password=12Lujho");            
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            
        }
    }
}