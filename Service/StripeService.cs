using Fitness.Interface.IService;
using Stripe;

namespace Fitness.Service
{
    public class StripeService : IStripeService
    {
        private readonly IConfiguration _configuration;

        public StripeService(IConfiguration configuration)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
        }

        public async Task<string> CreatePaymentIntentAsync(decimal amount, string currency)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100),
                Currency = currency,
                PaymentMethodTypes = new List<string> { "card" }
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);
            return paymentIntent.Id;
        }

        public async Task<bool> ConfirmPaymentAsync(string paymentIntentId)
        {
            var service = new PaymentIntentService();
            var paymentIntent = await service.GetAsync(paymentIntentId);
            return paymentIntent.Status == "succeeded";
        }
    }
}
