package core.api.jsonRespons;

public class ResponseFail extends TRespons {
    private MessageText response;
    public MessageText getResponse() {
        return response;
    }
    public void setResponse(MessageText response) {
        this.response = response;
    }
}