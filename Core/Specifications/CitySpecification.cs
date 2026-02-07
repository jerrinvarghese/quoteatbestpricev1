using Core.Entities;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class CitySpecification : BaseSpecification<City>
    {
        public CitySpecification(string search)
            : base(c => c.Name.ToLower().StartsWith(search.ToLower()))
        {
            AddOrderBy(c => c.Name);
            ApplyPaging(0, 10);
        }
    }
}