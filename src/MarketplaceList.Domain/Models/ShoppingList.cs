using System.Collections.Generic;

namespace MarketplaceList.Domain.Models
{
    public class ShoppingList : Entity
    {
        public ShoppingList(string name)
        {
            Name = name;
            Itens = new List<Item>();
        }

        public string Name { get; private set; }
        public ICollection<Item> Itens { get; private set; }
    }
}