namespace OKRs.Models.UserViewModels
{
    public class UserListItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsInactive { get; set; }
    }
}