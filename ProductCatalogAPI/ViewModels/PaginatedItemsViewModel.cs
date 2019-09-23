using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.ViewModels
{
    //ViewModel Class
    public class PaginatedItemsViewModel<TEntity>where TEntity:class
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public long Count { get; set; }

        public IEnumerable<TEntity> Data { get; set; }
    }
}
