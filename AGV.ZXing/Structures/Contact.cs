using OutSystems.Model.ExternalLibraries.SDK;
using System.Collections.Generic;
using MixERP.Net.VCards;
using MixERP.Net.VCards.Types;
using MixERP.Net.VCards.Models;
using MixERP.Net.VCards.Serializer;
using System;


namespace AGV.ZXing.Structures {

    [OSStructure(Description = "Defines a this to be shared as a QR code")]
    public struct Contact {
        [OSStructureField(DataType = OSDataType.Text, Description = "Formated name")]
        public string formatedName;

        [OSStructureField(Description = "Composed name")]
        public Structures.ComposedName composedName;

        [OSStructureField(DataType = OSDataType.Text, Description = "Organization")]
        public string organization;

        [OSStructureField(DataType = OSDataType.Text, Description = "Title")]
        public string title;

        [OSStructureField(DataType = OSDataType.PhoneNumber, Description = "Home phone number")]
        public string homePhone;

        [OSStructureField(DataType = OSDataType.PhoneNumber, Description = "Work phone number")]
        public string workPhone;

        [OSStructureField(DataType = OSDataType.PhoneNumber, Description = "Mobile phone number")]
        public string mobilePhone;

        [OSStructureField(DataType = OSDataType.Email, Description = "Email")]
        public string email;

        [OSStructureField(DataType = OSDataType.Email, Description = "Address")]
        public string address;

        [OSStructureField(DataType = OSDataType.Email, Description = "Website")]
        public string website;

        [OSStructureField(DataType = OSDataType.Email, Description = "Notes")]
        public string notes;

        public Contact(string formatedName, ComposedName composedName, string organization, string title, 
                        string homePhone, string workPhone, string mobilePhone, string email, string address, string website, string notes):this() {
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

        public Contact(Contact c):this(){
            this.formatedName = c.formatedName;
            this.composedName = new ComposedName(c.composedName);
            this.organization = c.organization;
            this.title = c.title;
            this.homePhone = c.homePhone;
            this.workPhone = c.workPhone;
            this.mobilePhone = c.mobilePhone;
            this.email = c.email;
            this.address = c.address;
            this.website = c.website;
            this.notes = c.notes;
        }

        public string ToVCardString()
        {
            var vcard = new VCard {
                Version = VCardVersion.V3,
                FormattedName = this.formatedName,
                FirstName = this.composedName.firstName ?? "",
                LastName = this.composedName.lastName ?? "",
                MiddleName = this.composedName.middleNames ?? "",
                Organization = this.organization,
                Title = this.title,
                Telephones = new Telephone[] { 
                    new Telephone { Type = TelephoneType.Home, Number = this.homePhone },
                    new Telephone { Type = TelephoneType.Work, Number = this.workPhone },
                    new Telephone { Type = TelephoneType.Cell, Number = this.mobilePhone }
                },
                Url = new UriBuilder(this.website).Uri,
                Emails = new Email[] {
                    new Email { Type = EmailType.Smtp, EmailAddress = this.email }
                },
                Addresses = new Address[] {
                    new Address { Type = AddressType.Home, ExtendedAddress = this.address }
                },
                Note = this.notes
            };
            return VCardSerializer.Serialize(vcard);
        }

        public string ToMeCardString()
        {
            //MECARD:N:name;ORG:company;TEL:123;URL:http\://;EMAIL:email@mail.com;ADR:address address2;NOTE:memotitle;;
            return $"MECARD:N:{ this.formatedName.encodeQRCode() };TEL:{ this.homePhone.encodeQRCode() };" +
            $"URL:{ this.website.encodeQRCode() };EMAIL:{ this.email.encodeQRCode() };ADR:{ this.address.encodeQRCode() }" + 
            $"NOTE: { this.notes.encodeQRCode() };;";

        }
    }
}