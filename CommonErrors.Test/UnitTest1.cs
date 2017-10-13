using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CommonErrorsKata.Shared;

namespace CommonErrors.Test
{
    [TestFixture]
    public class AnswerQueueTestFixture
    {
        [Test]
        public void ShouldOnlyAllowTenAnswers()
        {
            //Arrange
            var size = 10;
            var stack = new AnswerQueue<TrueFalseAnswer>(size);

            //Act
            for (int i = 0; i < size+1; i++)
                stack.Enqueue(new TrueFalseAnswer(true));

            //Assert
            Assert.IsTrue(stack.Count <= 10);
        }

        [Test]
        public void ShouldForgetAtCapacity()
        {
            //Arrange
            var size = 10;
            var stack = new AnswerQueue<TrueFalseAnswer>(size);
            stack.Enqueue(new TrueFalseAnswer(false));
            for (var i =0; i< 10; i++)
                stack.Enqueue(new TrueFalseAnswer(true));
            
            //Act
            var grade = stack.Grade;
            
            //Assert
            Assert.AreEqual(100, grade);
        }

        [Test]
        public void ShouldReturnExpectedAverage()
        {
            //Arrange
            var size = 10;
            var stack = new AnswerQueue<TrueFalseAnswer>(size);
            stack.Enqueue(new TrueFalseAnswer(false));
            stack.Enqueue(new TrueFalseAnswer(true));
            stack.Enqueue(new TrueFalseAnswer(true));
            stack.Enqueue(new TrueFalseAnswer(false));

            //Act
            var grade = stack.Grade;

            //Assert
            Assert.AreEqual(50, grade);
        }

        [TestFixture]
        public class CommonAnswerTestFixture
        {
            [Test]
            public void ShouldOnlyAllowBetweenZeroAndOneHundred()
            {
                var answer = new CommonAnswer();

                Assert.Throws<InvalidGradeException>(() =>
                {
                    answer.Grade = -1;
                });

                Assert.Throws<InvalidGradeException>(() =>
                {
                    answer.Grade = 101;
                });
            }
        }
    }
}
