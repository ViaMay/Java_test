package core.api.jsonRespons;

import java.util.List;

public class ResponseWarehouses extends TRespons {

    private List<MessageWarehouse> response;
    public List<MessageWarehouse> getResponse() {
        return response;
    }
    public void setResponse(List<MessageWarehouse> response) {
        this.response = response;
    }
}