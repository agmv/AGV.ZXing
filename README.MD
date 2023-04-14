# AGV.ZXing

ZXingLib is an OutSystems library that provides actions that allow to encode or decode barcodes in different formats. It wraps the [ZXing library](https://github.com/zxing/zxing) (in this case the [.NET port from Michael Jahn](https://github.com/micjahn/ZXing.Net)).

The following barcodes are supported by the decoder: UPC-A, UPC-E, EAN-8, EAN-13, Code 39, Code 93, Code 128, ITF, Codabar, MSI, RSS-14 (all variants), QR Code, Data Matrix, Aztec and PDF-417. The encoder supports the following formats: UPC-A, EAN-8, EAN-13, Code 39, Code 128, ITF, Codabar, Plessey, MSI, QR Code, PDF-417, Aztec, Data Matrix

Additionally this library also provides accelerators to generate QR Codes encoding different types of content such as WiFi connection credentials, Calendar events, or Contact cards.

## Actions

### Decode

Scans barcode from a given image.

```csharp
Structures.Barcode? Decode(byte[] image, string? formatHint)
```
| Parameter | Description |
|-|-|
| image | Image containing the barcode. |
| formatHint | Optional parameter to indicate what type of barcode to scan the image for, e.g. `QR_CODE`. |

Return type [`Barcode`](#barcode).

<!--
### DecodeMulti

Scans barcode from a given image.
```csharp
IEnumerable<Structures.Barcode>? DecodeMulti(byte[] image, string? formatHint)
```
| Parameter | Description |
|-|-|
| image | Image containing the barcode. |
| formatHint | Optional parameter to indicate what type of barcode to scan the image for. E.g. `QR_CODE`. |

Return type [`IEnumerable<Barcode>`](#barcode).
-->

### Encode

Generates a barcode with the received input data.

```csharp
public byte[] Encode(string contents, string format, 
    int width, int height, int margin,
    bool pureBarcode, bool gS1Format, bool noPadding,
    string? encoding = null, string? ecl = null, int? qRCodeVersion = null,
    byte[]? overlayImage = null);
```

| Parameter | Description |
|-|-|
| contents | Contents to encode as a barcode. |
| format | Optional parameter to indicate what type of barcode to scan the image for. E.g. `QR_CODE`. |
| width | Width of the barcode image. |
| height | Height of the barcode image. |
| margin | Specifies margin, in pixels, to use when generating the barcode. The meaning can vary by format; for example it controls margin before and after the barcode horizontally for most 1D formats. |
| pureBarcode | Don't put the content string into the output image. |
| gS1Format | Specifies whether the data should be encoded to the GS1 standard; FNC1 character is added in front of the data. |
| noPadding | Don't add a white area around the generated barcode if the requested size is larger than then barcode. |
| encoding | Specifies what character encoding to use where applicable, e.g. UTF-8, ISO-8859-1 |
| ecl | Specifies what degree of error correction to use, for example in QR Codes. Type depends on the encoder.<br/>For example for QR codes it can be L (~7% correction), M (~15% correction), Q (~20% correction), and H (~30% correction).<br/>For Aztec it is integer representing the minimal percentage of error correction words. Note: an Aztec symbol should have a minimum of 25% EC words.<br/>For PDF417 it is integer between 0 and 8. |
| qRCodeVersion | Specifies the exact version of QR code to be encoded. |
| overlayImage | Overlay image for QR Code. |

Return type `byte[]`.

### EncodeCalendarEvent

Generates a barcode with the received calendar event data.

```csharp
public byte[] EncodeCalendarEvent(CalendarEvent calendarEvent, int size, byte[]? overlayImage = null);
```

| Parameter | Description |
|-|-|
| calendarEvent | Calendar event to encode as a barcode of type [`CalendarEvent`](#calendarevent). |
| size | Width of the barcode image. |
| overlayImage | Overlay image for QR Code. |

Return type `byte[]`.

### EncodeContact

Generates a barcode with the received contact data.

```csharp
public byte[] EncodeContact(Contact contact, bool isMeCard, int size, byte[]? overlayImage = null);
```

| Parameter | Description |
|-|-|
| contact | Contact to encode as a barcode of type [`Contact`](#contact). |
| isMeCard | Indicates if it uses MECARD format instead of VCARD format. |
| size | Width of the barcode image. |
| overlayImage | Overlay image for QR Code. |

Return type `byte[]`.

### EncodeEmail

Generates a barcode with the received email address.

```csharp
public byte[] EncodeEmail(string email, int size, byte[]? overlayImage = null);
```

| Parameter | Description |
|-|-|
| email | Email to encode. |
| size | Width of the barcode image. |
| overlayImage | Overlay image for QR Code. |

Return type `byte[]`.

### EncodeLocation

Generates a barcode with the received geo location.

```csharp
public byte[] EncodeLocation(string latitude, string longitude, int size, byte[]? overlayImage = null);
```

| Parameter | Description |
|-|-|
| latitude | Latitude. |
| longitude | Longitude. |
| size | Width of the barcode image. |
| overlayImage | Overlay image for QR Code. |

Return type `byte[]`.

### EncodePhoneNumber

Generates a barcode with the received phone number.

```csharp
public byte[] EncodePhoneNumber(string phoneNumber, bool isFacetime, int size, byte[]? overlayImage = null);
```

| Parameter | Description |
|-|-|
| phoneNumber | Phone number to encode. |
| isFacetime | Indicates if it generates a facetime call. |
| size | Width of the barcode image. |
| overlayImage | Overlay image for QR Code. |

Return type `byte[]`.

### EncodeSMS

Generates a barcode with the received SMS content.

```csharp
public byte[] EncodeSMS(string phoneNumber, string message, int size, byte[]? overlayImage = null);
```

| Parameter | Description |
|-|-|
| phoneNumber | Phone number. |
| message | Message. |
| size | Width of the barcode image. |
| overlayImage | Overlay image for QR Code. |

Return type `byte[]`.


### EncodeWifi

Generates a barcode with the Wifi connection information.

```csharp
public byte[] EncodeWifi(Wifi wifi, int size, byte[]? overlayImage = null);
```

| Parameter | Description |
|-|-|
| wifi | Wifi data of type [`Wifi`](#wifi). |
| size | Width of the barcode image. |
| overlayImage | Overlay image for QR Code. |

Return type `byte[]`.


## Structures

### Barcode

`AGV.ZXing.Structures.Barcode`

Structure that holds decoded barcodes

| Attribute | Type | Description |
|-|-|-|
| value | `string` | Barcode decoded value |
| rawBytes | `byte[]` | Barcode raw bytes |
| format | `string` | Barcode format |
<!--| metadata | [`IEnumerable<Metadata>`](#metada)| Barcode metada |

### Metadata `AGV.ZXing.Structures.Metada`
| Attribute | Type | Description |
|-|-|-|
| key | `string` | Metadata key |
| value | `string` | Metadata value |
-->

### CalendarEvent

`AGV.ZXing.Structures.CalendarEvent`

Defines a calendar event to be shared as a QR code

| Attribute | Type | Mandatory | Length | Description |
|-|-|:-:|-:|-|
| title | `string` | :white_check_mark: | | Event title. |
| isAllDay | `bool` | :white_check_mark: | | Indicates if it is an all day event or not. |
| startDateTime | `DateTime` | :white_check_mark: | | Event start date. |
| endDateTime | `DateTime` | :white_check_mark: | | Event end date. |
| location | `string` |  | 100 | Event location. |
| description | `string` | | 2000 | Event description. |
| eventClass | `string` | | 20 | Event class, e.g. PUBLIC or PRIVATE. |
| organizer | `string` | | 50 | Event organizer. |
| priority | `int` | | 50 | Event priority. Value between 1 and 4 is Low priority, 5 is Medium priority, and between 6 and 9 is High priority. |
| showAsBusy | `bool` | | | Show as busy in calendar. |

### Contact

Defines a this to be shared as a QR code.

| Attribute | Type | Mandatory | Length | Description |
|-|-|:-:|-:|-|
| formatedName | `string` | :white_check_mark: | 100 | Formated name. |
| composedName | [`ComposedName`](#composedname) |  |  | Composed name. |
| organization | `string` |  | 50 | Organization. |
| title | `string` |  | 50 | Title. |
| homePhone | `string` |  |  | Home phone number. |
| workPhone | `string` |  |  | Work phone number. |
| mobilePhone | `string` |  |  | Mobile phone number. |
| email | `string` |  |  | Email. |
| address | `string` |  | 100 | Address. |
| website | `string` |  | 100 | Website. |
| notes | `string` |  | 2000 | Notes. |

### ComposedName

Defines a name by its components.


| Attribute | Type | Mandatory | Length | Description |
|-|-|:-:|-:|-|
| firstName | `string` |  | 50 | First name. |
| lastName | `string` |  | 50 | Last name. |
| middleNames | `string` |  | 100 | Middle names. |
| prefix | `string` |  | 20 | Name prefix. |
| sufix | `string` |  | 20 | Name sufix. |

### Wifi

Defines a Wifi connection.

| Attribute | Type | Mandatory | Length | Description |
|-|-|:-:|-:|-|
| SSID | `string` |  | 50 | SSID. |
| password | `string` |  | 50 | Password. |
| authentication | `string` |  | 10 | Type of authentication. Possible values: WEP, WPA, WPA2-EAP, nopass. |
| isHidden | `bool` |  |  | Is hidden. |
| eapMethod | `string` |  | 10 | EAP method, like TTLS or PWD (WPA2-EAP only). |
| anonymousIdentity | `string` |  | 50 | Anonymous identity (WPA2-EAP only). |
| identity | `string` |  | 50 | Identity (WPA2-EAP only). |
| phase2Method | `string` |  | 10 | Phase 2 method like MSCHAPV2 (WPA2-EAP only). |


# Thanks

Many thanks to the team of the [zxing project](https://github.com/zxing/zxing) for their great work and to Michael Jahn for creating the .NET port!