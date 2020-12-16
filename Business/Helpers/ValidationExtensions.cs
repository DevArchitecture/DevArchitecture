using System.Text.RegularExpressions;

namespace Business.Helpers
{
    public static class ValidationExtensions
    {
        public static bool IsCidValid(this string citizenId)
        {
            if (citizenId != null && citizenId.Length == 11)
                return IsCidValid(long.Parse(citizenId));
            else
                return false;
        }

        public static long AddChecksumToCid(long cidWithoutChecksum)
        {
            var checksum = ComputeCidChecksum(cidWithoutChecksum);
            return (cidWithoutChecksum / 100) * 100 + checksum;
        }

        public static bool IsCidValid(this long citizenId)
        {
            long TcNo = citizenId;
            var ATCNO = TcNo / 100;
            var checksum = ComputeCidChecksum(citizenId);
            var returnValue = (ATCNO * 100 + checksum) == TcNo;

            return returnValue;
        }

        public static long ComputeCidChecksum(this long citizenId)
        {
            long TcNo = citizenId;
            long ATCNO, BTCNO;
            long C1, C2, C3, C4, C5, C6, C7, C8, C9, Q1, Q2;
            ATCNO = TcNo / 100; BTCNO = TcNo / 100; C1 = ATCNO % 10; ATCNO = ATCNO / 10; C2 = ATCNO % 10; ATCNO = ATCNO / 10;
            C3 = ATCNO % 10; ATCNO = ATCNO / 10; C4 = ATCNO % 10; ATCNO = ATCNO / 10; C5 = ATCNO % 10; ATCNO = ATCNO / 10;
            C6 = ATCNO % 10; ATCNO = ATCNO / 10; C7 = ATCNO % 10; ATCNO = ATCNO / 10; C8 = ATCNO % 10; ATCNO = ATCNO / 10;
            C9 = ATCNO % 10; ATCNO = ATCNO / 10;
            Q1 = ((10 - ((((C1 + C3 + C5 + C7 + C9) * 3) + (C2 + C4 + C6 + C8)) % 10)) % 10);
            Q2 = ((10 - (((((C2 + C4 + C6 + C8) + Q1) * 3) + (C1 + C3 + C5 + C7 + C9)) % 10)) % 10);
            var returnValue = Q1 * 10 + Q2;

            return returnValue;
        }

        public static bool IsPhoneValid(this string mobilePhone)
        {
            if (string.IsNullOrWhiteSpace(mobilePhone))
                return false;
            else
            {
                mobilePhone = Regex.Replace(mobilePhone, "[^0-9]", "");
                return mobilePhone.StartsWith("05") && mobilePhone.Length == 11;
            }
        }
    }
}
