using Ardalis.Specification;

namespace Domain.AggregatesModel.UserAggregate.Specifications
{
    public class UserByEmailSpecification : Specification<User>
    {
        public UserByEmailSpecification(string email)
        {
            Query.Where(u => u.Email == email);
        }
    }
}