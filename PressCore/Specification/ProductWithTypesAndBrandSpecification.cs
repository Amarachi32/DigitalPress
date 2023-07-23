using Microsoft.IdentityModel.Tokens;
using PressCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PressCore.Specification
{
    public class ProductWithTypesAndBrandSpecification : BaseSpecification<Product>
    {
        /*public ProductWithTypesAndBrandSpecification(Expression<Func<Product, bool>> criteria, List<Expression<Func<Product, object>>> includes) : base(criteria, includes)
        {
           AddInclude(x => x.ProductType);
           AddInclude(x => x.ProductBrand);
       }*/
        public ProductWithTypesAndBrandSpecification(string id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
           
        }
        public ProductWithTypesAndBrandSpecification()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        //filter, sorting
        public ProductWithTypesAndBrandSpecification(ProductSpecParam productSpec)
            :base(x => 
            (string.IsNullOrEmpty(productSpec.BrandId) || x.ProductBrandId == productSpec.BrandId) && 
            (string.IsNullOrEmpty(productSpec.TypeId)  || x.ProductTypeId == productSpec.TypeId)
            )
        
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);

            if (!string.IsNullOrEmpty(productSpec.Sort))
            {
                switch (productSpec.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;

                }
            }
        }

    }
}
