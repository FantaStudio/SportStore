using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class Product
    {
        [Key]
        public long ProductID { get; set; }

        [Required(ErrorMessage = "Введите название товара")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите описание товара")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Введите правильную цену!")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "Выберите категорию")]
        public string Category { get; set; }

        public string ImageFilePath { get; set; }

        public float Discount { get; set; } = 0;

    }
}
