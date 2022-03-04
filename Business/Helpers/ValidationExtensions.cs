using System.Text.RegularExpressions;

namespace Business.Helpers
{
    public static class ValidationExtensions
    {
        public static bool IsCidValid(this string citizenId)
        {
            return citizenId is { Length: 11 } && IsCidValid(long.Parse(citizenId));
        }

        public static long AddChecksumToCid(long cidWithoutChecksum)
        {
            var checksum = ComputeCidChecksum(cidWithoutChecksum);
            return (cidWithoutChecksum / 100) * 100 + checksum;
        }

        public static bool IsCidValid(this long citizenId)
        {
            var identificationNumber = citizenId;
            var atcno = identificationNumber / 100;
            var checksum = ComputeCidChecksum(citizenId);
            var returnValue = (atcno * 100 + checksum) == identificationNumber;

            return returnValue;
        }

        public static long ComputeCidChecksum(this long citizenId)
        {
            var atcno = citizenId / 100;

            long CalculateCVariable()
            {
                long c = atcno % 10;
                atcno /= 10;
                return c;
            }

            long c1 = CalculateCVariable();

            long c2 = CalculateCVariable();

            long c3 = CalculateCVariable();

            long c4 = CalculateCVariable();

            long c5 = CalculateCVariable();

            long c6 = CalculateCVariable();

            long c7 = CalculateCVariable();

            long c8 = CalculateCVariable();

            long c9 = CalculateCVariable();

            long q1 = (10 - ((((c1 + c3 + c5 + c7 + c9) * 3) + (c2 + c4 + c6 + c8)) % 10)) % 10;
            long q2 = (10 - (((((c2 + c4 + c6 + c8) + q1) * 3) + (c1 + c3 + c5 + c7 + c9)) % 10)) % 10;
            long returnValue = q1 * 10 + q2;

            return returnValue;
        }

        public static bool IsPhoneValid(this string mobilePhone)
        {
            if (string.IsNullOrWhiteSpace(mobilePhone))
            {
                return false;
            }

            mobilePhone = Regex.Replace(mobilePhone, "[^0-9]", string.Empty);
            return mobilePhone.StartsWith("05") && mobilePhone.Length == 11;
        }
    }
}