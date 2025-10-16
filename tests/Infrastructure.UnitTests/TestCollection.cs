using Xunit;

namespace Infrastructure.UnitTests
{
    [CollectionDefinition("TestCollection")]
    public class TestCollection : ICollectionFixture<TestSetup>
    {
        // Class này không chứa code, chỉ là nơi để áp dụng CollectionDefinition và ICollectionFixture.
        // Constructor tĩnh của TestSetup sẽ được gọi khi collection này được khởi tạo.
    }
}
