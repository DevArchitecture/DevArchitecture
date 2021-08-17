using System.Text.RegularExpressions;

namespace Business.Helpers
{
    public static class ValidationExtensions
    {
        public static bool IsCidValid(this string citizenId)
        {
            if (citizenId != null && citizenId.Length == 11)
            {
                return IsCidValid(long.Parse(citizenId));
            }
            else
            {
                return false;
            }
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
            var identificationNumber = citizenId;
            long atcno, btcno;
            long c1, c2, c3, c4, c5, c6, c7, c8, c9, q1, q2;
            atcno = identificationNumber / 100;
            btcno = identificationNumber / 100;
            c1 = atcno % 10;
            atcno = atcno / 10;
            c2 = atcno % 10;
            atcno = atcno / 10;
            c3 = atcno % 10;
            atcno = atcno / 10;
            c4 = atcno % 10;
            atcno = atcno / 10;
            c5 = atcno % 10;
            atcno = atcno / 10;
            c6 = atcno % 10;
            atcno = atcno / 10;
            c7 = atcno % 10;
            atcno = atcno / 10;
            c8 = atcno % 10;
            atcno = atcno / 10;
            c9 = atcno % 10;
            atcno = atcno / 10;
            q1 = (10 - ((((c1 + c3 + c5 + c7 + c9) * 3) + (c2 + c4 + c6 + c8)) % 10)) % 10;
            q2 = (10 - (((((c2 + c4 + c6 + c8) + q1) * 3) + (c1 + c3 + c5 + c7 + c9)) % 10)) % 10;
            var returnValue = q1 * 10 + q2;

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