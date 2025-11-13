using ShoppingList.Application.Interfaces;
using ShoppingList.Application.Services;
using ShoppingList.Domain.Models;
using Xunit;

namespace ShoppingList.Tests;



/// <summary>
/// Unit tests for ShoppingListService.
///
/// IMPORTANT: Write your tests here using Test Driven Development (TDD)
///
/// TDD Workflow:
/// 1. Write a test for a specific behavior (RED - test fails)
/// 2. Implement minimal code to make the test pass (GREEN - test passes)
/// 3. Refactor the code if needed (REFACTOR - improve without changing behavior)
/// 4. Repeat for the next behavior
///
/// Test Examples:
/// - See ShoppingItemTests.cs for examples of well-structured unit tests
/// - Follow the Arrange-Act-Assert pattern
/// - Use descriptive test names: Method_Scenario_ExpectedBehavior
///
/// What to Test:
/// - Happy path scenarios (normal, expected usage)
/// - Input validation (null/empty IDs, invalid parameters)
/// - Edge cases (empty array, array expansion, last item, etc.)
/// - Array management (shifting after delete, compacting, reordering)
/// - Search functionality (case-insensitive, matching in name/notes)
///
/// Recommended Test Categories:
///
/// GetAll() tests:
/// - GetAll_WhenEmpty_ShouldReturnEmptyList
/// - GetAll_WithItems_ShouldReturnAllItems
/// - GetAll_ShouldNotReturnMoreThanActualItemCount
///
/// GetById() tests:
/// - GetById_WithValidId_ShouldReturnItem
/// - GetById_WithInvalidId_ShouldReturnNull
/// - GetById_WithNullId_ShouldReturnNull
/// - GetById_WithEmptyId_ShouldReturnNull
///
/// Add() tests:
/// - Add_WithValidInput_ShouldReturnItem
/// - Add_ShouldGenerateUniqueId
/// - Add_ShouldIncrementItemCount
/// - Add_WhenArrayFull_ShouldExpandArray
/// - Add_AfterArrayExpansion_ShouldContinueWorking
/// - Add_ShouldSetIsPurchasedToFalse
///
/// Update() tests:
/// - Update_WithValidId_ShouldUpdateAndReturnItem
/// - Update_WithInvalidId_ShouldReturnNull
/// - Update_ShouldNotChangeId
/// - Update_ShouldNotChangeIsPurchased
///
/// Delete() tests:
/// - Delete_WithValidId_ShouldReturnTrue
/// - Delete_WithInvalidId_ShouldReturnFalse
/// - Delete_ShouldRemoveItemFromList
/// - Delete_ShouldShiftRemainingItems
/// - Delete_ShouldDecrementItemCount
/// - Delete_LastItem_ShouldWork
/// - Delete_FirstItem_ShouldWork
/// - Delete_MiddleItem_ShouldWork
///
/// Search() tests:
/// - Search_WithEmptyQuery_ShouldReturnAllItems
/// - Search_WithNullQuery_ShouldReturnAllItems
/// - Search_MatchingName_ShouldReturnItem
/// - Search_MatchingNotes_ShouldReturnItem
/// - Search_ShouldBeCaseInsensitive
/// - Search_WithNoMatches_ShouldReturnEmpty
/// - Search_ShouldFindPartialMatches
///
/// ClearPurchased() tests:
/// - ClearPurchased_WithNoPurchasedItems_ShouldReturnZero
/// - ClearPurchased_ShouldRemoveOnlyPurchasedItems
/// - ClearPurchased_ShouldReturnCorrectCount
/// - ClearPurchased_ShouldShiftRemainingItems
///
/// TogglePurchased() tests:
/// - TogglePurchased_WithValidId_ShouldReturnTrue
/// - TogglePurchased_WithInvalidId_ShouldReturnFalse
/// - TogglePurchased_ShouldToggleFromFalseToTrue
/// - TogglePurchased_ShouldToggleFromTrueToFalse
///
/// Reorder() tests:
/// - Reorder_WithValidOrder_ShouldReturnTrue
/// - Reorder_WithInvalidId_ShouldReturnFalse
/// - Reorder_WithMissingIds_ShouldReturnFalse
/// - Reorder_WithDuplicateIds_ShouldReturnFalse
/// - Reorder_ShouldChangeItemOrder
/// - Reorder_WithEmptyList_ShouldReturnFalse
/// </summary>
///

public class ShoppingListServiceTests
{
    
    
    
    // TODO: Write your tests here following the TDD workflow
    
    ShoppingListService _sut = new ShoppingListService();

    [Theory]
    [InlineData("Namn", 1, "Notes")]

    public void Add_WithValidInput_ShouldReturnItem(string name, int quantity, string? notes)
    {
        //Arrange
        var expected = _sut.Add(name, quantity, notes);

        //Act
        var result = new ShoppingItem()
        {
            Id = "ID",
            Name = name,
            Quantity = quantity,
            Notes = notes
        };

        //Assert
        
        Assert.Equal(expected.Name, result.Name);
        Assert.Equal(expected.Quantity, result.Quantity);
        Assert.Equal(expected.Notes, result.Notes);
    }
    
    [Fact]
    public void Add_ShouldAddItem()
    {
        // Arrange
        var service = new ShoppingListService();
    
        // Act
        var item = service.Add("Milk", 2, "Lactose-free");

        var items = service.GetAll();
        
    
        // Assert
        Assert.NotNull(item);
        Assert.Equal("Milk", item!.Name);
        Assert.Equal(2, item.Quantity);
        Assert.Contains(item, items);
    }

    [Fact]
    public void GetAll_WhenCalled_ShouldReturnAllItems()
    {
        //Arrange
        var expectedList = new ShoppingListService().GetAll();


        //Act
        var actualList = _sut.GetAll();


        //Assert
        Assert.NotNull(actualList);
    }
    
    [Theory]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("3")]
    public void GetById_WhenCalled_ShouldReturnItem(string id)
    {
        // Arrange
        var service = new ShoppingListService();
    
        // Act
        var items = service.GetAll();
        var item = service.GetById(id);
        
        var lastItem = service.GetById("3");
        
    
        // Assert
        Assert.NotNull(item);
        Assert.Contains(items, i => i.Id == id);
        
        
    }

    [Fact]
    
    public void Delete_ShouldDeleteItemAndUpdateIndex()
    {
        //Arrange
        var index = "2";
        var service = new ShoppingListService();
        var allItems = service.GetAll();
        

        //Act
        var deletedItem = service.GetById(index);
        var deleteItem = service.Delete(index);

        //Assert
        Assert.True(deleteItem);
        Assert.DoesNotContain(deletedItem, allItems);
        Assert.Equal(allItems[2].Name, "Toothpaste");
    }

    // Example test structure:
    // [Fact]
    // public void Add_WithValidInput_ShouldReturnItem()
    // {
    //     // Arrange
    //     var service = new ShoppingListService();
    //
    //     // Act
    //     var item = service.Add("Milk", 2, "Lactose-free");
    //
    //     // Assert
    //     Assert.NotNull(item);
    //     Assert.Equal("Milk", item!.Name);
    //     Assert.Equal(2, item.Quantity);
    // }
    [Theory]
    [InlineData(2)]
    public void GetAll_ShouldGetCorrectAmountOfItems(int expected)
    {
        //Arrange
        var service = new ShoppingListService();
        //GenerateDemoItems(service);
        for (int i = 0; i < expected; i++)
        {
            service.Add("Banana", 1, "Just a banana");
        }


        //Act
        var actual = service.GetAll();
        var actualItems = service._Test_items;


        //Assert
        Assert.Equal(expected, actualItems.Length);
    }


        [Theory]
        [InlineData("app")]
        [InlineData("to")]
        [InlineData("dis")]
    

    public void Search_ByString_ShouldReturnCorrectItems(string search)
    {
        //Arrange
        var  service = new ShoppingListService();
        
        var allItems = service.GetAll();
        

        //Act
        var foundItems = service.Search(search);

        //Assert
        Assert.NotNull(foundItems);
        

    }
    
    
    
    private void GenerateDemoItems(IShoppingListService service)
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
        foreach (var item in items)
        {
            service.Add(item.Name, item.Quantity, item.Notes);
        }
        service.Add("Banana", 1, "Just a banana");
        //return items;
    }
    
}

