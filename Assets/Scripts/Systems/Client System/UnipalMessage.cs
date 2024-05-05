public class UnipalMessage {
    public bool receiveMessageSuccess;
    public string failedMessage;
    public object receivedMessage;
}

public class UnipalMessage<ReceiveType> : UnipalMessage {
    public new ApiReceiveObject<ReceiveType> receivedMessage;
}