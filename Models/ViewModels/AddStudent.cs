using Models.DomainModels;

namespace Models.ViewModels
{
    public class AddStudent
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalCode { get; set; }
        public Gender Gender { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public ICollection<AddAddress> AddAddresses { get; set; }
    }
}
