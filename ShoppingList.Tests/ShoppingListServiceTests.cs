using ShoppingList.Application.Services;
using ShoppingList.Domain.Models;
using Xunit;

namespace ShoppingList.Tests;


public class ShoppingListServiceTests
{
    // TODO: Write your tests here following the TDD workflow


   
    [Fact]
    public void Add_ShouldReturnShoppingItemWithCorrectValues()
    {
        // Arrange
        var service = new ShoppingListService();

        // Act
        var result = service.Add("Milk", 2, "Lactose-free");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Milk", result.Name);
        Assert.Equal(2, result.Quantity);
        Assert.Equal("Lactose-free", result.Notes);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(0)]
    [InlineData(100)]
    public void Return_CorrectListSize(int expected)
    {
        var service = new ShoppingListService();
        for (int i = 0; i < expected; i++)
        {
            service.Add("Milk", 2, "Lactose-free");
            
        }

        var actual = service.GetAll();
        Assert.Equal(expected, actual.Count);
    }
    
    
    
    
}

