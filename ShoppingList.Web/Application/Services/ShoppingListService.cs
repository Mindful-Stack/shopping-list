using ShoppingList.Application.Interfaces;
using ShoppingList.Components;
using ShoppingList.Domain.Models;

namespace ShoppingList.Application.Services;

public class ShoppingListService : IShoppingListService
{
    private ShoppingItem[] _items = new ShoppingItem[9];
    private int _nextIndex;

    public ShoppingListService()
    {
        // Initialize with demo data for UI demonstration
        // TODO: Students can remove or comment this out when running unit tests
        // _items = GenerateDemoItems();
        _nextIndex = 0; 
    }

    public IReadOnlyList<ShoppingItem> GetAll()
    {
        return _items;
    }

    public ShoppingItem? GetById(string id)
    {
        // TODO: Students - Find and return the item with the matching id
        return null;
    }

    public ShoppingItem? Add(string name, int quantity, string? notes)
    {
        ShoppingItem shoppingItem = new ShoppingItem()
        {
            Name = name,
            Quantity = quantity,
            Notes = notes
        };
        
        _items[_nextIndex] = shoppingItem;
        _nextIndex++;
        
        return shoppingItem;
    }
    
    

    public ShoppingItem? Update(string id, string name, int quantity, string? notes)
    {
        // TODO: Students - Implement this method
        // Return the updated item, or null if not found
        return null;
    }

    public bool Delete(string id)
    {
        // TODO: Students - Implement this method
        // Return true if deleted, false if not found
        return false;
    }

    public IReadOnlyList<ShoppingItem> Search(string query)
    {
        // TODO: Students - Implement this method
        // Return the filtered items
        return [];
    }

    public int ClearPurchased()
    {
        // TODO: Students - Implement this method
        // Return the count of removed items
        return 0;
    }

    public bool TogglePurchased(string id)
    {
        // TODO: Students - Implement this method
        // Return true if successful, false if item not found
        return false;
    }

    public bool Reorder(IReadOnlyList<string> orderedIds)
    {
        // TODO: Students - Implement this method
        // Return true if successful, false otherwise
        return false;
    }

    private ShoppingItem[] GenerateDemoItems()
    {
        var items = new ShoppingItem[5];
        items[0] = new ShoppingItem
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Dishwasher tablets",
            Quantity = 1,
            Notes = "80st/pack - Rea",
            IsPurchased = false
        };
        items[1] = new ShoppingItem
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Ground meat",
            Quantity = 1,
            Notes = "2kg - origin Sweden",
            IsPurchased = false
        };
        items[2] = new ShoppingItem
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Apples",
            Quantity = 10,
            Notes = "Pink Lady",
            IsPurchased = false
        };
        items[3] = new ShoppingItem
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Toothpaste",
            Quantity = 1,
            Notes = "Colgate",
            IsPurchased = false
        };
        return items;
    }
}