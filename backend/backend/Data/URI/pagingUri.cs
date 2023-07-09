using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace backend.Data.URI
{
    
    public class pagingUri
    {
        [BindProperty(SupportsGet = true)]
        [Required]
        public int pageSize {  get; set; }

        [BindProperty(SupportsGet = true)]
        [Required]
        public int pageNo { get; set; }

        [BindProperty(SupportsGet = true)]
        [Required]
        public string startDate { get; set; }

        [BindProperty(SupportsGet = true)]
        [Required]
        public string endDate { get; set; }
    }
}
