package core.api.jsonRespons;

public class ResponseDeleteObject extends TRespons {
    private MessageText response;
    public MessageText getResponse() {
        return response;
    }
    public void setResponse(MessageText response) {
        this.response = response;
    }
}