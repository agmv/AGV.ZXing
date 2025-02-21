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
        public string formatedName;

        [OSStructureField(Description = "Composed name")]
        public ComposedName composedName;

        [OSStructureField(Description = "Organization", Length = 50)]
        public string organization;

        [OSStructureField(Description = "Title", Length = 50)]
        public string title;

        [OSStructureField(DataType = OSDataType.PhoneNumber, Description = "Home phone number")]
        public string homePhone;

        [OSStructureField(DataType = OSDataType.PhoneNumber, Description = "Work phone number")]
        public string workPhone;

        [OSStructureField(DataType = OSDataType.PhoneNumber, Description = "Mobile phone number")]
        public string mobilePhone;

        [OSStructureField(DataType = OSDataType.Email, Description = "Email")]
        public string email;

        [OSStructureField(Description = "Address", Length = 100)]
        public string address;

        [OSStructureField(Description = "Website", Length = 100)]
        public string website;

        [OSStructureField(Description = "Notes", Length = 2000)]
        public string notes;

        public Contact(string formatedName, ComposedName composedName, string organization, string title,
                        string homePhone, string workPhone, string mobilePhone, string email, string address, string website, string notes) : this()
        {
            this.formatedName = formatedName;
            this.composedName = new ComposedName(composedName);
            this.organization = organization;
            this.title = title;
            this.homePhone = homePhone;
            this.workPhone = workPhone;
            this.mobilePhone = mobilePhone;
            this.email = email;
            this.address = address;
            this.website = website;
            this.notes = notes;
        }

        public Contact(Contact c) : this()
        {
            formatedName = c.formatedName;
            composedName = new ComposedName(c.composedName);
            organization = c.organization;
            title = c.title;
            homePhone = c.homePhone;
            workPhone = c.workPhone;
            mobilePhone = c.mobilePhone;
            email = c.email;
            address = c.address;
            website = c.website;
            notes = c.notes;
        }

        public readonly string ToVCardString()
        {
            var vcard = new VCard
            {
                Version = VCardVersion.V3,
                FormattedName = formatedName,
                FirstName = composedName.firstName ?? "",
                LastName = composedName.lastName ?? "",
                MiddleName = composedName.middleNames ?? "",
                Organization = organization,
                Title = title,
                Telephones = [
                    new Telephone { Type = TelephoneType.Home, Number = homePhone },
                    new Telephone { Type = TelephoneType.Work, Number = workPhone },
                    new Telephone { Type = TelephoneType.Cell, Number = mobilePhone }
                ],
                Url = new UriBuilder(website).Uri,
                Emails = [
                    new Email { Type = EmailType.Smtp, EmailAddress = email }
                ],
                Addresses = [
                    new Address { Type = AddressType.Home, ExtendedAddress = address }
                ],
                Note = notes
            };
            return VCardSerializer.Serialize(vcard);
        }

        public readonly string ToMeCardString()
        {
            //MECARD:N:name;ORG:company;TEL:123;URL:http\://;EMAIL:email@mail.com;ADR:address address2;NOTE:memotitle;;
            return $"MECARD:N:{formatedName.EncodeQRCode()};TEL:{homePhone.EncodeQRCode()};" +
            $"URL:{website.EncodeQRCode()};EMAIL:{email.EncodeQRCode()};ADR:{address.EncodeQRCode()}" +
            $"NOTE: {notes.EncodeQRCode()};;";

        }
    }
}