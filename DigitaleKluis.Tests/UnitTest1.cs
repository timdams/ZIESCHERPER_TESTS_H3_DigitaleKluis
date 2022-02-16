using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOPClassBasicsTesterLibrary;

namespace DeDigitaleKluis.Tests
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void BasicsCheck()
        {
            TimsEpicClassAnalyzer tester = new TimsEpicClassAnalyzer(new DigitaleKluis(420));
            tester.CheckFullProperty("Code", typeof(int), propType: TimsEpicClassAnalyzer.PropertyTypes.PrivateSetPublicGet);
            tester.CheckAutoProperty("CanShowCode", typeof(bool));
            tester.CheckFullProperty("CodeLevel", typeof(int), propType: TimsEpicClassAnalyzer.PropertyTypes.NoSet);
            tester.CheckMethod("TryCode", typeof(bool), new System.Type[] { typeof(int) });



        }

        [TestMethod]
        public void CodeCheck()
        {
            const int STARTCODE = 420;
            TimsEpicClassAnalyzer tester = new TimsEpicClassAnalyzer(new DigitaleKluis(STARTCODE));
            if (tester.CheckAutoProperty("CanShowCode", typeof(bool)) && tester.CheckFullProperty("Code", typeof(int), propType: TimsEpicClassAnalyzer.PropertyTypes.PrivateSetPublicGet))
            {
                tester.SetProp("CanShowCode", false);
                Assert.AreEqual(-666, tester.GetProp("Code"));
                tester.SetProp("CanShowCode", true);
                Assert.AreEqual(STARTCODE, tester.GetProp("Code"));
            }
        }

        [TestMethod]
        public void CodeLevelCheck()
        {
            TimsEpicClassAnalyzer tester = new TimsEpicClassAnalyzer(new DigitaleKluis(9999));

            if (tester.CheckFullProperty("CodeLevel", typeof(int), propType: TimsEpicClassAnalyzer.PropertyTypes.NoSet))
            {
                for (int i = 500; i < 9000; i += 600)
                {
                    TimsEpicClassAnalyzer testerl = new TimsEpicClassAnalyzer(new DigitaleKluis(i));
                    Assert.AreEqual(i / 1000, testerl.GetProp("CodeLevel"));
                }
            }
        }

        [TestMethod]
        public void TryCodeCheck()
        {
            const int correctCode = 4500;
            TimsEpicClassAnalyzer tester = new TimsEpicClassAnalyzer(new DigitaleKluis(correctCode));
            if (tester.CheckMethod("TryCode", typeof(bool), new System.Type[] { typeof(int) }))
            {
                string res = tester.TestMethod("TryCode", new object[] { -666 }, false);
                Assert.IsTrue(res.ToLower().Contains("cheater"), "Er zou cheater op het scherm moeten komen als ik de waarde -666 probeer");
                string res2 = tester.TestMethod("TryCode", new object[] { 2000 }, false);
                Assert.IsTrue(res2.ToLower().Contains("geen geldige code"),"Er verschijnt geen 'geen geldige code' fout op hetscherm");
                string res3 = tester.TestMethod("TryCode", new object[] { correctCode }, true);
                Assert.IsTrue(res3.ToLower().Contains("geldig") && res3.Contains("3"), "Ik heb bij de derde poging juist geraden maar de boodschap die verschijnt lijkt niet te kloppen. Foutboodschap was"+ res3);

                for (int i = 0; i < 7; i++)
                {
                    string res4 = tester.TestMethod("TryCode", new object[] { correctCode }, true);
                    Assert.IsTrue(res4.Contains((i+4).ToString()), "Het aantal pogingen komt verkeerd op het scherm. Foutboodschap was" + res4);
                }
                string res5 = tester.TestMethod("TryCode", new object[] { 420 }, false);
                Assert.IsTrue(res5.Contains("pogingen opgebruikt"), "Het aantal pogingen was meer dan 10, maar ik kreeg geen foutboodschap met de tekst 'pogingen opgebruikt'");

            }
        }
    }
}
