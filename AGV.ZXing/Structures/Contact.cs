using OutSystems.ExternalLibraries.SDK;
using MixERP.Net.VCards;
using MixERP.Net.VCards.Types;
using MixERP.Net.VCards.Models;
using MixERP.Net.VCards.Serializer;
using System;


namespace AGV.ZXing.Structures
{

    [OSStructure(Description = "Defines a this to be shared as a QR code")]
    public struct Contact
    {
        [OSStructureField(IsMandatory = true, Description = "Formated name", Length = 100)]
        public string FormatedName { get; set; } = "";

        [OSStructureField(Description = "Composed name")]
        public ComposedName ComposedName { get; set; } = new ComposedName();

        [OSStructureField(Description = "Organization", Length = 50)]
        public string Organization { get; set; } = "";

        [OSStructureField(Description = "Title", Length = 50)]
        public string Title { get; set; } = "";

        [OSStructureField(DataType = OSDataType.PhoneNumber, Description = "Home phone number")]
        public string HomePhone { get; set; } = "";

        [OSStructureField(DataType = OSDataType.PhoneNumber, Description = "Work phone number")]
        public string WorkPhone { get; set; } = "";

        [OSStructureField(DataType = OSDataType.PhoneNumber, Description = "Mobile phone number")]
        public string MobilePhone { get; set; } = "";

        [OSStructureField(DataType = OSDataType.Email, Description = "Email")]
        public string Email { get; set; } = "";

        [OSStructureField(Description = "Address", Length = 100)]
        public string Address { get; set; } = "";

        [OSStructureField(Description = "Website", Length = 100)]
        public string Website { get; set; } = "";

        [OSStructureField(Description = "Notes", Length = 2000)]
        public string Notes { get; set; } = "";

        public Contact(string formatedName, ComposedName composedName, string organization, string title,
                        string homePhone, string workPhone, string mobilePhone, string email, string address, string website, string notes) : this()
        {
            this.FormatedName = formatedName;
            this.ComposedName = new ComposedName(composedName);
            this.Organization = organization;
            this.Title = title;
            this.HomePhone = homePhone;
            this.WorkPhone = workPhone;
            this.MobilePhone = mobilePhone;
            this.Email = email;
            this.Address = address;
            this.Website = website;
            this.Notes = notes;
        }

        public Contact(Contact c) : this()
        {
            FormatedName = c.FormatedName;
            ComposedName = new ComposedName(c.ComposedName);
            Organization = c.Organization;
            Title = c.Title;
            HomePhone = c.HomePhone;
            WorkPhone = c.WorkPhone;
            MobilePhone = c.MobilePhone;
            Email = c.Email;
            Address = c.Address;
            Website = c.Website;
            Notes = c.Notes;
        }

        public readonly string ToVCardString()
        {
            var vcard = new VCard
            {
                Version = VCardVersion.V3,
                FormattedName = FormatedName,
                FirstName = ComposedName.FirstName ?? "",
                LastName = ComposedName.LastName ?? "",
                MiddleName = ComposedName.MiddleNames ?? "",
                Organization = Organization,
                Title = Title,
                Telephones = [
                    new Telephone { Type = TelephoneType.Home, Number = HomePhone },
                    new Telephone { Type = TelephoneType.Work, Number = WorkPhone },
                    new Telephone { Type = TelephoneType.Cell, Number = MobilePhone }
                ],
                Url = new UriBuilder(Website).Uri,
                Emails = [
                    new Email { Type = EmailType.Smtp, EmailAddress = Email }
                ],
                Addresses = [
                    new Address { Type = AddressType.Home, ExtendedAddress = Address }
                ],
                Note = Notes
            };
            return VCardSerializer.Serialize(vcard);
        }

        public readonly string ToMeCardString()
        {
            return $"MECARD:N:{FormatedName.EncodeQRCode()};TEL:{HomePhone.EncodeQRCode()};" +
            $"URL:{Website.EncodeQRCode()};EMAIL:{Email.EncodeQRCode()};ADR:{Address.EncodeQRCode()}" +
            $"NOTE: {Notes.EncodeQRCode()};;";

        }
    }
}