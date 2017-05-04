using System.IO;
using System.Text;

namespace ZopaQuote.Test.FileServiceTest
{
    public static class DataHelper
    {
        public static MemoryStream ToMemoryStream(this string data)
        {
            var byteArray = Encoding.ASCII.GetBytes(data);
            return new MemoryStream(byteArray);
        }

        public const string ThreeLinesOfValidDataOffFive =
            @"HeaderLine
invalid data,12,svbjk
First,0.02,1000
Second,0.05,4000
invalid data
Third,0.23,10000";

        public const string ThreeLinesOfValidData =
            @"HeaderLine
First,0.02,1000
Second,0.05,4000
Third,0.23,10000";

        public const string CompleteRubbishData =
            @"HeaderLine
First,1000
Second,0.05
Third,
dsfsdvdsv
dsfvsdfs sdf sdf sdfg
s gsf
 g
sdf g
sf
 sf
g 
sf 
ad fdas f
dfdsfdsf df df
";
    }
}