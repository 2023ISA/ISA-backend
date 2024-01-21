namespace ISA.Application.API.Models.Requests
{
    using ISA.Core.Domain.Entities.User;

    public class CompanyRegistrationRequestModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public int StartinWorkingHour { get; set; }
        public int EndWorkingHour { get; set; }
    }
}
