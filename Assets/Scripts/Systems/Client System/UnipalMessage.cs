namespace Unipal.API {
    public class UnipalMessage {
        public bool receiveMessageSuccess;
        public string failedMessage;
        public object receivedMessage;

        public override string ToString() {
            return $"{receiveMessageSuccess} - {failedMessage} - {receivedMessage.ToString()}";
        }
    }

    public class UnipalMessage<ReceiveType> : UnipalMessage {
        public new ApiReceiveObject<ReceiveType> receivedMessage;
    }
}