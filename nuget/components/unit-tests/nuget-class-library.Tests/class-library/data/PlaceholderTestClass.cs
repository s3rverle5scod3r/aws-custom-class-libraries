using Xunit;

namespace nuget_class_library.Tests.class_library.data
{
	public class PlaceholderTestClass
	{
		[Theory]
		[InlineData("s3rverle5scod3r")]
		public void Default_Test(string placeholder)
		{
			// Arrange

			// Act

			// Assert
			Assert.Equal("s3rverle5scod3r", placeholder);
		}
	}
}
