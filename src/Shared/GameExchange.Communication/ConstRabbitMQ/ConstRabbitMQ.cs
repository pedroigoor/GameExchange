namespace GameExchange.Communication.ConstRabbitMQ
{
    public abstract class ConstRabbitMQ
    {

        public const string PAYMENT_EXCHANGE = "payment.exchange";
        public const string PAYMENT_REQUESTED_ROUTING_KEY = "payment.requested";
        public const string PAYMENT_REQUESTED_QUEUE = "payment.requested.queue";


        public const string PAYMENT_APROVE_ROUTING_KEY = "payment.aprove";
        public const string PAYMENT_APROVE_QUEUE = "payment.aprove.queue";

        public const string PAYMENT_REJECTED_ROUTING_KEY = "payment.rejected";
        public const string PAYMENT_REJECTED_QUEUE = "payment.rejected.queue";


        public const string ORDER_EXCHANGE = "order.exchange";
        public const string ORDER_CREATED_ROUTING_KEY = "order.created";
        public const string ORDER_CREATED_QUEUE = "order.created.queue";



    }
}
