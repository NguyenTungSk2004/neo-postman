using Yitter.IdGenerator;

namespace Domain.UnitTests
{
    public class TestSetup
    {
        static TestSetup()
        {
            // Khởi tạo YitIdHelper một lần cho tất cả các test
            var options = new IdGeneratorOptions(1); // WorkerId = 1
            YitIdHelper.SetIdGenerator(options);
        }
    }
}
