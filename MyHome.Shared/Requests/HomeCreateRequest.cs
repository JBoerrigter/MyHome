using System;

namespace MyHome.Shared.Requests
{
public record HomeCreateRequest(
    int FamilyId,
    string Street,
    string Number,
    string PostalCode,
    string City);
}