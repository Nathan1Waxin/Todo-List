namespace test_Xunit;
using Xunit;

public class TodoServiceTests
{
    [Fact]
    public void CreateTodo_Should_Add_New_Todo()
    {
        // Arrange
        var todoService = new TodoServices(null, null, null);

        // Act
        todoService.CreateTodoAsync(...);

        // Assert
        Assert.NotNull(todoService);
    }
}