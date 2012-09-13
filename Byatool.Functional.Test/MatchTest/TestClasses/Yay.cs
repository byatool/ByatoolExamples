namespace Byatool.Functional.Test.MatchTest.TestClasses
{
    public class Yay
    {
        #region Properties

        public BabyYay Baby { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }

        #endregion

        #region Constructors

        public Yay()
        {

        }

        public Yay(BabyYay baby)
        {
            Baby = baby;
        }

        #endregion

    }
}