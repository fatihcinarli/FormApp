using System.ComponentModel.DataAnnotations;

namespace FormApp.Models
{
    public class Product
    {
        [Display(Name ="Urun Id")]
        public int ProductId { get; set; }

        [Display(Name ="Urun Adı")]
        public string? Name { get; set; }

        [Display(Name ="Urun Fiyatı")]
        public decimal Price { get; set; }

        [Display(Name ="Urun Resmi")]
        public string? Image { get; set; }

        public bool IsActive { get; set; }

        [Display(Name ="Kategori")]
        public int CategoryId { get; set; }
    }
}