﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Ewk.BandWebsite.Common.Cryptography
{
    /// <summary>
    /// This class uses a symmetric key algorithm (Rijndael/AES) to encrypt and
    /// decrypt data. As long as it is initialized with the same constructor
    /// parameters, the class will use the same key. Before performing encryption,
    /// the class can prepend random bytes to plain text and generate different
    /// encrypted values from the same plain text, encryption key, initialization
    /// vector, and other parameters. This class is thread-safe.
    /// </summary>
    /// <remarks>
    /// Be careful when performing encryption and decryption. There is a bug
    /// ("feature"?) in .NET Framework, which causes corruption of encryptor/
    /// decryptor if a cryptographic exception occurs during encryption/
    /// decryption operation. To correct the problem, re-initialize the class
    /// instance when a cryptographic exception occurs.
    /// </remarks>
    public class RijndaelEnhanced
    {
        #region Private members

        private readonly object _lock = new object();

        // If key size is not specified, use the longest 256-bit key.
        private const int DefaultKeySize = 256;

        // Do not allow salt to be longer than 255 bytes, because we have only
        // 1 byte to store its length. 
        private const int MaxAllowedSaltLen = 255;

        // Do not allow salt to be smaller than 4 bytes, because we use the first
        // 4 bytes of salt to store its length. 
        private const int MinAllowedSaltLen = 4;

        // Random salt value will be between 4 and 8 bytes long.
        private const int DefaultMinSaltLen = MinAllowedSaltLen;
        private const int DefaultMaxSaltLen = 8;

        // Use these members to save min and max salt lengths.
        private readonly int _minSaltLen = -1;
        private readonly int _maxSaltLen = -1;

        // These members will be used to perform encryption and decryption.
        private readonly ICryptoTransform _encryptor;
        private readonly ICryptoTransform _decryptor;

        #endregion

        #region Constructors

        /// <summary>
        /// Use this constructor if you are planning to perform encryption/
        /// decryption with 256-bit key, derived using 1 password iteration,
        /// hashing without salt, no initialization vector, electronic codebook
        /// (ECB) mode, SHA-1 hashing algorithm, and 4-to-8 byte long salt.
        /// </summary>
        /// <param name="passphrase">
        /// Passphrase from which a pseudo-random password will be derived.
        /// The derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that the
        /// passphrase is an ASCII string. Passphrase value must be kept in
        /// secret.
        /// </param>
        /// <remarks>
        /// This constructor is not recommended because it does not use
        /// initialization vector and uses the ECB cipher mode, which is less
        /// secure than the CBC mode.
        /// </remarks>
        public RijndaelEnhanced(string passphrase)
            : this(passphrase, null)
        {
        }

        /// <summary>
        /// Use this constructor if you are planning to perform encryption/
        /// decryption with 256-bit key, derived using 1 password iteration,
        /// hashing without salt, cipher block chaining (CBC) mode, SHA-1
        /// hashing algorithm, and 4-to-8 byte long salt.
        /// </summary>
        /// <param name="passphrase">
        /// Passphrase from which a pseudo-random password will be derived.
        /// The derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that the
        /// passphrase is an ASCII string. Passphrase value must be kept in
        /// secret.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long. IV value does not have to be kept
        /// in secret.
        /// </param>
        public RijndaelEnhanced(string passphrase,
                                string initVector)
            : this(passphrase, initVector, -1)
        {
        }

        /// <summary>
        /// Use this constructor if you are planning to perform encryption/
        /// decryption with 256-bit key, derived using 1 password iteration,
        /// hashing without salt, cipher block chaining (CBC) mode, SHA-1 
        /// hashing algorithm, and 0-to-8 byte long salt.
        /// </summary>
        /// <param name="passphrase">
        /// Passphrase from which a pseudo-random password will be derived.
        /// The derived password will be used to generate the encryption key
        /// Passphrase can be any string. In this example we assume that the
        /// passphrase is an ASCII string. Passphrase value must be kept in
        /// secret.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long. IV value does not have to be kept
        /// in secret.
        /// </param>
        /// <param name="minSaltLen">
        /// Min size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is less than 4, the default min value will be used (currently 4
        /// bytes).
        /// </param>
        public RijndaelEnhanced(string passphrase,
                                string initVector,
                                int minSaltLen)
            : this(passphrase, initVector, minSaltLen, -1)
        {
        }

        /// <summary>
        /// Use this constructor if you are planning to perform encryption/
        /// decryption with 256-bit key, derived using 1 password iteration,
        /// hashing without salt, cipher block chaining (CBC) mode, SHA-1
        /// hashing algorithm. Use the minSaltLen and maxSaltLen parameters to
        /// specify the size of randomly generated salt.
        /// </summary>
        /// <param name="passphrase">
        /// Passphrase from which a pseudo-random password will be derived.
        /// The derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that the
        /// passphrase is an ASCII string. Passphrase value must be kept in
        /// secret.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long. IV value does not have to be kept
        /// in secret.
        /// </param>
        /// <param name="minSaltLen">
        /// Min size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is less than 4, the default min value will be used (currently 4
        /// bytes).
        /// </param>
        /// <param name="maxSaltLen">
        /// Max size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is negative or greater than 255, the default max value will be
        /// used (currently 8 bytes). If max value is 0 (zero) or if it is smaller
        /// than the specified min value (which can be adjusted to default value),
        /// salt will not be used and plain text value will be encrypted as is.
        /// In this case, salt will not be processed during decryption either.
        /// </param>
        public RijndaelEnhanced(string passphrase,
                                string initVector,
                                int minSaltLen,
                                int maxSaltLen)
            : this(passphrase, initVector, minSaltLen, maxSaltLen, -1)
        {
        }

        /// <summary>
        /// Use this constructor if you are planning to perform encryption/
        /// decryption using the key derived from 1 password iteration,
        /// hashing without salt, cipher block chaining (CBC) mode, and
        /// SHA-1 hashing algorithm.
        /// </summary>
        /// <param name="passphrase">
        /// Passphrase from which a pseudo-random password will be derived.
        /// The derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that the
        /// passphrase is an ASCII string. Passphrase value must be kept in
        /// secret.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long. IV value does not have to be kept
        /// in secret.
        /// </param>
        /// <param name="minSaltLen">
        /// Min size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is less than 4, the default min value will be used (currently 4
        /// bytes).
        /// </param>
        /// <param name="maxSaltLen">
        /// Max size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is negative or greater than 255, the default max value will be 
        /// used (currently 8 bytes). If max value is 0 (zero) or if it is smaller
        /// than the specified min value (which can be adjusted to default value),
        /// salt will not be used and plain text value will be encrypted as is.
        /// In this case, salt will not be processed during decryption either.
        /// </param>
        /// <param name="keySize">
        /// Size of symmetric key (in bits): 128, 192, or 256.
        /// </param>
        public RijndaelEnhanced(string passphrase,
                                string initVector,
                                int minSaltLen,
                                int maxSaltLen,
                                int keySize)
            : this(passphrase, initVector, minSaltLen, maxSaltLen, keySize, null)
        {
        }

        /// <summary>
        /// Use this constructor if you are planning to perform encryption/
        /// decryption using the key derived from 1 password iteration, and
        /// cipher block chaining (CBC) mode.
        /// </summary>
        /// <param name="passphrase">
        /// Passphrase from which a pseudo-random password will be derived.
        /// The derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that the
        /// passphrase is an ASCII string. Passphrase value must be kept in
        /// secret.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long. IV value does not have to be kept
        /// in secret.
        /// </param>
        /// <param name="minSaltLen">
        /// Min size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is less than 4, the default min value will be used (currently 4
        /// bytes).
        /// </param>
        /// <param name="maxSaltLen">
        /// Max size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is negative or greater than 255, the default max value will be
        /// used (currently 8 bytes). If max value is 0 (zero) or if it is smaller
        /// than the specified min value (which can be adjusted to default value),
        /// salt will not be used and plain text value will be encrypted as is.
        /// In this case, salt will not be processed during decryption either.
        /// </param>
        /// <param name="keySize">
        /// Size of symmetric key (in bits): 128, 192, or 256.
        /// </param>
        /// <param name="saltValue">
        /// Salt value used for password hashing during key generation. This is
        /// not the same as the salt we will use during encryption. This parameter
        /// can be any string.
        /// </param>
        public RijndaelEnhanced(string passphrase,
                                string initVector,
                                int minSaltLen,
                                int maxSaltLen,
                                int keySize,
                                string saltValue)
            : this(passphrase, initVector, minSaltLen, maxSaltLen, keySize, saltValue, 1)
        {
        }

        /// <summary>
        /// Use this constructor if you are planning to perform encryption/
        /// decryption with the key derived from the explicitly specified
        /// parameters.
        /// </summary>
        /// <param name="passphrase">
        /// Passphrase from which a pseudo-random password will be derived.
        /// The derived password will be used to generate the encryption key
        /// Passphrase can be any string. In this example we assume that the
        /// passphrase is an ASCII string. Passphrase value must be kept in
        /// secret.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long. IV value does not have to be kept
        /// in secret.
        /// </param>
        /// <param name="minSaltLen">
        /// Min size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is less than 4, the default min value will be used (currently 4
        /// bytes).
        /// </param>
        /// <param name="maxSaltLen">
        /// Max size (in bytes) of randomly generated salt which will be added at
        /// the beginning of plain text before encryption is performed. When this
        /// value is negative or greater than 255, the default max value will be
        /// used (currently 8 bytes). If max value is 0 (zero) or if it is smaller
        /// than the specified min value (which can be adjusted to default value),
        /// salt will not be used and plain text value will be encrypted as is.
        /// In this case, salt will not be processed during decryption either.
        /// </param>
        /// <param name="keySize">
        /// Size of symmetric key (in bits): 128, 192, or 256.
        /// </param>
        /// <param name="saltValue">
        /// Salt value used for password hashing during key generation. This is
        /// not the same as the salt we will use during encryption. This parameter
        /// can be any string.
        /// </param>
        /// <param name="passwordIterations">
        /// Number of iterations used to hash password. More iterations are
        /// considered more secure but may take longer.
        /// </param>
        public RijndaelEnhanced(string passphrase,
                                string initVector,
                                int minSaltLen,
                                int maxSaltLen,
                                int keySize,
                                string saltValue,
                                int passwordIterations)
        {
            if (string.IsNullOrEmpty(passphrase)) throw new ArgumentNullException("passphrase");

            // Save min salt length; set it to default if invalid value is passed.
            _minSaltLen = minSaltLen < MinAllowedSaltLen
                              ? DefaultMinSaltLen
                              : minSaltLen;

            // Save max salt length; set it to default if invalid value is passed.
            _maxSaltLen = (maxSaltLen < 0 || maxSaltLen > MaxAllowedSaltLen)
                              ? DefaultMaxSaltLen
                              : maxSaltLen;

            // Set the size of cryptographic key.
            if (keySize <= 0)
            {
                keySize = DefaultKeySize;
            }

            // Initialization vector converted to a byte array.
            // Get bytes of initialization vector.
            var initVectorBytes = initVector == null
                                      ? new byte[0]
                                      : Encoding.ASCII.GetBytes(initVector);

            // Salt used for password hashing (to generate the key, not during encryption) converted to a byte array.
            // Get bytes of salt (used in hashing).
            var saltValueBytes = saltValue == null
                                     ? new byte[0]
                                     : Encoding.ASCII.GetBytes(saltValue);

            // Generate password, which will be used to derive the key.
            using (var password = new Rfc2898DeriveBytes(passphrase,
                                                         saltValueBytes,
                                                         passwordIterations))
            {
                // Convert key to a byte array adjusting the size from bits to bytes.
                var keyBytes = password.GetBytes(keySize / 8);

                // Initialize Rijndael key object.
                using (var symmetricKey = new RijndaelManaged
                {
                    // If we do not have initialization vector, we cannot use the CBC mode.
                    // The only alternative is the ECB mode (which is not as good).
                    Mode = initVectorBytes.Length == 0
                               ? CipherMode.ECB
                               : CipherMode.CBC
                })
                {
                    // Create encryptor and decryptor, which we will use for cryptographic
                    // operations.
                    _encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                    _decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                }
            }
        }

        #endregion

        #region Encryption routines

        /// <summary>
        /// Encrypts a string value generating a base64-encoded string.
        /// </summary>
        /// <param name="value">
        /// Plain text string to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a base64-encoded string.
        /// </returns>
        public string Encrypt(string value)
        {
            if (value == null) throw new ArgumentNullException("value");

            return Encrypt(Encoding.UTF8.GetBytes(value));
        }

        /// <summary>
        /// Encrypts a byte array generating a base64-encoded string.
        /// </summary>
        /// <param name="value">
        /// Plain text bytes to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a base64-encoded string.
        /// </returns>
        public string Encrypt(byte[] value)
        {
            if (value == null) throw new ArgumentNullException("value");

            return Convert.ToBase64String(EncryptToBytes(value));
        }

        /// <summary>
        /// Encrypts a string value generating a byte array of cipher text.
        /// </summary>
        /// <param name="value">
        /// Plain text string to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a byte array.
        /// </returns>
        public byte[] EncryptToBytes(string value)
        {
            if (value == null) throw new ArgumentNullException("value");

            return EncryptToBytes(Encoding.UTF8.GetBytes(value));
        }

        /// <summary>
        /// Encrypts a byte array generating a byte array of cipher text.
        /// </summary>
        /// <param name="value">
        /// Plain text bytes to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a byte array.
        /// </returns>
        public byte[] EncryptToBytes(byte[] value)
        {
            if (value == null) throw new ArgumentNullException("value");

            // Add salt at the beginning of the plain text bytes (if needed).
            var plainTextBytesWithSalt = AddSalt(value);

            lock (_lock)
            {
                // Encryption will be performed using memory stream.
                using (var memoryStream = new MemoryStream())
                // Let's make cryptographic operations thread-safe.
                // To perform encryption, we must use the Write mode.
                using (var cryptoStream = new CryptoStream(memoryStream,
                                                           _encryptor,
                                                           CryptoStreamMode.Write))
                {
                    // Start encrypting data.
                    cryptoStream.Write(plainTextBytesWithSalt,
                                       0,
                                       plainTextBytesWithSalt.Length);

                    // Finish the encryption operation.
                    cryptoStream.FlushFinalBlock();

                    // Move encrypted data from memory into a byte array.
                    var cipherTextBytes = memoryStream.ToArray();

                    // Close memory streams.
                    memoryStream.Close();
                    cryptoStream.Close();

                    // Return encrypted data.
                    return cipherTextBytes;
                }
            }
        }

        #endregion

        #region Decryption routines

        /// <summary>
        /// Decrypts a base64-encoded cipher text value generating a string result.
        /// </summary>
        /// <param name="encryptedValue">
        /// Base64-encoded cipher text string to be decrypted.
        /// </param>
        /// <returns>
        /// Decrypted string value.
        /// </returns>
        public string Decrypt(string encryptedValue)
        {
            if (encryptedValue == null) throw new ArgumentNullException("encryptedValue");

            return Decrypt(Convert.FromBase64String(encryptedValue));
        }

        /// <summary>
        /// Decrypts a byte array containing cipher text value and generates a
        /// string result.
        /// </summary>
        /// <param name="encryptedValue">
        /// Byte array containing encrypted data.
        /// </param>
        /// <returns>
        /// Decrypted string value.
        /// </returns>
        public string Decrypt(byte[] encryptedValue)
        {
            if (encryptedValue == null) throw new ArgumentNullException("encryptedValue");

            return Encoding.UTF8.GetString(DecryptToBytes(encryptedValue));
        }

        /// <summary>
        /// Decrypts a base64-encoded cipher text value and generates a byte array
        /// of plain text data.
        /// </summary>
        /// <param name="encryptedValue">
        /// Base64-encoded cipher text string to be decrypted.
        /// </param>
        /// <returns>
        /// Byte array containing decrypted value.
        /// </returns>
        public byte[] DecryptToBytes(string encryptedValue)
        {
            if (encryptedValue == null) throw new ArgumentNullException("encryptedValue");

            return DecryptToBytes(Convert.FromBase64String(encryptedValue));
        }

        /// <summary>
        /// Decrypts a base64-encoded cipher text value and generates a byte array
        /// of plain text data.
        /// </summary>
        /// <param name="encryptedValue">
        /// Byte array containing encrypted data.
        /// </param>
        /// <returns>
        /// Byte array containing decrypted value.
        /// </returns>
        public byte[] DecryptToBytes(byte[] encryptedValue)
        {
            if (encryptedValue == null) throw new ArgumentNullException("encryptedValue");

            int decryptedByteCount;
            var saltLen = 0;
            // Since we do not know how big decrypted value will be, use the same
            // size as cipher text. Cipher text is always longer than plain text
            // (in block cipher encryption), so we will just use the number of
            // decrypted data byte after we know how big it is.
            var decryptedBytes = new byte[encryptedValue.Length];

            // Let's make cryptographic operations thread-safe.
            lock (_lock)
            {
                using (var memoryStream = new MemoryStream(encryptedValue))
                // To perform decryption, we must use the Read mode.
                using (var cryptoStream = new CryptoStream(memoryStream,
                                                           _decryptor,
                                                           CryptoStreamMode.Read))
                {

                    // Decrypting data and get the count of plain text bytes.
                    decryptedByteCount = cryptoStream.Read(decryptedBytes,
                                                           0,
                                                           decryptedBytes.Length);
                    // Release memory.
                    memoryStream.Close();
                    cryptoStream.Close();
                }
            }

            // If we are using salt, get its length from the first 4 bytes of plain
            // text data.
            if (_maxSaltLen > 0 && _maxSaltLen >= _minSaltLen)
            {
                saltLen = (decryptedBytes[0] & 0x03) |
                          (decryptedBytes[1] & 0x0c) |
                          (decryptedBytes[2] & 0x30) |
                          (decryptedBytes[3] & 0xc0);
            }

            // Allocate the byte array to hold the original plain text (without salt).
            var plainTextBytes = new byte[decryptedByteCount - saltLen];

            // Copy original plain text discarding the salt value if needed.
            Array.Copy(decryptedBytes, saltLen, plainTextBytes,
                       0, decryptedByteCount - saltLen);

            // Return original plain text value.
            return plainTextBytes;
        }

        #endregion

        #region Helper functions

        /// <summary>
        /// Adds an array of randomly generated bytes at the beginning of the
        /// array holding original plain text value.
        /// </summary>
        /// <param name="valueBytes">
        /// Byte array containing original plain text value.
        /// </param>
        /// <returns>
        /// Either original array of plain text bytes (if salt is not used) or a
        /// modified array containing a randomly generated salt added at the 
        /// beginning of the plain text bytes. 
        /// </returns>
        private byte[] AddSalt(byte[] valueBytes)
        {
            // The max salt value of 0 (zero) indicates that we should not use 
            // salt. Also do not use salt if the max salt value is smaller than
            // the min value.
            if (_maxSaltLen == 0 || _maxSaltLen < _minSaltLen)
            {
                return valueBytes;
            }

            // Generate the salt.
            var saltBytes = GenerateSalt();

            // Allocate array which will hold salt and plain text bytes.
            var plainTextBytesWithSalt = new byte[valueBytes.Length +
                                                  saltBytes.Length];
            // First, copy salt bytes.
            Array.Copy(saltBytes, plainTextBytesWithSalt, saltBytes.Length);

            // Append plain text bytes to the salt value.
            Array.Copy(valueBytes,
                       0,
                       plainTextBytesWithSalt,
                       saltBytes.Length,
                       valueBytes.Length);

            return plainTextBytesWithSalt;
        }

        /// <summary>
        /// Generates an array holding cryptographically strong bytes.
        /// </summary>
        /// <returns>
        /// Array of randomly generated bytes.
        /// </returns>
        /// <remarks>
        /// Salt size will be defined at random or exactly as specified by the
        /// minSlatLen and maxSaltLen parameters passed to the object constructor.
        /// The first four bytes of the salt array will contain the salt length
        /// split into four two-bit pieces.
        /// </remarks>
        private byte[] GenerateSalt()
        {
            // If min and max salt values are the same, it should not be random.
            var saltLen = _minSaltLen == _maxSaltLen
                              ? _minSaltLen
                              : GenerateRandomNumber(_minSaltLen, _maxSaltLen);

            // Allocate byte array to hold our salt.
            var salt = new byte[saltLen];

            // Populate salt with cryptographically strong bytes.
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(salt);
            }

            // Split salt length (always one byte) into four two-bit pieces and
            // store these pieces in the first four bytes of the salt array.
            salt[0] = (byte)((salt[0] & 0xfc) | (saltLen & 0x03));
            salt[1] = (byte)((salt[1] & 0xf3) | (saltLen & 0x0c));
            salt[2] = (byte)((salt[2] & 0xcf) | (saltLen & 0x30));
            salt[3] = (byte)((salt[3] & 0x3f) | (saltLen & 0xc0));

            return salt;
        }

        /// <summary>
        /// Generates random integer.
        /// </summary>
        /// <param name="minValue">
        /// Min value (inclusive).
        /// </param>
        /// <param name="maxValue">
        /// Max value (inclusive).
        /// </param>
        /// <returns>
        /// Random integer value between the min and max values (inclusive).
        /// </returns>
        /// <remarks>
        /// This methods overcomes the limitations of .NET Framework's Random
        /// class, which - when initialized multiple times within a very short
        /// period of time - can generate the same "random" number.
        /// </remarks>
        private static int GenerateRandomNumber(int minValue, int maxValue)
        {
            // We will make up an integer seed from 4 bytes of this array.
            var randomBytes = new byte[4];

            // Generate 4 random bytes.
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            // Convert four random bytes into a positive integer value.
            var seed = ((randomBytes[0] & 0x7f) << 24) |
                       (randomBytes[1] << 16) |
                       (randomBytes[2] << 8) |
                       (randomBytes[3]);

            // Now, this looks more like real randomization.
            var random = new Random(seed);

            // Calculate a random number.
            return random.Next(minValue, maxValue + 1);
        }

        #endregion
    }
}