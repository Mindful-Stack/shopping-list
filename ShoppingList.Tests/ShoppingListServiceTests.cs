using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using ShoppingList.Application.Interfaces;
using ShoppingList.Application.Services;
using ShoppingList.Domain.Models;
using System.Runtime.CompilerServices;
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
/// - Add_ShouldIncrementItemCount
/// - Add_WhenArrayFull_ShouldExpandArray
/// - Add_AfterArrayExpansion_ShouldContinueWorking
///
/// Update() tests
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
public class ShoppingListServiceTests
{
    // TODO: Write your tests here following the TDD workflow

    // Example test structure:
    [Fact]
    public void Add_WithValidInput_ShouldReturnItem()
    {
        // Arrange
        var service = new ShoppingListService();
        // Act
        var item = service.Add("Milk", 2, "Lactose-free");
        // Assert
        Assert.NotNull(item);
        Assert.Equal("Milk", item!.Name);
        Assert.Equal(2, item.Quantity);
    }

    [Fact]
    public void Add_ShouldGenerateUniqueId()
    {
        // Arrange
        var service = new ShoppingListService();
        var items = new ShoppingItem[3];
        items[0] = service.Add("Bread", 1, "Whole grain");
        items[1] = service.Add("Eggs", 12, "Free range");
        items[2] = service.Add("Milk", 2, "Lactose-free");

        // Act
        var newItem = service.Add("Butter", 1, "Unsalted");

        // Assert
        Assert.DoesNotContain(items, item => item.Id == newItem.Id);
    }

    [Fact]
    public void Add_ShouldSetIsPurchasedToFalse()
    {
        // Arrange
        var service = new ShoppingListService();
        // Act
        var item = service.Add("Cheese", 1, "Cheddar");
        // Assert
        Assert.False(item.IsPurchased);

    }

    [Fact]
    public void Add_ShouldIncrementItemCount()
    {
        // Arrange
        var service = new ShoppingListService();
        int initialCount = service.GetAll().Count;


        // Act
        service.Add("Yogurt", 3, "Greek");


        // Assert
        Assert.Equal(initialCount , service.GetAll().Count);
        //Assert.Equal(initialCount, service.GetAll().Count);
    }




    //private void AddTestData(IShoppingListService service)
    //{
    //    service.Add("Bread", 1, "Whole grain");
    //    service.Add("Eggs", 12, "Free range");
    //    service.Add("Milk", 2, "Lactose-free");
    //    service.Add("Butter", 1, "Unsalted");
    //    service.Add("Cheese", 1, "Cheddar");
    //}





    [Theory]
    [InlineData("Apple", 10, null)]
    [InlineData("Bananas", 5, "Riped and ready to eat")]
    [InlineData("Cucumber", 2, "Bent to perfection")]

    public void Update_ShouldIncrementItemCount(string expectedName, int expectedQuantity, string? expectedNote)
    {
        // Arrange
        var service = new ShoppingListService();
        var ShoppingItem = service.GetAll();


        // Act
        service.Update(ShoppingItem[0].Id, expectedName, expectedQuantity, expectedNote);

        // Assert
        Assert.Equal(expectedName, ShoppingItem[0].Name);
        Assert.Equal(expectedQuantity, ShoppingItem[0].Quantity);
        Assert.Equal(expectedNote, ShoppingItem[0].Notes);

    }

    [Theory]
    [InlineData("", 10, null)]
    [InlineData(null, 5, "Riped and ready to eat")]
    public void Update_ShouldThrowArgumentException(string newName, int newQuantity, string? newNote)
    {
        // Arrange
        var service = new ShoppingListService();
        var ShoppingItem = service.GetAll();


        // Act

        // Assert
        Assert.Throws<ArgumentException>(() => service.Update(ShoppingItem[0].Id, newName, newQuantity, newNote));

    }


    [Theory]
    [InlineData("Köttbullar", -10, null)]
    [InlineData("KanelBUllar", -5, "Riped and ready to eat")]
    public void Update_ShouldThrowException(string newName, int newQuantity, string? newNote)
    {
        // Arrange
        var service = new ShoppingListService();
        var ShoppingItem = service.GetAll();


        // Act

        // Assert
        Assert.Throws<Exception>(() => service.Update(ShoppingItem[0].Id, newName, newQuantity, newNote));

    }

    /// - Add_ShouldGenerateUniqueId
    /// - Add_WhenArrayFull_ShouldExpandArray
    /// - Add_AfterArrayExpansion_ShouldContinueWorking
    /// - Add_ShouldSetIsPurchasedToFalse

    [Fact]
    public void Get_All_ShouldReturnAllItems()
    {
        // Arrange
        var service = new ShoppingListService();

        // Act
        var items = service.GetAll();

        // Assert
        Assert.NotEmpty(items);
    }

    /// - GetAll_WithItems_ShouldReturnAllItems
    //[Fact]
    //public void GetAll_WithItems_ShouldReturnAllItems()
    //{
    //    // Arrange
    //    var service = new ShoppingListService();
    //    AddTestData(service);
    //    // Act
    //    var items = service.GetAll();
    //    // Assert
    //    Assert.Equal(5, items.Count);
    //}
}

