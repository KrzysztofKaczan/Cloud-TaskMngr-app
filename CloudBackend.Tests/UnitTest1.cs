using Xunit;
using CloudBackend.Models; 

namespace CloudBackend.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void NewTask_ShouldNotBeCompleted()
        {
            // Arrange & Act
            var task = new CloudTask
            {
                Name = "Przetestować bezpiecznik"
            };

            // Assert
            Assert.False(task.IsCompleted);
        }
    }
}