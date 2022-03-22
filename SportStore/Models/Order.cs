using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class Order
    {
        [Key]
        [BindNever]
        public int OrderID { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите своё имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите свой адрес")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите свой город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите область")]
        public string CityArea { get; set; }

        public string Zip { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите свою страну")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }

        [BindNever]
        public bool IsShipped { get; set; }
    }
}
