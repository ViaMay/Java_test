package core.api.jsonRespons;

public class ResponseLkAuth extends TRespons {
    private MessageTokenTtl response;
    public MessageTokenTtl getResponse() {
        return response;
    }
    public void setResponse(MessageTokenTtl response) {
        this.response = response;
    }
}