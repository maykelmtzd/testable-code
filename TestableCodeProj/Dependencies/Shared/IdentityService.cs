using System;

namespace TestableCodeDemos.Module4.Shared
{
    public class IdentityService : IIdentityService
    {
        public string GetUserName()
        {
            throw new NotImplementedException();
        }
    }
}
