using System;

namespace SystemOfTest
{
    internal class StartTestHandler
    {
        private Func<bool> processStartTest;

        public StartTestHandler(Func<bool> processStartTest)
        {
            this.processStartTest = processStartTest;
        }
    }
}