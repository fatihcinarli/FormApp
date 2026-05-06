using System.ComponentModel.DataAnnotations;

namespace FormApp.Models
{
    public class Product
    {
        [Display(Name ="Urun Id")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Ürün Adı Doldurulması Zorunludur !")]
        [StringLength(100)]
        [Display(Name ="Urun Adı")]
        public string? Name { get; set; }

        [Range(0,100000)]
        [Required]
        [Display(Name ="Urun Fiyatı")]
        public decimal Price { get; set; }

        [Display(Name ="Urun Resmi")]
        public string? Image { get; set; }

        [Display(Name ="Urun Aktif")]
        public bool IsActive { get; set; }

        [Required]
        [Display(Name ="Kategori")]
        public int? CategoryId { get; set; }
    }
}