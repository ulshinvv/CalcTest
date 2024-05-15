using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF2022UserLib.Tests
{
    [TestClass]
    public class CalculationsTests
    {
        [TestMethod]
        public void AvailablePeriods_NullStartTimes()//startTimes is null
        {
            var calculations = new Calculations();
            TimeSpan[] startTimes = null;
            int[] durations = { 30, 45 };

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                calculations.AvailablePeriods(startTimes, durations, TimeSpan.FromHours(9), TimeSpan.FromHours(17), 60);
            });
        }

        [TestMethod]
        public void AvailablePeriods_NullDurations()//durations is null
        {
            var calculations = new Calculations();
            TimeSpan[] startTimes = { TimeSpan.FromHours(9), TimeSpan.FromHours(10) };
            int[] durations = null;

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                calculations.AvailablePeriods(startTimes, durations, TimeSpan.FromHours(9), TimeSpan.FromHours(17), 60);
            });
        }

        [TestMethod]
        public void AvailablePeriods_ArraysLengthMismatch()//startTimes.Length != durations.Length
        {
            var calculations = new Calculations();
            TimeSpan[] startTimes = { TimeSpan.FromHours(9), TimeSpan.FromHours(10) };
            int[] durations = { 30 };

            Assert.ThrowsException<ArrayMismatchException>(() =>
            {
                calculations.AvailablePeriods(startTimes, durations, TimeSpan.FromHours(9), TimeSpan.FromHours(17), 60);
            });
        }

        [TestMethod]
        public void AvailablePeriods_NegativeConsultationTime()//consultationTime <= 0
        {
            var calculations = new Calculations();
            TimeSpan[] startTimes = { TimeSpan.FromHours(9), TimeSpan.FromHours(10) };
            int[] durations = { 30, 45 };

            Assert.ThrowsException<ArgumentException>(() =>
            {
                calculations.AvailablePeriods(startTimes, durations, TimeSpan.FromHours(9), TimeSpan.FromHours(17), -30);
            });
        }


        [TestMethod]
        public void AvailablePeriods_EndBeforeStart()//beginWorkingTime > endWorkingTime
        {
            var calculations = new Calculations();
            TimeSpan[] startTimes = { TimeSpan.FromHours(15) };
            int[] durations = { 30 };

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                calculations.AvailablePeriods(startTimes, durations, TimeSpan.FromHours(17), TimeSpan.FromHours(9), 60);
                                           //(TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime)
            });
        }

        [TestMethod]
        public void AvailablePeriods_OutsideWorkingHours()
        {
            var calculations = new Calculations();
            TimeSpan[] startTimes = { TimeSpan.FromHours(2) };
            int[] durations = { 30 };

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                calculations.AvailablePeriods(startTimes, durations, TimeSpan.FromHours(9), TimeSpan.FromHours(17), 60);

            });
        }

        [TestMethod]
        public void AvailablePeriods_NoExistingConsultations()
        {
            var calculations = new Calculations();
            TimeSpan[] startTimes = { };
            int[] durations = { };

            var result = calculations.AvailablePeriods(startTimes, durations, TimeSpan.FromHours(9), TimeSpan.FromHours(17), 60);

            Assert.AreEqual(result.Length, 8);
        }

        [TestMethod]
        public void AvailablePeriods_SingleConsultation()
        {
            var calculations = new Calculations();
            TimeSpan[] startTimes = { TimeSpan.FromHours(9) };
            int[] durations = { 30 };

            var result = calculations.AvailablePeriods(startTimes, durations, TimeSpan.FromHours(9), TimeSpan.FromHours(17), 60);

            Assert.AreEqual(result.Length, 7);
        }

        [TestMethod]
        public void AvailablePeriods_MultipleConsultations()
        {
            var calculations = new Calculations();
            TimeSpan[] startTimes = { TimeSpan.FromHours(9), TimeSpan.FromHours(11), TimeSpan.FromHours(13) };
            int[] durations = { 30, 45, 60 };

            var result = calculations.AvailablePeriods(startTimes, durations, TimeSpan.FromHours(9), TimeSpan.FromHours(17), 60);

            Assert.AreEqual(result.Length, 5);
        }

        [TestMethod]
        public void Format_TimeSpan_ReturnsCorrectFormat()
        {
            
            TimeSpan ts = new TimeSpan(3, 30, 0); 

            
            string formattedTime = Calculations.Format(ts);

            
            Assert.AreEqual("03:30", formattedTime);
        }

    }
}
