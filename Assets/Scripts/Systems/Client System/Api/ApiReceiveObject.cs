public class ApiReceiveObject<T> {
    public string status;
    public T body;
}

/*
Ex for login:

{
    "responseCode": "201",
    "body": {
        "message": "Login successful"
    }
}

*/