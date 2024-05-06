using System;

namespace MyHome.Shared.Requests
{
public record HomeCreateRequest(
    Guid FamilyId,
    string Street,
    string Number,
    string PostalCode,
    string City);
}