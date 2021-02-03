using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TesteVogal.Teste
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var esperado = 'e';
            var stream = "aAbBABacfe";

            var primeiraVogalNaoRepitida = Vogal.FirstChar(new CharStream(stream));
            Assert.AreEqual(primeiraVogalNaoRepitida, esperado);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var esperado = 'i';
            var stream = "aAbBABacfelkdjfhlkdjfhkjdhfekjdfhldjkhfldjhkfdikdjfdkfjbdfbfjdhfgdfhjdfb";

            var primeiraVogalNaoRepitida = Vogal.FirstChar(new CharStream(stream));
            Assert.AreEqual(primeiraVogalNaoRepitida, esperado);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var esperado = 'u';
            var stream = "aAbBABacfelkdjfhlkdjfhkjdhfekjdfhldjkhfldjhkfdikdjfdkfjbdfbfjdhfgdfihjdfbullllkfdjhgkfjghkfjhgfjkhgkfjghkfjdshgksjfdhgjkdsfhgkjsdfhgksjdfhgsdkjfgholkjsdfhg";

            var primeiraVogalNaoRepitida = Vogal.FirstChar(new CharStream(stream));
            Assert.AreEqual(primeiraVogalNaoRepitida, esperado);
        }
    }
}
