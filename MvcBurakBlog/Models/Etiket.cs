using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcBurakBlog.Models
{
    public class Etiket
    {
        public int EtiketId { get; set; }

        [Required(ErrorMessage = "Lütfen etiketin içerigini giriniz.")]
        [StringLength(50, ErrorMessage = "Etiketin içerigi 50 karakterden uzun olamaz.")]
        public string Icerik { get; set; }

        //Ayni etiket, birden çok makale de kullaniliyor olabilir.
        public ICollection<Makale> Makales { get; set; }
    }
}