package core.api.jsonRespons;

import java.util.List;

public class ResponseCalculation extends TRespons {

    private List<MessageCalculation> response;
    public List<MessageCalculation> getResponse() {
        return response;
    }
    public void setResponse(List<MessageCalculation> response) {
        this.response = response;
    }
}