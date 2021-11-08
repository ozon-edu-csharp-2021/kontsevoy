using System;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests
{
    public class EmailValueObjectTests
    {
        [Fact(DisplayName = "Email - Плохие адреса")]
        public void SomeBadEmails()
        {
            Assert.Throws<ArgumentException>(() => new Email("some bad email"));
            Assert.Throws<ArgumentException>(() => new Email("some bad e.mail"));
            Assert.Throws<ArgumentException>(() => new Email("some bad e-mail"));
        }
        
        [Fact(DisplayName = "Email - Хорошие адреса")]
        public void SomeGoodEmails()
        {
            var email = "some+good@e-mail.ru";
            Assert.Equal(email, new Email(email).Value);
            email = "other@good.email";
            Assert.Equal(email, new Email(email).Value);
        }

        [Fact(DisplayName = "Равенство экземпляров")]
        public void Equality()
        {
            Assert.Equal(new Email("some@email.ru"), new Email("some@email.ru"));
            Assert.Equal(new Email("other@email.ru"), new Email("other@email.ru"));
            Assert.NotEqual(new Email("good@email.ru"), new Email("other@email.ru"));
            Assert.NotEqual(new Email("good1@email.ru"), new Email("good2@email.ru"));
        }
    }
}