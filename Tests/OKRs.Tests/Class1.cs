using OKRs.Models;
using OKRs.Models.ObjectiveViewModels;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace OKRs.Tests
{
    public class KeyResultCompletionRateTests
    {
        [Fact]
        public void KeyResult_Should_Have_Default_CompletionRate_Zero()
        {
            // Arrange & Act
            var keyResult = new KeyResult();
            
            // Assert
            Assert.Equal(0.0m, keyResult.CompletionRate);
        }
        
        [Theory]
        [InlineData(0.0)]
        [InlineData(0.5)]
        [InlineData(1.0)]
        public void KeyResult_Should_Accept_Valid_CompletionRate(decimal completionRate)
        {
            // Arrange & Act
            var keyResult = new KeyResult
            {
                CompletionRate = completionRate
            };
            
            // Assert
            Assert.Equal(completionRate, keyResult.CompletionRate);
        }
        
        [Theory]
        [InlineData(0.0, true)]
        [InlineData(0.5, true)]
        [InlineData(1.0, true)]
        [InlineData(-0.1, false)]
        [InlineData(1.1, false)]
        public void SaveKeyResultFormModel_Should_Validate_CompletionRate_Range(decimal completionRate, bool isValid)
        {
            // Arrange
            var formModel = new SaveKeyResultFormModel
            {
                Description = "Test Key Result",
                CompletionRate = completionRate
            };
            
            var context = new ValidationContext(formModel, null, null);
            var results = new List<ValidationResult>();
            
            // Act
            bool actualIsValid = Validator.TryValidateObject(formModel, context, results, true);
            
            // Assert
            Assert.Equal(isValid, actualIsValid);
            
            if (!isValid)
            {
                Assert.Contains(results, r => r.MemberNames.Contains(nameof(SaveKeyResultFormModel.CompletionRate)));
            }
        }
        
        [Fact]
        public void KeyResultListItemViewModel_Should_Include_CompletionRate()
        {
            // Arrange & Act
            var viewModel = new KeyResultListItemViewModel
            {
                Id = Guid.NewGuid(),
                Description = "Test Key Result",
                CompletionRate = 0.75m
            };
            
            // Assert
            Assert.Equal(0.75m, viewModel.CompletionRate);
        }
        
        [Fact]
        public void KeyResultDetailsViewModel_Should_Include_CompletionRate()
        {
            // Arrange & Act
            var viewModel = new KeyResultDetailsViewModel
            {
                ObjectiveTitle = "Test Objective",
                Description = "Test Key Result",
                CompletionRate = 0.5m,
                Id = Guid.NewGuid(),
                ObjectiveId = Guid.NewGuid(),
                Created = DateTime.Now
            };
            
            // Assert
            Assert.Equal(0.5m, viewModel.CompletionRate);
        }
    }
}
