using ShoppingList.Application.Interfaces;
using ShoppingList.Domain.Models;

namespace ShoppingList.Application.Services;

public class ShoppingListService : IShoppingListService
{
    private ShoppingItem[] _items;
    private int _nextIndex;

    public ShoppingListService()
    {
        // Initialize with demo data for UI demonstration
        // TODO: Students can remove or comment this out when running unit tests
        _items = GenerateDemoItems();
        _nextIndex = 4; // We have 4 demo items initialized
    }

    public IReadOnlyList<ShoppingItem> GetAll()
    {
        // TODO: Students - Return all items from the array (up to _nextIndex)
        return _items;
        //return _items.Where(item => item.Id != "").ToArray();
        //return GenerateDemoItems();
    }

    public ShoppingItem? GetById(string id)
    {
        // TODO: Students - Find and return the item with the matching id
        var item = _items.FirstOrDefault(i => i.Id == id);
        return item;
    }

    public ShoppingItem? Add(string name, int quantity, string? notes)
    {
        // TODO: Students - Implement this method
        ShoppingItem item = new ShoppingItem
        {
            Id = _nextIndex.ToString(),
            Name = name,
            Quantity = quantity,
            Notes = notes
        };
        _items[_nextIndex] = item;
        _nextIndex++;
        // Return the created item
        return item;
        
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
        
        var deleteItem = _items.FirstOrDefault(i => i.Id == id);
        if (deleteItem != null)
        {
            for (int i = 0; i <= _nextIndex; i++)
            {
                if (_items[i] == deleteItem)
                {
                    for (int j = i; j <= _nextIndex - 1; j++)
                    {
                        _items[j] = _items[j + 1];
                    }
                        
                    
                }
            }
            return true;
        }
        
        return false;
        
        
        // Return true if deleted, false if not found
        
    }

    public IReadOnlyList<ShoppingItem> Search(string query)
    {
        // TODO: Students - Implement this method
        // Return the filtered items

        var foundItems = new ShoppingItem[5];
        int j = 0;
        foreach (var item in _items)
        {
            if (item.Name.Contains(query, StringComparison.CurrentCultureIgnoreCase))
            {
                foundItems[j] = item;
                j++;
                // for (int i = 0; i <= _items.Length; i++)
                // {
                //     foundItems[i] = item;
                // }
            }
            
        }
        
        
        return foundItems;
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
            Id = "0",
            Name = "Dishwasher tablets",
            Quantity = 1,
            Notes = "80st/pack - Rea",
            IsPurchased = false
        };
        items[1] = new ShoppingItem
        {
            Id = "1",
            Name = "Ground meat",
            Quantity = 1,
            Notes = "2kg - origin Sweden",
            IsPurchased = false
        };
        items[2] = new ShoppingItem
        {
            Id = "2",
            Name = "Apples",
            Quantity = 10,
            Notes = "Pink Lady",
            IsPurchased = false
        };
        items[3] = new ShoppingItem
        {
            Id = "3",
            Name = "Toothpaste",
            Quantity = 1,
            Notes = "Colgate",
            IsPurchased = false
        };
        return items;
    }

    public ShoppingItem[] _Test_items => _items;
}