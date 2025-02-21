using System;
using AGV.ZXing.Structures;

namespace AGV.ZXing.Tests
{
    [TestFixture]
    public partial class QRCodeTests
    {
        private ZXingLib z = Tests.zxing;

        byte[] overlay = Utils.LoadResource("ZXingLibTest.resources.osring.png");

        [Test(Description = "Execute tests with overlays")]
        [TestCase("Q")]
        [TestCase("H")]
        [TestCase(null)]
        [Category("QRCode")]
        public void TestEncode_Overlay(string? ecl)
        {
            var bytes = z.Encode("https://www.outsystems.com", "QR_CODE", 330, 330, 0, true, false, true, "UTF-8", ecl, null, this.overlay, null);
#if DEBUG
            File.WriteAllBytes($"output/qrcode/overlay_{ecl ?? "null"}.png", bytes);
#endif
            var barcode = z.Decode(bytes, "QR_CODE");
            Assert.That(barcode, Is.Not.Null);
            var b = barcode.GetValueOrDefault();
            Assert.That(b.value, Is.EqualTo("https://www.outsystems.com"));
        }


        [Test(Description = "Execxute tests with calendar events QR codes")]
        [TestCase("Event 1", true, 1, "Room A", "This is event 1", "PUBLIC", "John Doe", 5, true)]
        [TestCase("Event 2", false, 45, "Rua Central Park 2 2A", "", "PRIVATE", "", 5, true)]
        [Category("QRCode")]
        public void TestEncodeExt_CalendarEvent(string title, bool isAllDay, int duration, string location, string description, string eClass, string organizer, int priority, bool showAsBusy)
        {
            var s = isAllDay ? new DateTime(2023, 4, 11) : new DateTime(2023, 4, 11, 18, 0, 0, DateTimeKind.Utc);
            var e = isAllDay ? new DateTime(2023, 4, 13) : new DateTime(2023, 4, 11, 18, 30, 0, DateTimeKind.Utc);
            var c = new CalendarEvent(title, isAllDay, s, e, location, description, eClass, organizer, priority, showAsBusy);
            var bytes = z.EncodeCalendarEvent(c, 330, this.overlay);
#if DEBUG
            File.WriteAllBytes($"output/qrcode/{title.Replace(" ", "")}.png", bytes);
#endif
            var barcode = z.Decode(bytes, "QR_CODE");
            Assert.That(barcode, Is.Not.Null);
            var b = barcode.GetValueOrDefault();
            var val1 = MyRegex().Replace(b.value, "");
            var val2 = MyRegex1().Replace(c.ToString(), "");
            Assert.That(val1, Is.EqualTo(val2));
        }

        [Test(Description = "Executes tests for contacts QR codes")]
        [TestCase(false)]
        [TestCase(true)]
        [Category("QRCode")]
        public void TestEncodeExt_Contact(bool isMeCard)
        {
            var c = new Contact("Jane Doe", new ComposedName("Jane", "Doe"), "ACME", "Designer", "555 321212", "555 321313", "", "jane.doe@acme.com", "Rua Central Park, 2 2A, 2795-242, Linda-a-Velha, Portugal", "www.ac.me", "Some notes");
            var bytes = z.EncodeContact(c, isMeCard, 200, null);
#if DEBUG
            File.WriteAllBytes($"output/qrcode/contact_{(isMeCard ? "mecard" : "vcard")}.png", bytes);
#endif
            var barcode = z.Decode(bytes, "QR_CODE");
            Assert.That(barcode, Is.Not.Null);
            var b = barcode.GetValueOrDefault();
            Assert.That(b.value, Is.EqualTo(isMeCard ? c.ToMeCardString() : c.ToVCardString()));
        }

        [Test(Description = "Execute test for email QR code")]
        [Category("QRCode")]
        public void TestEncodeExt_Email()
        {
            var bytes = z.EncodeEmail(@"andre.vieira@outsystems.com", 200, null);
#if DEBUG
            File.WriteAllBytes("output/qrcode/email.png", bytes);
#endif
            var barcode = z.Decode(bytes, "QR_CODE");
            Assert.That(barcode, Is.Not.Null);
            var b = barcode.GetValueOrDefault();
            Assert.That(b.value, Is.EqualTo("mailto:andre.vieira@outsystems.com"));
        }

        [Test(Description = "Execute tests for location QR code")]
        [Category("QRCode")]
        public void TestEncodeExt_Location()
        {
            var bytes = z.EncodeLocation("38.7210876", "-9.2390245", 200, null);
#if DEBUG
            File.WriteAllBytes("output/qrcode/location.png", bytes);
#endif
            var barcode = z.Decode(bytes, "QR_CODE");
            Assert.That(barcode, Is.Not.Null);
            var b = barcode.GetValueOrDefault();
            Assert.That(b.value, Is.EqualTo("geo:38.7210876,-9.2390245"));
        }

        [Test(Description = "Execute tests for phone number QR codes")]
        [TestCase("5553344222", false, "output/qrcode/call.png")]
        [TestCase("5553344222", true, "output/qrcode/facetime.png")]
        [Category("QRCode")]
        public void TestEncodeExt_PhoneNumber(string phoneNumber, bool isFacetime, string path)
        {
            var bytes = z.EncodePhoneNumber(phoneNumber, isFacetime, 200, null);
#if DEBUG
            File.WriteAllBytes(path, bytes);
#endif        
            var barcode = z.Decode(bytes, "QR_CODE");
            Assert.That(barcode, Is.Not.Null);
            var b = barcode.GetValueOrDefault();
            Assert.That(b.value, Is.EqualTo(isFacetime ? "facetime:5553344222" : "tel:5553344222"));
        }

        [Test(Description = "Execute tests for SMS QR code")]
        [Category("QRCode")]
        public void TestEncodeExt_SMS()
        {
            var bytes = z.EncodeSMS("5553322444", "This is a message", 200, null);
#if DEBUG
            File.WriteAllBytes("output/qrcode/sms.png", bytes);
#endif
            var barcode = z.Decode(bytes, "QR_CODE");
            Assert.That(barcode, Is.Not.Null);
            var b = barcode.GetValueOrDefault();
            Assert.That(b.value, Is.EqualTo("smsto:5553322444:This is a message"));
        }

        [Test(Description = "Execute tests for Wifi QR code")]
        [Category("QRCode")]
        public void TestEncodeExt_Wifi()
        {
            var w = new Wifi("SUPERFAST-123", "StrongerThanYouThink", "WEP", false, "", "", "", "");
            var bytes = z.EncodeWifi(w, 200, null);
#if DEBUG
            File.WriteAllBytes("output/qrcode/wifi.png", bytes);
#endif
            var barcode = z.Decode(bytes, "QR_CODE");
            Assert.That(barcode, Is.Not.Null);
            var b = barcode.GetValueOrDefault();
            Assert.That(b.value, Is.EqualTo(w.ToString()));
        }

        [System.Text.RegularExpressions.GeneratedRegex("DTSTAMP:.*")]
        private static partial System.Text.RegularExpressions.Regex MyRegex();
        [System.Text.RegularExpressions.GeneratedRegex("DTSTAMP:.*")]
        private static partial System.Text.RegularExpressions.Regex MyRegex1();
    }
}