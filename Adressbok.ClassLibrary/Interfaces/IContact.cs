namespace Adressbok.ClassLibrary.Interfaces;

public interface IContact
{
    string FirstName { get; set; }
    string LastName { get; set; }
    string Email { get; set; }
    string Phone { get; set; }
    string City { get; set; }
    string Street { get; set; }
    string PostalCode { get; set; }
    int ID { get; set; }
}
