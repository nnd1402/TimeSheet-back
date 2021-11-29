using System.Collections.Generic;

namespace TimeSheet.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public int CountryId { get; set; }
    }
}
