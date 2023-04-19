using System.ComponentModel.DataAnnotations;
namespace AP003.Models
{
    public class ProductoModel
    {
        [Display (Name="Código")] public int IdProducto { get; set; }
        /*? esto significa que su valor puede ser nulo*/
        [Display(Name = "Descripción")] public string? NombreProducto { get; set; }
        [Display(Name = "Precio Unidad")] public decimal? PrecioUnidad { get; set; }
        [Display(Name = "Categoria")] public string? NombreCategoria { get; set; }
        [Display(Name = "Proveedor")] public string? NombreProveedor { get; set; }
        [Display(Name = "Unidades en Existencia")] public int stcokProducto { get; set; }



    }
}
