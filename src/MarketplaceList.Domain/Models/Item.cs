using System;

namespace MarketplaceList.Domain.Models
{
    public class Item : Entity
    {
        protected Item() { }
        public Item(string name, int qtd)
        {
            Name = name;
            Qtd = qtd;
        }

        public Item(Guid id, string name, int qtd, Guid shoppingListId)
        {
            Id = id;
            Name = name;
            Qtd = qtd;
            ShoppingListId = shoppingListId;
        }

        public string Name { get; private set; }
        public int Qtd { get; private set; }
        public ShoppingList ShoppingList { get; private set; }
        public Guid ShoppingListId { get; private set; }
    }
}
