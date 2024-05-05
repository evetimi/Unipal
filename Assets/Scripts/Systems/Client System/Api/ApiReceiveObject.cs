public class ApiReceiveObject<T> {
    public string responseCode;
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