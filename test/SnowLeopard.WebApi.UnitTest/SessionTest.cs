using System.Collections.Generic;
using Newtonsoft.Json;
using SnowLeopard.WebApi.UnitTest.Models;
using Xunit;
using Xunit.Abstractions;

namespace SnowLeopard.WebApi.UnitTest
{
    [Collection("TestCollection")]
    public class SessionTest
    {
        public readonly ITestOutputHelper _testOutputHelper;
        public SessionTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact(DisplayName = "Session")]
        public void Test1()
        {
            _testOutputHelper.WriteLine("Start");
            var testCount = 5000;
            var testResultList = new SessionTestResult[testCount];
            for (int i = 0; i < testCount; i++)
            {
                //var strResult = APIHelper.CallAPI("http://session.alienwow.cc/session/sessiontest?num=" + i);
                //testResultList[i] = JsonConvert.DeserializeObject<SessionTestResult>(strResult);

                var strResult = APIHelper.CallAPI<SessionTestResultData>("http://localhost:33052/api/v1/session/sessiontest?num=" + i);
                testResultList[i] = JsonConvert.DeserializeObject<SessionTestResult>(strResult.Data);

                //var strResult = APIHelper.CallAPI("http://session1.alienwow.cc/api/v1/session/sessiontest?num=" + i);
                //testResultList[i] = JsonConvert.DeserializeObject<SessionTestResult>(strResult);

                _testOutputHelper.WriteLine(JsonConvert.SerializeObject(testResultList[i]));
            }

            var successCount = 0;
            var errorResult = new List<ErrorResult>();
            for (int i = 1; i < testCount; i++)
            {
                if (testResultList[i].Last == testResultList[i - 1].Current &&
                    testResultList[i].Current == testResultList[i].Writed)
                {
                    successCount++;
                }
                else // ¼ÇÂ¼´íÎóµÄ session
                {
                    errorResult.Add(new ErrorResult
                    {
                        Index = i,
                        Last = testResultList[i - 1],
                        Curr = testResultList[i]
                    });
                }
            }
            _testOutputHelper.WriteLine("End");
            _testOutputHelper.WriteLine("Result:");
            _testOutputHelper.WriteLine("SuccessCount:" + successCount);
            _testOutputHelper.WriteLine("ErrorCount:" + errorResult.Count);

            Assert.Equal(successCount, testCount - 1);
        }
    }
}
