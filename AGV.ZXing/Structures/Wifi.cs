using OutSystems.ExternalLibraries.SDK;
using System.Text;

namespace AGV.ZXing.Structures
{

    [OSStructure(Description = "Defines a Wifi connection")]
    public struct Wifi
    {
        [OSStructureField(IsMandatory = true, Description = "SSID", Length = 50)]
        public string SSID { get; set; } = "";

        [OSStructureField(Description = "Password", Length = 50)]
        public string Password { get; set; } = "";

        [OSStructureField(IsMandatory = true, Description = "Type of authentication. Possible values: WEP, WPA, WPA2-EAP, nopass.", Length = 10)]
        public string Authentication { get; set; } = "";

        [OSStructureField(Description = "Is hidden")]
        public bool IsHidden { get; set; } = false;

        [OSStructureField(Description = "EAP method, like TTLS or PWD (WPA2-EAP only)", Length = 10)]
        public string EapMethod { get; set; } = "";

        [OSStructureField(Description = "Anonymous identity (WPA2-EAP only)", Length = 50)]
        public string AnonymousIdentity { get; set; } = "";

        [OSStructureField(Description = "Identity (WPA2-EAP only)", Length = 50)]
        public string Identity { get; set; } = "";

        [OSStructureField(Description = "Phase 2 method like MSCHAPV2 (WPA2-EAP only)", Length = 10)]
        public string Phase2Method { get; set; } = "";

        public Wifi(string ssid, string password, string authentication, bool isHidden, string eapMethod, string anonymousIdentity, string identity, string phase2Method) : this()
        {
            SSID = ssid;
            this.Password = password;
            this.Authentication = authentication;
            this.IsHidden = isHidden;
            this.EapMethod = eapMethod;
            this.AnonymousIdentity = anonymousIdentity;
            this.Identity = identity;
            this.Phase2Method = phase2Method;
        }

        public Wifi(Wifi w) : this()
        {
            SSID = w.SSID;
            Password = w.Password;
            Authentication = w.Authentication;
            IsHidden = w.IsHidden;
            EapMethod = w.EapMethod;
            AnonymousIdentity = w.AnonymousIdentity;
            Identity = w.Identity;
            Phase2Method = w.Phase2Method;
        }

        public override string ToString()
        {
            var s = new StringBuilder();
            s.Append($"WIFI:S:{SSID.EncodeQRCode()};T:{Authentication};");

            if (Password != null && Password != "")
                s.Append($"P:{Password.EncodeQRCode()};");

            s.Append($"H:{IsHidden};");

            if (Authentication == "WPA2-EAP")
                s.Append($"E:{EapMethod};A:{AnonymousIdentity};I:{Identity};PH2:{Phase2Method};");
            return s.Append(";").ToString();
        }
    }
}