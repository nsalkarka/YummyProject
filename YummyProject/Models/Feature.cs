using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YummyProject.Models
{
    public class Feature
    {
        public int FeatureId { get; set; }

        
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Başlık boş bırakılamaz")]
        [MinLength(5,ErrorMessage ="Başlık en az 5 karakter olmalıdır.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Açıklama boş bırakılamaz")]
        [MaxLength(100,ErrorMessage ="Açıklama en fazla 100 karakter olmalıdır.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "VideoURL boş bırakılamaz")]
        public string VideoUrl { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}