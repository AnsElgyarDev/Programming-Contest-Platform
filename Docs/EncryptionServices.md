# AES Encryption & Decryption Flowcharts

## 1. Encryption Pipeline

### Visual Diagram
```mermaid
graph TD
    A["plainText: 01012345678"] -->|sw.Write| B["StreamWriter: Converts text to UTF-8 Bytes"]
    B --> C["CryptoStream: Encrypts Bytes using Key + IV"]
    C --> D["MemoryStream: Stores 16 Bytes IV + Encrypted Bytes"]
    D --> E["Output: Base64 Encrypted String"]
```

### Text Flowchart
<!-- prettier-ignore -->
```text
[plainText: "01012345678"]
       │
       ▼ (sw.Write)
[StreamWriter: Converts text to UTF-8 Bytes]
       │
       ▼
[CryptoStream: Encrypts Bytes using Key + IV]
       │
       ▼
[MemoryStream: Stores (16 Bytes IV) + (Encrypted Bytes)]
       │
       ▼
[Output: Base64 Encrypted String]
```

---

## 2. Decryption Pipeline

### Visual Diagram
```mermaid
graph TD
    A["cipherText: Base64 String"] -->|Convert.FromBase64String| B["fullCipher: Byte Array"]
    B --> C1["Slice First 16 Bytes -> Extract IV to populate aes.IV"]
    B -->|Remaining Encrypted Payload Only| C2["MemoryStream: Reads Encrypted Bytes"]
    C2 --> D["CryptoStream Read Mode: Decrypts Bytes using Key + IV"]
    D --> E["StreamReader: Converts Decrypted Bytes to String"]
    E --> F["plainText: 01012345678"]
```

### Text Flowchart
<!-- prettier-ignore -->
```text
[cipherText: Base64 String]
       │
       ▼ (Convert.FromBase64String)
[fullCipher: Byte Array]
       │
       ├──────────────► [Slice First 16 Bytes] ──► (Extract IV to populate aes.IV)
       │
       ▼ [Remaining Encrypted Payload Only]
[MemoryStream: Reads Encrypted Bytes]
       │
       ▼
[CryptoStream (Read Mode): Decrypts Bytes using Key + IV]
       │
       ▼
[StreamReader: Converts Decrypted Bytes to String]
       │
       ▼
[plainText: "01012345678"]
```
