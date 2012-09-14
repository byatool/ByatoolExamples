using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Byatool.Shared
{
    public class RandomTool
    {
        #region Fields

        private const int DefaultStringLength = 10;

        private static readonly Lazy<Random> RandomGenerator = new Lazy<Random>(() => new Random());

        private static readonly Lazy<List<string>> FirstNameList =
            new Lazy<List<string>>(() => new List<string>
			    {
					"Bennet", "Charles","Christopher","Daniel", "Darby", "Dauid",
                    "Dauvid", "Dennis", "Doughan", "Edwarde", "Erasmus", "Frauncis",
                    "Gabriell", "Geffery", "George", "Harvye", "Haunce", "Henry",
                    "Hugh", "Humfrey", "Iames", "Ieremie", "Iohn", "Ioseph", "Marmaduke",
                    "Nicholas", "Philip", "Philppe", "Polyson", "Richard", "Rise", "Robert",
                    "Roger", "Rowland", "Russe", "Smolkin", "Steuen", "Syluester", "Thomas",
                    "Valentine", "Vincent", "Walter", "William" });

        private static readonly Lazy<IList<string>> LastNameList =
            new Lazy<IList<string>>(() => new List<string>
				{
					"Allyne", "Anthony", "Anwike", "Backhouse", "Barecombe", "Barnes", "Beale", 
                    "Beching", "Biscombe", "Blunt", "Bookener", "Borges", "Brocke", "Cage",
                    "Chappell", "Chaundeler", "Cheyne", "Chipping", "Churchman", "Clefs",
                    "Constable", "Courtenay", "Deane", "Eseuen", "Evans", "Farthowe", "Feuer",
                    "Foxe", "Gannes", "Garden", "Gilbert", "Glande", "Gostigo", "Griffyn", "Harris",
                    "Harrye", "Hesket", "Hulme", "Humfrey", "Ireland", "Kelle", "Ketcheman", "Kettell",
                    "Large", "Lasie", "Latham", "Linsey", "Lowde", "Lyne", "Man", "Marshall", "Mason",
                    "Mayne", "Michel", "Millard", "Myll", "Norris", "North", "Nugen",  "Parre","Philippes",
                    "Phillippes", "Pomarie", "Poore", "Potkin", "Randes", "Robyns", "Rogers",
                    "Rottenbury", "Salter", "Sare", "Seklemore", "Skeuelabs", "Skinner", "Smart",
                    "Snelling", "Stevenson", "Swabber", "Taylor", "Tenche", "Thomas", "Twyt",
                    "Walters", "Wasse", "White", "Whitton", "Williams", "Wisse", "Wright", "Yong"
			    });

        #endregion

        #region Methods

        public static bool CreateABoolean()
        {
            return (RandomGenerator.Value.NextDouble() > 0.5);
        }

        public static char CreateAChar()
        {
            return (char)('A' + RandomGenerator.Value.Next(0, 25));
        }

        public static decimal CreateACashAmount()
        {
            return (decimal)(CreateAnInt32(1, 1000) + CreateAnInt32(0, 99) / 100.0);
        }

        public static DateTime CreateADate()
        {
            return new DateTime(CreateAnInt32(1970, 2000), CreateAnInt32(1, 12), CreateAnInt32(1, 28));
        }

        public static decimal CreateADecimal()
        {
            return RandomGenerator.Value.Next();
        }

        public static String CreateAnEmail()
        {
            return CreateAString(5) + "@" + CreateAString(5) + ".com";
        }

        public static E CreateAnEnumeration<E>()
        {
            var enumerationToCheck = Activator.CreateInstance<E>();

            if (enumerationToCheck as Enum == null)
            {
                throw new InvalidOperationException();
            }

            var names = Enum.GetNames(typeof(E));

            if (names.Length > 0)
            {
                var indexToUse = CreateAnInt32(0, names.Length);
                enumerationToCheck = (E)Enum.Parse(typeof(E), names[indexToUse]);
            }

            return enumerationToCheck;
        }

        public static byte[] CreateAnImage()
        {
            Func<int, int, byte[]> createBlankImage = 
                (height, width) =>
                    {
                        byte[] bitmapData;

                        using (var memoryStream = new MemoryStream())
                        {
                            (new Bitmap(width, height)).Save(memoryStream, ImageFormat.Png);
                            bitmapData = memoryStream.ToArray();
                        }

                        return bitmapData;
                    };

            return createBlankImage(50, 50);
        }

        public static Int16 CreateAnInt16()
        {
            return (Int16)RandomGenerator.Value.Next(Int16.MinValue, Int16.MaxValue);
        }

        public static Int32 CreateAnInt32()
        {
            return RandomGenerator.Value.Next();
        }

        public static Int32 CreateAnInt32(Int32 minimum, Int32 maximum)
        {
            return RandomGenerator.Value.Next(minimum, maximum);
        }

        public static object CreateAnInt64()
        {
            return (Int64)RandomGenerator.Value.Next(Int32.MinValue, Int32.MaxValue);
        }

        public static Int32 CreateANegativeInt32()
        {
            return CreateAnInt32(-999999, -1);
        }

        public static string CreateAName()
        {
            var randomFirstNameIndex = CreateAnInt32(0, FirstNameList.Value.Count - 1);
            var randomLastNameIndex = CreateAnInt32(0, LastNameList.Value.Count - 1);

            return FirstNameList.Value[randomFirstNameIndex] + " " + LastNameList.Value[randomLastNameIndex];
        }

        public static string CreateAParagraph(int length)
        {
            var paragraph = new StringBuilder();

            do
            {
                paragraph.Append(CreateAString(CreateAnInt32(0, 20)) + " ");

            } while (paragraph.Length < length);

            if (paragraph.Length > length)
            {
                paragraph = paragraph.Remove(length, paragraph.Length - length - 1);
            }
            return paragraph.ToString();
        }

        public static string CreateASocialSecurityNumber(bool includeHyphens)
        {
            Func<bool, string> showHyphenIfNeeded = needed => needed ? "-" : string.Empty;

            return
                (new StringBuilder())
                    .Append(CreateAnInt32(100, 999))
                    .Append(showHyphenIfNeeded(includeHyphens))
                    .Append(CreateAnInt32(10, 99))
                    .Append(showHyphenIfNeeded(includeHyphens))
                    .Append(CreateAnInt32(1000, 9999))
                    .ToString();
        }

        public static String CreateAString()
        {
            return CreateAString(DefaultStringLength);
        }

        public static String CreateAString(Int32 length)
        {
            return new string(Enumerable.Range(0, length).Select(i => (char)('A' + RandomGenerator.Value.Next(0, 25))).ToArray());
        }

        #endregion
    }
}