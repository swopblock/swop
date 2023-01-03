// Copywrite (c) 2022 Swopblock LLC   (see https://github.com/swopblock)
// Created December 1, 2022 12:13 AM ET by Jeff Hilde, jeff@swopblock.org

namespace Swopblock.API.Data
{
    public record Amount(decimal amount);

    public record Location(byte[] location);

    public record Signature(byte[] signature);

    public record Term(Amount Available, Amount Expiration);

    public record Sample(Amount amount, Location Location, Signature Signature);

    public record Medium(Amount amount, Location Location, Signature Signature) : Sample(amount, Location, Signature);

    public record Population(Amount AtLeast, Amount AtMost);

    public record Asset(decimal amount) : Amount(amount);

    public record Cash(decimal amount) : Asset(amount);

    public record Btc(decimal amount) : Asset(amount);

    public record Eth(decimal amount) : Asset(amount);

    public record Message()
    {
        Term Term;

        Medium Quote;

        Sample Offer;

        Population Return;

        Sample Trade;
        
        public virtual string Text()
        {
            return "I.";
        }

        public static Message Parse(string intention)
        {
            var message = new Message();

            if (intention.StartsWith("I"))
            {
                var result = new Success();

                result.Message = message;

                return result;
            }

            else
            {
                var result = new Failure("Error: parsing intention.");

                result.Message = message;

                return result;
            }
        }
    }

    public record Report2(bool Pass) : Message
    {
        public Message Message;

        public override string Text()
        {
            return "We.";
        }

        public static Report2 Parse(string report)
        {
            if (report.StartsWith("We"))
            {
                return new Success();
            }

            else
            {
                return new Failure("Error: parsing report.");
            }
        }
    }

    public record Success() : Report2(true)
    {

    }

    public record Failure(string ErrorText) : Report2(false)
    {

    }

    public record Resourcing : Message { }

    public record Booking : Message { }

    public record Ordering : Resourcing { }

    public record Changing : Resourcing { }

    public record Bidding : Ordering { }

    public record Asking : Ordering { }

    public record Paying :  Changing { }

    public record Cashing : Changing { }

    public record Invoicing : Booking { }

    public record Receipting : Booking { }

    public record Buying : Invoicing { }

    public record Selling : Invoicing { }

    public record Expencing : Receipting { }

    public record Incoming : Receipting { }
 }
