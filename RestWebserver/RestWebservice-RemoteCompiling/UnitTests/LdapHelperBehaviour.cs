using NUnit.Framework;
using RestWebservice_RemoteCompiling.Helpers;

namespace UnitTests
{
    public class LdapHelperBehaviour
    {
        private LdapHelper _helper;
        [SetUp]
        public void Setup()
        {
            _helper = new LdapHelper();
        }

        [Test]
        public void ExecuteCodeValidator_OK()
        {
            _helper.LogInUser("if19b072", "");
        }

    }
}