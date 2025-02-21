using OutSystems.ExternalLibraries.SDK;
using System.Text;

namespace AGV.ZXing.Structures
{

    [OSStructure(Description = "Defines a Wifi connection")]
    public struct Wifi
    {
        [OSStructureField(IsMandatory = true, Description = "SSID", Length = 50)]
        public string SSID;

        [OSStructureField(Description = "Password", Length = 50)]
        public string password;

        [OSStructureField(IsMandatory = true, Description = "Type of authentication. Possible values: WEP, WPA, WPA2-EAP, nopass.", Length = 10)]
        public string authentication;

        [OSStructureField(Description = "Is hidden")]
        public bool isHidden;

        [OSStructureField(Description = "EAP method, like TTLS or PWD (WPA2-EAP only)", Length = 10)]
        public string eapMethod;

        [OSStructureField(Description = "Anonymous identity (WPA2-EAP only)", Length = 50)]
        public string anonymousIdentity;

        [OSStructureField(Description = "Identity (WPA2-EAP only)", Length = 50)]
        public string identity;

        [OSStructureField(Description = "Phase 2 method like MSCHAPV2 (WPA2-EAP only)", Length = 10)]
        public string phase2Method;

        public Wifi(string ssid, string password, string authentication, bool isHidden, string eapMethod, string anonymousIdentity, string identity, string phase2Method) : this()
        {
            SSID = ssid;
            this.password = password;
            this.authentication = authentication;
            this.isHidden = isHidden;
            this.eapMethod = eapMethod;
            this.anonymousIdentity = anonymousIdentity;
            this.identity = identity;
            this.phase2Method = phase2Method;
        }

        public Wifi(Wifi w) : this()
        {
            SSID = w.SSID;
            password = w.password;
            authentication = w.authentication;
            isHidden = w.isHidden;
            eapMethod = w.eapMethod;
            anonymousIdentity = w.anonymousIdentity;
            identity = w.identity;
            phase2Method = w.phase2Method;
        }

        public override string ToString()
        {
            var s = new StringBuilder();
            s.Append($"WIFI:S:{SSID.EncodeQRCode()};T:{authentication};");

            if (password != null && password != "")
                s.Append($"P:{password.EncodeQRCode()};");

            s.Append($"H:{isHidden};");

            if (authentication == "WPA2-EAP")
                s.Append($"E:{eapMethod};A:{anonymousIdentity};I:{identity};PH2:{phase2Method};");
            return s.Append(";").ToString();
        }
    }
}