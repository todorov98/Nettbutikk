using Nettbutikk.Models;

namespace Nettbutikk.Factories
{
    public class ProductOrderRelationFactory
    {
        public ProductOrderRelationFactory()
        {

        }

        /// <summary>
        /// Creates a single ProductOrderRelation instance.
        /// </summary>
        /// <returns>Newly created ProductOrderRelation instance.</returns>
        public ProductOrderRelation CreateRelation(Product product, Order order, int count)
        {
            return new ProductOrderRelation(product, order, count);
        }
    }
}
