using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Models.Response
{
    public class EditCatalogItemResponse
    {
        public CatalogItem CatalogItem { get; set; } = new();
    }
}
