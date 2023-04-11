using OutSystems.Model.ExternalLibraries.SDK;
using System.Collections.Generic;
using System.Text;

namespace AGV.ZXing.Structures {

    [OSStructure(Description = "Defines a Wifi connection", OriginalName = "Wifi")]
    public struct Wifi {
        [OSStructureField(IsMandatory = true, Description = "SSID", OriginalName = "SSID")]
        public string SSID;

        [OSStructureField(Description = "Password", OriginalName = "Password")]
        public string password;

        [OSStructureField(IsMandatory = true, Description = "Type of authentication. Possible values: WEP, WPA, WPA2-EAP, nopass.", OriginalName = "AuthenticationType")]
        public string authentication;

        [OSStructureField(Description = "Is hidden", OriginalName = "IsHidden")]
        public bool isHidden;

        [OSStructureField(Description = "EAP method, like TTLS or PWD (WPA2-EAP only)", OriginalName = "EAPMethod")]
        public string eapMethod;
        
        [OSStructureField(Description = "Anonymous identity (WPA2-EAP only)", OriginalName = "AnonymousIdentity")]
        public string anonymousIdentity;
        
        [OSStructureField(Description = "Identity (WPA2-EAP only)", OriginalName = "Identity")]
        public string identity;
        
        [OSStructureField(Description = "Phase 2 method like MSCHAPV2 (WPA2-EAP only)", OriginalName = "Phase2Method")]
        public string phase2Method;

        public Wifi(string ssid, string password, string authentication, bool isHidden, string eapMethod, string anonymousIdentity, string identity, string phase2Method):this() {
            this.SSID = ssid;
            this.password = password;
            this.authentication = authentication;
            this.isHidden = isHidden;
            this.eapMethod = eapMethod;
            this.anonymousIdentity = anonymousIdentity;
            this.identity = identity;
            this.phase2Method = phase2Method;
        }

        public Wifi(Wifi w):this(){
            this.SSID = w.SSID;
            this.password = w.password;
            this.authentication = w.authentication;
            this.isHidden = w.isHidden;
            this.eapMethod = w.eapMethod;
            this.anonymousIdentity = w.anonymousIdentity;
            this.identity = w.identity;
            this.phase2Method = w.phase2Method;
        }

        public override string ToString()
        {
            var s = new StringBuilder();
            s.Append($"WIFI:S:{ this.SSID.encodeQRCode() };T:{ this.authentication };");
            
            if (this.password != null && this.password != "")
                s.Append($"P:{ this.password.encodeQRCode() };");
            
            s.Append($"H:{ this.isHidden };");

            if (this.authentication == "WPA2-EAP")
                s.Append($"E:{ this.eapMethod };A:{ this.anonymousIdentity };I:{ this.identity };PH2:{ this.phase2Method };");
            return s.Append(";").ToString();
        }
    }
}