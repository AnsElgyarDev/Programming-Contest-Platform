# AES Encryption & Decryption Flowcharts

## 1. Encryption Pipeline

### Mermaid Diagram

```mermaid
graph TD
    A["plainText: '01012345678'"] -->|sw.Write| B[StreamWriter: Converts string to UTF-8 Bytes]
    B --> C[CryptoStream: Encrypts Bytes using Key + IV]
    C --> D["MemoryStream: Stores (16 Bytes IV) + (Encrypted Bytes)"]
    D --> E["Output: Base64 Ciphertext String"]

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


graph TD
    A["cipherText: Base64 String"] -->|Convert.FromBase64String| B["fullCipher: Byte Array"]
    B --> C1["Extract First 16 Bytes -> Populate aes.IV"]
    B -->|Remaining Bytes Only| C2["MemoryStream: Reads Encrypted Payload"]
    C2 --> D["CryptoStream (Read Mode): Decrypts Payload using Key + IV"]
    D --> E["StreamReader: Converts Decrypted Bytes to String"]
    E --> F["plainText: '01012345678'"]

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
