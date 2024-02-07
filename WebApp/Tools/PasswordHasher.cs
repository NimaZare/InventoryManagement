namespace Tools;

public class PasswordHasher : IPasswordHasher
{
    private const int _saltSize = 128 / 8;
    private const int _keySize = 256 / 8;
    private const int _iterations = 10000;
    private const char _delimiter = ';';
    private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;

    /// <summary>
    /// ساخت هش برای پسورد کاربران
    /// </summary>
    /// <param name="password">پسورد کاربر</param>
    /// <returns>رشته هش شده نهایی</returns>
    public string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(_saltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, _hashAlgorithmName, _keySize);

        return string.Join(_delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }

    /// <summary>
    /// بررسی صحت دو هش  پسورد طبق الگوریتم
    /// </summary>
    /// <param name="passwordHash">پسورد از قبل هش شده در دیتابیس</param>
    /// <param name="inputPassword">پسورد وارد شده جدید</param>
    /// <returns>مشخص می شود که آیا این پسورد ورودی همان پسورد قبلی است یا خیر </returns>
    public bool Verify(string passwordHash, string inputPassword)
    {
        var elements = passwordHash.Split(_delimiter);
        var salt = Convert.FromBase64String(elements[0]);
        var hash = Convert.FromBase64String(elements[1]);

        var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, _iterations, _hashAlgorithmName, _keySize);

        return CryptographicOperations.FixedTimeEquals(hash, hashInput);
    }
}
