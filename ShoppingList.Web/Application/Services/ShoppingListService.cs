using ShoppingList.Application.Interfaces;
using ShoppingList.Domain.Models;
using System.Linq.Expressions;

namespace ShoppingList.Application.Services;

public class ShoppingListService : IShoppingListService
{
    private ShoppingItem[] _items;
    private int _nextIndex;

    public ShoppingListService()
    {
        // Initialize with demo data for UI demonstration
        // TODO: Students can remove or comment this out when running unit tests
        _items = GenerateDemoItems(); // databas
        _nextIndex = 4; // We have 4 demo items initialized
    }

    public IReadOnlyList<ShoppingItem> GetAll()
    {
        // TODO: Students - Return all items from the array (up to _nextIndex)
        if(_items.Length > 0)
        {
            return _items;
        }

        return [];
    }

    public ShoppingItem? GetById(string id)
    {
        // TODO: Students - Find and return the item with the matching id
        return null;
    }

    public ShoppingItem? Add(string name, int quantity, string? notes)
    {
        // TODO: Students - Implement this method
        // Return the created item
        var newItem = new ShoppingItem
        {
            Name = name,
            Quantity = quantity,
            Notes = notes
        };

        if (newItem == null)
        {
            throw new Exception("Item invalid");
        }

        for(int i = 0; i < _items.Length; i++)
        {
            if(_items[i] == null)
            {
                _items[i] = newItem;
                return newItem;
            }
        }
        return newItem;
    }

    public ShoppingItem? Update(string id, string name, int quantity, string? notes)
    {
        // TODO: Students - Implement this method
        // Return the updated item, or null if not found
        if(string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name invalid, cant be empty");
        }
        if(quantity < 0)
        {
            throw new Exception("Quantity invalid, must be greater than zero");
        }
        var items = _items;
        var itemToUpdate = items.Where(p => p.Id == id).FirstOrDefault();

        if (itemToUpdate != null)
        {
            itemToUpdate.Name = name;
            itemToUpdate.Quantity = quantity;
            itemToUpdate.Notes = notes;
            return itemToUpdate;
        }

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